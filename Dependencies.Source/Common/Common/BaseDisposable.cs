using System;

namespace Common;

public class BaseDisposable : IDisposable
{
	public bool IsDisposed { get; private set; }

	public void Dispose()
	{
		if (!IsDisposed)
		{
			IsDisposed = true;
			try
			{
				Dispose(disposing: true);
			}
			catch
			{
			}
			GC.SuppressFinalize(this);
		}
	}

	protected virtual void Dispose(bool disposing)
	{
	}

	~BaseDisposable()
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
