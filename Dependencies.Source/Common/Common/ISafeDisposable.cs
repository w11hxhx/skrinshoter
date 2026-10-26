using System;

namespace Common;

public interface ISafeDisposable : IDisposable
{
	bool IsDisposed { get; }

	object DisposeLockObject { get; }
}
