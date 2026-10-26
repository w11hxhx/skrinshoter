using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;
using SkrinshoterApp.SkrSlaveApp;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;

namespace SkrinshoterApp.Commands;

public class MakeActiveMonitorScreenshotCommandHandler : MakeScreenshotCommandHandler, IRequestHandler<MakeActiveMonitorScreenshotCommand, Unit>
{
	public MakeActiveMonitorScreenshotCommandHandler(VideoModuleAppProxy videoModule, SettingsManager settingsManager, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, NotificationService notificationService, ClipboardService clipboardService, AudioPlayer audioPlayer, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, IMediator mediator, ScreenshotUploadCoordinator screenshotUploadCoordinator, ILogger<MakeActiveMonitorScreenshotCommandHandler> logger)
		: base(videoModule, settingsManager, appInfoProvider, foregroundWindowInfoProvider, notificationService, clipboardService, audioPlayer, operationStateService, templateFileNameResolver, screenshotFileFormatsProvider, mediator, screenshotUploadCoordinator, logger)
	{
	}

	public async Task<Unit> Handle(MakeActiveMonitorScreenshotCommand request, CancellationToken cancellationToken)
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
		_ = 1;
		try
		{
			string windowTitle = foregroundWindowInfoProvider.Title;
			MonitorInfo monitorInfo = new MonitorInfo(PInvoke.MonitorFromWindow(PInvoke.GetForegroundWindow(), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTOPRIMARY));
			Rectangle rectangle = monitorInfo.GetRectangle();
			using SharedMemoryRawImage image = new SharedMemoryRawImage(new ImageFormat(rectangle.Size, PixelFormat.BGRA));
			await new TakeScreenshotSlaveAppCommand(videoModule, monitorInfo.MonitorName, rectangle, image.SharedMemoryName, settings.DrawCursorOnScreenshot, ScreenCaptureApi.Auto, logger).Execute();
			await PerformScreenshotEditingAction(new ScreenshotEditingResult(ScreenshotEditingAction.Default, image), windowTitle);
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to make active monitor screenshot");
		}
	}
}
