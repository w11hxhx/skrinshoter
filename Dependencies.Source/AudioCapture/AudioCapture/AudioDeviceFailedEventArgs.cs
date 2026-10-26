using System;
using AudioDevices;

namespace AudioCapture;

public class AudioDeviceFailedEventArgs : EventArgs
{
	public AudioDataFlow DataFlow { get; }

	public string DeviceId { get; }

	public Exception Exception { get; }

	public AudioDeviceFailedEventArgs(AudioDataFlow dataFlow, string deviceId, Exception exception)
	{
		DataFlow = dataFlow;
		DeviceId = deviceId;
		Exception = exception;
	}
}
