using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("B4DB1657-70D7-485E-8E3E-6FCB5A5C1802")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IModalWindow
{
	void Show(HWND hwndOwner);
}
