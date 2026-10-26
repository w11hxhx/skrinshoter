using System.Collections.Generic;
using AudioDevices;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class SetAudioDeviceCommand : Command
{
	public AudioDataFlow DataFlow { get; }

	public string DeviceId { get; }

	public bool IsMuted { get; }

	public SetAudioDeviceCommand(IReadOnlyCollection<string> args)
		: base(VideoModuleCommand.setAudioDevice)
	{
		CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 3);
		DataFlow = commandArgumentReader.ReadEnum<AudioDataFlow>();
		string text = commandArgumentReader.ReadString();
		DeviceId = ((text == "") ? null : text);
		IsMuted = commandArgumentReader.ReadFlag("IsMuted");
	}
}
