using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IFileDialogEvents
{
	void OnFileOk(IFileDialog pfd);

	void OnFolderChanging(IFileDialog pfd, IShellItem psiFolder);

	void OnFolderChange(IFileDialog pfd);

	void OnSelectionChange(IFileDialog pfd);

	unsafe void OnShareViolation(IFileDialog pfd, IShellItem psi, FDE_SHAREVIOLATION_RESPONSE* pResponse);

	void OnTypeChange(IFileDialog pfd);

	unsafe void OnOverwrite(IFileDialog pfd, IShellItem psi, FDE_OVERWRITE_RESPONSE* pResponse);
}
