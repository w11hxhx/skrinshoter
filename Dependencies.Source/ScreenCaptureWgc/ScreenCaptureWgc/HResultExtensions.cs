using System.Runtime.InteropServices;
using Common;
using Windows.Win32.Foundation;

namespace ScreenCaptureWgc;

internal static class HResultExtensions
{
	public static void ThrowOnFailure(this HRESULT hr, string message)
	{
		if (hr.Failed)
		{
			throw new WinApiException(message, Marshal.GetExceptionForHR(hr));
		}
	}
}
