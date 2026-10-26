using System;
using ScreenRecorderCommon;

namespace ScreenRecorder.OutgoingMessages;

internal abstract class OutgoingMessage
{
	public abstract override string ToString();

	protected static ErrorCode GetErrorCode(Exception ex)
	{
		if (ex is AggregateException ex2)
		{
			ex = ex2.Flatten().InnerException;
		}
		if (!(ex is ErrorCodeException ex3))
		{
			return ErrorCode.General;
		}
		return ex3.ErrorCode;
	}

	protected static string GetExceptionMessage(Exception ex)
	{
		if (ex is AggregateException ex2)
		{
			ex = ex2.Flatten().InnerException;
		}
		if (!(ex is ErrorCodeException { InnerException: var innerException }))
		{
			return ex.Message;
		}
		return innerException?.Message;
	}
}
