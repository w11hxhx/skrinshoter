using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public class NamedPipeServer
{
	private NamedPipeServerStream _pipeServer;

	private readonly string _pipeName;

	private bool _isRunning;

	public event EventHandler<string> OnMessage;

	public event EventHandler OnConnected;

	public event EventHandler OnDisconnected;

	public NamedPipeServer(string pipeName)
	{
		_pipeName = pipeName;
	}

	public async Task StartServerAsync()
	{
		_isRunning = true;
		while (_isRunning)
		{
			_pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, -1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
			await _pipeServer.WaitForConnectionAsync();
			if (_pipeServer.IsConnected)
			{
				OnConnected?.Invoke(this, new EventArgs());
			}
			while (_pipeServer.IsConnected)
			{
				string text = await ReadAsync();
				if (text != null)
				{
					OnMessage?.Invoke(this, text);
					continue;
				}
				OnDisconnected?.Invoke(this, new EventArgs());
				break;
			}
			_pipeServer.Dispose();
		}
	}

	public async Task WriteAsync(string message)
	{
		if (_pipeServer.IsConnected)
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(message);
				await _pipeServer.WriteAsync(bytes, 0, bytes.Length);
			}
			catch (IOException ex)
			{
				AppLogger.Logger.Error("Write to pipe: " + ex.Message);
				_pipeServer.Close();
			}
		}
	}

	public void StopServer()
	{
		_isRunning = false;
		try
		{
			_pipeServer?.Close();
		}
		catch
		{
		}
	}

	private async Task<string> ReadAsync()
	{
		byte[] buffer = new byte[4096];
		try
		{
			int num = await _pipeServer.ReadAsync(buffer, 0, buffer.Length);
			if (num == 0)
			{
				return null;
			}
			return Encoding.UTF8.GetString(buffer, 0, num);
		}
		catch
		{
			return null;
		}
	}
}
