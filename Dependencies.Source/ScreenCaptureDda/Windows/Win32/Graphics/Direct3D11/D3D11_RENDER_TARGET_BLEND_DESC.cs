using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_RENDER_TARGET_BLEND_DESC
{
	internal BOOL BlendEnable;

	internal D3D11_BLEND SrcBlend;

	internal D3D11_BLEND DestBlend;

	internal D3D11_BLEND_OP BlendOp;

	internal D3D11_BLEND SrcBlendAlpha;

	internal D3D11_BLEND DestBlendAlpha;

	internal D3D11_BLEND_OP BlendOpAlpha;

	internal byte RenderTargetWriteMask;
}
