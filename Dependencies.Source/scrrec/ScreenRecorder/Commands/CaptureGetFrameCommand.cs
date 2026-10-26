using System;
using System.Collections.Generic;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class CaptureGetFrameCommand : Command
{
	public string SharedMemoryName { get; }

	public TimeSpan Timeout { get; }

	public CaptureGetFrameCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.captureGetFrame)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 2);
		SharedMemoryName = commandArgumentReader.ReadNonEmptyString("SharedMemoryName");
		int num = commandArgumentReader.ReadInt32("Timeout", 0, int.MaxValue);
		Timeout = TimeSpan.FromMilliseconds(num);
	}
}
