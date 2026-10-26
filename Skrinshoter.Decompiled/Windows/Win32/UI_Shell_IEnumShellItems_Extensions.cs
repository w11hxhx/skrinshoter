using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.UI.Shell;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IEnumShellItems_Extensions
{
	[OverloadResolutionPriority(1)]
	internal unsafe static void Next(this IEnumShellItems @this, IShellItem[] rgelt, uint* pceltFetched)
	{
		@this.Next((uint)(rgelt?.Length ?? 0), rgelt, pceltFetched);
	}
}
