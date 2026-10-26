using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.System.WinRT.Graphics.Capture;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_WinRT_Graphics_Capture_IGraphicsCaptureItemInterop_Extensions
{
	internal unsafe static HRESULT CreateForWindow(this IGraphicsCaptureItemInterop.Interface @this, HWND window, in Guid riid, out void* result)
	{
		fixed (void** result2 = &result)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.CreateForWindow(window, riid2, result2);
			}
		}
	}

	internal unsafe static HRESULT CreateForMonitor(this IGraphicsCaptureItemInterop.Interface @this, HMONITOR monitor, in Guid riid, out void* result)
	{
		fixed (void** result2 = &result)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.CreateForMonitor(monitor, riid2, result2);
			}
		}
	}
}
