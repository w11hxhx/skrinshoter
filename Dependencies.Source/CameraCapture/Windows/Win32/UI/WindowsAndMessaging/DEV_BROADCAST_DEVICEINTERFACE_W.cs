using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DEV_BROADCAST_DEVICEINTERFACE_W
{
	internal uint dbcc_size;

	internal uint dbcc_devicetype;

	internal uint dbcc_reserved;

	internal Guid dbcc_classguid;

	internal VariableLengthInlineArray<char, ushort> dbcc_name;

	internal unsafe static int SizeOf(int count)
	{
		int num = sizeof(DEV_BROADCAST_DEVICEINTERFACE_W);
		if (count > 1)
		{
			num += checked((count - 1) * 2);
		}
		else if (count < 0)
		{
			throw new ArgumentOutOfRangeException();
		}
		return num;
	}
}
