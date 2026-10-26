using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.Common;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IFileOpenDialog_Extensions
{
	[OverloadResolutionPriority(1)]
	internal unsafe static void SetFileTypes(this IFileOpenDialog @this, ReadOnlySpan<COMDLG_FILTERSPEC> rgFilterSpec)
	{
		fixed (COMDLG_FILTERSPEC* rgFilterSpec2 = rgFilterSpec)
		{
			@this.SetFileTypes((uint)rgFilterSpec.Length, rgFilterSpec2);
		}
	}

	internal unsafe static void GetOptions(this IFileOpenDialog @this, out FILEOPENDIALOGOPTIONS pfos)
	{
		fixed (FILEOPENDIALOGOPTIONS* pfos2 = &pfos)
		{
			@this.GetOptions(pfos2);
		}
	}

	internal unsafe static void SetFileName(this IFileOpenDialog @this, string pszName)
	{
		fixed (char* ptr = pszName)
		{
			@this.SetFileName(ptr);
		}
	}

	internal unsafe static void GetFileName(this IFileOpenDialog @this, out PWSTR pszName)
	{
		fixed (PWSTR* pszName2 = &pszName)
		{
			@this.GetFileName(pszName2);
		}
	}

	internal unsafe static void SetTitle(this IFileOpenDialog @this, string pszTitle)
	{
		fixed (char* ptr = pszTitle)
		{
			@this.SetTitle(ptr);
		}
	}

	internal unsafe static void SetOkButtonLabel(this IFileOpenDialog @this, string pszText)
	{
		fixed (char* ptr = pszText)
		{
			@this.SetOkButtonLabel(ptr);
		}
	}

	internal unsafe static void SetFileNameLabel(this IFileOpenDialog @this, string pszLabel)
	{
		fixed (char* ptr = pszLabel)
		{
			@this.SetFileNameLabel(ptr);
		}
	}

	internal unsafe static void SetDefaultExtension(this IFileOpenDialog @this, string pszDefaultExtension)
	{
		fixed (char* ptr = pszDefaultExtension)
		{
			@this.SetDefaultExtension(ptr);
		}
	}

	internal unsafe static void SetClientGuid(this IFileOpenDialog @this, in Guid guid)
	{
		fixed (Guid* clientGuid = &guid)
		{
			@this.SetClientGuid(clientGuid);
		}
	}
}
