using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using KeyboardHook;
using MediatR;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class SettingsWindowVM : LocalizableObservableObject
{
	private readonly SettingsManager settingsManager;

	private readonly IMediator mediator;

	private readonly LanguagesProvider languagesProvider;

	private readonly ScreenshotFileFormatsProvider screenshotFileFormatsProvider;

	private readonly JpegQualityLevelsProvider jpegQualityLevelsProvider;

	private readonly VideoQualityLevelsProvider videoQualityLevelsProvider;

	private readonly ClipboardService clipboardService;

	[ObservableProperty]
	private string windowTitle;

	[ObservableProperty]
	private int selectedTabIndex = 1;

	[ObservableProperty]
	private string version;

	[ObservableProperty]
	private bool isSkrinshoter;

	[ObservableProperty]
	private bool isJpegSelected;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? switchToAboutTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<Window>? selectScreenshotSaveFolderCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<Window>? selectVideoSaveFolderCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? checkForUpdatesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? reportErrorCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyAppIdToClipboardCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<string>? openUrlCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? editScreenshotFileNameTemplateCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? editVideoFileNameTemplateCommand;

	private RelayCommand? selectAllScreenshotUploadServicesCommand;

	public Settings Settings => settingsManager.Settings;

	public bool IsMutedMicrophoneWarningSupported => WindowCaptureExclusion.CanHideWindowsFromCapture();

	public AppInfoProvider AppInfoProvider { get; }

	public ObservableCollection<SelectionItem<string>> Languages { get; } = new ObservableCollection<SelectionItem<string>>();

	public ObservableCollection<SelectionItem<ScreenshotFileFormat>> ScreenshotFileFormats { get; } = new ObservableCollection<SelectionItem<ScreenshotFileFormat>>();

	public ObservableCollection<SelectionItem<int>> JpegQualityLevels { get; } = new ObservableCollection<SelectionItem<int>>();

	public ObservableCollection<SelectionItem<VideoQualityLevel>> VideoQualityLevels { get; } = new ObservableCollection<SelectionItem<VideoQualityLevel>>();

	public ObservableCollection<SelectionItem<TrayClickAction>> TrayClickActions { get; } = new ObservableCollection<SelectionItem<TrayClickAction>>();

	public SelectAudioDevicesVM SelectAudioDevicesVM { get; }

	public SelectCameraDevicesVM SelectCameraDevicesVM { get; }

	public KeyboardHotkeyMonitor KeyboardHotkeyMonitor { get; }

	public CloudStorageVM CloudStorageVM { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string WindowTitle
	{
		get
		{
			return windowTitle;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(windowTitle, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowTitle);
				windowTitle = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowTitle);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int SelectedTabIndex
	{
		get
		{
			return selectedTabIndex;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(selectedTabIndex, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedTabIndex);
				selectedTabIndex = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedTabIndex);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Version
	{
		get
		{
			return version;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(version, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Version);
				version = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Version);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSkrinshoter
	{
		get
		{
			return isSkrinshoter;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isSkrinshoter, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsSkrinshoter);
				isSkrinshoter = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsSkrinshoter);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsJpegSelected
	{
		get
		{
			return isJpegSelected;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isJpegSelected, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsJpegSelected);
				isJpegSelected = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsJpegSelected);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SwitchToAboutTabCommand => switchToAboutTabCommand ?? (switchToAboutTabCommand = new RelayCommand(SwitchToAboutTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<Window> SelectScreenshotSaveFolderCommand => selectScreenshotSaveFolderCommand ?? (selectScreenshotSaveFolderCommand = new RelayCommand<Window>(SelectScreenshotSaveFolder));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<Window> SelectVideoSaveFolderCommand => selectVideoSaveFolderCommand ?? (selectVideoSaveFolderCommand = new RelayCommand<Window>(SelectVideoSaveFolder));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CheckForUpdatesCommand => checkForUpdatesCommand ?? (checkForUpdatesCommand = new AsyncRelayCommand(CheckForUpdates));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ReportErrorCommand => reportErrorCommand ?? (reportErrorCommand = new AsyncRelayCommand(ReportError));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyAppIdToClipboardCommand => copyAppIdToClipboardCommand ?? (copyAppIdToClipboardCommand = new RelayCommand(CopyAppIdToClipboard));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<string> OpenUrlCommand => openUrlCommand ?? (openUrlCommand = new AsyncRelayCommand<string>(OpenUrl));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand EditScreenshotFileNameTemplateCommand => editScreenshotFileNameTemplateCommand ?? (editScreenshotFileNameTemplateCommand = new AsyncRelayCommand(EditScreenshotFileNameTemplate));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand EditVideoFileNameTemplateCommand => editVideoFileNameTemplateCommand ?? (editVideoFileNameTemplateCommand = new AsyncRelayCommand(EditVideoFileNameTemplate));

	public IRelayCommand SelectAllScreenshotUploadServicesCommand => selectAllScreenshotUploadServicesCommand ?? (selectAllScreenshotUploadServicesCommand = new RelayCommand(SelectAllScreenshotUploadServices));

	public SettingsWindowVM(SettingsManager settingsManager, IMediator mediator, AppInfoProvider appInfoProvider, LanguagesProvider languagesProvider, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, JpegQualityLevelsProvider jpegQualityLevelsProvider, VideoQualityLevelsProvider videoQualityLevelsProvider, SelectAudioDevicesVM selectAudioDevicesVM, SelectCameraDevicesVM selectCameraDevicesVM, ClipboardService clipboardService, KeyboardHotkeyMonitor keyboardHotkeyMonitor, CloudStorageVM cloudStorageVM)
	{
		this.settingsManager = settingsManager;
		this.mediator = mediator;
		AppInfoProvider = appInfoProvider;
		this.languagesProvider = languagesProvider;
		this.screenshotFileFormatsProvider = screenshotFileFormatsProvider;
		this.jpegQualityLevelsProvider = jpegQualityLevelsProvider;
		this.videoQualityLevelsProvider = videoQualityLevelsProvider;
		SelectAudioDevicesVM = selectAudioDevicesVM;
		SelectCameraDevicesVM = selectCameraDevicesVM;
		this.clipboardService = clipboardService;
		KeyboardHotkeyMonitor = keyboardHotkeyMonitor;
		CloudStorageVM = cloudStorageVM;
		version = appInfoProvider.Version.ToString();
		isSkrinshoter = AppInfoProvider.IsSkrinshoter;
		settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
		UpdateProperties();
	}

	[RelayCommand]
	private void SwitchToAboutTab()
	{
		SelectedTabIndex = 4;
	}

	[RelayCommand]
	private void SelectScreenshotSaveFolder(Window owner)
	{
		string text = new SelectFolderDialog().ShowDialog(owner, Settings.ScreenshotSavePath, Resources.SelectSaveFolder_WindowTitle);
		if (text != null && new PathIsOkChecker(text).Check())
		{
			Settings.ScreenshotSavePath = text;
			settingsManager.SaveSettings();
		}
	}

	[RelayCommand]
	private void SelectVideoSaveFolder(Window owner)
	{
		string text = new SelectFolderDialog().ShowDialog(owner, Settings.VideoSavePath, Resources.SelectSaveFolder_WindowTitle);
		if (text != null && new PathIsOkChecker(text).Check())
		{
			Settings.VideoSavePath = text;
			settingsManager.SaveSettings();
		}
	}

	[RelayCommand]
	private Task CheckForUpdates()
	{
		return mediator.Send((IRequest<Unit>)new ShowAppUpdateCheckWindowCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task ReportError()
	{
		return mediator.Send((IRequest<Unit>)new UploadLogsToCloudCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private void CopyAppIdToClipboard()
	{
		clipboardService.SetText(AppInfoProvider.AppId);
	}

	[RelayCommand]
	private Task OpenUrl(string url)
	{
		return mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(url), default(CancellationToken));
	}

	[RelayCommand]
	private Task EditScreenshotFileNameTemplate()
	{
		return mediator.Send((IRequest<Unit>)new ShowFileNameTemplateWindowCommand(isScreenshotTemplate: true), default(CancellationToken));
	}

	[RelayCommand]
	private Task EditVideoFileNameTemplate()
	{
		return mediator.Send((IRequest<Unit>)new ShowFileNameTemplateWindowCommand(isScreenshotTemplate: false), default(CancellationToken));
	}

	private void SelectAllScreenshotUploadServices()
	{
		Settings.ScreenshotUploadDestinations = ScreenshotUploadDestination.All;
		OnPropertyChanged("Settings");
	}

	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		UpdateProperties();
	}

	public void SaveSettings()
	{
		settingsManager.SaveSettings();
	}

	private void UpdateProperties()
	{
		WindowTitle = ProductResources.AppName + " " + Version;
		string language = Settings.Language;
		Languages.Clear();
		foreach (SelectionItem<string> item in LanguagesProvider.SupportedLanguages.Select((string x) => new SelectionItem<string>(x, languagesProvider.GetDisplayName(x), isAccessible: true)))
		{
			Languages.Add(item);
		}
		Settings.Language = language;
		ScreenshotFileFormat screenshotFileFormat = Settings.ScreenshotFileFormat;
		ScreenshotFileFormats.Clear();
		foreach (SelectionItem<ScreenshotFileFormat> item2 in ScreenshotFileFormatsProvider.SupportedFormats.Select((ScreenshotFileFormat x) => new SelectionItem<ScreenshotFileFormat>(x, screenshotFileFormatsProvider.GetDisplayName(x), isAccessible: true)))
		{
			ScreenshotFileFormats.Add(item2);
		}
		Settings.ScreenshotFileFormat = screenshotFileFormat;
		IsJpegSelected = Settings.ScreenshotFileFormat == ScreenshotFileFormat.Jpeg;
		int jpegQualityLevel = Settings.JpegQualityLevel;
		JpegQualityLevels.Clear();
		foreach (SelectionItem<int> item3 in from x in JpegQualityLevelsProvider.SupportedLevels
			orderby x descending
			select new SelectionItem<int>(x, jpegQualityLevelsProvider.GetDisplayName(x), isAccessible: true))
		{
			JpegQualityLevels.Add(item3);
		}
		Settings.JpegQualityLevel = jpegQualityLevel;
		VideoQualityLevel videoQualityLevel = Settings.VideoQualityLevel;
		VideoQualityLevels.Clear();
		foreach (SelectionItem<VideoQualityLevel> item4 in VideoQualityLevelsProvider.SupportedLevels.Select((VideoQualityLevel x) => new SelectionItem<VideoQualityLevel>(x, videoQualityLevelsProvider.GetDisplayName(x), isAccessible: true)))
		{
			VideoQualityLevels.Add(item4);
		}
		Settings.VideoQualityLevel = videoQualityLevel;
		TrayClickAction trayLeftClickAction = Settings.TrayLeftClickAction;
		TrayClickActions.Clear();
		TrayClickActions.Add(new SelectionItem<TrayClickAction>(TrayClickAction.ShowPanel, Resources.Settings_General_Tray_ShowPanel, isAccessible: true));
		TrayClickActions.Add(new SelectionItem<TrayClickAction>(TrayClickAction.MakeRegionScreenshot, Resources.Settings_General_Tray_MakeRegionScreenshot, isAccessible: true));
		TrayClickActions.Add(new SelectionItem<TrayClickAction>(TrayClickAction.RecordVideo, Resources.Settings_General_Tray_RecordVideo, isAccessible: true));
		TrayClickActions.Add(new SelectionItem<TrayClickAction>(TrayClickAction.MakeScrollingCapture, Resources.Settings_General_Tray_MakeScrollingCapture, isAccessible: true));
		Settings.TrayLeftClickAction = trayLeftClickAction;
		OnPropertyChanged("Settings");
	}
}
