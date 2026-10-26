using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace Common;

public class MultimediaTimer : SafeDisposable
{
	private delegate void SystemTimerCallback(uint uTimerID, uint uMsg, IntPtr dwUser, uint dw1, uint dw2);

	public const uint Resolution = 1u;

	private uint timerID;

	private readonly SystemTimerCallback systemCallback;

	private TimerCallback userCallback;

	private object userState;

	private const uint TIME_PERIODIC = 1u;

	private const uint TIME_CALLBACK_FUNCTION = 0u;

	private const uint TIME_KILL_SYNCHRONOUS = 256u;

	public MultimediaTimer(TimerCallback callback, object state = null)
	{
		systemCallback = SystemCallback;
		userCallback = callback ?? throw new ArgumentNullException("callback");
		userState = state;
	}

	private void SystemCallback(uint uTimerID, uint uMsg, IntPtr dwUser, uint dw1, uint dw2)
	{
		userCallback(userState);
	}

	public void Start(uint interval)
	{
		if (interval == 0)
		{
			throw new ArgumentOutOfRangeException("interval");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			if (timerID != 0)
			{
				throw new InvalidOperationException($"Object {this} is already running.");
			}
			timerID = timeSetEvent(interval, 1u, systemCallback, IntPtr.Zero, 257u);
			if (timerID == 0)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}
	}

	public void Stop()
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed)
			{
				DoStop();
			}
		}
	}

	private void DoStop()
	{
		if (timerID != 0)
		{
			timeKillEvent(timerID);
			timerID = 0u;
		}
	}

	protected override void Dispose(bool disposing)
	{
		DoStop();
		if (disposing)
		{
			userCallback = null;
			userState = null;
		}
	}

	[DllImport("winmm.dll", SetLastError = true)]
	private static extern uint timeSetEvent(uint uDelay, uint uResolution, SystemTimerCallback lpTimeProc, IntPtr dwUser, uint fuEvent);

	[DllImport("winmm.dll", SetLastError = true)]
	private static extern uint timeKillEvent(uint uTimerID);
}
