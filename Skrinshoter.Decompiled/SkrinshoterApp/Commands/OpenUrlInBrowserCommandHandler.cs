using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp.Commands;

public class OpenUrlInBrowserCommandHandler : IRequestHandler<OpenUrlInBrowserCommand, Unit>
{
	private readonly ILogger logger;

	public OpenUrlInBrowserCommandHandler(ILogger<OpenUrlInBrowserCommandHandler> logger)
	{
		this.logger = logger;
	}

	public Task<Unit> Handle(OpenUrlInBrowserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = request.Url,
				UseShellExecute = true
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to open URL in browser");
		}
		return Unit.Task;
	}
}
