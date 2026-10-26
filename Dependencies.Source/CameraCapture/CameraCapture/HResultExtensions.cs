using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace CameraCapture;

internal static class HResultExtensions
{
	internal static void ThrowOnFailure(this HRESULT hr, string message)
	{
		if (hr.Failed)
		{
			throw new MediaFoundationException(message, Marshal.GetExceptionForHR(hr));
		}
	}
}
