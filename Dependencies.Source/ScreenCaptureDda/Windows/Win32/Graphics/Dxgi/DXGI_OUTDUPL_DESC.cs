using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_OUTDUPL_DESC
{
	internal DXGI_MODE_DESC ModeDesc;

	internal DXGI_MODE_ROTATION Rotation;

	internal BOOL DesktopImageInSystemMemory;
}
