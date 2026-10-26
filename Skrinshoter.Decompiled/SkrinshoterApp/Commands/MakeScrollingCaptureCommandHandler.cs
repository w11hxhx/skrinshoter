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

public class MakeScrollingCaptureCommandHandler : MakeScreenshotCommandHandler, IRequestHandler<MakeScrollingCaptureCommand, Unit>
{
	private readonly ScrollingCaptureWindowManager scrollingCaptureWindowManager;

	private readonly ScrollingCaptureAvailabilityService scrollingCaptureAvailabilityService;

	public MakeScrollingCaptureCommandHandler(VideoModuleAppProxy videoModule, SettingsManager settingsManager, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, NotificationService notificationService, ClipboardService clipboardService, AudioPlayer audioPlayer, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, IMediator mediator, ScrollingCaptureWindowManager scrollingCaptureWindowManager, ScrollingCaptureAvailabilityService scrollingCaptureAvailabilityService, ScreenshotUploadCoordinator screenshotUploadCoordinator, ILogger<MakeScrollingCaptureCommandHandler> logger)
		: base(videoModule, settingsManager, appInfoProvider, foregroundWindowInfoProvider, notificationService, clipboardService, audioPlayer, operationStateService, templateFileNameResolver, screenshotFileFormatsProvider, mediator, screenshotUploadCoordinator, logger)
	{
		this.scrollingCaptureWindowManager = scrollingCaptureWindowManager;
		this.scrollingCaptureAvailabilityService = scrollingCaptureAvailabilityService;
	}

	public async Task<Unit> Handle(MakeScrollingCaptureCommand request, CancellationToken cancellationToken)
	{
		if (operationStateService.TryStartOperation(OperationType.Screenshot))
		{
			try
			{
				await Execute();
			}
			finally
			{
				operationStateService.StopOperation();
			}
		}
		return Unit.Value;
	}

	private async Task Execute()
	{
		if (!scrollingCaptureAvailabilityService.CanCreateScrollingCapture())
		{
			notificationService.ShowMessage(string.Format(Resources.Notify_ScrollingCaptureLimitReached, scrollingCaptureAvailabilityService.ScrollingCaptureSuccessCount, 20));
			logger.LogWarning("Failed to make scrolling capture: limit reached");
			return;
		}
		try
		{
			string windowTitle = foregroundWindowInfoProvider.Title;
			ScreenshotEditingResult screenshotEditingResult = await scrollingCaptureWindowManager.Show(Path.GetFileNameWithoutExtension(GetLocalFileName(windowTitle)));
			if (screenshotEditingResult.Action != ScreenshotEditingAction.Close)
			{
				await PerformScreenshotEditingAction(screenshotEditingResult, windowTitle, isScrollingCapture: true);
				scrollingCaptureAvailabilityService.OnScrollingCaptureSucceeded();
			}
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to make scrolling capture");
		}
		finally
		{
			scrollingCaptureWindowManager.Hide();
		}
	}
}
