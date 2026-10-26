using System;
using System.Threading;
using SkrinshoterApp.Common;

namespace SkrinshoterApp.Services.Progress;

internal sealed class NullProgressSession : IProgressSession, IDisposable
{
	private readonly CancellationToken token;

	public IProgress<int> Progress { get; } = new SynchronousProgress<int>((int _) =>
	{
	});

	public CancellationToken Token => token;

	public NullProgressSession(CancellationToken token)
	{
		this.token = token;
	}

	public void UpdateMessage(string message)
	{
	}

	public void SetIndeterminate(bool value)
	{
	}

	public void Dispose()
	{
	}
}
