using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_DEPTH_STENCIL_DESC
{
	internal BOOL DepthEnable;

	internal D3D11_DEPTH_WRITE_MASK DepthWriteMask;

	internal D3D11_COMPARISON_FUNC DepthFunc;

	internal BOOL StencilEnable;

	internal byte StencilReadMask;

	internal byte StencilWriteMask;

	internal D3D11_DEPTH_STENCILOP_DESC FrontFace;

	internal D3D11_DEPTH_STENCILOP_DESC BackFace;
}
