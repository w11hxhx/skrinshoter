using System;
using System.CodeDom.Compiler;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IPersistStream_Extensions
{
	internal unsafe static void GetClassID(this IPersistStream @this, out Guid pClassID)
	{
		fixed (Guid* pClassID2 = &pClassID)
		{
			@this.GetClassID(pClassID2);
		}
	}
}
