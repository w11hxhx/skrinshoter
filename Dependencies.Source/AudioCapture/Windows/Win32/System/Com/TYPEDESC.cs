using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.System.Ole;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct TYPEDESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal unsafe TYPEDESC* lptdesc;

		[FieldOffset(0)]
		internal unsafe ARRAYDESC* lpadesc;

		[FieldOffset(0)]
		internal uint hreftype;
	}

	internal _Anonymous_e__Union Anonymous;

	internal VARENUM vt;
}
