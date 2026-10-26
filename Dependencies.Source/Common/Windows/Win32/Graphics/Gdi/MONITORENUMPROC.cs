using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Gdi;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal unsafe delegate BOOL MONITORENUMPROC(HMONITOR param0, HDC param1, RECT* param2, LPARAM param3);
