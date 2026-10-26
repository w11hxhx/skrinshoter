using System;
using AudioDevices;
using Common;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Events;

internal class AudioDeviceFailedEvent : Event
{
	public AudioDataFlow DataFlow { get; }

	public string DeviceId { get; }

	public Exception Exception { get; }

	public AudioDeviceFailedEvent(AudioDataFlow dataFlow, string deviceId, Exception exception)
		: base(VideoModuleEvent.audioDeviceFailed)
	{
		if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
		{
			throw new ArgumentException($"Invalid value: {dataFlow}", "dataFlow");
		}
		DataFlow = dataFlow;
		DeviceId = deviceId ?? throw new ArgumentNullException("deviceId");
		Exception = exception ?? throw new ArgumentNullException("exception");
	}

	public override string ToString()
	{
		return base.ToString() + $"|{DataFlow}|{DeviceId}|{OutgoingMessage.GetErrorCode(Exception)}|{OutgoingMessage.GetExceptionMessage(Exception)}";
	}
}
