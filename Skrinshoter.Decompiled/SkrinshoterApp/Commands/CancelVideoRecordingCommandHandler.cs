using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class CancelVideoRecordingCommandHandler : IRequestHandler<CancelVideoRecordingCommand, Unit>
{
	private readonly VideoModuleAppProxy videoModule;

	private readonly NotificationService notificationService;

	private readonly ILogger logger;

	public CancelVideoRecordingCommandHandler(VideoModuleAppProxy videoModule, NotificationService notificationService, ILogger<CancelVideoRecordingCommandHandler> logger)
	{
		this.videoModule = videoModule;
		this.notificationService = notificationService;
		this.logger = logger;
	}

	public async Task<Unit> Handle(CancelVideoRecordingCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await videoModule.SendCommand(VideoModuleCommand.cancel);
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to cancel video recording");
		}
		return Unit.Value;
	}
}
