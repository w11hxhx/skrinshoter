using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ScreenRecorderCommon;

namespace SkrinshoterApp.SkrChildApp;

public class CaptureStartSlaveAppCommand
{
	private const VideoModuleCommand Command = VideoModuleCommand.captureStart;

	private readonly TimeSpan MaxExecutionTime = TimeSpan.FromSeconds(10.0);

	private readonly IIPCProxy videoModule;

	private readonly string monitorName;

	private readonly Rectangle captureArea;

	private readonly ScreenCaptureApi screenCaptureApi;

	public CaptureStartSlaveAppCommand(IIPCProxy videoModule, string monitorName, Rectangle captureArea, ScreenCaptureApi screenCaptureApi)
	{
		this.videoModule = videoModule;
		this.monitorName = monitorName;
		this.captureArea = captureArea;
		this.screenCaptureApi = screenCaptureApi;
	}

	public async Task Execute()
	{
		for (int retriesCounter = 0; retriesCounter < 4; retriesCounter++)
		{
			if (retriesCounter > 0)
			{
				await VideoModuleAppProxy.RestartVideoModuleAsync(videoModule, retriesCounter);
			}
			await videoModule.SendCommand(VideoModuleCommand.captureStart, new List<string>(3)
			{
				screenCaptureApi.ToString(),
				monitorName,
				string.Join(",", new _003C_003Ez__ReadOnlyArray<int>(new int[4] { captureArea.Left, captureArea.Top, captureArea.Width, captureArea.Height }))
			});
			string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.captureStart, MaxExecutionTime);
			if (array == null || array.Count() < 1)
			{
				throw new Exception($"Invalid response from video module for command '{VideoModuleCommand.captureStart}'.");
			}
			if (!(array[0].ToLower() == "error"))
			{
				return;
			}
			if (Enum.TryParse<ErrorCode>(array[1], out var result))
			{
				switch (result)
				{
				default:
					throw new ErrorCodeException(result, null);
				case ErrorCode.General:
					break;
				case ErrorCode.DuplicateOutputUnsupported:
					continue;
				}
			}
			throw new Exception($"Command '{VideoModuleCommand.captureStart}' failed with code {array[1]}: {array[2]}.");
		}
		throw new Exception("Cannot start capture session after 4 tries.");
	}
}
