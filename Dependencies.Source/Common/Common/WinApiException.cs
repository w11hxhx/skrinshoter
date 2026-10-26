using System;

namespace Common;

public class WinApiException : Exception
{
	public WinApiException(string message, Exception inner = null)
		: base(message, inner)
	{
	}
}
