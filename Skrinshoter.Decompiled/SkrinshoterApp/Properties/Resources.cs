using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SkrinshoterApp.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				resourceMan = new ResourceManager("SkrinshoterApp.Properties.Resources", typeof(Resources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	public static string AppUpdate_DownloadButtonText => ResourceManager.GetString("AppUpdate_DownloadButtonText", resourceCulture);

	public static string AppUpdate_LatestVersionInstalled => ResourceManager.GetString("AppUpdate_LatestVersionInstalled", resourceCulture);

	public static string AppUpdate_WindowTitle => ResourceManager.GetString("AppUpdate_WindowTitle", resourceCulture);

	public static string AudioDevice_Default => ResourceManager.GetString("AudioDevice_Default", resourceCulture);

	public static string Camera_GeneralError => ResourceManager.GetString("Camera_GeneralError", resourceCulture);

	public static string Camera_UsedByAnotherApp => ResourceManager.GetString("Camera_UsedByAnotherApp", resourceCulture);

	public static string CloudStorage_Usage => ResourceManager.GetString("CloudStorage_Usage", resourceCulture);

	public static string CopyToClipboardToolTip => ResourceManager.GetString("CopyToClipboardToolTip", resourceCulture);

	public static string HotkeyBox_HotkeyNotSet => ResourceManager.GetString("HotkeyBox_HotkeyNotSet", resourceCulture);

	public static string HotkeyBox_InputHotkey => ResourceManager.GetString("HotkeyBox_InputHotkey", resourceCulture);

	public static string Microphone_AccessDenied => ResourceManager.GetString("Microphone_AccessDenied", resourceCulture);

	public static string Microphone_GeneralError => ResourceManager.GetString("Microphone_GeneralError", resourceCulture);

	public static string Notify_ => ResourceManager.GetString("Notify_", resourceCulture);

	public static string Notify_AppUpdateAvailable => ResourceManager.GetString("Notify_AppUpdateAvailable", resourceCulture);

	public static string Notify_CloudStorageQuotaExceeded_DeleteOnly => ResourceManager.GetString("Notify_CloudStorageQuotaExceeded_DeleteOnly", resourceCulture);

	public static string Notify_FailedToChangeAutostartSettings => ResourceManager.GetString("Notify_FailedToChangeAutostartSettings", resourceCulture);

	public static string Notify_FailedToCopyFileLinkToClipboard => ResourceManager.GetString("Notify_FailedToCopyFileLinkToClipboard", resourceCulture);

	public static string Notify_FailedToCopyScreenshotToClipboard => ResourceManager.GetString("Notify_FailedToCopyScreenshotToClipboard", resourceCulture);

	public static string Notify_FailedToCopyTextToClipboard => ResourceManager.GetString("Notify_FailedToCopyTextToClipboard", resourceCulture);

	public static string Notify_FailedToCopyToClipboardExplanation => ResourceManager.GetString("Notify_FailedToCopyToClipboardExplanation", resourceCulture);

	public static string Notify_FailedToCopyVideoLinkToClipboard => ResourceManager.GetString("Notify_FailedToCopyVideoLinkToClipboard", resourceCulture);

	public static string Notify_FailedToCopyVideoToClipboard => ResourceManager.GetString("Notify_FailedToCopyVideoToClipboard", resourceCulture);

	public static string Notify_FailedToDownloadDataFromServer => ResourceManager.GetString("Notify_FailedToDownloadDataFromServer", resourceCulture);

	public static string Notify_FailedToMakeVideo => ResourceManager.GetString("Notify_FailedToMakeVideo", resourceCulture);

	public static string Notify_FailedToStartVideoModuleProcess => ResourceManager.GetString("Notify_FailedToStartVideoModuleProcess", resourceCulture);

	public static string Notify_FailedToUploadFile => ResourceManager.GetString("Notify_FailedToUploadFile", resourceCulture);

	public static string Notify_FailedToUploadFileOfFileType => ResourceManager.GetString("Notify_FailedToUploadFileOfFileType", resourceCulture);

	public static string Notify_FailedToUploadImageToServer => ResourceManager.GetString("Notify_FailedToUploadImageToServer", resourceCulture);

	public static string Notify_FailedToUploadLogs => ResourceManager.GetString("Notify_FailedToUploadLogs", resourceCulture);

	public static string Notify_FailedToUploadScreenshot => ResourceManager.GetString("Notify_FailedToUploadScreenshot", resourceCulture);

	public static string Notify_FailedToUploadScreenshotAndStoredInUserFolder => ResourceManager.GetString("Notify_FailedToUploadScreenshotAndStoredInUserFolder", resourceCulture);

	public static string Notify_FailedToUploadScreenshotAndStoredOnDesktop => ResourceManager.GetString("Notify_FailedToUploadScreenshotAndStoredOnDesktop", resourceCulture);

	public static string Notify_FailedToUploadVideo => ResourceManager.GetString("Notify_FailedToUploadVideo", resourceCulture);

	public static string Notify_FailedToUploadVideoAndStoredOnDesktop => ResourceManager.GetString("Notify_FailedToUploadVideoAndStoredOnDesktop", resourceCulture);

	public static string Notify_FileSavedOnDisk => ResourceManager.GetString("Notify_FileSavedOnDisk", resourceCulture);

	public static string Notify_FileSavedOnDiskAndToClipboard => ResourceManager.GetString("Notify_FileSavedOnDiskAndToClipboard", resourceCulture);

	public static string Notify_LowDiskSpace => ResourceManager.GetString("Notify_LowDiskSpace", resourceCulture);

	public static string Notify_ScreenshotPathNotAccessible => ResourceManager.GetString("Notify_ScreenshotPathNotAccessible", resourceCulture);

	public static string Notify_ScrollingCaptureLimitReached => ResourceManager.GetString("Notify_ScrollingCaptureLimitReached", resourceCulture);

	public static string Notify_TextNotFoundInImage => ResourceManager.GetString("Notify_TextNotFoundInImage", resourceCulture);

	public static string Notify_UnknownError => ResourceManager.GetString("Notify_UnknownError", resourceCulture);

	public static string Notify_VideoPathNotAccessible => ResourceManager.GetString("Notify_VideoPathNotAccessible", resourceCulture);

	public static string Notify_VideoRecordingStoppedDueToLowDiskSpace => ResourceManager.GetString("Notify_VideoRecordingStoppedDueToLowDiskSpace", resourceCulture);

	public static string OkButtonText => ResourceManager.GetString("OkButtonText", resourceCulture);

	public static string PrintJobName => ResourceManager.GetString("PrintJobName", resourceCulture);

	public static string Progress_LinkToFileAlreadyInClipboard => ResourceManager.GetString("Progress_LinkToFileAlreadyInClipboard", resourceCulture);

	public static string Progress_LinkToVideoAlreadyInClipboard => ResourceManager.GetString("Progress_LinkToVideoAlreadyInClipboard", resourceCulture);

	public static string Progress_UploadingFileToCloud => ResourceManager.GetString("Progress_UploadingFileToCloud", resourceCulture);

	public static string Progress_UploadingVideoToCloud => ResourceManager.GetString("Progress_UploadingVideoToCloud", resourceCulture);

	public static string ProgressWindow_CancelButtonText => ResourceManager.GetString("ProgressWindow_CancelButtonText", resourceCulture);

	public static string ReadyButtonText => ResourceManager.GetString("ReadyButtonText", resourceCulture);

	public static string ScreenshotEditing_ArrowToolTip => ResourceManager.GetString("ScreenshotEditing_ArrowToolTip", resourceCulture);

	public static string ScreenshotEditing_BlurToolTip => ResourceManager.GetString("ScreenshotEditing_BlurToolTip", resourceCulture);

	public static string ScreenshotEditing_CloseToolTip => ResourceManager.GetString("ScreenshotEditing_CloseToolTip", resourceCulture);

	public static string ScreenshotEditing_ColorToolTip => ResourceManager.GetString("ScreenshotEditing_ColorToolTip", resourceCulture);

	public static string ScreenshotEditing_CopyToClipboardToolTip => ResourceManager.GetString("ScreenshotEditing_CopyToClipboardToolTip", resourceCulture);

	public static string ScreenshotEditing_CustomColorButtonText => ResourceManager.GetString("ScreenshotEditing_CustomColorButtonText", resourceCulture);

	public static string ScreenshotEditing_LineThicknessToolTip => ResourceManager.GetString("ScreenshotEditing_LineThicknessToolTip", resourceCulture);

	public static string ScreenshotEditing_LineToolTip => ResourceManager.GetString("ScreenshotEditing_LineToolTip", resourceCulture);

	public static string ScreenshotEditing_MarkerToolTip => ResourceManager.GetString("ScreenshotEditing_MarkerToolTip", resourceCulture);

	public static string ScreenshotEditing_NumberToolTip => ResourceManager.GetString("ScreenshotEditing_NumberToolTip", resourceCulture);

	public static string ScreenshotEditing_OvalToolTip => ResourceManager.GetString("ScreenshotEditing_OvalToolTip", resourceCulture);

	public static string ScreenshotEditing_PencilToolTip => ResourceManager.GetString("ScreenshotEditing_PencilToolTip", resourceCulture);

	public static string ScreenshotEditing_PrintToolTip => ResourceManager.GetString("ScreenshotEditing_PrintToolTip", resourceCulture);

	public static string ScreenshotEditing_RectangleToolTip => ResourceManager.GetString("ScreenshotEditing_RectangleToolTip", resourceCulture);

	public static string ScreenshotEditing_SaveToFileToolTip => ResourceManager.GetString("ScreenshotEditing_SaveToFileToolTip", resourceCulture);

	public static string ScreenshotEditing_TextToolTip => ResourceManager.GetString("ScreenshotEditing_TextToolTip", resourceCulture);

	public static string ScreenshotEditing_UndoToolTip => ResourceManager.GetString("ScreenshotEditing_UndoToolTip", resourceCulture);

	public static string ScreenshotEditing_UploadToCloudToolTip => ResourceManager.GetString("ScreenshotEditing_UploadToCloudToolTip", resourceCulture);

	public static string ScrollingCapture_Message_AutoScrollHint => ResourceManager.GetString("ScrollingCapture_Message_AutoScrollHint", resourceCulture);

	public static string ScrollingCapture_Message_ReachedMaxHeight => ResourceManager.GetString("ScrollingCapture_Message_ReachedMaxHeight", resourceCulture);

	public static string ScrollingCapture_StartButtonText => ResourceManager.GetString("ScrollingCapture_StartButtonText", resourceCulture);

	public static string ScrollingCapture_StartToolTip => ResourceManager.GetString("ScrollingCapture_StartToolTip", resourceCulture);

	public static string SelectArea => ResourceManager.GetString("SelectArea", resourceCulture);

	public static string SelectFileForUpload_WindowTitle => ResourceManager.GetString("SelectFileForUpload_WindowTitle", resourceCulture);

	public static string SelectSaveFolder_WindowTitle => ResourceManager.GetString("SelectSaveFolder_WindowTitle", resourceCulture);

	public static string Settings_About_AppId => ResourceManager.GetString("Settings_About_AppId", resourceCulture);

	public static string Settings_About_AppIdCopiedToolTip => ResourceManager.GetString("Settings_About_AppIdCopiedToolTip", resourceCulture);

	public static string Settings_About_CheckForUpdatesButtonText => ResourceManager.GetString("Settings_About_CheckForUpdatesButtonText", resourceCulture);

	public static string Settings_About_GroupHeader => ResourceManager.GetString("Settings_About_GroupHeader", resourceCulture);

	public static string Settings_About_ReportErrorButtonText => ResourceManager.GetString("Settings_About_ReportErrorButtonText", resourceCulture);

	public static string Settings_About_Version => ResourceManager.GetString("Settings_About_Version", resourceCulture);

	public static string Settings_FileNameTemplate_ApplyButtonText => ResourceManager.GetString("Settings_FileNameTemplate_ApplyButtonText", resourceCulture);

	public static string Settings_FileNameTemplate_Error => ResourceManager.GetString("Settings_FileNameTemplate_Error", resourceCulture);

	public static string Settings_FileNameTemplate_Error_Empty => ResourceManager.GetString("Settings_FileNameTemplate_Error_Empty", resourceCulture);

	public static string Settings_FileNameTemplate_Error_General => ResourceManager.GetString("Settings_FileNameTemplate_Error_General", resourceCulture);

	public static string Settings_FileNameTemplate_Error_InvalidCharacter => ResourceManager.GetString("Settings_FileNameTemplate_Error_InvalidCharacter", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Day => ResourceManager.GetString("Settings_FileNameTemplate_Param_Day", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Hours => ResourceManager.GetString("Settings_FileNameTemplate_Param_Hours", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Minutes => ResourceManager.GetString("Settings_FileNameTemplate_Param_Minutes", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Month => ResourceManager.GetString("Settings_FileNameTemplate_Param_Month", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Seconds => ResourceManager.GetString("Settings_FileNameTemplate_Param_Seconds", resourceCulture);

	public static string Settings_FileNameTemplate_Param_SequenceNumber => ResourceManager.GetString("Settings_FileNameTemplate_Param_SequenceNumber", resourceCulture);

	public static string Settings_FileNameTemplate_Param_SequenceNumberToolTip => ResourceManager.GetString("Settings_FileNameTemplate_Param_SequenceNumberToolTip", resourceCulture);

	public static string Settings_FileNameTemplate_Param_WindowTitle => ResourceManager.GetString("Settings_FileNameTemplate_Param_WindowTitle", resourceCulture);

	public static string Settings_FileNameTemplate_Param_WindowTitle_PreviewValue => ResourceManager.GetString("Settings_FileNameTemplate_Param_WindowTitle_PreviewValue", resourceCulture);

	public static string Settings_FileNameTemplate_Param_Year => ResourceManager.GetString("Settings_FileNameTemplate_Param_Year", resourceCulture);

	public static string Settings_FileNameTemplate_Placeholders => ResourceManager.GetString("Settings_FileNameTemplate_Placeholders", resourceCulture);

	public static string Settings_FileNameTemplate_Preview => ResourceManager.GetString("Settings_FileNameTemplate_Preview", resourceCulture);

	public static string Settings_FileNameTemplate_Template => ResourceManager.GetString("Settings_FileNameTemplate_Template", resourceCulture);

	public static string Settings_FileNameTemplate_WindowTitleForScreenshot => ResourceManager.GetString("Settings_FileNameTemplate_WindowTitleForScreenshot", resourceCulture);

	public static string Settings_FileNameTemplate_WindowTitleForVideo => ResourceManager.GetString("Settings_FileNameTemplate_WindowTitleForVideo", resourceCulture);

	public static string Settings_General_Autostart => ResourceManager.GetString("Settings_General_Autostart", resourceCulture);

	public static string Settings_General_CheckForUpdates => ResourceManager.GetString("Settings_General_CheckForUpdates", resourceCulture);

	public static string Settings_General_GroupHeader => ResourceManager.GetString("Settings_General_GroupHeader", resourceCulture);

	public static string Settings_General_Language => ResourceManager.GetString("Settings_General_Language", resourceCulture);

	public static string Settings_General_SilentMode => ResourceManager.GetString("Settings_General_SilentMode", resourceCulture);

	public static string Settings_General_SilentModeToolTip => ResourceManager.GetString("Settings_General_SilentModeToolTip", resourceCulture);

	public static string Settings_General_Tray_Action => ResourceManager.GetString("Settings_General_Tray_Action", resourceCulture);

	public static string Settings_General_Tray_MakeRegionScreenshot => ResourceManager.GetString("Settings_General_Tray_MakeRegionScreenshot", resourceCulture);

	public static string Settings_General_Tray_MakeScrollingCapture => ResourceManager.GetString("Settings_General_Tray_MakeScrollingCapture", resourceCulture);

	public static string Settings_General_Tray_RecordVideo => ResourceManager.GetString("Settings_General_Tray_RecordVideo", resourceCulture);

	public static string Settings_General_Tray_ShowPanel => ResourceManager.GetString("Settings_General_Tray_ShowPanel", resourceCulture);

	public static string Settings_General_UploadWindowTitleToCloud => ResourceManager.GetString("Settings_General_UploadWindowTitleToCloud", resourceCulture);

	public static string Settings_Hotkeys_ActiveMonitorScreenshot => ResourceManager.GetString("Settings_Hotkeys_ActiveMonitorScreenshot", resourceCulture);

	public static string Settings_Hotkeys_ActiveWindowScreenshot => ResourceManager.GetString("Settings_Hotkeys_ActiveWindowScreenshot", resourceCulture);

	public static string Settings_Hotkeys_AreaScreenshot => ResourceManager.GetString("Settings_Hotkeys_AreaScreenshot", resourceCulture);

	public static string Settings_Hotkeys_DesktopScreenshot => ResourceManager.GetString("Settings_Hotkeys_DesktopScreenshot", resourceCulture);

	public static string Settings_Hotkeys_GroupHeader => ResourceManager.GetString("Settings_Hotkeys_GroupHeader", resourceCulture);

	public static string Settings_Hotkeys_PauseVideo => ResourceManager.GetString("Settings_Hotkeys_PauseVideo", resourceCulture);

	public static string Settings_Hotkeys_RecordVideo => ResourceManager.GetString("Settings_Hotkeys_RecordVideo", resourceCulture);

	public static string Settings_Hotkeys_UseMouseForScreenshot => ResourceManager.GetString("Settings_Hotkeys_UseMouseForScreenshot", resourceCulture);

	public static string Settings_Hotkeys_UseMouseForVideo => ResourceManager.GetString("Settings_Hotkeys_UseMouseForVideo", resourceCulture);

	public static string Settings_Screenshot_AlwaysShowEditor => ResourceManager.GetString("Settings_Screenshot_AlwaysShowEditor", resourceCulture);

	public static string Settings_Screenshot_DrawObjectShadows => ResourceManager.GetString("Settings_Screenshot_DrawObjectShadows", resourceCulture);

	public static string Settings_Screenshot_EditorDefaultTool_Arrow => ResourceManager.GetString("Settings_Screenshot_EditorDefaultTool_Arrow", resourceCulture);

	public static string Settings_Screenshot_EditorDefaultTool_Header => ResourceManager.GetString("Settings_Screenshot_EditorDefaultTool_Header", resourceCulture);

	public static string Settings_Screenshot_EditorDefaultTool_LastUsed => ResourceManager.GetString("Settings_Screenshot_EditorDefaultTool_LastUsed", resourceCulture);

	public static string Settings_Screenshot_EditorDefaultTool_None => ResourceManager.GetString("Settings_Screenshot_EditorDefaultTool_None", resourceCulture);

	public static string Settings_Screenshot_FileNameTemplate => ResourceManager.GetString("Settings_Screenshot_FileNameTemplate", resourceCulture);

	public static string Settings_Screenshot_FileNameTemplateDefaultValue => ResourceManager.GetString("Settings_Screenshot_FileNameTemplateDefaultValue", resourceCulture);

	public static string Settings_Screenshot_Format => ResourceManager.GetString("Settings_Screenshot_Format", resourceCulture);

	public static string Settings_Screenshot_GroupHeader_Editor => ResourceManager.GetString("Settings_Screenshot_GroupHeader_Editor", resourceCulture);

	public static string Settings_Screenshot_GroupHeader_FormatAndQuality => ResourceManager.GetString("Settings_Screenshot_GroupHeader_FormatAndQuality", resourceCulture);

	public static string Settings_Screenshot_GroupHeader_Misc => ResourceManager.GetString("Settings_Screenshot_GroupHeader_Misc", resourceCulture);

	public static string Settings_Screenshot_GroupHeader_WhereToSave => ResourceManager.GetString("Settings_Screenshot_GroupHeader_WhereToSave", resourceCulture);

	public static string Settings_Screenshot_JpegQuality => ResourceManager.GetString("Settings_Screenshot_JpegQuality", resourceCulture);

	public static string Settings_Screenshot_OpenInBrowser => ResourceManager.GetString("Settings_Screenshot_OpenInBrowser", resourceCulture);

	public static string Settings_Screenshot_PlaySound => ResourceManager.GetString("Settings_Screenshot_PlaySound", resourceCulture);

	public static string Settings_Screenshot_SavePath => ResourceManager.GetString("Settings_Screenshot_SavePath", resourceCulture);

	public static string Settings_Screenshot_SaveType_Clipboard => ResourceManager.GetString("Settings_Screenshot_SaveType_Clipboard", resourceCulture);

	public static string Settings_Screenshot_SaveType_Cloud => ResourceManager.GetString("Settings_Screenshot_SaveType_Cloud", resourceCulture);

	public static string Settings_Screenshot_SaveType_File => ResourceManager.GetString("Settings_Screenshot_SaveType_File", resourceCulture);

	public static string Settings_Screenshot_ShowCursor => ResourceManager.GetString("Settings_Screenshot_ShowCursor", resourceCulture);

	public static string Settings_Screenshot_UseLastSelectionArea => ResourceManager.GetString("Settings_Screenshot_UseLastSelectionArea", resourceCulture);

	public static string Settings_TabHeader_About => ResourceManager.GetString("Settings_TabHeader_About", resourceCulture);

	public static string Settings_TabHeader_General => ResourceManager.GetString("Settings_TabHeader_General", resourceCulture);

	public static string Settings_TabHeader_Hotkeys => ResourceManager.GetString("Settings_TabHeader_Hotkeys", resourceCulture);

	public static string Settings_TabHeader_Screenshots => ResourceManager.GetString("Settings_TabHeader_Screenshots", resourceCulture);

	public static string Settings_TabHeader_Video => ResourceManager.GetString("Settings_TabHeader_Video", resourceCulture);

	public static string Settings_Video_FileNameTemplate => ResourceManager.GetString("Settings_Video_FileNameTemplate", resourceCulture);

	public static string Settings_Video_FileNameTemplateDefaultValue => ResourceManager.GetString("Settings_Video_FileNameTemplateDefaultValue", resourceCulture);

	public static string Settings_Video_GroupHeader_Misc => ResourceManager.GetString("Settings_Video_GroupHeader_Misc", resourceCulture);

	public static string Settings_Video_GroupHeader_Quality => ResourceManager.GetString("Settings_Video_GroupHeader_Quality", resourceCulture);

	public static string Settings_Video_GroupHeader_Sources => ResourceManager.GetString("Settings_Video_GroupHeader_Sources", resourceCulture);

	public static string Settings_Video_GroupHeader_WhereToSave => ResourceManager.GetString("Settings_Video_GroupHeader_WhereToSave", resourceCulture);

	public static string Settings_Video_HighlightMouseClicks => ResourceManager.GetString("Settings_Video_HighlightMouseClicks", resourceCulture);

	public static string Settings_Video_HighlightMousePointer => ResourceManager.GetString("Settings_Video_HighlightMousePointer", resourceCulture);

	public static string Settings_Video_MaxDuration_Cloud_Free => ResourceManager.GetString("Settings_Video_MaxDuration_Cloud_Free", resourceCulture);

	public static string Settings_Video_OpenInBrowser => ResourceManager.GetString("Settings_Video_OpenInBrowser", resourceCulture);

	public static string Settings_Video_Quality => ResourceManager.GetString("Settings_Video_Quality", resourceCulture);

	public static string Settings_Video_SavePath => ResourceManager.GetString("Settings_Video_SavePath", resourceCulture);

	public static string Settings_Video_SaveType_Cloud => ResourceManager.GetString("Settings_Video_SaveType_Cloud", resourceCulture);

	public static string Settings_Video_SaveType_File => ResourceManager.GetString("Settings_Video_SaveType_File", resourceCulture);

	public static string Settings_Video_ShowControlPanel => ResourceManager.GetString("Settings_Video_ShowControlPanel", resourceCulture);

	public static string Settings_Video_ShowCursor => ResourceManager.GetString("Settings_Video_ShowCursor", resourceCulture);

	public static string Settings_Video_ShowMutedMicrophoneWarning => ResourceManager.GetString("Settings_Video_ShowMutedMicrophoneWarning", resourceCulture);

	public static string Settings_Video_Source_Camera => ResourceManager.GetString("Settings_Video_Source_Camera", resourceCulture);

	public static string Settings_Video_Source_CameraNotConnected => ResourceManager.GetString("Settings_Video_Source_CameraNotConnected", resourceCulture);

	public static string Settings_Video_Source_Microphone => ResourceManager.GetString("Settings_Video_Source_Microphone", resourceCulture);

	public static string Settings_Video_Source_MicrophoneNotConnected => ResourceManager.GetString("Settings_Video_Source_MicrophoneNotConnected", resourceCulture);

	public static string Settings_Video_Source_SystemSound => ResourceManager.GetString("Settings_Video_Source_SystemSound", resourceCulture);

	public static string Settings_Video_Source_SystemSoundNoSource => ResourceManager.GetString("Settings_Video_Source_SystemSoundNoSource", resourceCulture);

	public static string Settings_Video_StartRecordingImmediatelyAfterSelection => ResourceManager.GetString("Settings_Video_StartRecordingImmediatelyAfterSelection", resourceCulture);

	public static string SystemSound_GeneralError => ResourceManager.GetString("SystemSound_GeneralError", resourceCulture);

	public static string Tray_ContextMenu_About => ResourceManager.GetString("Tray_ContextMenu_About", resourceCulture);

	public static string Tray_ContextMenu_CheckForUpdates => ResourceManager.GetString("Tray_ContextMenu_CheckForUpdates", resourceCulture);

	public static string Tray_ContextMenu_Exit => ResourceManager.GetString("Tray_ContextMenu_Exit", resourceCulture);

	public static string Tray_ContextMenu_History => ResourceManager.GetString("Tray_ContextMenu_History", resourceCulture);

	public static string Tray_ContextMenu_MakeScreenshot => ResourceManager.GetString("Tray_ContextMenu_MakeScreenshot", resourceCulture);

	public static string Tray_ContextMenu_MakeScrollingCapture => ResourceManager.GetString("Tray_ContextMenu_MakeScrollingCapture", resourceCulture);

	public static string Tray_ContextMenu_RecordVideo => ResourceManager.GetString("Tray_ContextMenu_RecordVideo", resourceCulture);

	public static string Tray_ContextMenu_ShowSettings => ResourceManager.GetString("Tray_ContextMenu_ShowSettings", resourceCulture);

	public static string Tray_ContextMenu_SilentMode => ResourceManager.GetString("Tray_ContextMenu_SilentMode", resourceCulture);

	public static string Tray_Popup_MakeScreenshotToolTip => ResourceManager.GetString("Tray_Popup_MakeScreenshotToolTip", resourceCulture);

	public static string Tray_Popup_MakeScrollingCaptureToolTip => ResourceManager.GetString("Tray_Popup_MakeScrollingCaptureToolTip", resourceCulture);

	public static string Tray_Popup_RecordVideoToolTip => ResourceManager.GetString("Tray_Popup_RecordVideoToolTip", resourceCulture);

	public static string Unit_B => ResourceManager.GetString("Unit_B", resourceCulture);

	public static string Unit_GB => ResourceManager.GetString("Unit_GB", resourceCulture);

	public static string Unit_KB => ResourceManager.GetString("Unit_KB", resourceCulture);

	public static string Unit_MB => ResourceManager.GetString("Unit_MB", resourceCulture);

	public static string Unit_TB => ResourceManager.GetString("Unit_TB", resourceCulture);

	public static string VideoQuality_ => ResourceManager.GetString("VideoQuality_", resourceCulture);

	public static string VideoQuality_FullHD => ResourceManager.GetString("VideoQuality_FullHD", resourceCulture);

	public static string VideoQuality_FullHD60fps => ResourceManager.GetString("VideoQuality_FullHD60fps", resourceCulture);

	public static string VideoQuality_HD => ResourceManager.GetString("VideoQuality_HD", resourceCulture);

	public static string VideoQuality_SD => ResourceManager.GetString("VideoQuality_SD", resourceCulture);

	public static string VideoQuality_UHD4K => ResourceManager.GetString("VideoQuality_UHD4K", resourceCulture);

	public static string VideoRecording_Cancel_ConfirmationText => ResourceManager.GetString("VideoRecording_Cancel_ConfirmationText", resourceCulture);

	public static string VideoRecording_Cancel_NoButtonText => ResourceManager.GetString("VideoRecording_Cancel_NoButtonText", resourceCulture);

	public static string VideoRecording_Cancel_WindowTitle => ResourceManager.GetString("VideoRecording_Cancel_WindowTitle", resourceCulture);

	public static string VideoRecording_Cancel_YesButtonText => ResourceManager.GetString("VideoRecording_Cancel_YesButtonText", resourceCulture);

	public static string VideoRecording_CancelToolTip => ResourceManager.GetString("VideoRecording_CancelToolTip", resourceCulture);

	public static string VideoRecording_CloseToolTip => ResourceManager.GetString("VideoRecording_CloseToolTip", resourceCulture);

	public static string VideoRecording_LowDiskSpaceText => ResourceManager.GetString("VideoRecording_LowDiskSpaceText", resourceCulture);

	public static string VideoRecording_MutedMicrophone_CloseToolTip => ResourceManager.GetString("VideoRecording_MutedMicrophone_CloseToolTip", resourceCulture);

	public static string VideoRecording_MutedMicrophone_WarningText => ResourceManager.GetString("VideoRecording_MutedMicrophone_WarningText", resourceCulture);

	public static string VideoRecording_MuteMicrophoneToolTip => ResourceManager.GetString("VideoRecording_MuteMicrophoneToolTip", resourceCulture);

	public static string VideoRecording_MuteSystemSoundToolTip => ResourceManager.GetString("VideoRecording_MuteSystemSoundToolTip", resourceCulture);

	public static string VideoRecording_NoCamerasToolTip => ResourceManager.GetString("VideoRecording_NoCamerasToolTip", resourceCulture);

	public static string VideoRecording_NoMicrophonesToolTip => ResourceManager.GetString("VideoRecording_NoMicrophonesToolTip", resourceCulture);

	public static string VideoRecording_NoSystemSoundSourcesToolTip => ResourceManager.GetString("VideoRecording_NoSystemSoundSourcesToolTip", resourceCulture);

	public static string VideoRecording_PauseButtonText => ResourceManager.GetString("VideoRecording_PauseButtonText", resourceCulture);

	public static string VideoRecording_PauseToolTip => ResourceManager.GetString("VideoRecording_PauseToolTip", resourceCulture);

	public static string VideoRecording_ResumeButtonText => ResourceManager.GetString("VideoRecording_ResumeButtonText", resourceCulture);

	public static string VideoRecording_ResumeToolTip => ResourceManager.GetString("VideoRecording_ResumeToolTip", resourceCulture);

	public static string VideoRecording_SelectCameraToolTip => ResourceManager.GetString("VideoRecording_SelectCameraToolTip", resourceCulture);

	public static string VideoRecording_SelectMicrophoneToolTip => ResourceManager.GetString("VideoRecording_SelectMicrophoneToolTip", resourceCulture);

	public static string VideoRecording_SelectSystemSoundToolTip => ResourceManager.GetString("VideoRecording_SelectSystemSoundToolTip", resourceCulture);

	public static string VideoRecording_StartButtonText => ResourceManager.GetString("VideoRecording_StartButtonText", resourceCulture);

	public static string VideoRecording_StartToolTip => ResourceManager.GetString("VideoRecording_StartToolTip", resourceCulture);

	public static string VideoRecording_StopButtonText => ResourceManager.GetString("VideoRecording_StopButtonText", resourceCulture);

	public static string VideoRecording_StopToolTip => ResourceManager.GetString("VideoRecording_StopToolTip", resourceCulture);

	public static string VideoRecording_TurnOffCameraToolTip => ResourceManager.GetString("VideoRecording_TurnOffCameraToolTip", resourceCulture);

	public static string VideoRecording_TurnOnCameraToolTip => ResourceManager.GetString("VideoRecording_TurnOnCameraToolTip", resourceCulture);

	public static string VideoRecording_UnmuteMicrophoneToolTip => ResourceManager.GetString("VideoRecording_UnmuteMicrophoneToolTip", resourceCulture);

	public static string VideoRecording_UnmuteSystemSoundToolTip => ResourceManager.GetString("VideoRecording_UnmuteSystemSoundToolTip", resourceCulture);

	public static string WarningWindowTitle => ResourceManager.GetString("WarningWindowTitle", resourceCulture);

	public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

	internal Resources()
	{
	}

	public static void RaiseAllPropertiesChanged()
	{
		StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(null));
	}

	static Resources()
	{
		if (AppDesignMode.Enabled)
		{
			Culture = CultureInfo.GetCultureInfo("ru");
		}
	}
}
