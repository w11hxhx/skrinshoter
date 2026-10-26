using System;

namespace ScreenRecorderCommon;

public class ErrorCodeException : Exception
{
	public ErrorCode ErrorCode { get; }

	public ErrorCodeException(ErrorCode errorCode, Exception inner)
		: base(errorCode.ToString(), inner)
	{
		ErrorCode = errorCode;
	}
}
