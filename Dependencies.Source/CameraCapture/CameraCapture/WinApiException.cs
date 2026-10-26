using System;
using System.ComponentModel;

namespace CameraCapture;

public class WinApiException : Exception
{
	public WinApiException(string message, Win32Exception inner)
		: base(message, inner)
	{
	}
}
