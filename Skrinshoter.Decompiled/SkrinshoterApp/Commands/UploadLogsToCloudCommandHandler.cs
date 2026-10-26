using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;
using SkrinshoterApp.UseCases;

namespace SkrinshoterApp.Commands;

public class UploadLogsToCloudCommandHandler : IRequestHandler<UploadLogsToCloudCommand, Unit>
{
	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly NotificationService notificationService;

	private readonly IMediator mediator;

	private readonly CloudFileUploadService cloudFileUploadService;

	private readonly ILogger logger;

	public UploadLogsToCloudCommandHandler(SettingsManager settingsManager, NotificationService notificationService, IMediator mediator, CloudFileUploadService cloudFileUploadService, ILogger<UploadLogsToCloudCommandHandler> logger)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.notificationService = notificationService;
		this.mediator = mediator;
		this.cloudFileUploadService = cloudFileUploadService;
		this.logger = logger;
	}

	public async Task<Unit> Handle(UploadLogsToCloudCommand request, CancellationToken cancellationToken)
	{
		_ = 3;
		try
		{
			await new SkrFileUids(settings).PopNext();
			settingsManager.SaveSettings();
			string tempFolderPath = Path.GetTempPath();
			string text = DateTime.Now.ToString("ddMMyy_HHmmss");
			string zipFileName = "skrlogs_" + text;
			string zipFilePath = "";
			try
			{
				await Task.Run(() => zipFilePath = new ArchiveAppDataFolderUseCase(zipFileName, tempFolderPath).Execute());
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to archive logs");
			}
			if (!File.Exists(zipFilePath))
			{
				logger.LogWarning("SendErrorLogsUseCase: Zip file of App Data not found");
				return Unit.Value;
			}
			long startTime = Stopwatch.GetTimestamp();
			string baseFileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(zipFileName.Replace(".zip", "")));
			string url = await cloudFileUploadService.UploadFile(zipFilePath, CloudFileUploadType.File, baseFileName, ".zip", "", isScrollingCapture: false, respectSilentMode: false);
			logger.LogInformation("Logs uploaded to cloud [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(startTime));
			await mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(url), default(CancellationToken));
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception exception2)
		{
			logger.LogError(exception2, "Failed to upload logs to cloud");
			notificationService.ShowError(Resources.Notify_FailedToUploadLogs);
		}
		return Unit.Value;
	}
}
