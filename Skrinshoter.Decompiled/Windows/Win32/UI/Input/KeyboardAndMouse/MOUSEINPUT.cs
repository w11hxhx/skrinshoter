using System.CodeDom.Compiler;

namespace Windows.Win32.UI.Input.KeyboardAndMouse;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct MOUSEINPUT
{
	internal int dx;

	internal int dy;

	internal uint mouseData;

	internal MOUSE_EVENT_FLAGS dwFlags;

	internal uint time;

	internal nuint dwExtraInfo;
}
