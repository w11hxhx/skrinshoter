using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Storage.FileSystem;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum MOVE_FILE_FLAGS : uint
{
	MOVEFILE_COPY_ALLOWED = 2u,
	MOVEFILE_CREATE_HARDLINK = 0x10u,
	MOVEFILE_DELAY_UNTIL_REBOOT = 4u,
	MOVEFILE_REPLACE_EXISTING = 1u,
	MOVEFILE_WRITE_THROUGH = 8u,
	MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x20u
}
