using System;
using System.Diagnostics;
using System.Threading;

namespace Common;

public class HighResolutionSleeper : SafeDisposable
{
	private class SleepNode
	{
		public EventWaitHandle WaitHandle;

		public SleepNode Next;

		public SleepNode(EventWaitHandle waitHandle)
		{
			WaitHandle = waitHandle;
		}
	}

	private static int objectCount;

	private static MultimediaTimer timer;

	private static readonly object staticLockObject = new object();

	private static SleepNode sleepQueue;

	private readonly SleepNode sleepNode;

	private readonly object sleepLockObject = new object();

	public HighResolutionSleeper()
	{
		sleepNode = new SleepNode(new AutoResetEvent(initialState: false));
		lock (staticLockObject)
		{
			objectCount++;
			if (objectCount == 1)
			{
				timer = new MultimediaTimer(TimerCallback);
				timer.Start(1u);
			}
		}
	}

	public void Sleep(int millisecondsTimeout)
	{
		if (millisecondsTimeout < 0)
		{
			throw new ArgumentOutOfRangeException("millisecondsTimeout");
		}
		long num = Stopwatch.GetTimestamp() + Stopwatch.Frequency * millisecondsTimeout / 1000;
		if (!Monitor.TryEnter(sleepLockObject))
		{
			if (IsDisposed)
			{
				return;
			}
			throw new InvalidOperationException("Operation is already running.");
		}
		try
		{
			if (!IsDisposed)
			{
				do
				{
					SleepInternal();
				}
				while (Stopwatch.GetTimestamp() < num && !IsDisposed);
			}
		}
		finally
		{
			Monitor.Exit(sleepLockObject);
		}
	}

	public void Sleep(TimeSpan timeout)
	{
		long num = (long)timeout.TotalMilliseconds;
		if (num < 0 || num > int.MaxValue)
		{
			throw new ArgumentOutOfRangeException("timeout");
		}
		Sleep((int)num);
	}

	private void SleepInternal()
	{
		do
		{
			sleepNode.Next = Interlocked.CompareExchange(ref sleepQueue, null, null);
		}
		while (Interlocked.CompareExchange(ref sleepQueue, sleepNode, sleepNode.Next) != sleepNode.Next);
		sleepNode.WaitHandle.WaitOne();
		sleepNode.Next = null;
	}

	private static void TimerCallback(object state)
	{
		SleepNode sleepNode = Interlocked.Exchange(ref sleepQueue, null);
		while (sleepNode != null)
		{
			SleepNode sleepNode2 = sleepNode;
			sleepNode = sleepNode.Next;
			sleepNode2.WaitHandle.Set();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			lock (sleepLockObject)
			{
			}
			sleepNode.WaitHandle.Dispose();
			sleepNode.WaitHandle = null;
			sleepNode.Next = null;
		}
		lock (staticLockObject)
		{
			objectCount--;
			if (objectCount == 0)
			{
				timer.Dispose();
				timer = null;
				Interlocked.Exchange(ref sleepQueue, null);
			}
		}
	}
}
