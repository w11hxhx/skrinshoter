using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SkrinshoterApp.SkrChildApp;

public abstract class ChildProcessBase : IChildProcess, IDisposable
{
	private Process _process;

	private bool _disposing;

	public abstract string ProcessName { get; }

	public abstract string ProcessPath { get; }

	protected ChildProcessBase()
	{
		AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
	}

	public void StartProcess()
	{
		if (_disposing || IsProcessRunning())
		{
			return;
		}
		_process = new Process
		{
			StartInfo = new ProcessStartInfo(ProcessName)
			{
				WorkingDirectory = Path.GetDirectoryName(ProcessPath)
			},
			EnableRaisingEvents = true
		};
		if (!Debugger.IsAttached)
		{
			_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		}
		_process.Exited += OnProcessExited;
		try
		{
			if (!_process.Start())
			{
				AppLogger.Logger.Warn(ProcessName + " start have returned false.");
			}
		}
		catch (Exception ex)
		{
			AppLogger.Logger.Error("Failed to start slave process " + ProcessName + ": " + ex.Message);
			_process = null;
			throw ex;
		}
	}

	private void OnProcessExited(object sender, EventArgs e)
	{
		if (!_disposing)
		{
			AppLogger.Logger.Error(ProcessName + " has been unexpectedly exited.");
		}
	}

	private void OnProcessExit(object sender, EventArgs e)
	{
		if (IsProcessRunning())
		{
			_process.Kill();
			_process = null;
		}
	}

	public void Dispose()
	{
		_disposing = true;
		if (IsProcessRunning())
		{
			_process.Kill();
			_process = null;
		}
	}

	private bool IsProcessRunning()
	{
		if (_process != null)
		{
			try
			{
				if (!_process.HasExited)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				AppLogger.Logger.Warn("IsProcessRunning: Process HasExited thrown: " + ex.Message);
			}
			_process = null;
		}
		return false;
	}

	public async Task WaitForProcessExited(TimeSpan timeout)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout)
		{
			if (!IsProcessRunning())
			{
				return;
			}
			await Task.Delay(TimeSpan.FromMilliseconds(10.0));
		}
		throw new TimeoutException("Waiting for Child app " + ProcessName + " exit has been timeouted.");
	}
}
