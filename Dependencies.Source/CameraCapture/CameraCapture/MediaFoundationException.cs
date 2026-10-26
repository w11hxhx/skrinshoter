using System;

namespace CameraCapture;

public class MediaFoundationException : Exception
{
	public MediaFoundationException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
