using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class MakeRegionScreenshotCommandHandler : MakeScreenshotCommandHandler, IRequestHandler<MakeRegionScreenshotCommand, Unit>
{
	private readonly ScreenshotEditingWindowManager screenshotEditingWindowManager;

	public MakeRegionScreenshotCommandHandler(VideoModuleAppProxy videoModule, SettingsManager settingsManager, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, NotificationService notificationService, ClipboardService clipboardService, AudioPlayer audioPlayer, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, IMediator mediator, ScreenshotEditingWindowManager screenshotEditingWindowManager, ScreenshotUploadCoordinator screenshotUploadCoordinator, ILogger<MakeRegionScreenshotCommandHandler> logger)
		: base(videoModule, settingsManager, appInfoProvider, foregroundWindowInfoProvider, notificationService, clipboardService, audioPlayer, operationStateService, templateFileNameResolver, screenshotFileFormatsProvider, mediator, screenshotUploadCoordinator, logger)
	{
		this.screenshotEditingWindowManager = screenshotEditingWindowManager;
	}

	public async Task<Unit> Handle(MakeRegionScreenshotCommand request, CancellationToken cancellationToken)
	{
		if (operationStateService.TryStartOperation(OperationType.Screenshot))
		{
			long operationId = operationStateService.OperationId;
			try
			{
				await Execute(request.CommandInvokeType);
			}
			finally
			{
				operationStateService.StopOperation(operationId);
			}
		}
		return Unit.Value;
	}

	private async Task Execute(CommandInvokeType commandInvokeType)
	{
		long operationId = operationStateService.OperationId;
		try
		{
			string windowTitle = foregroundWindowInfoProvider.Title;
			ScreenshotEditingResult screenshotEditingResult = await screenshotEditingWindowManager.Show(Path.GetFileNameWithoutExtension(GetLocalFileName(windowTitle)), commandInvokeType);
			if (screenshotEditingResult.Action != ScreenshotEditingAction.Close)
			{
				await PerformScreenshotEditingAction(screenshotEditingResult, windowTitle);
			}
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to make region screenshot");
		}
		finally
		{
			if (operationStateService.OperationId == operationId)
				screenshotEditingWindowManager.Hide();
		}
	}
}
