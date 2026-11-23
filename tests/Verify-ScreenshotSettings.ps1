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

    # Use the official JSON field names and non-default bindings. No real profile is loaded.
    $settingsLogger = [Microsoft.Extensions.Logging.Abstractions.NullLogger[SkrinshoterApp.SettingsStorage]]::Instance
    $settingsStore = New-Object SkrinshoterApp.SettingsStorage $normalizer, $settingsLogger
    $legacyJson = '{"hotkey_region_screenshot":"17,16,65","hotkey_active_window_screenshot":"17,16,87","hotkey_full_screenshot":"17,16,77","hotkey_desktop_screenshot":"17,16,68","hotkey_video_recoding_activation":"17,16,82","hotkey_video_recoding_pause":"17,16,80"}'
    $legacy = $settingsStore.Deserialize($legacyJson)
    $normalizer.Normalize($legacy)
    $expected = @{ AreaScreenshotHotkey = 'A'; ActiveWindowScreenshotHotkey = 'W'; ActiveMonitorScreenshotHotkey = 'M'; DesktopScreenshotHotkey = 'D'; RecordVideoHotkey = 'R'; PauseVideoHotkey = 'P' }
    foreach ($name in $expected.Keys) {
        if ($legacy.$name.Key.ToString() -ne $expected[$name] -or
            $legacy.$name.Modifiers -ne ([Windows.Input.ModifierKeys]::Control -bor [Windows.Input.ModifierKeys]::Shift)) {
            throw "Official binding $name was reset during migration."
        }
    }
    Write-Host 'PASS: all six non-default official screenshot/recording bindings survive deserialization and normalization.'

    $results = [SkrinshoterApp.ScreenshotUploadResult[]]@(
        (New-Object SkrinshoterApp.ScreenshotUploadResult ([SkrinshoterApp.ScreenshotUploadDestination]::Skrinshoter), 'Service', 'https://example.com/image.png')
    )
    if ([SkrinshoterApp.ScreenshotUploadCoordinator]::FormatLinks($results, $false) -ne 'https://example.com/image.png') { throw 'Bare links contain service names.' }
    if ([SkrinshoterApp.ScreenshotUploadCoordinator]::FormatLinks($results, $true) -ne 'Service: https://example.com/image.png') { throw 'Named links lost service names.' }

    $provider = New-Object SkrinshoterApp.CustomUploadProvider
    $provider.Name = 'Custom test'
    $provider.Url = 'https://example.com/upload'
    $provider.ImageLink = 'data.url'
    $provider.Headers.Add('Authorization', 'Bearer test')
	$provider.FormFields.Add('album', 'screenshots')
    $first.CustomUploadProviders.Add($provider)
    $first.ShowEditorPrint = $false
    $first.IncludeServiceNames = $false
    $first.CheckForUpdates = $false
    $first.SkippedForkUpdateVersion = '9.8.7.6'
    $first.ImageSearchGoogle = $false
	$first.VideoResolutionHeight = 1080
	$first.VideoFrameRate = 60
	$first.VideoBitrateKbps = 12000
    $json = [Newtonsoft.Json.JsonConvert]::SerializeObject($first)
    $restored = [Newtonsoft.Json.JsonConvert]::DeserializeObject($json, [SkrinshoterApp.Settings])
    if ($restored.CheckForUpdates -or $restored.SkippedForkUpdateVersion -ne '9.8.7.6') { throw 'Fork update preferences did not survive serialization.' }
    if ($restored.ShowEditorPrint -or $restored.IncludeServiceNames -or $restored.ImageSearchGoogle) { throw 'New settings did not survive serialization.' }
    if ($restored.CustomUploadProviders.Count -ne 1 -or $restored.CustomUploadProviders[0].Headers['Authorization'] -ne 'Bearer test') { throw 'Custom services did not survive serialization.' }
	if ($restored.CustomUploadProviders[0].FormFields['album'] -ne 'screenshots') { throw 'Custom form fields did not survive serialization.' }
    if (-not $first.ScreenshotUploadDestinations.HasFlag([SkrinshoterApp.ScreenshotUploadDestination]::Nuuls)) { throw 'Nuuls missing from defaults.' }
	if ($restored.VideoResolutionHeight -ne 1080 -or $restored.VideoFrameRate -ne 60 -or $restored.VideoBitrateKbps -ne 12000) { throw 'Independent video settings did not survive serialization.' }

	$sxcuPath = Join-Path ([IO.Path]::GetTempPath()) ('skrinshoter-' + [Guid]::NewGuid().ToString('N') + '.sxcu')
	try {
		'{"Version":"15.0.0","Name":"Imported","DestinationType":"ImageUploader, FileUploader","RequestMethod":"POST","RequestURL":"https://example.com/upload","Headers":{"X-Token":"secret"},"Body":"MultipartFormData","Arguments":{"album":"test"},"FileFormName":"upload","URL":"{json:data.url}","DeletionURL":"$json:data.delete$"}' | Set-Content -LiteralPath $sxcuPath -Encoding UTF8
		$imported = [SkrinshoterApp.ShareXCustomUploaderImporter]::Import($sxcuPath)
		if ($imported.Name -ne 'Imported' -or $imported.FileField -ne 'upload' -or $imported.ImageLink -ne '{json:data.url}' -or $imported.Headers['X-Token'] -ne 'secret' -or $imported.FormFields['album'] -ne 'test') { throw 'ShareX config import failed.' }
	} finally {
		if (Test-Path -LiteralPath $sxcuPath) { Remove-Item -LiteralPath $sxcuPath -Force }
	}
    Write-Host 'PASS: separate cloud identities, migration, copied profiles, link formatting and saved settings.'
} finally {
    [AppDomain]::CurrentDomain.remove_AssemblyResolve($resolve)
}
