using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class StartResponse : Response
{
	public override string SuccessfulStatus { get; }

	public StartResponse(bool canceled, Exception exception = null)
		: base(VideoModuleCommand.start, exception)
	{
		SuccessfulStatus = (canceled ? "canceled" : "ready");
	}
}
