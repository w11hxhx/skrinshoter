using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Dxgi.Common;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_GAMMA_CONTROL_CAPABILITIES
{
	internal BOOL ScaleAndOffsetSupported;

	internal float MaxConvertedValue;

	internal float MinConvertedValue;

	internal uint NumGammaControlPoints;

	internal __float_1025 ControlPointPositions;
}
