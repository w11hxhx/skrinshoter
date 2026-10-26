using System.CodeDom.Compiler;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DISPPARAMS
{
	internal unsafe VARIANT* rgvarg;

	internal unsafe int* rgdispidNamedArgs;

	internal uint cArgs;

	internal uint cNamedArgs;
}
