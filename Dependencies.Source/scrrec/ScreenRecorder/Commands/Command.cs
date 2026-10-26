using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class Command
{
	public VideoModuleCommand Kind { get; }

	public Command(VideoModuleCommand kind)
	{
		Kind = kind;
	}
}
