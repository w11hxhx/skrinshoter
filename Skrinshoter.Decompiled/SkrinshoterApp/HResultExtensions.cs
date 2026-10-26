using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace SkrinshoterApp;

internal static class HResultExtensions
{
	internal static void ThrowOnFailure(this HRESULT hr, string message)
	{
		if (hr.Failed)
		{
			throw new COMException(message, Marshal.GetExceptionForHR(hr));
		}
	}
}
