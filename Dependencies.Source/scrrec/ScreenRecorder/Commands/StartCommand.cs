using System;
using System.Collections.Generic;
using System.Drawing;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class StartCommand : Command
{
	public ScreenCaptureApi ScreenCaptureApi { get; }

	public string MonitorName { get; }

	public Rectangle RecordingArea { get; }

	public int MaxOutputVideoHeight { get; }

	public int VideoFrameRate { get; }

	public string OutputFile { get; }

	public string InputAudioDeviceId { get; }

	public bool IsInputAudioDeviceMuted { get; }

	public string OutputAudioDeviceId { get; }

	public bool IsOutputAudioDeviceMuted { get; }

	public TimeSpan MaxRecordDuration { get; }

	public bool ShowMousePointer { get; }

	public bool HighlightMousePointer { get; }

	public bool HighlightMouseClicks { get; }

	public StartCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.start)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 14);
		ScreenCaptureApi = commandArgumentReader.ReadEnum<ScreenCaptureApi>();
		MonitorName = commandArgumentReader.ReadNonEmptyString("MonitorName");
		RecordingArea = commandArgumentReader.ReadRectangle("RecordingArea");
		if (RecordingArea.Left < 0 || RecordingArea.Top < 0)
		{
			throw new FormatException(string.Format("{0} left and top must not be negative: {1},{2}.", "RecordingArea", RecordingArea.Left, RecordingArea.Top));
		}
		if (RecordingArea.Width % 2 != 0 || RecordingArea.Height % 2 != 0)
		{
			throw new FormatException(string.Format("{0} width and height must be divisible by 2: {1},{2}.", "RecordingArea", RecordingArea.Width, RecordingArea.Height));
		}
		MaxOutputVideoHeight = commandArgumentReader.ReadInt32("MaxOutputVideoHeight", 1, int.MaxValue);
		if (MaxOutputVideoHeight % 2 != 0)
		{
			throw new FormatException(string.Format("{0} must be divisible by 2: {1}.", "MaxOutputVideoHeight", MaxOutputVideoHeight));
		}
		VideoFrameRate = commandArgumentReader.ReadInt32("VideoFrameRate", 1, 1000);
		OutputFile = commandArgumentReader.ReadNonEmptyString("OutputFile");
		string text = commandArgumentReader.ReadString();
		InputAudioDeviceId = ((text == "") ? null : text);
		IsInputAudioDeviceMuted = commandArgumentReader.ReadFlag("IsInputAudioDeviceMuted");
		string text2 = commandArgumentReader.ReadString();
		OutputAudioDeviceId = ((text2 == "") ? null : text2);
		IsOutputAudioDeviceMuted = commandArgumentReader.ReadFlag("IsOutputAudioDeviceMuted");
		int num = commandArgumentReader.ReadInt32("MaxRecordDuration", 1, int.MaxValue);
		MaxRecordDuration = TimeSpan.FromMinutes(num);
		ShowMousePointer = commandArgumentReader.ReadFlag("ShowMousePointer");
		HighlightMousePointer = commandArgumentReader.ReadFlag("HighlightMousePointer");
		HighlightMouseClicks = commandArgumentReader.ReadFlag("HighlightMouseClicks");
	}
}
