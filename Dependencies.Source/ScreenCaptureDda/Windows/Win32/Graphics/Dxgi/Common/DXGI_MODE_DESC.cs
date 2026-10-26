using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Dxgi.Common;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_MODE_DESC
{
	internal uint Width;

	internal uint Height;

	internal DXGI_RATIONAL RefreshRate;

	internal DXGI_FORMAT Format;

	internal DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

	internal DXGI_MODE_SCALING Scaling;
}
