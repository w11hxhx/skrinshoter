using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class StopVideoRecordingCommandHandler : IRequestHandler<StopVideoRecordingCommand, Unit>
{
	private readonly VideoModuleAppProxy videoModule;

	private readonly NotificationService notificationService;

	private readonly ILogger logger;

	public StopVideoRecordingCommandHandler(VideoModuleAppProxy videoModule, NotificationService notificationService, ILogger<StopVideoRecordingCommandHandler> logger)
	{
		this.videoModule = videoModule;
		this.notificationService = notificationService;
		this.logger = logger;
	}

	public async Task<Unit> Handle(StopVideoRecordingCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await videoModule.SendCommand(VideoModuleCommand.stop);
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to stop video recording");
		}
		return Unit.Value;
	}
}
