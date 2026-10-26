using System;

namespace SkrinshoterApp;

public class ServerResponseException : Exception
{
	public ServerResponseException(Exception inner)
		: base("Received unexpected response from server.", inner)
	{
	}
}
