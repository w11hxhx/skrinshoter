using System;
using System.Collections.Generic;
using System.Drawing;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class CaptureStartCommand : Command
{
	public ScreenCaptureApi ScreenCaptureApi { get; }

	public string MonitorName { get; }

	public Rectangle CaptureArea { get; }

	public CaptureStartCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.captureStart)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 3);
		ScreenCaptureApi = commandArgumentReader.ReadEnum<ScreenCaptureApi>();
		MonitorName = commandArgumentReader.ReadNonEmptyString("MonitorName");
		CaptureArea = commandArgumentReader.ReadRectangle("CaptureArea");
		if (CaptureArea.Left < 0 || CaptureArea.Top < 0)
		{
			throw new FormatException(string.Format("{0} left and top must not be negative: {1},{2}.", "CaptureArea", CaptureArea.Left, CaptureArea.Top));
		}
	}
}
