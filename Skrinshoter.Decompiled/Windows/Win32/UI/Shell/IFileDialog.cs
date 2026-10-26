using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell.Common;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("42F85136-DB7E-439C-85F1-E4075D135FC8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IFileDialog : IModalWindow
{
	new void Show(HWND hwndOwner);

	unsafe void SetFileTypes(uint cFileTypes, COMDLG_FILTERSPEC* rgFilterSpec);

	void SetFileTypeIndex(uint iFileType);

	void GetFileTypeIndex(out uint piFileType);

	void Advise(IFileDialogEvents pfde, out uint pdwCookie);

	void Unadvise(uint dwCookie);

	void SetOptions(FILEOPENDIALOGOPTIONS fos);

	unsafe void GetOptions(FILEOPENDIALOGOPTIONS* pfos);

	void SetDefaultFolder(IShellItem psi);

	void SetFolder(IShellItem psi);

	void GetFolder(out IShellItem ppsi);

	void GetCurrentSelection(out IShellItem ppsi);

	void SetFileName(PCWSTR pszName);

	unsafe void GetFileName(PWSTR* pszName);

	void SetTitle(PCWSTR pszTitle);

	void SetOkButtonLabel(PCWSTR pszText);

	void SetFileNameLabel(PCWSTR pszLabel);

	void GetResult(out IShellItem ppsi);

	void AddPlace(IShellItem psi, FDAP fdap);

	void SetDefaultExtension(PCWSTR pszDefaultExtension);

	void Close(HRESULT hr);

	unsafe void SetClientGuid(Guid* guid);

	void ClearClientData();

	void SetFilter(IShellItemFilter pFilter);
}
