using System;

namespace SkrinshoterApp;

public class NetworkException : Exception
{
	public NetworkException(Exception inner)
		: base("No response was received from server.", inner)
	{
	}
}
