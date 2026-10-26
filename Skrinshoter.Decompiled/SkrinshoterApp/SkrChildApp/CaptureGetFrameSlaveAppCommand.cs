using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScreenRecorderCommon;

namespace SkrinshoterApp.SkrChildApp;

public class CaptureGetFrameSlaveAppCommand
{
	private const VideoModuleCommand Command = VideoModuleCommand.captureGetFrame;

	private readonly TimeSpan MaxExecutionTime = TimeSpan.FromSeconds(5.0);

	private readonly IIPCProxy videoModule;

	private readonly string sharedMemoryName;

	private readonly TimeSpan timeout;

	public CaptureGetFrameSlaveAppCommand(IIPCProxy videoModule, string sharedMemoryName, TimeSpan timeout)
	{
		this.videoModule = videoModule;
		this.sharedMemoryName = sharedMemoryName;
		this.timeout = timeout;
	}

	public async Task<bool> Execute()
	{
		await videoModule.SendCommand(VideoModuleCommand.captureGetFrame, new List<string>(2)
		{
			sharedMemoryName,
			timeout.TotalMilliseconds.ToString()
		});
		string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.captureGetFrame, MaxExecutionTime);
		if (array == null || array.Count() < 1)
		{
			throw new Exception($"Invalid response from video module for command '{VideoModuleCommand.captureGetFrame}'.");
		}
		string text = array[0].ToLower();
		if (text == "error")
		{
			throw new Exception($"'{VideoModuleCommand.captureGetFrame}' command failed with code {array[1]}: {array[2]}.");
		}
		return text == "ready";
	}
}
