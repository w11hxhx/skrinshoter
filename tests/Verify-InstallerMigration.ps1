param([string]$Compiler = (Join-Path $PSScriptRoot '..\.tools\InnoSetup7\ISCC.exe'))
$ErrorActionPreference = 'Stop'
$repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$testId = [Guid]::NewGuid().ToString('N')
$work = Join-Path $repo ('.tools\migration-tests\' + $testId)
$profile = Join-Path $work 'profile'
$backups = Join-Path $work 'backups'
$payload = Join-Path $work 'payload'
$registryRoot = 'Software\SkrinshoterForkMigrationTests\' + $testId + '\Uninstall'
$officialNames = @('Скриншотер_is1', 'Скриншотер')
New-Item -ItemType Directory -Force $profile,$payload | Out-Null
[IO.File]::WriteAllText((Join-Path $payload 'fork-installed.txt'), 'fixture payload')
$env:SKR_MIGRATION_TEST_ROOT = $registryRoot
$env:SKR_MIGRATION_TEST_PROFILE = $profile
$env:SKR_MIGRATION_TEST_WORK = $work
$fakeUninstaller = Join-Path $work 'official-uninstaller.exe'
$fakeCode = @"
using System;
using System.IO;
using Microsoft.Win32;
public static class FakeOfficialUninstaller {
    public static int Main() {
        string root = Environment.GetEnvironmentVariable("SKR_MIGRATION_TEST_ROOT");
        string profile = Environment.GetEnvironmentVariable("SKR_MIGRATION_TEST_PROFILE");
        string work = Environment.GetEnvironmentVariable("SKR_MIGRATION_TEST_WORK");
        if (root == null || !root.StartsWith(@"Software\SkrinshoterForkMigrationTests\")) return 99;
        File.WriteAllText(Path.Combine(work, "called.txt"), "called");
        File.Delete(Path.Combine(profile, "settings.json"));
        int result = int.Parse(File.ReadAllText(Path.Combine(work, "exit-code.txt")));
        if (result != 0) return result;
        Registry.CurrentUser.DeleteSubKeyTree(root + @"\Скриншотер_is1", false);
        Registry.CurrentUser.DeleteSubKeyTree(root + @"\Скриншотер", false);
        File.Delete(Path.Combine(work, "official-installed.txt"));
        return 0;
    }
}
"@
Add-Type -TypeDefinition $fakeCode -OutputAssembly $fakeUninstaller -OutputType ConsoleApplication
$compileArgs = @('/DMigrationTest', "/DMigrationRegistryRoot=$registryRoot", "/DMigrationProfileDirectory=$profile",
    "/DMigrationBackupDirectory=$backups", "/DRuntimeDir=$payload", (Join-Path $repo 'installer\Skrinshoter.iss'))
& $Compiler @compileArgs > (Join-Path $work 'compile.log')
if ($LASTEXITCODE -ne 0) { throw "Migration installer compilation failed. See $work\compile.log" }
$setup = Join-Path $repo 'artifacts\Skrinshoter-migration-test.exe'
$settingsPath = Join-Path $profile 'settings.json'
# Non-default official-format bindings; compare the complete UTF-8 file byte for byte.
$settings = '{"hotkey_region_screenshot":"17,16,65","hotkey_active_window_screenshot":"17,16,87","hotkey_full_screenshot":"17,16,77","hotkey_desktop_screenshot":"17,16,68","hotkey_video_recoding_activation":"17,16,82","hotkey_video_recoding_pause":"17,16,80","language":"ru"}'
$settingsBytes = [Text.UTF8Encoding]::new($false).GetBytes($settings)
function Reset-Fixture([string]$keyName, [int]$exitCode) {
    $base = [Microsoft.Win32.Registry]::CurrentUser
    $base.DeleteSubKeyTree($registryRoot, $false)
    if ($keyName) {
        $key = $base.CreateSubKey($registryRoot + '\' + $keyName)
        try {
            $key.SetValue('DisplayName', 'Скриншотер')
            $key.SetValue('UninstallString', '"' + $fakeUninstaller + '"')
        } finally { $key.Dispose() }
    }
    [IO.File]::WriteAllBytes($settingsPath, $settingsBytes)
    [IO.File]::WriteAllText((Join-Path $work 'exit-code.txt'), [string]$exitCode)
    [IO.File]::WriteAllText((Join-Path $work 'official-installed.txt'), 'official fixture')
    if (Test-Path (Join-Path $work 'called.txt')) { Remove-Item -LiteralPath (Join-Path $work 'called.txt') }
}
function Run-Setup([string]$name, [bool]$expectSuccess) {
    $destination = Join-Path $work $name
    $arguments = @('/VERYSILENT', '/SUPPRESSMSGBOXES', '/NORESTART', "/DIR=`"$destination`"", "/LOG=`"$work\$name.log`"")
    $process = Start-Process -FilePath $setup -ArgumentList $arguments -WindowStyle Hidden -Wait -PassThru
    if (($process.ExitCode -eq 0) -ne $expectSuccess) { throw "$name returned unexpected setup exit code $($process.ExitCode). See $work\$name.log" }
    if ((Test-Path (Join-Path $destination 'fork-installed.txt')) -ne $expectSuccess) { throw "$name did not respect uninstall success/failure." }
    if (-not (Test-Path $settingsPath) -or
        [Convert]::ToBase64String([IO.File]::ReadAllBytes($settingsPath)) -ne [Convert]::ToBase64String($settingsBytes)) {
        throw "$name lost or changed original settings/hotkeys."
    }
}
try {
    # If a recovery copy cannot be written, uninstall must never be launched.
    [IO.File]::WriteAllText($backups, 'blocked backup directory fixture')
    Reset-Fixture $officialNames[0] 0
    Run-Setup 'backup-failure' $false
    if (Test-Path (Join-Path $work 'called.txt')) { throw 'Uninstaller ran before a successful backup.' }
    [IO.File]::Delete($backups)
    foreach ($name in $officialNames) {
        Reset-Fixture $name 0
        Run-Setup ('migrate-' + [array]::IndexOf($officialNames, $name)) $true
        if (-not (Test-Path (Join-Path $work 'called.txt')) -or (Test-Path (Join-Path $work 'official-installed.txt'))) { throw 'Official installation was not removed.' }
        if ([Microsoft.Win32.Registry]::CurrentUser.OpenSubKey($registryRoot + '\' + $name)) { throw 'Official uninstall entry remains.' }
    }
    foreach ($exitCode in @(1, 1602)) {
        Reset-Fixture $officialNames[0] $exitCode
        Run-Setup ('abort-' + $exitCode) $false
    }
    Reset-Fixture '{7E133262-6DE0-4D2E-9157-AC1CD74B4C02}_is1' 0
    Run-Setup 'fork-upgrade' $true
    if (Test-Path (Join-Path $work 'called.txt')) { throw 'Fork upgrade attempted to uninstall itself.' }
    $backupFiles = @(Get-ChildItem -LiteralPath $backups -Filter '*.json')
    if ($backupFiles.Count -lt 4) { throw 'Persistent recovery backups are missing.' }
    foreach ($file in $backupFiles) {
        if ([Convert]::ToBase64String([IO.File]::ReadAllBytes($file.FullName)) -ne [Convert]::ToBase64String($settingsBytes)) { throw 'Recovery backup differs from original settings.' }
    }
    Write-Host 'PASS: installer removes official Inno/NSIS fixtures, preserves exact settings/hotkeys, aborts on failure/cancellation and leaves fork upgrades alone.'
    Write-Host "Isolated test files: $work"
} finally {
    # Only this run's randomly named test namespace is owned by the test.
    [Microsoft.Win32.Registry]::CurrentUser.DeleteSubKeyTree('Software\SkrinshoterForkMigrationTests\' + $testId, $false)
    Remove-Item Env:SKR_MIGRATION_TEST_ROOT,Env:SKR_MIGRATION_TEST_PROFILE,Env:SKR_MIGRATION_TEST_WORK -ErrorAction SilentlyContinue
}
