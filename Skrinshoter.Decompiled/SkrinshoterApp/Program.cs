using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp;

internal class Program
{
	private const string StartStopLogSeparator = "====================";

	private static Mutex mutex;

	public static IServiceProvider Services { get; private set; }

	[STAThread]
	public static void Main()
	{
		CommandLineArguments commandLineArguments = new CommandLineArguments();
		string name = string.Format(ProductResources.AppMutexName, WindowsIdentity.GetCurrent().User);
		mutex = new Mutex(initiallyOwned: true, name, out var createdNew);
		if (!createdNew)
		{
			if (commandLineArguments.Action == CommandLineAction.None)
			{
				commandLineArguments = new CommandLineArguments(CommandLineAction.ShowSettingsWindow);
			}
			AppInstanceCommandChannel.SendToRunningInstance(commandLineArguments);
			return;
		}
		GlobalDiagnosticsContext.Set("AppFolderName", ProductResources.AppFolderName);
		Logger logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();
		if (commandLineArguments.Action == CommandLineAction.Shutdown)
		{
			logger.Info("Shutdown requested, but no running instance found, exiting");
			return;
		}
		string informationalVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
		logger.Info("");
		logger.Info("==================== " + ProductResources.AppInvariantName + " " + informationalVersion + " started ====================");
		Dispatcher.CurrentDispatcher.UnhandledException += (object s, DispatcherUnhandledExceptionEventArgs e) =>
		{
			logger.Error(e.Exception, "Unhandled exception on UI thread.");
			e.Handled = true;
		};
		AppDomain.CurrentDomain.UnhandledException += (object s, UnhandledExceptionEventArgs e) =>
		{
			logger.Fatal(e.ExceptionObject as Exception, "Unhandled exception on non-UI thread.");
		};
		ThreadPool.SetMinThreads(20, 20);
		IHost host = null;
		try
		{
			Resources.Culture = CultureInfo.GetCultureInfo(LanguagesProvider.Default);
			IHostBuilder hostBuilder = Host.CreateDefaultBuilder().ConfigureLogging((ILoggingBuilder builder) =>
			{
				builder.ClearProviders();
				builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				builder.AddNLog();
			}).ConfigureServices((IServiceCollection services) =>
			{
				services.Configure((HostOptions o) =>
				{
					o.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
				});
			})
				.ConfigureServices(new Startup().ConfigureServices)
				.ConfigureServices((IServiceCollection services) =>
				{
					services.AddSingleton(commandLineArguments);
				});
			host = hostBuilder.Build();
			Services = host.Services;
			UIThread.Initialize(host.Services.GetRequiredService<Dispatcher>());
			Services.GetRequiredService<LocalizationManager>().SetInitialLanguage();
			SkrApiHttpClient.AppID = host.Services.GetRequiredService<AppInfoProvider>().AppId;
			SkrApiHttpClient.AppVersion = host.Services.GetRequiredService<AppInfoProvider>().Version.ToString();
			SkrApiHttpClient.AppName = ProductResources.AppInvariantName;
			App requiredService = host.Services.GetRequiredService<App>();
			requiredService.InitializeComponent();
			Services.GetRequiredService<SystemInfoLogger>().LogSystemInfo();
			Services.GetRequiredService<CameraMicrophoneLinker>();
			requiredService.Dispatcher.InvokeAsync(async () =>
			{
				Services.GetServices<IHostedService>().ToList();
				await host.StartAsync();
			});
			requiredService.Run();
		}
		catch (Exception exception)
		{
			logger.Fatal(exception, "Unhandled exception.");
		}
		finally
		{
			try
			{
				host?.Dispose();
			}
			catch
			{
			}
			logger.Info("==================== " + ProductResources.AppInvariantName + " " + informationalVersion + " exited ====================");
		}
	}
}
