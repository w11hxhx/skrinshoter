using System.CodeDom.Compiler;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IStream_Extensions
{
	internal unsafe static void Stat(this IStream @this, out STATSTG pstatstg, STATFLAG grfStatFlag)
	{
		fixed (STATSTG* pstatstg2 = &pstatstg)
		{
			@this.Stat(pstatstg2, grfStatFlag);
		}
	}
}
