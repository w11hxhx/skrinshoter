using MediatR;

namespace SkrinshoterApp;

public class NotificationOptions
{
	public IBaseRequest Command { get; private set; }

	public bool RespectSilentMode { get; private set; }

	public NotificationOptions WithCommand(IBaseRequest command)
	{
		Command = command;
		return this;
	}

	public NotificationOptions WithRespectSilentMode(bool value)
	{
		RespectSilentMode = value;
		return this;
	}
}
