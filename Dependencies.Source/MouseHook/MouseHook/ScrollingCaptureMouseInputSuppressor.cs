using System;
using System.Drawing;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MouseHook;

public sealed class ScrollingCaptureMouseInputSuppressor : MouseHookBase
{
	public const nuint AutoScrollExtraInfo = 1396920908u;

	private readonly string monitorName;

	private readonly uint currentProcessId;

	public ScrollingCaptureMouseInputSuppressor(string monitorName, ILogger<ScrollingCaptureMouseInputSuppressor> logger)
		: base(logger)
	{
		this.monitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		currentProcessId = PInvoke.GetCurrentProcessId();
		StartMouseHook();
	}

	private protected override bool OnMouseMessage(uint message, ref MSLLHOOKSTRUCT data)
	{
		if ((data.flags & 1) != 0)
		{
			if ((message == 522 || message == 526) && data.dwExtraInfo == 1396920908)
			{
				return IsAnyModifierKeyDown();
			}
			return false;
		}
		if (IsSuppressibleMessage(message) && IsPointOnCapturedMonitor(data.pt))
		{
			return !IsPointOverCurrentProcessWindow(data.pt);
		}
		return false;
	}

	private static bool IsAnyModifierKeyDown()
	{
		if (!IsVirtualKeyDown(VIRTUAL_KEY.VK_CONTROL) && !IsVirtualKeyDown(VIRTUAL_KEY.VK_SHIFT) && !IsVirtualKeyDown(VIRTUAL_KEY.VK_MENU) && !IsVirtualKeyDown(VIRTUAL_KEY.VK_LWIN))
		{
			return IsVirtualKeyDown(VIRTUAL_KEY.VK_RWIN);
		}
		return true;
	}

	private static bool IsVirtualKeyDown(VIRTUAL_KEY vKey)
	{
		return (PInvoke.GetAsyncKeyState((int)vKey) & 0x8000) != 0;
	}

	private static bool IsSuppressibleMessage(uint message)
	{
		if (message - 513 <= 13)
		{
			return true;
		}
		return false;
	}

	private unsafe bool IsPointOnCapturedMonitor(Point point)
	{
		HMONITOR hMonitor = PInvoke.MonitorFromPoint(point, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONULL);
		if (hMonitor.IsNull)
		{
			return false;
		}
		MONITORINFOEXW mONITORINFOEXW = new MONITORINFOEXW
		{
			monitorInfo = 
			{
				cbSize = (uint)sizeof(MONITORINFOEXW)
			}
		};
		if (!PInvoke.GetMonitorInfo(hMonitor, (MONITORINFO*)(&mONITORINFOEXW)))
		{
			return false;
		}
		return mONITORINFOEXW.szDevice.Equals(monitorName);
	}

	private unsafe bool IsPointOverCurrentProcessWindow(Point point)
	{
		HWND hWnd = PInvoke.WindowFromPoint(point);
		if (hWnd.IsNull)
		{
			return false;
		}
		uint num = 0u;
		PInvoke.GetWindowThreadProcessId(hWnd, &num);
		return num == currentProcessId;
	}
}
