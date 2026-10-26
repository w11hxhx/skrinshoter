[CmdletBinding()]
param(
    [ValidateSet('Release', 'Debug')]
    [string]$Configuration = 'Release',

    [ValidateSet('x64')]
    [string]$Platform = 'x64',

    [switch]$NoRestore,
    [switch]$NoPackage
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath($PSScriptRoot)
$solution = Join-Path $repoRoot 'Skrinshoter.Reconstructed.sln'
$sdkVersion = '10.0.402'
$localDotnetRoot = Join-Path $repoRoot '.dotnet'
$dotnet = Join-Path $localDotnetRoot 'dotnet.exe'

if (-not (Test-Path -LiteralPath $dotnet)) {
    Write-Host "Installing .NET SDK $sdkVersion locally..."
    $toolsDirectory = Join-Path $repoRoot '.tools'
    New-Item -ItemType Directory -Path $toolsDirectory -Force | Out-Null
    $installer = Join-Path $toolsDirectory 'dotnet-install.ps1'
    Invoke-WebRequest 'https://dot.net/v1/dotnet-install.ps1' -OutFile $installer
    & powershell.exe -NoProfile -ExecutionPolicy Bypass -File $installer `
        -Version $sdkVersion `
        -InstallDir $localDotnetRoot `
        -NoPath
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to install .NET SDK $sdkVersion."
    }
}

$env:DOTNET_ROOT = $localDotnetRoot
$env:PATH = "$localDotnetRoot;$env:PATH"
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'

if (-not $NoRestore) {
    & $dotnet restore $solution "-p:Platform=$Platform"
    if ($LASTEXITCODE -ne 0) {
        throw 'NuGet restore failed.'
    }
}

& $dotnet build $solution `
    -c $Configuration `
    "-p:Platform=$Platform" `
    --no-restore
if ($LASTEXITCODE -ne 0) {
    throw 'Build failed.'
}

$buildOutput = Join-Path $repoRoot "Skrinshoter.Decompiled\bin\$Configuration\net48"
$requiredFiles = @(
    'Skrinshoter.exe',
    'Skrinshoter.exe.config',
    'NLog.config',
    'scrrec\scrrec.exe',
    'scrrec\avcodec-60.dll',
    'scrrec\avformat-60.dll',
    'scrrec\avutil-58.dll'
)

foreach ($relativePath in $requiredFiles) {
    $requiredPath = Join-Path $buildOutput $relativePath
    if (-not (Test-Path -LiteralPath $requiredPath)) {
        throw "Required runtime file is missing: $requiredPath"
    }
}

if (-not $NoPackage) {
    $artifactsDirectory = Join-Path $repoRoot 'artifacts'
    $runtimeDirectory = Join-Path $artifactsDirectory 'Skrinshoter-win-x64'
    $runtimeZip = Join-Path $artifactsDirectory 'Skrinshoter-portable-win-x64.zip'
    $legacyRuntimeZip = Join-Path $artifactsDirectory 'Skrinshoter-win-x64.zip'
    $expectedPrefix = [IO.Path]::GetFullPath($artifactsDirectory) + [IO.Path]::DirectorySeparatorChar
    $resolvedRuntime = [IO.Path]::GetFullPath($runtimeDirectory)

    if (-not $resolvedRuntime.StartsWith($expectedPrefix, [StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to replace a directory outside artifacts: $resolvedRuntime"
    }

    New-Item -ItemType Directory -Path $artifactsDirectory -Force | Out-Null
    if (Test-Path -LiteralPath $runtimeDirectory) {
        Remove-Item -LiteralPath $runtimeDirectory -Recurse -Force
    }
    New-Item -ItemType Directory -Path $runtimeDirectory | Out-Null
    Copy-Item -Path (Join-Path $buildOutput '*') -Destination $runtimeDirectory -Recurse -Force
    Get-ChildItem -LiteralPath $runtimeDirectory -Recurse -File |
        Where-Object { $_.Extension -in @('.pdb', '.xml') } |
        Remove-Item -Force
    if (-not (Test-Path -LiteralPath (Join-Path $runtimeDirectory 'Skrinshoter.exe'))) {
        throw 'Runtime packaging failed: Skrinshoter.exe is missing.'
    }

    if (Test-Path -LiteralPath $runtimeZip) {
        Remove-Item -LiteralPath $runtimeZip -Force
    }
    if (Test-Path -LiteralPath $legacyRuntimeZip) {
        Remove-Item -LiteralPath $legacyRuntimeZip -Force
    }
    Compress-Archive -Path (Join-Path $runtimeDirectory '*') -DestinationPath $runtimeZip -CompressionLevel Optimal
    Write-Host "Runtime package: $runtimeZip"
}

Write-Host "Build complete: $(Join-Path $buildOutput 'Skrinshoter.exe')"
