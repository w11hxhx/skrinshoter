using System;
using System.Threading;
using SkrinshoterApp.Common;

namespace SkrinshoterApp.Services.Progress;

internal sealed class RealProgressSession : IProgressSession, IDisposable
{
	private readonly ProgressWindow window;

	private readonly ProgressWindowVM vm;

	private readonly CancellationTokenSource cts;

	private int disposed;

	public IProgress<int> Progress { get; }

	public CancellationToken Token => cts.Token;

	public RealProgressSession(ProgressWindow window, ProgressWindowVM vm, CancellationTokenSource cts)
	{
		this.window = window;
		this.vm = vm;
		this.cts = cts;
		Progress = new SynchronousProgress<int>((int value) =>
		{
			vm.Progress = value;
		});
	}

	public void UpdateMessage(string message)
	{
		vm.Message = message;
	}

	public void SetIndeterminate(bool value)
	{
		vm.IsIndeterminate = value;
	}

	public void Dispose()
	{
		if (Interlocked.Exchange(ref disposed, 1) != 0)
		{
			return;
		}
		try
		{
			if (window.Dispatcher.CheckAccess())
			{
				window.ForceClose();
			}
			else
			{
				window.Dispatcher.Invoke(window.ForceClose);
			}
		}
		catch
		{
		}
		cts.Dispose();
	}
}
