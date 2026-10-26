using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace SkrinshoterApp.Common;

public static class AsyncAutoResetEventExtensions
{
	public static async Task<bool> WaitAsync(this AsyncAutoResetEvent ev, TimeSpan timeout, CancellationToken cancellationToken)
	{
		if (ev == null)
		{
			throw new ArgumentNullException("ev");
		}
		if (timeout < TimeSpan.Zero && timeout != Timeout.InfiniteTimeSpan)
		{
			throw new ArgumentOutOfRangeException("timeout");
		}
		if (timeout == TimeSpan.Zero)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return false;
		}
		if (timeout == Timeout.InfiniteTimeSpan)
		{
			await ev.WaitAsync(cancellationToken);
			return true;
		}
		using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		cts.CancelAfter(timeout);
		try
		{
			await ev.WaitAsync(cts.Token);
			return true;
		}
		catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
		{
			return false;
		}
	}
}
