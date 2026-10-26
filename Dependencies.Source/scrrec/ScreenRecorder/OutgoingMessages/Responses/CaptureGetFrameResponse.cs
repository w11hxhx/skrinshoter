using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class CaptureGetFrameResponse : Response
{
	public override string SuccessfulStatus
	{
		get
		{
			if (!Ready)
			{
				return "timeout";
			}
			return "ready";
		}
	}

	public bool Ready { get; }

	public CaptureGetFrameResponse(bool ready)
		: base(VideoModuleCommand.captureGetFrame)
	{
		Ready = ready;
	}

	public CaptureGetFrameResponse(Exception exception)
		: base(VideoModuleCommand.captureGetFrame, exception)
	{
		if (exception == null)
		{
			throw new ArgumentNullException("exception");
		}
	}
}
