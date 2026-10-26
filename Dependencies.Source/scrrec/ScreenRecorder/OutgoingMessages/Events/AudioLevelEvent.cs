using System;
using System.Globalization;
using AudioDevices;
using Common;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Events;

internal class AudioLevelEvent : Event
{
	public AudioDataFlow DataFlow { get; }

	public float Level { get; }

	public AudioLevelEvent(AudioDataFlow dataFlow, float level)
		: base(VideoModuleEvent.audioLevel)
	{
		if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
		{
			throw new ArgumentException($"Invalid value: {dataFlow}", "dataFlow");
		}
		DataFlow = dataFlow;
		Level = level;
	}

	public override string ToString()
	{
		return base.ToString() + string.Format("|{0}|{1}", DataFlow, Level.ToString("0.######", CultureInfo.InvariantCulture));
	}
}
