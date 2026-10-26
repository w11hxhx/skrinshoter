using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp.Commands;

public class OpenFolderWithFileSelectedCommandHandler : IRequestHandler<OpenFolderWithFileSelectedCommand, Unit>
{
	private readonly ILogger logger;

	public OpenFolderWithFileSelectedCommandHandler(ILogger<OpenFolderWithFileSelectedCommandHandler> logger)
	{
		this.logger = logger;
	}

	public Task<Unit> Handle(OpenFolderWithFileSelectedCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (File.Exists(request.Path))
			{
				Process.Start("explorer.exe", "/select,\"" + request.Path + "\"");
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to open folder with file selected");
		}
		return Unit.Task;
	}
}
