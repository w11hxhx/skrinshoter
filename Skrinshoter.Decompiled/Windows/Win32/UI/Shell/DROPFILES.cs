using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DROPFILES
{
	internal uint pFiles;

	internal Point pt;

	internal BOOL fNC;

	internal BOOL fWide;
}
