[CmdletBinding()]
param(
    [ValidateSet('Release', 'Debug')]
    [string]$Configuration = 'Release',

    [switch]$NoRestore,
    [switch]$SkipInstaller
)

$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath($PSScriptRoot)
$artifactsDirectory = Join-Path $repoRoot 'artifacts'
$appVersion = '4.87.4.1'

$buildParameters = @{
    Configuration = $Configuration
}
if ($NoRestore) {
    $buildParameters.NoRestore = $true
}
& (Join-Path $repoRoot 'build.ps1') @buildParameters
if ($LASTEXITCODE -ne 0) {
    throw 'Application build failed.'
}

if (-not $SkipInstaller) {
    $innoVersion = '7.1.0'
    $innoDirectory = Join-Path $repoRoot '.tools\InnoSetup7'
    $iscc = Join-Path $innoDirectory 'ISCC.exe'
    if (-not (Test-Path -LiteralPath $iscc)) {
        $downloadDirectory = Join-Path $repoRoot '.tools\downloads'
        New-Item -ItemType Directory -Path $downloadDirectory -Force | Out-Null
        $innoInstaller = Join-Path $downloadDirectory "innosetup-$innoVersion-x64.exe"
        if (-not (Test-Path -LiteralPath $innoInstaller)) {
            $innoUrl = "https://github.com/jrsoftware/issrc/releases/download/is-7_1_0/innosetup-$innoVersion-x64.exe"
            Invoke-WebRequest -Uri $innoUrl -OutFile $innoInstaller
        }

        $signature = Get-AuthenticodeSignature -LiteralPath $innoInstaller
        if ($signature.Status -ne 'Valid' -or $signature.SignerCertificate.Subject -notmatch 'Pyrsys B\.V\.') {
            throw "The Inno Setup download has an invalid or unexpected signature: $($signature.Status), $($signature.SignerCertificate.Subject)"
        }

        New-Item -ItemType Directory -Path $innoDirectory -Force | Out-Null
        $arguments = @(
            '/VERYSILENT',
            '/SUPPRESSMSGBOXES',
            '/NORESTART',
            '/CURRENTUSER',
            "/DIR=$innoDirectory"
        )
        $process = Start-Process -FilePath $innoInstaller -ArgumentList $arguments -WindowStyle Hidden -Wait -PassThru
        if ($process.ExitCode -ne 0 -or -not (Test-Path -LiteralPath $iscc)) {
            throw "Inno Setup installation failed with exit code $($process.ExitCode)."
        }
    }

    & $iscc "/DAppVersion=$appVersion" (Join-Path $repoRoot 'installer\Skrinshoter.iss')
    if ($LASTEXITCODE -ne 0) {
        throw 'Installer build failed.'
    }
}

$sourceDirectory = Join-Path $artifactsDirectory 'Skrinshoter-source'
$sourceZip = Join-Path $artifactsDirectory 'Skrinshoter-source.zip'
$artifactsPrefix = [IO.Path]::GetFullPath($artifactsDirectory) + [IO.Path]::DirectorySeparatorChar
$resolvedSourceDirectory = [IO.Path]::GetFullPath($sourceDirectory)
if (-not $resolvedSourceDirectory.StartsWith($artifactsPrefix, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing to replace a directory outside artifacts: $resolvedSourceDirectory"
}
if (Test-Path -LiteralPath $sourceDirectory) {
    Remove-Item -LiteralPath $sourceDirectory -Recurse -Force
}
New-Item -ItemType Directory -Path $sourceDirectory | Out-Null

$sourceTrees = @('Dependencies.Source', 'Skrinshoter.Decompiled', '.github', 'installer')
foreach ($tree in $sourceTrees) {
    $source = Join-Path $repoRoot $tree
    $destination = Join-Path $sourceDirectory $tree
    New-Item -ItemType Directory -Path $destination -Force | Out-Null
    & robocopy.exe $source $destination /E /XD bin obj .git /XF *.user *.suo *.log | Out-Null
    if ($LASTEXITCODE -ge 8) {
        throw "Failed to copy source tree: $tree (robocopy exit code $LASTEXITCODE)."
    }
}
$global:LASTEXITCODE = 0

$rootFiles = @(
    '.gitattributes',
    '.gitignore',
    'build.ps1',
    'release.ps1',
    'Directory.Build.targets',
    'global.json',
    'README.md',
    'Skrinshoter.Reconstructed.sln'
)
foreach ($file in $rootFiles) {
    Copy-Item -LiteralPath (Join-Path $repoRoot $file) -Destination $sourceDirectory
}

if (Test-Path -LiteralPath $sourceZip) {
    Remove-Item -LiteralPath $sourceZip -Force
}
Get-ChildItem -LiteralPath $sourceDirectory -Force |
    Compress-Archive -DestinationPath $sourceZip -CompressionLevel Optimal

$releaseFiles = @(
    (Join-Path $artifactsDirectory 'Skrinshoter-portable-win-x64.zip'),
    (Join-Path $artifactsDirectory 'Skrinshoter-source.zip')
)
$installerPath = Join-Path $artifactsDirectory 'Skrinshoter-setup-win-x64.exe'
if (-not $SkipInstaller -or (Test-Path -LiteralPath $installerPath)) {
    $releaseFiles += $installerPath
}
$checksumPath = Join-Path $artifactsDirectory 'SHA256SUMS.txt'
$checksumLines = foreach ($file in $releaseFiles) {
    $hash = Get-FileHash -Algorithm SHA256 -LiteralPath $file
    "$($hash.Hash.ToLowerInvariant())  $([IO.Path]::GetFileName($file))"
}
[IO.File]::WriteAllLines($checksumPath, $checksumLines, [Text.UTF8Encoding]::new($false))

Write-Host "Portable package: $(Join-Path $artifactsDirectory 'Skrinshoter-portable-win-x64.zip')"
if (-not $SkipInstaller) {
    Write-Host "Installer: $(Join-Path $artifactsDirectory 'Skrinshoter-setup-win-x64.exe')"
}
Write-Host "Source package: $sourceZip"
Write-Host "Checksums: $checksumPath"
exit 0
