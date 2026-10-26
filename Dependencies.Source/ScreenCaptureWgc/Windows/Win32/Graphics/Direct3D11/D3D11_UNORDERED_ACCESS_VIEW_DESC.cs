using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_UNORDERED_ACCESS_VIEW_DESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal D3D11_BUFFER_UAV Buffer;

		[FieldOffset(0)]
		internal D3D11_TEX1D_UAV Texture1D;

		[FieldOffset(0)]
		internal D3D11_TEX1D_ARRAY_UAV Texture1DArray;

		[FieldOffset(0)]
		internal D3D11_TEX2D_UAV Texture2D;

		[FieldOffset(0)]
		internal D3D11_TEX2D_ARRAY_UAV Texture2DArray;

		[FieldOffset(0)]
		internal D3D11_TEX3D_UAV Texture3D;
	}

	internal DXGI_FORMAT Format;

	internal D3D11_UAV_DIMENSION ViewDimension;

	internal _Anonymous_e__Union Anonymous;
}
