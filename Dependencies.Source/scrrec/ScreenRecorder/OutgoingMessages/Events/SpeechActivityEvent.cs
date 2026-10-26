using System;
using AudioDevices;
using Common;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Events;

internal class SpeechActivityEvent : Event
{
	public AudioDataFlow DataFlow { get; }

	public bool IsActive { get; }

	public SpeechActivityEvent(AudioDataFlow dataFlow, bool isActive)
		: base(VideoModuleEvent.speechActivity)
	{
		if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
		{
			throw new ArgumentException($"Invalid value: {dataFlow}", "dataFlow");
		}
		DataFlow = dataFlow;
		IsActive = isActive;
	}

	public override string ToString()
	{
		return base.ToString() + $"|{DataFlow}|{(IsActive ? 1 : 0)}";
	}
}
