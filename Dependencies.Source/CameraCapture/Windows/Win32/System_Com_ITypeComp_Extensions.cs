using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_ITypeComp_Extensions
{
	internal unsafe static HRESULT Bind(this ITypeComp.Interface @this, PWSTR szName, uint lHashVal, ushort wFlags, ITypeInfo** ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr)
	{
		fixed (BINDPTR* pBindPtr2 = &pBindPtr)
		{
			fixed (DESCKIND* pDescKind2 = &pDescKind)
			{
				return @this.Bind(szName, lHashVal, wFlags, ppTInfo, pDescKind2, pBindPtr2);
			}
		}
	}
}
