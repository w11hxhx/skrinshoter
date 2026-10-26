using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class ScreenshotResponse : Response
{
	public override string SuccessfulStatus => "ready";

	public ScreenshotResponse(Exception exception = null)
		: base(VideoModuleCommand.screenshot, exception)
	{
	}
}
