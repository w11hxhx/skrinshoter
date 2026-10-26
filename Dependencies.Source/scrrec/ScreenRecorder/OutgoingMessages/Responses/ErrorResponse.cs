using System;

namespace ScreenRecorder.OutgoingMessages.Responses;

internal class ErrorResponse : OutgoingMessage
{
	public string CommandName { get; }

	public Exception Exception { get; }

	public ErrorResponse(string commandName, Exception exception)
	{
		CommandName = commandName;
		Exception = exception;
	}

	public override string ToString()
	{
		return $"response|{CommandName}|error|{OutgoingMessage.GetErrorCode(Exception)}|{OutgoingMessage.GetExceptionMessage(Exception)}";
	}
}
