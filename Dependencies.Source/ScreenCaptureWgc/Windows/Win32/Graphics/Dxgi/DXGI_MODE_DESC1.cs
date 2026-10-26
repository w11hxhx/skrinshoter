using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_MODE_DESC1
{
	internal uint Width;

	internal uint Height;

	internal DXGI_RATIONAL RefreshRate;

	internal DXGI_FORMAT Format;

	internal DXGI_MODE_SCANLINE_ORDER ScanlineOrdering;

	internal DXGI_MODE_SCALING Scaling;

	internal BOOL Stereo;
}
