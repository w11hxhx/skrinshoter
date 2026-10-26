using System;
using System.Threading;

namespace Common;

public class SafeDisposable : ISafeDisposable, IDisposable
{
	private int isDisposed;

	public bool IsDisposed
	{
		get
		{
			return Interlocked.CompareExchange(ref isDisposed, 0, 0) != 0;
		}
		private set
		{
			Interlocked.Exchange(ref isDisposed, Convert.ToInt32(value));
		}
	}

	public object DisposeLockObject { get; } = new object();

	protected object InternalDisposeLockObject { get; } = new object();

	public void Dispose()
	{
		lock (DisposeLockObject)
		{
			lock (InternalDisposeLockObject)
			{
				if (IsDisposed)
				{
					return;
				}
				IsDisposed = true;
			}
		}
		try
		{
			Dispose(disposing: true);
		}
		catch
		{
		}
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
	}

	~SafeDisposable()
	{
		try
		{
			Dispose(disposing: false);
		}
		catch
		{
		}
	}
}
