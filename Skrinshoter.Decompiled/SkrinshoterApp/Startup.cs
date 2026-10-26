using System;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using AudioDevices;
using CameraCapture;
using KeyboardHook;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MouseHook;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.CommandBehaviors;
using SkrinshoterApp.Services.Progress;
using SkrinshoterApp.SkrApi;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		if (AppDesignMode.Enabled)
		{
			services.AddSingleton<IMediator, Mediator>();
			services.AddSingleton((Func<IServiceProvider, IPublisher>)((IServiceProvider sp) => sp.GetRequiredService<IMediator>()));
		}
		else
		{
			RegisterMediatRRuntime(services);
		}
		if (AppDesignMode.Enabled)
		{
			services.AddSingleton((Func<IServiceProvider, IHost>)((IServiceProvider sp) => new DesignTimeHost(sp)));
		}
		services.AddSingleton<LoggerFactory>();
		services.AddSingleton<SystemInfoLogger>();
		services.AddSingleton<AppInfoProvider>();
		services.AddSingleton<App>();
		services.AddSingleton<AppShutdownService>();
		services.AddSingleton((IServiceProvider _) => Dispatcher.CurrentDispatcher);
		if (AppDesignMode.Enabled)
		{
			services.AddSingleton<CommandLineArguments>();
		}
		services.AddSingleton<CommandLineActionExecutor>();
		services.AddSingleton<VideoModuleAppProxy>();
		services.AddSingleton((IServiceProvider sp) => new AudioDeviceEnumerator(subscribeToChanges: true, sp.GetRequiredService<ILoggerFactory>()));
		services.AddSingleton<AudioDeviceResolver>();
		services.AddSingleton((Func<IServiceProvider, Func<AudioCaptureSession>>)((IServiceProvider sp) => () => ActivatorUtilities.CreateInstance<AudioCaptureSession>(sp, Array.Empty<object>())));
		services.AddSingleton<MediaFoundationInitializer>();
		services.AddSingleton<CameraDeviceMonitor>();
		services.AddSingleton<UserQuotaService>();
		services.AddSingleton<AppUpdateChecker>();
		services.AddSingleton<AudioPlayer>();
		services.AddSingleton<LocalizationManager>();
		services.AddSingleton<LanguagesProvider>();
		services.AddSingleton<UserCountryProvider>();
		services.AddSingleton<ScreenshotFileFormatsProvider>();
		services.AddSingleton<JpegQualityLevelsProvider>();
		services.AddSingleton<VideoQualityLevelsProvider>();
		services.AddSingleton<OperationStateService>();
		services.AddSingleton((Func<IServiceProvider, IOperationStateProvider>)((IServiceProvider sp) => sp.GetRequiredService<OperationStateService>()));
		services.AddSingleton<TrayIconService>();
		services.AddSingleton<NotificationService>();
		services.AddSingleton<ClipboardService>();
		services.AddSingleton<KeyboardHotkeyMonitor>();
		services.AddSingleton<MouseHotkeyMonitor>();
		services.AddSingleton<AutostartHandler>();
		services.AddSingleton<QuotaApiClient>();
		services.AddSingleton<ForegroundWindowTracker>();
		services.AddSingleton((Func<IServiceProvider, IForegroundWindowInfoProvider>)((IServiceProvider sp) => sp.GetRequiredService<ForegroundWindowTracker>()));
		services.AddSingleton<TemplateFileNameResolver>();
		services.AddSingleton<ScrollingCaptureAvailabilityService>();
		services.AddSingleton<ScreenshotSaveFileDialog>();
		services.AddSingleton<CloudFileUploadService>();
		services.AddSingleton<ExternalScreenshotUploadService>();
		services.AddSingleton<ScreenshotUploadCoordinator>();
		services.AddSingleton<SettingsStorage>();
		if (AppDesignMode.Enabled)
		{
			services.AddSingleton<Settings>();
		}
		else
		{
			services.AddSingleton((IServiceProvider sp) => sp.GetRequiredService<SettingsStorage>().Load());
		}
		services.AddSingleton<SettingsNormalizer>();
		services.AddSingleton<SettingsManager>();
		services.AddSingleton<CameraMicrophoneLinker>();
		services.AddSingleton<TrialStateStorage>();
		services.AddSingleton<TrialStateManager>();
		services.AddSingleton<NotifyIcon>();
		services.AddSingleton<NotifyIconVM>();
		services.AddSingleton<SettingsWindow>();
		services.AddSingleton<SettingsWindowVM>();
		services.AddSingleton<SelectAudioDevicesVM>();
		services.AddSingleton<SelectCameraDevicesVM>();
		services.AddSingleton<VideoRecordingWindowManager>();
		services.AddSingleton<VideoRecordingMutedMicrophoneWarningManager>();
		services.AddSingleton<ScreenshotEditingWindowManager>();
		services.AddSingleton<ScrollingCaptureWindowManager>();
		services.AddTransient<AppUpdateCheckWindowVM>();
		services.AddTransient<ProgressWindow>();
		services.AddSingleton<IProgressWindowService, ProgressWindowService>();
		if (AppDesignMode.Enabled)
		{
			services.AddSingleton((IServiceProvider sp) => new CloudStorageVMDesignFactory().Create(sp));
		}
		else
		{
			services.AddSingleton<CloudStorageVM>();
		}
		services.AddTransient<FileNameTemplateWindow>();
		services.AddTransient<FileNameTemplateWindowVM>();
		services.AddHostedService((IServiceProvider sp) => sp.GetRequiredService<UserQuotaService>());
		services.AddHostedService<AppInstanceCommandChannel>();
		services.AddHostedService<VideoModuleAppProxyServer>();
		services.AddHostedService<FreeSettingsEnforcer>();
		services.AddHostedService<KeyboardHotkeyService>();
		services.AddHostedService<MouseHotkeyService>();
		services.AddHostedService((IServiceProvider sp) => sp.GetRequiredService<AppUpdateChecker>());
		if (AppInfoProvider.IsSkrinshoter)
		{
			services.AddHostedService<StatsSender>();
		}
		services.AddHostedService((IServiceProvider sp) => sp.GetRequiredService<ForegroundWindowTracker>());
		services.AddHostedService<VisualObjectsShadowEffectController>();
		services.AddHttpClients();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void RegisterMediatRRuntime(IServiceCollection services)
	{
		services.AddMediatR((MediatRServiceConfiguration cfg) =>
		{
			cfg.RegisterServicesFromAssemblyContaining<Startup>();
		});
		services.AddTransient(typeof(IPipelineBehavior<, >), typeof(CommandValidationBehavior<, >));
		services.AddTransient(typeof(IPipelineBehavior<, >), typeof(CommandLoggingBehavior<, >));
	}
}
