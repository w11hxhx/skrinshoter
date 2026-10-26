using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum KBDLLHOOKSTRUCT_FLAGS : uint
{
	LLKHF_EXTENDED = 1u,
	LLKHF_ALTDOWN = 0x20u,
	LLKHF_UP = 0x80u,
	LLKHF_INJECTED = 0x10u,
	LLKHF_LOWER_IL_INJECTED = 2u
}
