using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dwm;

namespace SkrinshoterApp.Commands;

public class MakeActiveWindowScreenshotCommandHandler : MakeScreenshotCommandHandler, IRequestHandler<MakeActiveWindowScreenshotCommand, Unit>
{
	public MakeActiveWindowScreenshotCommandHandler(VideoModuleAppProxy videoModule, SettingsManager settingsManager, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, NotificationService notificationService, ClipboardService clipboardService, AudioPlayer audioPlayer, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, IMediator mediator, ScreenshotUploadCoordinator screenshotUploadCoordinator, ILogger<MakeActiveWindowScreenshotCommandHandler> logger)
		: base(videoModule, settingsManager, appInfoProvider, foregroundWindowInfoProvider, notificationService, clipboardService, audioPlayer, operationStateService, templateFileNameResolver, screenshotFileFormatsProvider, mediator, screenshotUploadCoordinator, logger)
	{
	}

	public async Task<Unit> Handle(MakeActiveWindowScreenshotCommand request, CancellationToken cancellationToken)
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
			HWND foregroundWindow = PInvoke.GetForegroundWindow();
			if (foregroundWindow == HWND.Null)
			{
				throw new WinApiException("GetForegroundWindow()");
			}
			RECT rECT = default;
			HRESULT hRESULT;
			unsafe
			{
				hRESULT = PInvoke.DwmGetWindowAttribute(foregroundWindow, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, &rECT, (uint)sizeof(RECT));
			}
			if (hRESULT.Failed)
			{
				throw new WinApiException("DwmGetWindowAttribute(DWMWA_EXTENDED_FRAME_BOUNDS)", Marshal.GetExceptionForHR(hRESULT));
			}
			Rectangle captureArea = rECT;
			List<MonitorInfo> list = (from x in VirtualScreen.GetMonitors()
				where x.GetRectangle().IntersectsWith(captureArea)
				select x).ToList();
			Rectangle rect = ((list.Count == 1) ? list[0].GetRectangle() : VirtualScreen.GetRectangle());
			captureArea.Intersect(rect);
			using SharedMemoryRawImage image = new SharedMemoryRawImage(new ImageFormat(captureArea.Size, PixelFormat.BGRA));
			await new TakeScreenshotSlaveAppCommand(videoModule, (list.Count == 1) ? list[0].MonitorName : "", captureArea, image.SharedMemoryName, settings.DrawCursorOnScreenshot, ScreenCaptureApi.Auto, logger).Execute();
			await PerformScreenshotEditingAction(new ScreenshotEditingResult(ScreenshotEditingAction.Default, image), windowTitle);
		}
		catch (Exception exception)
		{
			notificationService.ShowError(Resources.Notify_UnknownError, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
			logger.LogError(exception, "Failed to make active window screenshot");
		}
	}
}
