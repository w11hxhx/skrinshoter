using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_SAMPLER_DESC
{
	internal D3D11_FILTER Filter;

	internal D3D11_TEXTURE_ADDRESS_MODE AddressU;

	internal D3D11_TEXTURE_ADDRESS_MODE AddressV;

	internal D3D11_TEXTURE_ADDRESS_MODE AddressW;

	internal float MipLODBias;

	internal uint MaxAnisotropy;

	internal D3D11_COMPARISON_FUNC ComparisonFunc;

	internal __float_4 BorderColor;

	internal float MinLOD;

	internal float MaxLOD;
}
