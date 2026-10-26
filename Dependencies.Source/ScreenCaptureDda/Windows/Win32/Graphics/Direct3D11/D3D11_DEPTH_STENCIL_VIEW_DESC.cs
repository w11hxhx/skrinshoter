using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_DEPTH_STENCIL_VIEW_DESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal D3D11_TEX1D_DSV Texture1D;

		[FieldOffset(0)]
		internal D3D11_TEX1D_ARRAY_DSV Texture1DArray;

		[FieldOffset(0)]
		internal D3D11_TEX2D_DSV Texture2D;

		[FieldOffset(0)]
		internal D3D11_TEX2D_ARRAY_DSV Texture2DArray;

		[FieldOffset(0)]
		internal D3D11_TEX2DMS_DSV Texture2DMS;

		[FieldOffset(0)]
		internal D3D11_TEX2DMS_ARRAY_DSV Texture2DMSArray;
	}

	internal DXGI_FORMAT Format;

	internal D3D11_DSV_DIMENSION ViewDimension;

	internal uint Flags;

	internal _Anonymous_e__Union Anonymous;
}
