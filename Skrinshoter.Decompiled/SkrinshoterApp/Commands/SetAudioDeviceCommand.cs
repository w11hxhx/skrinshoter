using AudioDevices;
using MediatR;

namespace SkrinshoterApp.Commands;

public class SetAudioDeviceCommand : IRequest<Unit>, IBaseRequest
{
	public AudioDataFlow DataFlow { get; }

	public string DeviceId { get; }

	public bool IsMuted { get; }

	public SetAudioDeviceCommand(AudioDataFlow dataFlow, string deviceId, bool isMuted)
	{
		DataFlow = dataFlow;
		DeviceId = deviceId;
		IsMuted = isMuted;
	}
}
