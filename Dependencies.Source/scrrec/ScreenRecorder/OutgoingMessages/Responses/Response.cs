using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class Response : OutgoingMessage
{
	public VideoModuleCommand Kind { get; }

	public Exception Exception { get; }

	public virtual string SuccessfulStatus => "ok";

	public Response(VideoModuleCommand kind, Exception exception = null)
	{
		Kind = kind;
		Exception = exception;
	}

	public override string ToString()
	{
		return $"response|{Kind}|" + ((Exception == null) ? SuccessfulStatus : $"error|{OutgoingMessage.GetErrorCode(Exception)}|{OutgoingMessage.GetExceptionMessage(Exception)}");
	}
}
