using System;

namespace AudioDevices;

public class AudioDevice
{
	public string Id { get; }

	public string FriendlyName { get; }

	public AudioDataFlow DataFlow { get; }

	public AudioDeviceState State { get; }

	public AudioDevice(string id, string friendlyName, AudioDataFlow dataFlow, AudioDeviceState state)
	{
		Id = id ?? throw new ArgumentNullException("id");
		FriendlyName = friendlyName ?? throw new ArgumentNullException("friendlyName");
		DataFlow = dataFlow;
		State = state;
	}
}
