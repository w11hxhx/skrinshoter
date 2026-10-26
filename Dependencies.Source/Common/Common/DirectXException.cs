using System;

namespace Common;

public class DirectXException : Exception
{
	public DirectXException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
