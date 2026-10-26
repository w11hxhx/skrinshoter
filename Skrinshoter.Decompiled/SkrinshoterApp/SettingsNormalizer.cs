using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Common;
using Microsoft.Extensions.Logging;
using MoreLinq.Extensions;

namespace SkrinshoterApp;

public class SettingsNormalizer
{
	private const string LegacyNoCameraDeviceId = "NoCamera";

	private const string LegacyNoSoundDeviceId = "NoSound";

	private const string LegacyDefaultConsoleDeviceId = "DefaultConsole";

	private const string LegacyDefaultCommunicationsDeviceId = "DefaultCommunications";

	private readonly TrialStateManager trialStateManager;

	private readonly ILogger logger;

	public SettingsNormalizer(TrialStateManager trialStateManager, ILogger<SettingsNormalizer> logger)
	{
		this.trialStateManager = trialStateManager;
		this.logger = logger;
	}

	public void Normalize(Settings settings)
	{
		string appId = trialStateManager.State.AppId;
		if (!string.IsNullOrEmpty(appId))
		{
			settings.AppId = appId;
		}
		else
		{
			appId = settings.AppId.Trim().ToUpperInvariant();
			if (!Guid.TryParseExact(appId, "B", out var _))
			{
				appId = Guid.NewGuid().ToString("B").ToUpperInvariant();
			}
			if (settings.AppId != appId)
			{
				NormalizeValue((string v) =>
				{
					settings.AppId = v;
				}, "AppId", settings.AppId, appId);
			}
			trialStateManager.State.AppId = appId;
			trialStateManager.SaveState();
		}
		if (!LanguagesProvider.SupportedLanguages.Contains(settings.Language))
		{
			NormalizeValue((string v) =>
			{
				settings.Language = v;
			}, "Language", settings.Language, LanguagesProvider.Default);
		}
		int num = JpegQualityLevelsProvider.SupportedLevels.Minima((int x) => Math.Abs(x - settings.JpegQualityLevel)).First();
		if (settings.JpegQualityLevel != num)
		{
			NormalizeValue((int v) =>
			{
				settings.JpegQualityLevel = v;
			}, "JpegQualityLevel", settings.JpegQualityLevel, num);
		}
		if (!VideoQualityLevelsProvider.SupportedLevels.Contains(settings.VideoQualityLevel))
		{
			NormalizeValue((VideoQualityLevel v) =>
			{
				settings.VideoQualityLevel = v;
			}, "VideoQualityLevel", settings.VideoQualityLevel, VideoQualityLevel.HD);
		}
		ScreenshotSaveType screenshotSaveType = settings.ScreenshotSaveType;
		if (!screenshotSaveType.AreFlagsDefined())
		{
			screenshotSaveType = ScreenshotSaveType.Cloud;
		}
		if (screenshotSaveType.HasFlag(ScreenshotSaveType.Cloud) && screenshotSaveType.HasFlag(ScreenshotSaveType.Clipboard))
		{
			screenshotSaveType ^= ScreenshotSaveType.Clipboard;
		}
		if (settings.ScreenshotSaveType != screenshotSaveType)
		{
			NormalizeValue((ScreenshotSaveType v) =>
			{
				settings.ScreenshotSaveType = v;
			}, "ScreenshotSaveType", settings.ScreenshotSaveType, screenshotSaveType);
		}
		if (settings.VideoSaveType != VideoSaveType.Cloud)
		{
			NormalizeValue((VideoSaveType v) =>
			{
				settings.VideoSaveType = v;
			}, "VideoSaveType", settings.VideoSaveType, VideoSaveType.Cloud);
		}
		ScreenshotUploadDestination screenshotUploadDestinations = settings.ScreenshotUploadDestinations;
		if (!settings.ScreenshotUploadDefaultsInitialized)
		{
			screenshotUploadDestinations = ScreenshotUploadDestination.All;
			settings.ScreenshotUploadDefaultsInitialized = true;
		}
		if ((screenshotUploadDestinations & ~ScreenshotUploadDestination.All) != 0)
		{
			screenshotUploadDestinations &= ScreenshotUploadDestination.All;
		}
		if (screenshotUploadDestinations == ScreenshotUploadDestination.None)
		{
			screenshotUploadDestinations = ScreenshotUploadDestination.Skrinshoter;
		}
		if (settings.ScreenshotUploadDestinations != screenshotUploadDestinations)
		{
			NormalizeValue((ScreenshotUploadDestination v) =>
			{
				settings.ScreenshotUploadDestinations = v;
			}, "ScreenshotUploadDestinations", settings.ScreenshotUploadDestinations, screenshotUploadDestinations);
		}
		settings.UseDirectLinkForScreenshot = false;
		settings.UseDirectLinkForVideo = false;
		Rect cameraWindowRelativeRect = settings.CameraWindowRelativeRect;
		cameraWindowRelativeRect.X = MathEx.Clamp(cameraWindowRelativeRect.X, 0.0, 1.0);
		cameraWindowRelativeRect.Y = MathEx.Clamp(cameraWindowRelativeRect.Y, 0.0, 1.0);
		cameraWindowRelativeRect.Width = MathEx.Clamp(cameraWindowRelativeRect.Width, 0.0, 1.0);
		cameraWindowRelativeRect.Height = MathEx.Clamp(cameraWindowRelativeRect.Height, 0.0, 1.0);
		if (cameraWindowRelativeRect.X + cameraWindowRelativeRect.Width > 1.0)
		{
			cameraWindowRelativeRect.X = 1.0 - cameraWindowRelativeRect.Width;
		}
		if (cameraWindowRelativeRect.Y + cameraWindowRelativeRect.Height > 1.0)
		{
			cameraWindowRelativeRect.Y = 1.0 - cameraWindowRelativeRect.Height;
		}
		if (settings.CameraWindowRelativeRect != cameraWindowRelativeRect)
		{
			NormalizeValue((Rect v) =>
			{
				settings.CameraWindowRelativeRect = v;
			}, "CameraWindowRelativeRect", settings.CameraWindowRelativeRect, cameraWindowRelativeRect);
		}
		string screenshotSavePath = settings.ScreenshotSavePath;
		try
		{
			screenshotSavePath = Path.GetFullPath(screenshotSavePath);
		}
		catch
		{
			screenshotSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		if (settings.ScreenshotSavePath != screenshotSavePath)
		{
			NormalizeValue((string v) =>
			{
				settings.ScreenshotSavePath = v;
			}, "ScreenshotSavePath", settings.ScreenshotSavePath, screenshotSavePath);
		}
		string videoSavePath = settings.VideoSavePath;
		try
		{
			videoSavePath = Path.GetFullPath(videoSavePath);
		}
		catch
		{
			videoSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		if (settings.VideoSavePath != videoSavePath)
		{
			NormalizeValue((string v) =>
			{
				settings.VideoSavePath = v;
			}, "VideoSavePath", settings.VideoSavePath, videoSavePath);
		}
		long num2 = MathEx.Clamp(settings.LastStatsSendDate, -62135596800L, 253402300799L);
		if (settings.LastStatsSendDate != num2)
		{
			NormalizeValue((long v) =>
			{
				settings.LastStatsSendDate = v;
			}, "LastStatsSendDate", settings.LastStatsSendDate, num2);
		}
		HashSet<Hotkey> hashSet = new HashSet<Hotkey>();
		if (!settings.AreaScreenshotHotkey.IsEmpty && !hashSet.Add(settings.AreaScreenshotHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.AreaScreenshotHotkey = v;
			}, "AreaScreenshotHotkey", settings.AreaScreenshotHotkey, Hotkey.Empty);
		}
		if (!settings.ActiveWindowScreenshotHotkey.IsEmpty && !hashSet.Add(settings.ActiveWindowScreenshotHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.ActiveWindowScreenshotHotkey = v;
			}, "ActiveWindowScreenshotHotkey", settings.ActiveWindowScreenshotHotkey, Hotkey.Empty);
		}
		if (!settings.ActiveMonitorScreenshotHotkey.IsEmpty && !hashSet.Add(settings.ActiveMonitorScreenshotHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.ActiveMonitorScreenshotHotkey = v;
			}, "ActiveMonitorScreenshotHotkey", settings.ActiveMonitorScreenshotHotkey, Hotkey.Empty);
		}
		if (!settings.DesktopScreenshotHotkey.IsEmpty && !hashSet.Add(settings.DesktopScreenshotHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.DesktopScreenshotHotkey = v;
			}, "DesktopScreenshotHotkey", settings.DesktopScreenshotHotkey, Hotkey.Empty);
		}
		if (!settings.RecordVideoHotkey.IsEmpty && !hashSet.Add(settings.RecordVideoHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.RecordVideoHotkey = v;
			}, "RecordVideoHotkey", settings.RecordVideoHotkey, Hotkey.Empty);
		}
		if (!settings.PauseVideoHotkey.IsEmpty && !hashSet.Add(settings.PauseVideoHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.PauseVideoHotkey = v;
			}, "PauseVideoHotkey", settings.PauseVideoHotkey, Hotkey.Empty);
		}
		if (!settings.UploadScreenshotToAllHotkey.IsEmpty && !hashSet.Add(settings.UploadScreenshotToAllHotkey))
		{
			NormalizeValue((Hotkey v) =>
			{
				settings.UploadScreenshotToAllHotkey = v;
			}, "UploadScreenshotToAllHotkey", settings.UploadScreenshotToAllHotkey, Hotkey.Empty);
		}
		if (settings.CaptureCameraDeviceId == "NoCamera")
		{
			NormalizeValue((string v) =>
			{
				settings.CaptureCameraDeviceId = v;
			}, "CaptureCameraDeviceId", settings.CaptureCameraDeviceId, "");
			settings.IsCaptureCameraDeviceOff = true;
		}
		string captureInputAudioDeviceId;
		if (settings.CaptureInputAudioDeviceId == "NoSound")
		{
			NormalizeValue((string v) =>
			{
				settings.CaptureInputAudioDeviceId = v;
			}, "CaptureInputAudioDeviceId", settings.CaptureInputAudioDeviceId, "Default");
			settings.IsCaptureInputAudioDeviceMuted = true;
		}
		else
		{
			captureInputAudioDeviceId = settings.CaptureInputAudioDeviceId;
			if ((captureInputAudioDeviceId == "DefaultConsole" || captureInputAudioDeviceId == "DefaultCommunications") ? true : false)
			{
				NormalizeValue((string v) =>
				{
					settings.CaptureInputAudioDeviceId = v;
				}, "CaptureInputAudioDeviceId", settings.CaptureInputAudioDeviceId, "Default");
			}
		}
		if (settings.CaptureOutputAudioDeviceId == "NoSound")
		{
			NormalizeValue((string v) =>
			{
				settings.CaptureOutputAudioDeviceId = v;
			}, "CaptureOutputAudioDeviceId", settings.CaptureOutputAudioDeviceId, "Default");
			settings.IsCaptureOutputAudioDeviceMuted = true;
			return;
		}
		captureInputAudioDeviceId = settings.CaptureOutputAudioDeviceId;
		if ((captureInputAudioDeviceId == "DefaultConsole" || captureInputAudioDeviceId == "DefaultCommunications") ? true : false)
		{
			NormalizeValue((string v) =>
			{
				settings.CaptureOutputAudioDeviceId = v;
			}, "CaptureOutputAudioDeviceId", settings.CaptureOutputAudioDeviceId, "Default");
		}
	}

	private void NormalizeValue<T>(Action<T> setValue, string settingName, T oldValue, T newValue)
	{
		setValue(newValue);
		logger.LogWarning("Setting '{settingName}' normalized: '{oldValue}' -> '{newValue}'", settingName, oldValue, newValue);
	}
}
