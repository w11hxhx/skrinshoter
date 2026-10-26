using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.HiDpi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint MONITORINFOF_PRIMARY = 1u;

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT GetDpiForMonitor(HMONITOR hmonitor, MONITOR_DPI_TYPE dpiType, out uint dpiX, out uint dpiY)
	{
		fixed (uint* dpiY2 = &dpiY)
		{
			fixed (uint* dpiX2 = &dpiX)
			{
				return GetDpiForMonitor(hmonitor, dpiType, dpiX2, dpiY2);
			}
		}
	}

	[DllImport("api-ms-win-shcore-scaling-l1-1-1.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT GetDpiForMonitor(HMONITOR hmonitor, MONITOR_DPI_TYPE dpiType, uint* dpiX, uint* dpiY);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL EnumDisplayMonitors(HDC hdc, RECT? lprcClip, MONITORENUMPROC lpfnEnum, LPARAM dwData)
	{
		RECT valueOrDefault = lprcClip.GetValueOrDefault();
		return EnumDisplayMonitors(hdc, lprcClip.HasValue ? (&valueOrDefault) : null, lpfnEnum, dwData);
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL EnumDisplayMonitors(HDC hdc, [Optional] RECT* lprcClip, [MarshalAs(UnmanagedType.FunctionPtr)] MONITORENUMPROC lpfnEnum, LPARAM dwData);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetMonitorInfo(HMONITOR hMonitor, ref MONITORINFO lpmi)
	{
		fixed (MONITORINFO* lpmi2 = &lpmi)
		{
			return GetMonitorInfo(hMonitor, lpmi2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetMonitorInfoW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetMonitorInfo(HMONITOR hMonitor, MONITORINFO* lpmi);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int GetSystemMetrics(SYSTEM_METRICS_INDEX nIndex);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMONITOR MonitorFromPoint(Point pt, MONITOR_FROM_FLAGS dwFlags);
}
