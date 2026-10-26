using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.System.Ole;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ELEMDESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal IDLDESC idldesc;

		[FieldOffset(0)]
		internal Windows.Win32.System.Ole.PARAMDESC paramdesc;
	}

	internal TYPEDESC tdesc;

	internal _Anonymous_e__Union Anonymous;
}
