using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_BUFFER_SRV
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous1_e__Union
	{
		[FieldOffset(0)]
		internal uint FirstElement;

		[FieldOffset(0)]
		internal uint ElementOffset;
	}

	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous2_e__Union
	{
		[FieldOffset(0)]
		internal uint NumElements;

		[FieldOffset(0)]
		internal uint ElementWidth;
	}

	internal _Anonymous1_e__Union Anonymous1;

	internal _Anonymous2_e__Union Anonymous2;
}
