using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Events;

internal class Event : OutgoingMessage
{
	public VideoModuleEvent Kind { get; }

	public Event(VideoModuleEvent kind)
	{
		Kind = kind;
	}

	public override string ToString()
	{
		return $"event|{Kind}";
	}
}
