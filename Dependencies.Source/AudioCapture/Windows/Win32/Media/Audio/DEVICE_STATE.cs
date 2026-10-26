using System.CodeDom.Compiler;

namespace Windows.Win32.Media.Audio;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum DEVICE_STATE : uint
{
	DEVICE_STATE_ACTIVE = 1u,
	DEVICE_STATE_DISABLED = 2u,
	DEVICE_STATE_NOTPRESENT = 4u,
	DEVICE_STATE_UNPLUGGED = 8u
}
