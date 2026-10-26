using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_OUTPUT_DESC
{
	internal __char_32 DeviceName;

	internal RECT DesktopCoordinates;

	internal BOOL AttachedToDesktop;

	internal DXGI_MODE_ROTATION Rotation;

	internal HMONITOR Monitor;
}
