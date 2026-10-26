using System.CodeDom.Compiler;
using System.Drawing;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct CURSORINFO
{
	internal uint cbSize;

	internal CURSORINFO_FLAGS flags;

	internal HCURSOR hCursor;

	internal Point ptScreenPos;
}
