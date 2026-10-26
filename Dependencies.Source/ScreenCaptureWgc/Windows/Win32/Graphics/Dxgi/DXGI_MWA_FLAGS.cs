using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Dxgi;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum DXGI_MWA_FLAGS : uint
{
	DXGI_MWA_NO_WINDOW_CHANGES = 1u,
	DXGI_MWA_NO_ALT_ENTER = 2u,
	DXGI_MWA_NO_PRINT_SCREEN = 4u,
	DXGI_MWA_VALID = DXGI_MWA_NO_WINDOW_CHANGES | DXGI_MWA_NO_ALT_ENTER | DXGI_MWA_NO_PRINT_SCREEN
}
