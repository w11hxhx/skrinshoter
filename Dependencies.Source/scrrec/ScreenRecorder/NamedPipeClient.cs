using System;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nito.AsyncEx;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages;
using ScreenRecorder.OutgoingMessages.Events;
using ScreenRecorder.OutgoingMessages.Responses;
using ScreenRecorderCommon;

namespace ScreenRecorder;

internal class NamedPipeClient
{
	private static readonly TimeSpan ConnectTimeout = TimeSpan.FromSeconds(5.0);

	private readonly ILogger logger;

	private readonly ICommandProducer messageChannel;

	private readonly CommandFactory commandFactory;

	private NamedPipeClientStream clientStream;

	public NamedPipeClient(ICommandProducer messageChannel, CommandFactory commandFactory, ILogger<NamedPipeClient> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.messageChannel = messageChannel ?? throw new ArgumentNullException("messageChannel");
		this.commandFactory = commandFactory ?? throw new ArgumentNullException("commandFactory");
	}

	public async Task RunAsync(string pipeName, CancellationToken cancellationToken)
	{
		using (clientStream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous))
		{
			using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			logger.LogInformation("Connecting to server...");
			await clientStream.ConnectAsync((int)ConnectTimeout.TotalMilliseconds, cts.Token);
			logger.LogInformation("Connected");
			clientStream.ReadMode = PipeTransmissionMode.Message;
			Task[] tasks = new Task[2]
			{
				ReadCommandsAsync(cts.Token),
				SendOutgoingMessagesAsync(cts.Token)
			};
			await Task.WhenAny(tasks);
			cts.Cancel();
			await Task.WhenAll(tasks);
		}
	}

	private async Task ReadCommandsAsync(CancellationToken cancellationToken)
	{
		byte[] buffer = new byte[4096];
		while (true)
		{
			int num = await clientStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).WaitAsync(cancellationToken);
			if (num == 0)
			{
				break;
			}
			try
			{
				string text = Encoding.UTF8.GetString(buffer, 0, num);
				string[] source = text.Split('|');
				string commandName = source.First();
				string[] commandArgs = source.Skip(1).ToArray();
				if (CanLogCommand(commandName))
				{
					logger.LogInformation("");
					logger.LogInformation("Received command \"{command}\"", text);
				}
				try
				{
					Command item = commandFactory.CreateCommand(commandName, commandArgs);
					await messageChannel.Commands.WriteAsync(item);
				}
				catch (Exception exception)
				{
					logger.LogError(exception, "Error parsing command \"{command}\"", commandName);
					await messageChannel.OutgoingMessages.Writer.WriteAsync(new ErrorResponse(commandName, exception));
				}
			}
			catch (Exception exception2)
			{
				logger.LogError(exception2, "Command parsing error");
			}
		}
		logger.LogInformation("Server closed the connection, exiting");
	}

	private async Task SendOutgoingMessagesAsync(CancellationToken cancellationToken)
	{
		while (true)
		{
			OutgoingMessage message = await messageChannel.OutgoingMessages.Reader.ReadAsync(cancellationToken);
			if (message is Event)
			{
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Sending event \"{event}\"", message);
				}
			}
			else if (message is Response response && CanLogCommand(response.Kind))
			{
				logger.LogInformation("Sending response \"{response}\"", message);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(message.ToString());
			await clientStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).WaitAsync(cancellationToken);
			if (message is Event)
			{
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Event sent");
				}
			}
			else if (message is Response response2 && CanLogCommand(response2.Kind))
			{
				logger.LogInformation("Response sent");
			}
			if (message is Response { Kind: VideoModuleCommand.terminate, Exception: null })
			{
				break;
			}
		}
		throw new OperationCanceledException();
	}

	private static bool CanLogCommand(string commandName)
	{
		return commandName != "captureGetFrame";
	}

	private static bool CanLogCommand(VideoModuleCommand commandKind)
	{
		return CanLogCommand(commandKind.ToString());
	}
}
