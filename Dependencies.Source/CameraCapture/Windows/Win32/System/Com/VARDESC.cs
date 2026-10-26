using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct VARDESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal uint oInst;

		[FieldOffset(0)]
		internal unsafe VARIANT* lpvarValue;
	}

	internal int memid;

	internal PWSTR lpstrSchema;

	internal _Anonymous_e__Union Anonymous;

	internal ELEMDESC elemdescVar;

	internal VARFLAGS wVarFlags;

	internal VARKIND varkind;
}
