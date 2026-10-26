using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using FFmpeg;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using ScreenRecorder.Commands;

namespace ScreenRecorder;

internal static class Program
{
	private const string StartStopLogSeparator = "====================";

	[STAThread]
	public static async Task Main()
	{
		string appName = "Skrinshoter";
		GlobalDiagnosticsContext.Set("AppFolderName", appName);
		ILoggerFactory loggerFactory = LoggerFactory.Create((ILoggingBuilder builder) =>
		{
			builder.AddNLog();
		});
		Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger(typeof(Program).FullName);
		string productVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
		logger.LogInformation("");
		logger.LogInformation("{separator} Video module for {appName} {version} started {separator}", "====================", appName, productVersion, "====================");
		try
		{
			ThreadPool.SetMinThreads(20, 20);
			FFmpegHelpers.SetupLogging(loggerFactory.CreateLogger("FFmpeg"));
			MessageChannel messageChannel = new MessageChannel();
			CommandFactory commandFactory = new CommandFactory();
			using CancellationTokenSource cts = new CancellationTokenSource();
			Task[] tasks = new Task[2]
			{
				new NamedPipeClient(messageChannel, commandFactory, loggerFactory.CreateLogger<NamedPipeClient>()).RunAsync($"{appName}VideoModulePipe_{WindowsIdentity.GetCurrent().User}", cts.Token),
				new CommandExecutor(messageChannel, loggerFactory).RunAsync(cts.Token)
			};
			await Task.WhenAny(tasks);
			cts.Cancel();
			await Task.WhenAll(tasks);
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception exception)
		{
			logger.LogCritical(exception, "Unhandled exception.");
		}
		finally
		{
			logger.LogInformation("{separator} Video module for {appName} {version} exited {separator}", "====================", appName, productVersion, "====================");
		}
	}
}
