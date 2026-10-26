using System;
using System.Drawing;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.HiDpi;

namespace Common;

public class MonitorInfo
{
	private HMONITOR hMonitor;

	public string MonitorName { get; }

	public MonitorInfo()
		: this(PInvoke.MonitorFromPoint(Point.Empty, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTOPRIMARY))
	{
	}

	public unsafe MonitorInfo(string monitorName)
	{
		MonitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		PInvoke.EnumDisplayMonitors(HDC.Null, (RECT?)null, (MONITORENUMPROC)MonitorEnumProc, (LPARAM)0);
		if (hMonitor == HMONITOR.Null)
		{
			throw new InvalidOperationException("Monitor " + monitorName + " not found.");
		}
	}

	public unsafe MonitorInfo(IntPtr hMonitor)
	{
		if (hMonitor == IntPtr.Zero)
		{
			throw new ArgumentNullException("hMonitor");
		}
		this.hMonitor = (HMONITOR)hMonitor;
		MONITORINFOEXW mONITORINFOEXW = new MONITORINFOEXW
		{
			monitorInfo = 
			{
				cbSize = (uint)sizeof(MONITORINFOEXW)
			}
		};
		if (!PInvoke.GetMonitorInfo(this.hMonitor, (MONITORINFO*)(&mONITORINFOEXW)))
		{
			throw new InvalidOperationException("Monitor not found.");
		}
		MonitorName = mONITORINFOEXW.szDevice.ToString();
	}

	internal MonitorInfo(HMONITOR hMonitor, string monitorName)
	{
		if (hMonitor == IntPtr.Zero)
		{
			throw new ArgumentNullException("hMonitor");
		}
		this.hMonitor = hMonitor;
		MonitorName = monitorName ?? throw new ArgumentNullException("monitorName");
	}

	public unsafe bool IsPrimary()
	{
		MONITORINFO lpmi = new MONITORINFO
		{
			cbSize = (uint)sizeof(MONITORINFO)
		};
		if (!PInvoke.GetMonitorInfo(hMonitor, ref lpmi))
		{
			throw new WinApiException("GetMonitorInfo()");
		}
		return (lpmi.dwFlags & 1) != 0;
	}

	public unsafe Rectangle GetRectangle()
	{
		MONITORINFO lpmi = new MONITORINFO
		{
			cbSize = (uint)sizeof(MONITORINFO)
		};
		if (!PInvoke.GetMonitorInfo(hMonitor, ref lpmi))
		{
			throw new WinApiException("GetMonitorInfo()");
		}
		return new Rectangle(lpmi.rcMonitor.X, lpmi.rcMonitor.Y, lpmi.rcMonitor.Width, lpmi.rcMonitor.Height);
	}

	public unsafe Rectangle GetWorkingArea()
	{
		MONITORINFO lpmi = new MONITORINFO
		{
			cbSize = (uint)sizeof(MONITORINFO)
		};
		if (!PInvoke.GetMonitorInfo(hMonitor, ref lpmi))
		{
			throw new WinApiException("GetMonitorInfo()");
		}
		return new Rectangle(lpmi.rcWork.X, lpmi.rcWork.Y, lpmi.rcWork.Width, lpmi.rcWork.Height);
	}

	public int GetDpi()
	{
		if (PInvoke.GetDpiForMonitor(hMonitor, MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out var dpiX, out var _) != HRESULT.S_OK)
		{
			throw new WinApiException("GetDpiForMonitor()");
		}
		return (int)dpiX;
	}

	public double GetScaleFactor()
	{
		return (double)GetDpi() / 96.0;
	}

	private unsafe BOOL MonitorEnumProc(HMONITOR hMonitor, HDC hDC, RECT* pRect, LPARAM dwData)
	{
		MONITORINFOEXW mONITORINFOEXW = new MONITORINFOEXW
		{
			monitorInfo = 
			{
				cbSize = (uint)sizeof(MONITORINFOEXW)
			}
		};
		if (!PInvoke.GetMonitorInfo(hMonitor, (MONITORINFO*)(&mONITORINFOEXW)))
		{
			return true;
		}
		if (mONITORINFOEXW.szDevice.ToString() != MonitorName)
		{
			return true;
		}
		this.hMonitor = hMonitor;
		return false;
	}
}
