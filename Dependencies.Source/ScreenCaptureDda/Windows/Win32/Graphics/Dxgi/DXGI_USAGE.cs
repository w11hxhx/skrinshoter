using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Dxgi;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum DXGI_USAGE : uint
{
	DXGI_USAGE_SHADER_INPUT = 0x10u,
	DXGI_USAGE_RENDER_TARGET_OUTPUT = 0x20u,
	DXGI_USAGE_BACK_BUFFER = 0x40u,
	DXGI_USAGE_SHARED = 0x80u,
	DXGI_USAGE_READ_ONLY = 0x100u,
	DXGI_USAGE_DISCARD_ON_PRESENT = 0x200u,
	DXGI_USAGE_UNORDERED_ACCESS = 0x400u
}
