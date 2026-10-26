using System.Runtime.InteropServices;

namespace CameraCapture;

internal static class PInvokeCorrections
{
	[DllImport("USER32.dll", EntryPoint = "RegisterClassW", ExactSpelling = true, SetLastError = true)]
	internal static extern ushort RegisterClass(WNDCLASSW lpWndClass);
}
