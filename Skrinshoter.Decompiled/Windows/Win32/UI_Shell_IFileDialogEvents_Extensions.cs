using System.CodeDom.Compiler;
using Windows.Win32.UI.Shell;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IFileDialogEvents_Extensions
{
	internal unsafe static void OnShareViolation(this IFileDialogEvents @this, IFileDialog pfd, IShellItem psi, out FDE_SHAREVIOLATION_RESPONSE pResponse)
	{
		fixed (FDE_SHAREVIOLATION_RESPONSE* pResponse2 = &pResponse)
		{
			@this.OnShareViolation(pfd, psi, pResponse2);
		}
	}

	internal unsafe static void OnOverwrite(this IFileDialogEvents @this, IFileDialog pfd, IShellItem psi, out FDE_OVERWRITE_RESPONSE pResponse)
	{
		fixed (FDE_OVERWRITE_RESPONSE* pResponse2 = &pResponse)
		{
			@this.OnOverwrite(pfd, psi, pResponse2);
		}
	}
}
