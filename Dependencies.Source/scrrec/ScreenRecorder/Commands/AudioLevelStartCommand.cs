using System.Collections.Generic;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class AudioLevelStartCommand : Command
{
	public string InputDeviceId { get; }

	public bool IsInputMuted { get; }

	public string OutputDeviceId { get; }

	public bool IsOutputMuted { get; }

	public AudioLevelStartCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.audioLevelStart)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 4);
		string text = commandArgumentReader.ReadString();
		InputDeviceId = ((text == "") ? null : text);
		IsInputMuted = commandArgumentReader.ReadFlag("IsInputMuted");
		string text2 = commandArgumentReader.ReadString();
		OutputDeviceId = ((text2 == "") ? null : text2);
		IsOutputMuted = commandArgumentReader.ReadFlag("IsOutputMuted");
	}
}
