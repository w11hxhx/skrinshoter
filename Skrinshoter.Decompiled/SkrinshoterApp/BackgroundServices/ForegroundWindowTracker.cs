using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common;
using Microsoft.Extensions.Hosting;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Accessibility;

namespace SkrinshoterApp.BackgroundServices;

public sealed class ForegroundWindowTracker : IHostedService, IForegroundWindowInfoProvider
{
	private readonly Dispatcher dispatcher;

	private readonly WINEVENTPROC winEventProc;

	private HWINEVENTHOOK hWinEventSystemForegroundHook;

	private HWINEVENTHOOK hWinEventObjectNameChangeHook;

	private HWND lastWindowHandle;

	private string lastWindowTitle = "";

	private readonly object lockObject = new object();

	public string Title
	{
		get
		{
			lock (lockObject)
			{
				return lastWindowTitle;
			}
		}
	}

	public ForegroundWindowTracker(Dispatcher dispatcher)
	{
		this.dispatcher = dispatcher;
		winEventProc = WinEventProc;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			lock (lockObject)
			{
				UpdateForegroundWindowInfo(PInvoke.GetForegroundWindow());
			}
			try
			{
				hWinEventSystemForegroundHook = PInvoke.SetWinEventHook(3u, 3u, HMODULE.Null, winEventProc, 0u, 0u, 0u);
				if (hWinEventSystemForegroundHook.IsNull)
				{
					throw new WinApiException("SetWinEventHook(EVENT_SYSTEM_FOREGROUND)");
				}
				hWinEventObjectNameChangeHook = PInvoke.SetWinEventHook(32780u, 32780u, HMODULE.Null, winEventProc, 0u, 0u, 0u);
				if (hWinEventObjectNameChangeHook.IsNull)
				{
					throw new WinApiException("SetWinEventHook(EVENT_OBJECT_NAMECHANGE)");
				}
			}
			catch
			{
				UnhookWinEvents();
				throw;
			}
			return Task.CompletedTask;
		});
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			UnhookWinEvents();
			return Task.CompletedTask;
		});
	}

	private void WinEventProc(HWINEVENTHOOK hWinEventHook, uint eventType, HWND hwnd, int idObject, int idChild, uint idEventThread, uint dwmsEventTime)
	{
		lock (lockObject)
		{
			switch (eventType)
			{
			case 3u:
				UpdateForegroundWindowInfo(hwnd);
				break;
			case 32780u:
				if (hwnd == lastWindowHandle && idObject == 0 && idChild == 0)
				{
					UpdateForegroundWindowInfo(hwnd);
				}
				break;
			}
		}
	}

	private void UpdateForegroundWindowInfo(HWND hwnd)
	{
		if (!(hwnd == HWND.Null) && !IsSystemTrayOverflowWindow(hwnd))
		{
			string windowTitle = GetWindowTitle(hwnd);
			if (!string.IsNullOrWhiteSpace(windowTitle))
			{
				lastWindowTitle = windowTitle;
				lastWindowHandle = hwnd;
			}
		}
	}

	private static bool IsSystemTrayOverflowWindow(HWND hwnd)
	{
		string windowClassName = GetWindowClassName(hwnd);
		if (windowClassName == "TopLevelWindowForOverflowXamlIsland" || windowClassName == "NotifyIconOverflowWindow")
		{
			return IsShellProcessWindow(hwnd);
		}
		return false;
	}

	private unsafe static bool IsShellProcessWindow(HWND hwnd)
	{
		if (hwnd == HWND.Null)
		{
			return false;
		}
		HWND shellWindow = PInvoke.GetShellWindow();
		if (shellWindow == HWND.Null)
		{
			return false;
		}
		uint num = default;
		PInvoke.GetWindowThreadProcessId(hwnd, &num);
		uint num2 = default;
		PInvoke.GetWindowThreadProcessId(shellWindow, &num2);
		if (num == 0 || num2 == 0)
		{
			return false;
		}
		return num == num2;
	}

	private static string GetWindowTitle(HWND hwnd)
	{
		char[] array = new char[256];
		int windowText = PInvoke.GetWindowText(hwnd, array);
		if (windowText > 0)
		{
			return new string(array, 0, windowText);
		}
		return string.Empty;
	}

	private static string GetWindowClassName(HWND hwnd)
	{
		char[] array = new char[256];
		int className = PInvoke.GetClassName(hwnd, array);
		if (className > 0)
		{
			return new string(array, 0, className);
		}
		return string.Empty;
	}

	private void UnhookWinEvents()
	{
		if (!hWinEventSystemForegroundHook.IsNull)
		{
			PInvoke.UnhookWinEvent(hWinEventSystemForegroundHook);
			hWinEventSystemForegroundHook = default;
		}
		if (!hWinEventObjectNameChangeHook.IsNull)
		{
			PInvoke.UnhookWinEvent(hWinEventObjectNameChangeHook);
			hWinEventObjectNameChangeHook = default;
		}
	}
}
