using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class TogglePauseResponse : Response
{
	public bool Paused { get; }

	public TogglePauseResponse(bool paused)
		: base(VideoModuleCommand.togglePause)
	{
		Paused = paused;
	}

	public TogglePauseResponse(Exception exception)
		: base(VideoModuleCommand.togglePause, exception)
	{
		if (exception == null)
		{
			throw new ArgumentNullException("exception");
		}
	}

	public override string ToString()
	{
		return base.ToString() + ((Exception == null) ? ("|" + Paused) : "");
	}
}
