using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.SystemServices;
using Windows.Win32.UI.Shell;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IShellItem_Extensions
{
	internal unsafe static void BindToHandler(this IShellItem @this, IBindCtx pbc, in Guid bhid, in Guid riid, out object ppv)
	{
		fixed (Guid* riid2 = &riid)
		{
			fixed (Guid* bhid2 = &bhid)
			{
				@this.BindToHandler(pbc, bhid2, riid2, out ppv);
			}
		}
	}

	internal unsafe static void GetDisplayName(this IShellItem @this, SIGDN sigdnName, out PWSTR ppszName)
	{
		fixed (PWSTR* ppszName2 = &ppszName)
		{
			@this.GetDisplayName(sigdnName, ppszName2);
		}
	}

	internal unsafe static void GetAttributes(this IShellItem @this, SFGAO_FLAGS sfgaoMask, out SFGAO_FLAGS psfgaoAttribs)
	{
		fixed (SFGAO_FLAGS* psfgaoAttribs2 = &psfgaoAttribs)
		{
			@this.GetAttributes(sfgaoMask, psfgaoAttribs2);
		}
	}
}
