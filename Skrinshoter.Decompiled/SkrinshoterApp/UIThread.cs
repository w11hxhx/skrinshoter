using System;
using System.Threading;
using System.Windows.Threading;

namespace SkrinshoterApp;

public static class UIThread
{
	private static volatile int uiThreadId;

	public static void Initialize(Dispatcher dispatcher)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		uiThreadId = dispatcher.Thread.ManagedThreadId;
	}

	public static void Ensure(string typeName, string memberName)
	{
		int num = uiThreadId;
		if (num == 0)
		{
			throw new InvalidOperationException("UI thread is not captured. Call UiThread.Initialize(Application.Current.Dispatcher) at startup.");
		}
		int managedThreadId = Thread.CurrentThread.ManagedThreadId;
		if (managedThreadId != num)
		{
			throw new InvalidOperationException($"{typeName}.{memberName} must be accessed on the UI thread. Current TID: {managedThreadId}, UI TID: {num}.");
		}
	}
}
