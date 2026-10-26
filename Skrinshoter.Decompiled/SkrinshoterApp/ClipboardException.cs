using System;

namespace SkrinshoterApp;

public sealed class ClipboardException : Exception
{
	public ClipboardException(string message, Exception innerException = null)
		: base(message, innerException)
	{
	}
}
