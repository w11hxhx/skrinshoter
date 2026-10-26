using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace CameraCapture;

internal struct WNDCLASSW
{
	internal WNDCLASS_STYLES style;

	internal WNDPROC lpfnWndProc;

	internal int cbClsExtra;

	internal int cbWndExtra;

	internal HINSTANCE hInstance;

	internal HICON hIcon;

	internal HCURSOR hCursor;

	internal HBRUSH hbrBackground;

	internal PCWSTR lpszMenuName;

	internal PCWSTR lpszClassName;
}
