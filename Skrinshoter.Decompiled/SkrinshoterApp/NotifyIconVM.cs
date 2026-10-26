using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class NotifyIconVM : LocalizableObservableObject
{
	private readonly Settings settings;

	private readonly IMediator mediator;

	private readonly IOperationStateProvider operationStateProvider;

	private readonly AppShutdownService appShutdownService;

	private readonly ILogger logger;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<(RoutedEventArgs e, MouseButton button)>? mouseUpCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<Action>? trayBalloonTipClickedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? contextMenuLoadedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popupOpenedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? makeScreenshotCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? makeScrollingCaptureCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? recordVideoCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? historyCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? showSettingsCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? aboutCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? checkForUpdatesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? exitCommand;

	public string MakeScreenshotHotkey => settings.AreaScreenshotHotkey.ToString();

	public string RecordVideoHotkey => settings.RecordVideoHotkey.ToString();

	public string IconToolTip => ProductResources.AppName;

	public string MakeScreenshotToolTip => Resources.Tray_Popup_MakeScreenshotToolTip + settings.AreaScreenshotHotkey.ToDisplaySuffix();

	public string RecordVideoToolTip => Resources.Tray_Popup_RecordVideoToolTip + settings.RecordVideoHotkey.ToDisplaySuffix();

	public string MakeScrollingCaptureToolTip => Resources.Tray_Popup_MakeScrollingCaptureToolTip;

	public Settings Settings => settings;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<(RoutedEventArgs e, MouseButton button)> MouseUpCommand => mouseUpCommand ?? (mouseUpCommand = new AsyncRelayCommand<(RoutedEventArgs, MouseButton)>(MouseUp));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<Action> TrayBalloonTipClickedCommand => trayBalloonTipClickedCommand ?? (trayBalloonTipClickedCommand = new AsyncRelayCommand<Action>(TrayBalloonTipClicked));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ContextMenuLoadedCommand => contextMenuLoadedCommand ?? (contextMenuLoadedCommand = new RelayCommand(ContextMenuLoaded));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopupOpenedCommand => popupOpenedCommand ?? (popupOpenedCommand = new RelayCommand(PopupOpened));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand MakeScreenshotCommand => makeScreenshotCommand ?? (makeScreenshotCommand = new AsyncRelayCommand(MakeScreenshot));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand MakeScrollingCaptureCommand => makeScrollingCaptureCommand ?? (makeScrollingCaptureCommand = new AsyncRelayCommand(MakeScrollingCapture));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand RecordVideoCommand => recordVideoCommand ?? (recordVideoCommand = new AsyncRelayCommand(RecordVideo));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand HistoryCommand => historyCommand ?? (historyCommand = new AsyncRelayCommand(History));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ShowSettingsCommand => showSettingsCommand ?? (showSettingsCommand = new AsyncRelayCommand(ShowSettings));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand AboutCommand => aboutCommand ?? (aboutCommand = new AsyncRelayCommand(About));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CheckForUpdatesCommand => checkForUpdatesCommand ?? (checkForUpdatesCommand = new AsyncRelayCommand(CheckForUpdates));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ExitCommand => exitCommand ?? (exitCommand = new AsyncRelayCommand(Exit));

	public NotifyIconVM(Settings settings, IMediator mediator, IOperationStateProvider operationStateProvider, AppShutdownService appShutdownService, ILogger<NotifyIconVM> logger)
	{
		this.settings = settings;
		this.mediator = mediator;
		this.operationStateProvider = operationStateProvider;
		this.appShutdownService = appShutdownService;
		this.logger = logger;
	}

	[RelayCommand]
	private async Task MouseUp((RoutedEventArgs e, MouseButton button) args)
	{
		if (operationStateProvider.CurrentOperation == OperationType.Video)
		{
			logger.LogInformation("Tray Icon: On mouse click when video recording is active");
			args.e.Handled = true;
			await mediator.Send((IRequest<Unit>)new StopVideoRecordingCommand(), default(CancellationToken));
		}
		else if (args.button == MouseButton.Left)
		{
			logger.LogInformation("Tray Icon: On left mouse click");
			switch (settings.TrayLeftClickAction)
			{
			case TrayClickAction.MakeRegionScreenshot:
				args.e.Handled = true;
				await mediator.Send((IRequest<Unit>)new MakeRegionScreenshotCommand(), default(CancellationToken));
				break;
			case TrayClickAction.RecordVideo:
				args.e.Handled = true;
				await mediator.Send((IRequest<Unit>)new RecordVideoCommand(), default(CancellationToken));
				break;
			case TrayClickAction.MakeScrollingCapture:
				args.e.Handled = true;
				await mediator.Send((IRequest<Unit>)new MakeScrollingCaptureCommand(), default(CancellationToken));
				break;
			}
		}
		else
		{
			logger.LogInformation("Tray Icon: On right mouse click");
		}
	}

	[RelayCommand]
	private async Task TrayBalloonTipClicked(Action action)
	{
		if (action != null)
		{
			action();
		}
		else
		{
			await mediator.Send((IRequest<Unit>)new ShowSettingsWindowCommand(), default(CancellationToken));
		}
	}

	[RelayCommand]
	private void ContextMenuLoaded()
	{
		OnPropertyChanged("MakeScreenshotHotkey");
		OnPropertyChanged("RecordVideoHotkey");
	}

	[RelayCommand]
	private void PopupOpened()
	{
		OnPropertyChanged("MakeScreenshotToolTip");
		OnPropertyChanged("RecordVideoToolTip");
	}

	[RelayCommand]
	private Task MakeScreenshot()
	{
		logger.LogInformation("Tray Menu: On make region screenshot");
		return mediator.Send((IRequest<Unit>)new MakeRegionScreenshotCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task MakeScrollingCapture()
	{
		logger.LogInformation("Tray Menu: On make scrolling capture");
		return mediator.Send((IRequest<Unit>)new MakeScrollingCaptureCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task RecordVideo()
	{
		logger.LogInformation("Tray Menu: On record video");
		return mediator.Send((IRequest<Unit>)new RecordVideoCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task History()
	{
		logger.LogInformation("Tray Menu: On history");
		return mediator.Send((IRequest<Unit>)new OpenHistoryCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task ShowSettings()
	{
		logger.LogInformation("Tray Menu: On settings");
		return mediator.Send((IRequest<Unit>)new ShowSettingsWindowCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task About()
	{
		logger.LogInformation("Tray Menu: On about");
		return mediator.Send((IRequest<Unit>)new ShowSettingsWindowCommand(switchToAboutTab: true), default(CancellationToken));
	}

	[RelayCommand]
	private Task CheckForUpdates()
	{
		logger.LogInformation("Tray Menu: On check for updates");
		return mediator.Send((IRequest<Unit>)new ShowAppUpdateCheckWindowCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task Exit()
	{
		logger.LogInformation("Tray Menu: On exit");
		return appShutdownService.ShutdownAsync();
	}
}
