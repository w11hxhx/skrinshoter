using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp.Commands;

public class OpenMicrophoneSettingsCommandHandler : IRequestHandler<OpenMicrophoneSettingsCommand, Unit>
{
	private readonly ILogger logger;

	public OpenMicrophoneSettingsCommandHandler(ILogger<OpenMicrophoneSettingsCommandHandler> logger)
	{
		this.logger = logger;
	}

	public Task<Unit> Handle(OpenMicrophoneSettingsCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "ms-settings:privacy-microphone",
				UseShellExecute = true
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to open Windows microphone privacy settings");
		}
		return Unit.Task;
	}
}
