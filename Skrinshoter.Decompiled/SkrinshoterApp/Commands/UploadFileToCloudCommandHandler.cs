using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.Commands;

public class UploadFileToCloudCommandHandler : IRequestHandler<UploadFileToCloudCommand, Unit>
{
	private readonly NotificationService notificationService;

	private readonly IMediator mediator;

	private readonly CloudFileUploadService cloudFileUploadService;

	private readonly ILogger logger;

	public UploadFileToCloudCommandHandler(NotificationService notificationService, IMediator mediator, CloudFileUploadService cloudFileUploadService, ILogger<UploadFileToCloudCommandHandler> logger)
	{
		this.notificationService = notificationService;
		this.mediator = mediator;
		this.cloudFileUploadService = cloudFileUploadService;
		this.logger = logger;
	}

	public async Task<Unit> Handle(UploadFileToCloudCommand request, CancellationToken cancellationToken)
	{
		_ = 1;
		try
		{
			string text = request.FilePath ?? ShowOpenFileDialog();
			if (text == null)
			{
				return Unit.Value;
			}
			if (!File.Exists(text))
			{
				logger.LogError("File not found: {filePath}", text);
				return Unit.Value;
			}
			long startTime = Stopwatch.GetTimestamp();
			string baseFileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Path.GetFileNameWithoutExtension(text)));
			string url = await cloudFileUploadService.UploadFile(text, CloudFileUploadType.File, baseFileName, Path.GetExtension(text), "", isScrollingCapture: false, respectSilentMode: false);
			logger.LogInformation("File uploaded to cloud [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(startTime));
			await mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(url), default(CancellationToken));
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to upload file to cloud");
			notificationService.ShowError(Resources.Notify_FailedToUploadFileOfFileType);
		}
		return Unit.Value;
	}

	private string ShowOpenFileDialog()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Title = Resources.SelectFileForUpload_WindowTitle
		};
		if (openFileDialog.ShowDialog() != true)
		{
			return null;
		}
		return openFileDialog.FileName;
	}
}
