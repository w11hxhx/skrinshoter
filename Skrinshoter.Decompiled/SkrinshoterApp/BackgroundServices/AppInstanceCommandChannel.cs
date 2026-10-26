using System;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.BackgroundServices;

internal sealed class AppInstanceCommandChannel : IHostedService
{
	private static readonly TimeSpan ConnectTimeout = TimeSpan.FromSeconds(5.0);

	private static readonly TimeSpan ErrorRetryDelay = TimeSpan.FromSeconds(1.0);

	private static readonly string PipeName = BuildPipeName();

	private readonly ILogger logger;

	private readonly Dispatcher dispatcher;

	private readonly CommandLineActionExecutor commandLineActionExecutor;

	public AppInstanceCommandChannel(Dispatcher dispatcher, CommandLineActionExecutor commandLineActionExecutor, ILogger<AppInstanceCommandChannel> logger)
	{
		this.dispatcher = dispatcher ?? throw new ArgumentNullException("dispatcher");
		this.commandLineActionExecutor = commandLineActionExecutor ?? throw new ArgumentNullException("commandLineActionExecutor");
		this.logger = logger ?? throw new ArgumentNullException("logger");
	}

	public static void SendToRunningInstance(CommandLineArguments arguments)
	{
		try
		{
			using NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.Out);
			namedPipeClientStream.Connect((int)ConnectTimeout.TotalMilliseconds);
			string s = arguments.Serialize();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			namedPipeClientStream.Write(bytes, 0, bytes.Length);
		}
		catch
		{
		}
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		Task.Run((Func<Task>)RunServerAsync);
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private static string BuildPipeName()
	{
		using WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
		return $"{ProductResources.AppInvariantName}InstanceCommandPipe_{windowsIdentity.User}";
	}

	private async Task RunServerAsync()
	{
		while (true)
		{
			try
			{
				await AcceptConnectionAsync();
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to handle app instance command channel connection");
				await Task.Delay(ErrorRetryDelay);
			}
		}
	}

	private async Task AcceptConnectionAsync()
	{
		using NamedPipeServerStream stream = new NamedPipeServerStream(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
		await stream.WaitForConnectionAsync();
		string text = await ReadMessageAsync(stream);
		logger.LogInformation("Received command line \"{message}\" from another application instance", text);
		CommandLineArguments arguments = CommandLineArguments.Deserialize(text);
		dispatcher.BeginInvoke((Func<Task>)(async () =>
		{
			await commandLineActionExecutor.ExecuteAsync(arguments);
		}));
	}

	private static async Task<string> ReadMessageAsync(NamedPipeServerStream stream)
	{
		byte[] buffer = new byte[4096];
		using MemoryStream messageStream = new MemoryStream();
		while (true)
		{
			int num = await stream.ReadAsync(buffer, 0, buffer.Length);
			if (num == 0)
			{
				break;
			}
			messageStream.Write(buffer, 0, num);
		}
		return Encoding.UTF8.GetString(messageStream.GetBuffer(), 0, (int)messageStream.Length).Trim();
	}
}
