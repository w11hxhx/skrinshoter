using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace CameraCapture;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate LRESULT WNDPROC(HWND hWnd, uint uMsg, WPARAM wParam, LPARAM lParam);
