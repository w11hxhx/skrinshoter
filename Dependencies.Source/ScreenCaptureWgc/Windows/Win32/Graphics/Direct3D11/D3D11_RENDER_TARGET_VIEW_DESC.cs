using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_RENDER_TARGET_VIEW_DESC
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		internal D3D11_BUFFER_RTV Buffer;

		[FieldOffset(0)]
		internal D3D11_TEX1D_RTV Texture1D;

		[FieldOffset(0)]
		internal D3D11_TEX1D_ARRAY_RTV Texture1DArray;

		[FieldOffset(0)]
		internal D3D11_TEX2D_RTV Texture2D;

		[FieldOffset(0)]
		internal D3D11_TEX2D_ARRAY_RTV Texture2DArray;

		[FieldOffset(0)]
		internal D3D11_TEX2DMS_RTV Texture2DMS;

		[FieldOffset(0)]
		internal D3D11_TEX2DMS_ARRAY_RTV Texture2DMSArray;

		[FieldOffset(0)]
		internal D3D11_TEX3D_RTV Texture3D;
	}

	internal DXGI_FORMAT Format;

	internal D3D11_RTV_DIMENSION ViewDimension;

	internal _Anonymous_e__Union Anonymous;
}
