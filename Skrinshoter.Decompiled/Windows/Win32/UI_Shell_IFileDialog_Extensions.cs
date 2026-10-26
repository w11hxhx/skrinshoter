using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.Common;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_IFileDialog_Extensions
{
	[OverloadResolutionPriority(1)]
	internal unsafe static void SetFileTypes(this IFileDialog @this, ReadOnlySpan<COMDLG_FILTERSPEC> rgFilterSpec)
	{
		fixed (COMDLG_FILTERSPEC* rgFilterSpec2 = rgFilterSpec)
		{
			@this.SetFileTypes((uint)rgFilterSpec.Length, rgFilterSpec2);
		}
	}

	internal unsafe static void GetOptions(this IFileDialog @this, out FILEOPENDIALOGOPTIONS pfos)
	{
		fixed (FILEOPENDIALOGOPTIONS* pfos2 = &pfos)
		{
			@this.GetOptions(pfos2);
		}
	}

	internal unsafe static void SetFileName(this IFileDialog @this, string pszName)
	{
		fixed (char* ptr = pszName)
		{
			@this.SetFileName(ptr);
		}
	}

	internal unsafe static void GetFileName(this IFileDialog @this, out PWSTR pszName)
	{
		fixed (PWSTR* pszName2 = &pszName)
		{
			@this.GetFileName(pszName2);
		}
	}

	internal unsafe static void SetTitle(this IFileDialog @this, string pszTitle)
	{
		fixed (char* ptr = pszTitle)
		{
			@this.SetTitle(ptr);
		}
	}

	internal unsafe static void SetOkButtonLabel(this IFileDialog @this, string pszText)
	{
		fixed (char* ptr = pszText)
		{
			@this.SetOkButtonLabel(ptr);
		}
	}

	internal unsafe static void SetFileNameLabel(this IFileDialog @this, string pszLabel)
	{
		fixed (char* ptr = pszLabel)
		{
			@this.SetFileNameLabel(ptr);
		}
	}

	internal unsafe static void SetDefaultExtension(this IFileDialog @this, string pszDefaultExtension)
	{
		fixed (char* ptr = pszDefaultExtension)
		{
			@this.SetDefaultExtension(ptr);
		}
	}

	internal unsafe static void SetClientGuid(this IFileDialog @this, in Guid guid)
	{
		fixed (Guid* clientGuid = &guid)
		{
			@this.SetClientGuid(clientGuid);
		}
	}
}
