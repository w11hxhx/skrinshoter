using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.SkrSlaveApp;

public class TakeScreenshotSlaveAppCommand
{
	private readonly TimeSpan MaxExecutionTime = TimeSpan.FromSeconds(10.0);

	private readonly IIPCProxy videoModule;

	private readonly string monitorName;

	private readonly Rectangle captureArea;

	private readonly string sharedMemoryName;

	private readonly bool showMousePointer;

	private readonly ScreenCaptureApi screenCaptureApi;

	private readonly ILogger logger;

	public TakeScreenshotSlaveAppCommand(IIPCProxy videoModule, string monitorName, Rectangle captureArea, string sharedMemoryName, bool showMousePointer, ScreenCaptureApi screenCaptureApi, ILogger logger)
	{
		this.videoModule = videoModule;
		this.monitorName = monitorName;
		this.captureArea = captureArea;
		this.sharedMemoryName = sharedMemoryName;
		this.showMousePointer = showMousePointer;
		this.screenCaptureApi = screenCaptureApi;
		this.logger = logger;
	}

	public async Task Execute()
	{
		if (screenCaptureApi != ScreenCaptureApi.Auto)
		{
			await TakeScreenshot(screenCaptureApi);
			return;
		}
		await TakeScreenshot(ScreenCaptureApi.GDI);
		if (IsEntireImageBlack())
		{
			logger.LogWarning("Screenshot is completely black");
		}
	}

	private async Task TakeScreenshot(ScreenCaptureApi api)
	{
		for (int retriesCounter = 0; retriesCounter < 4; retriesCounter++)
		{
			if (retriesCounter > 0)
			{
				await VideoModuleAppProxy.RestartVideoModuleAsync(videoModule, retriesCounter);
			}
			Rectangle rectangle = captureArea;
			if (api != ScreenCaptureApi.GDI)
			{
				Rectangle rectangle2 = new MonitorInfo(monitorName).GetRectangle();
				rectangle.Offset(-rectangle2.X, -rectangle2.Y);
			}
			await videoModule.SendCommand(VideoModuleCommand.screenshot, new List<string>(5)
			{
				api.ToString(),
				monitorName,
				string.Join(",", new _003C_003Ez__ReadOnlyArray<int>(new int[4] { rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height })),
				sharedMemoryName,
				showMousePointer ? "1" : "0"
			});
			string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.screenshot, MaxExecutionTime);
			if (array == null || array.Count() < 1)
			{
				throw new Exception("Invalid response from video module for command 'screenshot'");
			}
			string text = array[0].ToLower();
			if (text == "ready")
			{
				return;
			}
			if (text == "error")
			{
				if (!Enum.TryParse<ErrorCode>(array[1], out var result) || result != ErrorCode.DuplicateOutputUnsupported)
				{
					throw new Exception("Taking screenshot has been failed with code " + array[1] + ": " + array[2]);
				}
				continue;
			}
			throw new Exception("TakeScreenshotSlaveAppCommand: Invalid execution path: " + string.Join(";", array));
		}
		throw new Exception("Cannot take screenshot after 4 tries.");
	}

	private unsafe bool IsEntireImageBlack()
	{
		using SharedMemoryRawImage sharedMemoryRawImage = new SharedMemoryRawImage(new ImageFormat(captureArea.Size, PixelFormat.BGRA), sharedMemoryName);
		using MemoryHandle memoryHandle = sharedMemoryRawImage.Data[0].Pin();
		int num = sharedMemoryRawImage.Format.Width * sharedMemoryRawImage.Format.Height;
		uint* pointer = (uint*)memoryHandle.Pointer;
		ulong* ptr = (ulong*)pointer;
		int num2 = num / 2;
		for (int i = 0; i < num2; i++)
		{
			if ((ptr[i] & 0xFFFFFF00FFFFFFL) != 0L)
			{
				return false;
			}
		}
		if ((num & 1) != 0 && (pointer[num - 1] & 0xFFFFFF) != 0)
		{
			return false;
		}
		return true;
	}
}
