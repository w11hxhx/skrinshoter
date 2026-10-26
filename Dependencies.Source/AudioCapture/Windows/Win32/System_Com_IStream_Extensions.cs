using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IStream_Extensions
{
	internal unsafe static HRESULT Stat(this IStream.Interface @this, out STATSTG pstatstg, uint grfStatFlag)
	{
		fixed (STATSTG* pstatstg2 = &pstatstg)
		{
			return @this.Stat(pstatstg2, grfStatFlag);
		}
	}
}
