using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Events;

internal class RecordingProgressEvent : Event
{
	public TimeSpan Duration { get; }

	public TimeSpan MaxDuration { get; }

	public RecordingProgressEvent(TimeSpan duration, TimeSpan maxDuration)
		: base(VideoModuleEvent.recordingProgress)
	{
		if (duration < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("duration");
		}
		if (maxDuration < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("maxDuration");
		}
		Duration = duration;
		MaxDuration = maxDuration;
	}

	public override string ToString()
	{
		return base.ToString() + $"|{Duration}|{MaxDuration}";
	}
}
