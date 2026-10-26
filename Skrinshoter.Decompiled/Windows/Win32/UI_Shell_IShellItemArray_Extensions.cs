using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.SystemServices;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IShellItemArray_Extensions
{
	internal unsafe static void BindToHandler(this IShellItemArray @this, IBindCtx pbc, in Guid bhid, in Guid riid, out object ppvOut)
	{
		fixed (Guid* riid2 = &riid)
		{
			fixed (Guid* bhid2 = &bhid)
			{
				@this.BindToHandler(pbc, bhid2, riid2, out ppvOut);
			}
		}
	}

	internal unsafe static void GetPropertyStore(this IShellItemArray @this, GETPROPERTYSTOREFLAGS flags, in Guid riid, out object ppv)
	{
		fixed (Guid* riid2 = &riid)
		{
			@this.GetPropertyStore(flags, riid2, out ppv);
		}
	}

	internal unsafe static void GetPropertyDescriptionList(this IShellItemArray @this, in PROPERTYKEY keyType, in Guid riid, out object ppv)
	{
		fixed (Guid* riid2 = &riid)
		{
			fixed (PROPERTYKEY* keyType2 = &keyType)
			{
				@this.GetPropertyDescriptionList(keyType2, riid2, out ppv);
			}
		}
	}

	internal unsafe static void GetAttributes(this IShellItemArray @this, SIATTRIBFLAGS AttribFlags, SFGAO_FLAGS sfgaoMask, out SFGAO_FLAGS psfgaoAttribs)
	{
		fixed (SFGAO_FLAGS* psfgaoAttribs2 = &psfgaoAttribs)
		{
			@this.GetAttributes(AttribFlags, sfgaoMask, psfgaoAttribs2);
		}
	}
}
