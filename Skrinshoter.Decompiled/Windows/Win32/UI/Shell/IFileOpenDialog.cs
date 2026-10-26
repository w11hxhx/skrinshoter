using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell.Common;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IFileOpenDialog : IFileDialog, IModalWindow
{
	[PreserveSig]
	new HRESULT Show(HWND hwndOwner);

	new unsafe void SetFileTypes(uint cFileTypes, COMDLG_FILTERSPEC* rgFilterSpec);

	new void SetFileTypeIndex(uint iFileType);

	new void GetFileTypeIndex(out uint piFileType);

	new void Advise(IFileDialogEvents pfde, out uint pdwCookie);

	new void Unadvise(uint dwCookie);

	new void SetOptions(FILEOPENDIALOGOPTIONS fos);

	new unsafe void GetOptions(FILEOPENDIALOGOPTIONS* pfos);

	new void SetDefaultFolder(IShellItem psi);

	new void SetFolder(IShellItem psi);

	new void GetFolder(out IShellItem ppsi);

	new void GetCurrentSelection(out IShellItem ppsi);

	new void SetFileName(PCWSTR pszName);

	new unsafe void GetFileName(PWSTR* pszName);

	new void SetTitle(PCWSTR pszTitle);

	new void SetOkButtonLabel(PCWSTR pszText);

	new void SetFileNameLabel(PCWSTR pszLabel);

	new void GetResult(out IShellItem ppsi);

	new void AddPlace(IShellItem psi, FDAP fdap);

	new void SetDefaultExtension(PCWSTR pszDefaultExtension);

	new void Close(HRESULT hr);

	new unsafe void SetClientGuid(Guid* guid);

	new void ClearClientData();

	new void SetFilter(IShellItemFilter pFilter);

	void GetResults(out IShellItemArray ppenum);

	void GetSelectedItems(out IShellItemArray ppsai);
}
