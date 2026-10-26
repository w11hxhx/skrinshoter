using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct WNDCLASSW
{
	internal WNDCLASS_STYLES style;

	internal unsafe delegate* unmanaged[Stdcall]<HWND, uint, WPARAM, LPARAM, LRESULT> lpfnWndProc;

	internal int cbClsExtra;

	internal int cbWndExtra;

	internal HINSTANCE hInstance;

	internal HICON hIcon;

	internal HCURSOR hCursor;

	internal HBRUSH hbrBackground;

	internal PCWSTR lpszMenuName;

	internal PCWSTR lpszClassName;
}
