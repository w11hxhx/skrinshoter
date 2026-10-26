using System.CodeDom.Compiler;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IBindCtx_Extensions
{
	internal unsafe static void SetBindOptions(this IBindCtx @this, in BIND_OPTS pbindopts)
	{
		fixed (BIND_OPTS* bindOptions = &pbindopts)
		{
			@this.SetBindOptions(bindOptions);
		}
	}

	internal unsafe static void GetBindOptions(this IBindCtx @this, ref BIND_OPTS pbindopts)
	{
		fixed (BIND_OPTS* pbindopts2 = &pbindopts)
		{
			@this.GetBindOptions(pbindopts2);
		}
	}
}
