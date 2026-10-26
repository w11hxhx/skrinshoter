using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class SetAudioDeviceCommandHandler : IRequestHandler<SetAudioDeviceCommand, Unit>
{
	private readonly VideoModuleAppProxy videoModule;

	private readonly ILogger logger;

	public SetAudioDeviceCommandHandler(VideoModuleAppProxy videoModule, ILogger<SetAudioDeviceCommandHandler> logger)
	{
		this.videoModule = videoModule;
		this.logger = logger;
	}

	public async Task<Unit> Handle(SetAudioDeviceCommand request, CancellationToken cancellationToken)
	{
		try
		{
			await videoModule.SendCommand(VideoModuleCommand.setAudioDevice, new List<string>(3)
			{
				request.DataFlow.ToString(),
				request.DeviceId,
				request.IsMuted ? "1" : "0"
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to set audio device");
		}
		return Unit.Value;
	}
}
