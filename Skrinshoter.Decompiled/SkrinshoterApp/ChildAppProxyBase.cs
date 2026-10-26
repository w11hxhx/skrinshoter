using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ScreenRecorderCommon;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp;

public abstract class ChildAppProxyBase : IIPCProxy
{
	private IPipeServer _skrPipeServer;

	private bool _connected;

	private IChildProcess _process;

	private Dictionary<string, Queue<string[]>> _responses = new Dictionary<string, Queue<string[]>>();

	private Dictionary<string, Queue<Action<object, string[]>>> _responseHandlers = new Dictionary<string, Queue<Action<object, string[]>>>();

	private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

	private Dictionary<string, Action<string[]>> _incomingCommandHandlers = new Dictionary<string, Action<string[]>>();

	public abstract char Delimiter { get; }

	public abstract string IncommingCommandPrefix { get; }

	public abstract string Name { get; }

	public IChildProcess Process => _process;

	public event EventHandler OnChildDisconnected;

	public event EventHandler OnChildConnected;

	public event Action OnFailedToRunChildProcess;

	protected abstract List<string> ExtractMessageFields(string message);

	public ChildAppProxyBase(IChildProcess childProcess, IPipeServer pipeServer)
	{
		_skrPipeServer = pipeServer;
		_skrPipeServer.OnResponse += PipeServer_OnResponse;
		_skrPipeServer.OnDisconnected += PipeServer_OnDisconnected;
		_skrPipeServer.OnConnected += PipeServer_OnConnected;
		_process = childProcess;
	}

	private void PipeServer_OnResponse(object sender, string response)
	{
		if (CanLogResponse(response))
		{
			AppLogger.Logger.Info("SkrPipeServer_OnResponse: " + Name + ": " + response);
		}
		string[] fields = ExtractMessageFields(response).ToArray();
		if (fields.Length < 1)
		{
			AppLogger.Logger.Error("SkrPipeServer_OnResponse: " + Name + ": empty string");
			return;
		}
		_semaphore.Wait();
		try
		{
			if (IncommingCommandPrefix != null && fields[0] == IncommingCommandPrefix)
			{
				if (fields.Length > 1 && _incomingCommandHandlers.ContainsKey(fields[1]))
				{
					Task.Run(() =>
					{
						_incomingCommandHandlers[fields[1]](fields.Skip(2).ToArray());
					});
				}
			}
			else if (fields.Length > 2 && fields[0].ToLower() == "response")
			{
				string key = fields[1];
				string[] arguments = fields.Skip(2).ToArray();
				if (_responseHandlers.ContainsKey(key) && _responseHandlers[key].Count() > 0)
				{
					Action<object, string[]> handler = _responseHandlers[key].Dequeue();
					Task.Run(() =>
					{
						handler(null, arguments);
					});
					return;
				}
				if (!_responses.ContainsKey(key))
				{
					_responses.Add(key, new Queue<string[]>());
				}
				_responses[key].Enqueue(arguments);
			}
			else
			{
				AppLogger.Logger.Warn("ChildAppProxy got something unexpected from the child app: " + response);
			}
		}
		finally
		{
			_semaphore.Release();
		}
	}

	private void PipeServer_OnDisconnected(object sender, EventArgs e)
	{
		_connected = false;
		AppLogger.Logger.Warn("Slave " + Name + " disconnected");
		OnChildDisconnected?.Invoke(this, e);
	}

	private void PipeServer_OnConnected(object sender, EventArgs e)
	{
		_connected = true;
		AppLogger.Logger.Info("Slave " + Name + " connected");
		OnChildConnected?.Invoke(this, new EventArgs());
	}

	public void RunAsync()
	{
		if (_connected)
		{
			AppLogger.Logger.Warn("ChildAppProxy: " + Name + ": child process is connected. No need to run.");
			return;
		}
		Task.Run(async () =>
		{
			try
			{
				await _skrPipeServer.StartAsync();
			}
			catch (Exception ex)
			{
				AppLogger.Logger.Error("ChildAppProxy: " + Name + ": Failed to run PipeServer: " + ex.Message);
				OnFailedToRunChildProcess?.Invoke();
			}
		});
		Task.Run(async () =>
		{
			try
			{
				await Task.Delay(TimeSpan.FromSeconds(1.0));
				_process.StartProcess();
			}
			catch (Exception ex)
			{
				AppLogger.Logger.Error("ChildAppProxy: " + Name + ": Failed to run child process: " + ex.Message);
				OnFailedToRunChildProcess?.Invoke();
			}
		});
	}

	public async Task SendCommand(VideoModuleCommand command, List<string> arguments = null)
	{
		if (arguments == null)
		{
			arguments = new List<string>();
		}
		if (!_connected)
		{
			AppLogger.Logger.Warn("Sending message but no connected module " + Name + ".");
			_process.StartProcess();
			await WaitForProcessConnected(TimeSpan.FromSeconds(5.0));
		}
		string text = command.ToString() + ((arguments.Count() > 0) ? (Delimiter + string.Join($"{Delimiter}", arguments)) : "");
		if (CanLogCommand(command))
		{
			AppLogger.Logger.Info("Sending command to " + Name + ": \"" + text + "\"");
		}
		await _skrPipeServer.SendAsync(text);
	}

	private async Task WaitForProcessConnected(TimeSpan timeout)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout)
		{
			if (_connected)
			{
				return;
			}
			await Task.Delay(TimeSpan.FromMilliseconds(10.0));
		}
		throw new TimeoutException("Waiting for Child app " + Name + " connection has been timeouted.");
	}

	public async Task WaitForProcessDisconnected(TimeSpan timeout)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout)
		{
			if (!_connected)
			{
				return;
			}
			await Task.Delay(TimeSpan.FromMilliseconds(10.0));
		}
		throw new TimeoutException("Waiting for Child app " + Name + " disconnection has been timeouted.");
	}

	public async Task<string[]> ReceiveCommandResult(VideoModuleCommand command, TimeSpan maxExecutionTime)
	{
		string commandTag = command.ToString();
		TaskCompletionSource<string[]> _eventResultIsReady = new TaskCompletionSource<string[]>();
		Action<object, string[]> responseHandler = (object sender, string[] commandArgs) =>
		{
			_eventResultIsReady.SetResult(commandArgs);
		};
		await _semaphore.WaitAsync();
		try
		{
			if (_responses.ContainsKey(commandTag) && _responses[commandTag].Count() > 0)
			{
				return _responses[commandTag].Dequeue();
			}
			if (!_responseHandlers.ContainsKey(commandTag))
			{
				_responseHandlers[commandTag] = new Queue<Action<object, string[]>>();
			}
			_responseHandlers[commandTag].Enqueue(responseHandler);
		}
		finally
		{
			_semaphore.Release();
		}
		using CancellationTokenSource cts = new CancellationTokenSource();
		Task delayTask = Task.Delay(maxExecutionTime, cts.Token);
		Task resultingTask = await Task.WhenAny(delayTask, _eventResultIsReady.Task);
		await _semaphore.WaitAsync();
		try
		{
			if (resultingTask == delayTask)
			{
				RemoveFromQueueInPlace(_responseHandlers[commandTag], responseHandler);
				throw new Exception("Timeout while waiting for slave app " + Name + " response for the command tag " + commandTag + ".");
			}
			cts.Cancel();
			return _eventResultIsReady.Task.Result;
		}
		finally
		{
			_semaphore.Release();
		}
	}

	private static void RemoveFromQueueInPlace<T>(Queue<T> queue, T itemToRemove)
	{
		int count = queue.Count;
		for (int i = 0; i < count; i++)
		{
			T item = queue.Dequeue();
			if (!item.Equals(itemToRemove))
			{
				queue.Enqueue(item);
			}
		}
	}

	public void RegisterHandler(VideoModuleEvent incomingEvent, Action<string[]> handler)
	{
		_semaphore.Wait();
		_incomingCommandHandlers.Add(incomingEvent.ToString(), handler);
		_semaphore.Release();
	}

	private static bool CanLogCommand(VideoModuleCommand command)
	{
		return command != VideoModuleCommand.captureGetFrame;
	}

	private static bool CanLogResponse(string response)
	{
		if (!response.StartsWith($"response|{VideoModuleCommand.captureGetFrame}|") && !response.StartsWith($"event|{VideoModuleEvent.audioLevel}|"))
		{
			return !response.StartsWith($"event|{VideoModuleEvent.speechActivity}|");
		}
		return false;
	}
}
