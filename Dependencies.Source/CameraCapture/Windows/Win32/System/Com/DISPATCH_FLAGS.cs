using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.System.Com;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum DISPATCH_FLAGS : ushort
{
	DISPATCH_METHOD = 1,
	DISPATCH_PROPERTYGET = 2,
	DISPATCH_PROPERTYPUT = 4,
	DISPATCH_PROPERTYPUTREF = 8
}
