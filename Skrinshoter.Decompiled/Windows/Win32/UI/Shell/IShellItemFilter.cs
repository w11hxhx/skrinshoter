using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("2659B475-EEB8-48B7-8F07-B378810F48CF")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IShellItemFilter
{
	void IncludeItem(IShellItem psi);

	void GetEnumFlagsForItem(IShellItem psi, out uint pgrfFlags);
}
