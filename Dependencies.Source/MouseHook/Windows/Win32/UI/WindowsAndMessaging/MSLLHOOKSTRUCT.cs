using System.CodeDom.Compiler;
using System.Drawing;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct MSLLHOOKSTRUCT
{
	internal Point pt;

	internal uint mouseData;

	internal uint flags;

	internal uint time;

	internal nuint dwExtraInfo;
}
