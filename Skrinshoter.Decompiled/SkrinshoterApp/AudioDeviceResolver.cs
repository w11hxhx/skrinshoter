using AudioDevices;

namespace SkrinshoterApp;

public sealed class AudioDeviceResolver
{
	private readonly AudioDeviceEnumerator audioDeviceEnumerator;

	public AudioDeviceResolver(AudioDeviceEnumerator audioDeviceEnumerator)
	{
		this.audioDeviceEnumerator = audioDeviceEnumerator;
	}

	public static AudioDeviceRole GetDefaultDeviceRole(AudioDataFlow dataFlow)
	{
		if (dataFlow != AudioDataFlow.Capture)
		{
			return AudioDeviceRole.Console;
		}
		return AudioDeviceRole.Communications;
	}

	public string Resolve(string deviceId, AudioDataFlow dataFlow)
	{
		if (deviceId != "Default")
		{
			AudioDevice device = audioDeviceEnumerator.GetDevice(deviceId);
			if (device != null && device.State.HasFlag(AudioDeviceState.Active))
			{
				return deviceId;
			}
		}
		return audioDeviceEnumerator.GetDefaultDevice(dataFlow, GetDefaultDeviceRole(dataFlow))?.Id;
	}
}
