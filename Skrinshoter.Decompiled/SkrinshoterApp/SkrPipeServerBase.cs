using System;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public abstract class SkrPipeServerBase : IDisposable, IPipeServer
{
	private NamedPipeServer _pipeServer;

	private bool _disposing;

	public event EventHandler OnConnected;

	public event EventHandler OnDisconnected;

	public event EventHandler<string> OnResponse;

	public abstract string GetPipeName();

	public SkrPipeServerBase()
	{
		_pipeServer = new NamedPipeServer(GetPipeName());
		_pipeServer.OnMessage += PipeServer_OnMessage;
		_pipeServer.OnDisconnected += PipeServer_OnDisconnected;
		_pipeServer.OnConnected += PipeServer_OnConnected;
	}

	public async Task StartAsync()
	{
		await _pipeServer.StartServerAsync();
	}

	private void PipeServer_OnConnected(object sender, EventArgs e)
	{
		if (!_disposing)
		{
			OnConnected?.Invoke(this, new EventArgs());
		}
	}

	private void PipeServer_OnDisconnected(object sender, EventArgs e)
	{
		if (!_disposing)
		{
			OnDisconnected?.Invoke(this, e);
		}
	}

	private void PipeServer_OnMessage(object sender, string msg)
	{
		if (!_disposing)
		{
			int num = msg.IndexOf('\0');
			string e = ((num < 0) ? msg : msg.Substring(0, num));
			OnResponse?.Invoke(this, e);
		}
	}

	public async Task SendAsync(string command)
	{
		if (!_disposing)
		{
			await _pipeServer.WriteAsync(command);
		}
	}

	public void Dispose()
	{
		_pipeServer.StopServer();
		_pipeServer = null;
	}
}
