using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class ToggleVideoRecordingPauseCommandHandler : IRequestHandler<ToggleVideoRecordingPauseCommand, Unit>
{
	private readonly VideoModuleAppProxy videoModule;

	private readonly VideoRecordingWindowManager videoRecordingWindowManager;

	private readonly NotificationService notificationService;

	private readonly ILogger logger;

	public ToggleVideoRecordingPauseCommandHandler(VideoModuleAppProxy videoModule, VideoRecordingWindowManager videoRecordingWindowManager, NotificationService notificationService, ILogger<ToggleVideoRecordingPauseCommandHandler> logger)
	{
		this.videoModule = videoModule;
		this.videoRecordingWindowManager = videoRecordingWindowManager;
		this.notificationService = notificationService;
		this.logger = logger;
	}

	public async Task<Unit> Handle(ToggleVideoRecordingPauseCommand request, CancellationToken cancellationToken)
	{
		_ = 1;
		try
		{
			await videoModule.SendCommand(VideoModuleCommand.togglePause);
			string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.togglePause, TimeSpan.FromSeconds(5.0));
			if (array == null)
			{
				throw new Exception("togglePause must return value, but returned null");
			}
			if (array.Length > 1 && array[0].ToLower() == "ok" && bool.TryParse(array[1], out var result))
			{
				videoRecordingWindowManager.SetPauseState(result);
			}
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to toggle video recording pause");
		}
		return Unit.Value;
	}
}
