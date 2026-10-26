using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct KBDLLHOOKSTRUCT
{
	internal uint vkCode;

	internal uint scanCode;

	internal KBDLLHOOKSTRUCT_FLAGS flags;

	internal uint time;

	internal nuint dwExtraInfo;
}
