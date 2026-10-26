using System.Runtime.InteropServices;
using Common;
using Windows.Win32.Foundation;

namespace ScreenCaptureDda;

internal static class HResultExtensions
{
	internal static void ThrowOnFailure(this HRESULT hr, string message)
	{
		if (hr.Failed)
		{
			throw new DirectXException(message, Marshal.GetExceptionForHR(hr));
		}
	}
}
