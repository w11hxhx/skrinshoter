using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MouseHook;

public abstract class MouseHookBase : SafeDisposable
{
	protected readonly ILogger logger;

	private Thread thread;

	private uint threadId;

	private TaskCompletionSource<bool> threadStarted;

	private static readonly FreeLibrarySafeHandle userLibrary = PInvoke.LoadLibrary("User32");

	private readonly HOOKPROC hookProc;

	private bool mouseHookStarted;

	protected MouseHookBase(ILogger logger)
	{
		this.logger = logger ?? NullLogger.Instance;
		hookProc = LowLevelMouseProc;
	}

	protected void StartMouseHook()
	{
		if (!mouseHookStarted)
		{
			threadStarted = new TaskCompletionSource<bool>();
			thread = new Thread(ThreadProc)
			{
				Name = "Low-level mouse hook",
				IsBackground = true,
				Priority = ThreadPriority.Highest
			};
			thread.Start();
			mouseHookStarted = threadStarted.Task.GetAwaiter().GetResult();
		}
	}

	protected void StopMouseHook()
	{
		if (mouseHookStarted)
		{
			mouseHookStarted = false;
			if ((bool)PInvoke.PostThreadMessage(threadId, 18u, 0u, 0))
			{
				thread.Join();
			}
		}
	}

	private void ThreadProc()
	{
		HHOOK hhk;
		try
		{
			threadId = PInvoke.GetCurrentThreadId();
			hhk = PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_MOUSE_LL, hookProc, (HINSTANCE)userLibrary.DangerousGetHandle(), 0u);
			if (hhk.IsNull)
			{
				throw new WinApiException("SetWindowsHookEx(WH_MOUSE_LL)", new Win32Exception());
			}
			PInvoke.PeekMessage(out var _, HWND.Null, 0u, 0u, PEEK_MESSAGE_REMOVE_TYPE.PM_NOREMOVE);
			logger.LogInformation("Mouse hook started");
			threadStarted.SetResult(result: true);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to start mouse hook");
			threadStarted.SetResult(result: false);
			return;
		}
		try
		{
			ProcessThreadMessages();
		}
		catch (Exception exception2)
		{
			logger.LogError(exception2, "Error in mouse hook thread event loop");
		}
		try
		{
			if (!PInvoke.UnhookWindowsHookEx(hhk))
			{
				throw new WinApiException("UnhookWindowsHookEx()", new Win32Exception());
			}
			logger.LogInformation("Mouse hook stopped");
		}
		catch (Exception exception3)
		{
			logger.LogError(exception3, "Failed to stop mouse hook");
		}
	}

	private static void ProcessThreadMessages()
	{
		BOOL message;
		MSG lpMsg;
		while ((int)(message = PInvoke.GetMessage(out lpMsg, HWND.Null, 0u, 0u)) != 0)
		{
			if ((int)message == -1)
			{
				throw new WinApiException("GetMessage()", new Win32Exception());
			}
			PInvoke.TranslateMessage(in lpMsg);
			PInvoke.DispatchMessage(in lpMsg);
		}
	}

	private unsafe LRESULT LowLevelMouseProc(int code, WPARAM wParam, LPARAM lParam)
	{
		if (code >= 0)
		{
			MSLLHOOKSTRUCT data = *(MSLLHOOKSTRUCT*)(void*)(IntPtr)(nint)lParam;
			if (OnMouseMessage((uint)(nuint)wParam, ref data))
			{
				return (LRESULT)1;
			}
		}
		return PInvoke.CallNextHookEx(HHOOK.Null, code, wParam, lParam);
	}

	private protected abstract bool OnMouseMessage(uint message, ref MSLLHOOKSTRUCT data);

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			StopMouseHook();
		}
	}
}
