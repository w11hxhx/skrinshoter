using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.System.Com;

[StructLayout(LayoutKind.Explicit)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct BINDPTR
{
	[FieldOffset(0)]
	internal unsafe FUNCDESC* lpfuncdesc;

	[FieldOffset(0)]
	internal unsafe VARDESC* lpvardesc;

	[FieldOffset(0)]
	internal unsafe ITypeComp* lptcomp;
}
