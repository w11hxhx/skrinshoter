using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IMoniker_Extensions
{
	internal unsafe static void GetClassID(this Windows.Win32.System.Com.IMoniker @this, out Guid pClassID)
	{
		fixed (Guid* pClassID2 = &pClassID)
		{
			@this.GetClassID(pClassID2);
		}
	}

	internal unsafe static void BindToObject(this Windows.Win32.System.Com.IMoniker @this, Windows.Win32.System.Com.IBindCtx pbc, Windows.Win32.System.Com.IMoniker pmkToLeft, in Guid riidResult, out object ppvResult)
	{
		fixed (Guid* riidResult2 = &riidResult)
		{
			@this.BindToObject(pbc, pmkToLeft, riidResult2, out ppvResult);
		}
	}

	internal unsafe static void BindToStorage(this Windows.Win32.System.Com.IMoniker @this, Windows.Win32.System.Com.IBindCtx pbc, Windows.Win32.System.Com.IMoniker pmkToLeft, in Guid riid, out object ppvObj)
	{
		fixed (Guid* riid2 = &riid)
		{
			@this.BindToStorage(pbc, pmkToLeft, riid2, out ppvObj);
		}
	}

	internal unsafe static void GetTimeOfLastChange(this Windows.Win32.System.Com.IMoniker @this, Windows.Win32.System.Com.IBindCtx pbc, Windows.Win32.System.Com.IMoniker pmkToLeft, out FILETIME pFileTime)
	{
		fixed (FILETIME* pFileTime2 = &pFileTime)
		{
			@this.GetTimeOfLastChange(pbc, pmkToLeft, pFileTime2);
		}
	}

	internal unsafe static void GetDisplayName(this Windows.Win32.System.Com.IMoniker @this, Windows.Win32.System.Com.IBindCtx pbc, Windows.Win32.System.Com.IMoniker pmkToLeft, out PWSTR ppszDisplayName)
	{
		fixed (PWSTR* ppszDisplayName2 = &ppszDisplayName)
		{
			@this.GetDisplayName(pbc, pmkToLeft, ppszDisplayName2);
		}
	}
}
