using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IEnumString_Extensions
{
	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT Next(this IEnumString @this, Span<PWSTR> rgelt, uint* pceltFetched)
	{
		fixed (PWSTR* rgelt2 = rgelt)
		{
			return @this.Next((uint)rgelt.Length, rgelt2, pceltFetched);
		}
	}
}
