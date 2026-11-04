param([string]$BuildDirectory = (Join-Path $PSScriptRoot '..\Skrinshoter.Decompiled\bin\Release\net48'))
$ErrorActionPreference = 'Stop'
$BuildDirectory = [IO.Path]::GetFullPath($BuildDirectory)
Add-Type -AssemblyName WindowsBase, PresentationCore, PresentationFramework
$resolve = [ResolveEventHandler] {
    param($sender, $eventArgs)
    $path = Join-Path $BuildDirectory ((New-Object Reflection.AssemblyName $eventArgs.Name).Name + '.dll')
    if (Test-Path -LiteralPath $path) { return [Reflection.Assembly]::LoadFrom($path) }
    return $null
}
[AppDomain]::CurrentDomain.add_AssemblyResolve($resolve)
try {
    $assembly = [Reflection.Assembly]::LoadFrom((Join-Path $BuildDirectory 'Skrinshoter.exe'))
    $null = [Reflection.Assembly]::LoadFrom((Join-Path $BuildDirectory 'Microsoft.Extensions.Logging.Abstractions.dll'))
    [SkrinshoterApp.UIThread]::Initialize([Windows.Threading.Dispatcher]::CurrentDispatcher)
    $first = New-Object SkrinshoterApp.Settings
    $second = New-Object SkrinshoterApp.Settings
    if ($first.AppId -eq $second.AppId) { throw 'Fresh profiles share a cloud identity.' }

    # Disable all real trial stores before constructing a manager: no user data is read or written.
    $storage = New-Object SkrinshoterApp.TrialStateStorage
    $storesField = $storage.GetType().GetField('stores', [Reflection.BindingFlags]'Instance,NonPublic')
    $storesField.SetValue($storage, [Array]::CreateInstance($assembly.GetType('SkrinshoterApp.ITrialStateStore'), 0))
    $manager = New-Object SkrinshoterApp.TrialStateManager $storage
    $logger = [Microsoft.Extensions.Logging.Abstractions.NullLogger[SkrinshoterApp.SettingsNormalizer]]::Instance
    $normalizer = New-Object SkrinshoterApp.SettingsNormalizer $manager, $logger
    $originalId = $first.AppId
    $normalizer.Normalize($first)
    if ($first.AppId -ne $originalId) { throw 'Existing account changed during upgrade.' }
    $normalizer.Normalize($first)
    if ($first.AppId -ne $originalId) { throw 'Account changed on repeated startup.' }
    $first.CloudIdentityOwner = 'different-computer|different-user'
    $first.FileUids.Add('another-users-file')
    $normalizer.Normalize($first)
    if ($first.AppId -eq $originalId -or $first.FileUids.Count -ne 0) { throw 'Copied profile retained another users cloud data.' }
    if ($manager.State.AppId -ne $first.AppId) { throw 'Cloud identity stores disagree.' }

    $results = [SkrinshoterApp.ScreenshotUploadResult[]]@(
        (New-Object SkrinshoterApp.ScreenshotUploadResult ([SkrinshoterApp.ScreenshotUploadDestination]::Skrinshoter), 'Service', 'https://example.com/image.png')
    )
    if ([SkrinshoterApp.ScreenshotUploadCoordinator]::FormatLinks($results, $false) -ne 'https://example.com/image.png') { throw 'Bare links contain service names.' }
    if ([SkrinshoterApp.ScreenshotUploadCoordinator]::FormatLinks($results, $true) -ne 'Service: https://example.com/image.png') { throw 'Named links lost service names.' }

    $first.ShowEditorPrint = $false
    $first.IncludeServiceNames = $false
    $first.ImageSearchGoogle = $false
    $json = [Newtonsoft.Json.JsonConvert]::SerializeObject($first)
    $restored = [Newtonsoft.Json.JsonConvert]::DeserializeObject($json, [SkrinshoterApp.Settings])
    if ($restored.ShowEditorPrint -or $restored.IncludeServiceNames -or $restored.ImageSearchGoogle) { throw 'New settings did not survive serialization.' }
    Write-Host 'PASS: separate cloud identities, migration, copied profiles, link formatting and saved settings.'
} finally {
    [AppDomain]::CurrentDomain.remove_AssemblyResolve($resolve)
}
