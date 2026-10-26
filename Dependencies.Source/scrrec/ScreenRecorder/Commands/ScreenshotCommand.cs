using System.Collections.Generic;
using System.Drawing;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class ScreenshotCommand : Command
{
	public ScreenCaptureApi ScreenCaptureApi { get; }

	public string MonitorName { get; }

	public Rectangle CaptureArea { get; }

	public string SharedMemoryName { get; }

	public bool ShowMousePointer { get; }

	public ScreenshotCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.screenshot)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 5);
		ScreenCaptureApi = commandArgumentReader.ReadEnum<ScreenCaptureApi>();
		MonitorName = commandArgumentReader.ReadString();
		CaptureArea = commandArgumentReader.ReadRectangle("CaptureArea");
		SharedMemoryName = commandArgumentReader.ReadNonEmptyString("SharedMemoryName");
		ShowMousePointer = commandArgumentReader.ReadFlag("ShowMousePointer");
	}
}
