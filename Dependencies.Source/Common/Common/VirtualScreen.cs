using System.Collections.Generic;
using System.Drawing;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Common;

public static class VirtualScreen
{
	public static Rectangle GetRectangle()
	{
		return new Rectangle(PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_XVIRTUALSCREEN), PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_YVIRTUALSCREEN), PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXVIRTUALSCREEN), PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYVIRTUALSCREEN));
	}

	public static unsafe List<MonitorInfo> GetMonitors()
	{
		List<MonitorInfo> monitorList = new List<MonitorInfo>();
		PInvoke.EnumDisplayMonitors(HDC.Null, (RECT?)null, (MONITORENUMPROC)MonitorEnumProc, (LPARAM)0);
		return monitorList;
		unsafe BOOL MonitorEnumProc(HMONITOR hMonitor, HDC hDC, RECT* pRect, LPARAM dwData)
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
			monitorList.Add(new MonitorInfo(hMonitor, mONITORINFOEXW.szDevice.ToString()));
			return true;
		}
	}
}
