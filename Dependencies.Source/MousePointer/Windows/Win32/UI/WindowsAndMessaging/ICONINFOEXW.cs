using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ICONINFOEXW
{
	internal uint cbSize;

	internal BOOL fIcon;

	internal uint xHotspot;

	internal uint yHotspot;

	internal HBITMAP hbmMask;

	internal HBITMAP hbmColor;

	internal ushort wResID;

	internal __char_260 szModName;

	internal __char_260 szResName;
}
