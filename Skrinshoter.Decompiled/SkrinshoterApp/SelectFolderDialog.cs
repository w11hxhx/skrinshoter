using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell;

namespace SkrinshoterApp;

public sealed class SelectFolderDialog
{
	private static readonly Guid CLSID_FileOpenDialog = new Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7");

	public unsafe string ShowDialog(Window owner, string initialPath = null, string title = null)
	{
		PInvoke.CoCreateInstance(in CLSID_FileOpenDialog, null, CLSCTX.CLSCTX_INPROC_SERVER, typeof(IFileOpenDialog).GUID, out var ppv).ThrowOnFailure("CoCreateInstance(CLSID_FileOpenDialog)");
		IFileOpenDialog fileOpenDialog = (IFileOpenDialog)ppv;
		try
		{
			fileOpenDialog.GetOptions(out var pfos);
			pfos |= FILEOPENDIALOGOPTIONS.FOS_PICKFOLDERS | FILEOPENDIALOGOPTIONS.FOS_FORCEFILESYSTEM | FILEOPENDIALOGOPTIONS.FOS_NOVALIDATE | FILEOPENDIALOGOPTIONS.FOS_DONTADDTORECENT;
			fileOpenDialog.SetOptions(pfos);
			if (!string.IsNullOrEmpty(title))
			{
				fileOpenDialog.SetTitle(title);
			}
			if (!string.IsNullOrEmpty(initialPath) && PInvoke.SHCreateItemFromParsingName(initialPath, null, typeof(IShellItem).GUID, out var ppv2).Succeeded && ppv2 != null)
			{
				fileOpenDialog.SetFolder((IShellItem)ppv2);
				Marshal.ReleaseComObject(ppv2);
			}
			HWND hwndOwner = ((owner != null) ? new HWND(new WindowInteropHelper(owner).Handle) : HWND.Null);
			HRESULT hRESULT = fileOpenDialog.Show(hwndOwner);
			if (hRESULT == PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_CANCELLED))
			{
				return null;
			}
			hRESULT.ThrowOnFailure("IFileOpenDialog.Show()");
			fileOpenDialog.GetResult(out var ppsi);
			try
			{
				ppsi.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out var ppszName);
				string result = ppszName.ToString();
				PInvoke.CoTaskMemFree((char*)ppszName);
				return result;
			}
			finally
			{
				Marshal.ReleaseComObject(ppsi);
			}
		}
		finally
		{
			Marshal.ReleaseComObject(fileOpenDialog);
		}
	}
}
