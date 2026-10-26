using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.BackgroundServices;

public class VideoModuleAppProxyServer : IHostedService
{
	private readonly ILogger logger;

	private readonly VideoModuleAppProxy videoModuleProxy;

	private readonly Dispatcher dispatcher;

	private readonly CommandLineActionExecutor commandLineActionExecutor;

	private readonly NotificationService notificationService;

	private readonly CommandLineArguments commandLineArguments;

	private bool startupActionExecuted;

	public VideoModuleAppProxyServer(VideoModuleAppProxy videoModuleProxy, Dispatcher dispatcher, CommandLineActionExecutor commandLineActionExecutor, NotificationService notificationService, CommandLineArguments commandLineArguments, VideoRecordingWindowManager videoRecordingWindowManager, VideoRecordingMutedMicrophoneWarningManager mutedMicrophoneWarningManager, ILogger<VideoModuleAppProxyServer> logger)
	{
		this.logger = logger;
		this.videoModuleProxy = videoModuleProxy;
		this.dispatcher = dispatcher;
		this.commandLineActionExecutor = commandLineActionExecutor;
		this.notificationService = notificationService;
		this.commandLineArguments = commandLineArguments;
		videoModuleProxy.OnChildConnected += VideoModuleProxy_OnChildConnected;
		videoModuleProxy.OnFailedToRunChildProcess += VideoModuleProxy_OnFailedToRunChildProcess;
		videoModuleProxy.RegisterHandler(VideoModuleEvent.recordingProgress, videoRecordingWindowManager.UpdateRecordingProgress);
		videoModuleProxy.RegisterHandler(VideoModuleEvent.audioLevel, videoRecordingWindowManager.UpdateAudioLevel);
		videoModuleProxy.RegisterHandler(VideoModuleEvent.audioDeviceFailed, videoRecordingWindowManager.UpdateAudioDeviceFailed);
		videoModuleProxy.RegisterHandler(VideoModuleEvent.speechActivity, mutedMicrophoneWarningManager.UpdateSpeechActivity);
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("Starting video module");
		videoModuleProxy.RunAsync();
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private void VideoModuleProxy_OnChildConnected(object sender, EventArgs e)
	{
		if (!startupActionExecuted)
		{
			startupActionExecuted = true;
			dispatcher.BeginInvoke((Func<Task>)(async () =>
			{
				await commandLineActionExecutor.ExecuteAsync(commandLineArguments);
			}));
		}
	}

	private void VideoModuleProxy_OnFailedToRunChildProcess()
	{
		notificationService.ShowError(Resources.Notify_FailedToStartVideoModuleProcess, (NotificationOptions x) =>
		{
			x.WithCommand(new OpenSupportPageCommand());
		});
	}
}
