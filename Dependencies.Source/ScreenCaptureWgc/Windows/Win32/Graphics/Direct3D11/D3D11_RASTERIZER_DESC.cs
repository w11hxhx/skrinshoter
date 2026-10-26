using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_RASTERIZER_DESC
{
	internal D3D11_FILL_MODE FillMode;

	internal D3D11_CULL_MODE CullMode;

	internal BOOL FrontCounterClockwise;

	internal int DepthBias;

	internal float DepthBiasClamp;

	internal float SlopeScaledDepthBias;

	internal BOOL DepthClipEnable;

	internal BOOL ScissorEnable;

	internal BOOL MultisampleEnable;

	internal BOOL AntialiasedLineEnable;
}
