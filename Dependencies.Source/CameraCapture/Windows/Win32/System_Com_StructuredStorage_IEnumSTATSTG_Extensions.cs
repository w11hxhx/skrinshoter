using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_StructuredStorage_IEnumSTATSTG_Extensions
{
	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT Next(this IEnumSTATSTG.Interface @this, Span<STATSTG> rgelt, uint* pceltFetched)
	{
		fixed (STATSTG* rgelt2 = rgelt)
		{
			return @this.Next((uint)rgelt.Length, rgelt2, pceltFetched);
		}
	}
}
