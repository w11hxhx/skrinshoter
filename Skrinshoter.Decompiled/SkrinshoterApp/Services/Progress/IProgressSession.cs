using System;
using System.Threading;

namespace SkrinshoterApp.Services.Progress;

public interface IProgressSession : IDisposable
{
	IProgress<int> Progress { get; }

	CancellationToken Token { get; }

	void UpdateMessage(string message);

	void SetIndeterminate(bool value);
}
