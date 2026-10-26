using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HDC CreateCompatibleDC(HDC hdc);

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DeleteDC(HDC hdc);

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DeleteObject(HGDIOBJ ho);

	[OverloadResolutionPriority(1)]
	internal static DeleteObjectSafeHandle CreateCompatibleBitmap_SafeHandle(HDC hdc, int cx, int cy)
	{
		return new DeleteObjectSafeHandle(CreateCompatibleBitmap(hdc, cx, cy));
	}

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HBITMAP CreateCompatibleBitmap(HDC hdc, int cx, int cy);

	[OverloadResolutionPriority(1)]
	internal static DeleteObjectSafeHandle SelectObject(HDC hdc, SafeHandle h)
	{
		bool success = false;
		try
		{
			if (h != null)
			{
				h.DangerousAddRef(ref success);
				HGDIOBJ h2 = (HGDIOBJ)h.DangerousGetHandle();
				return new DeleteObjectSafeHandle(SelectObject(hdc, h2));
			}
			throw new ArgumentNullException("h");
		}
		finally
		{
			if (success)
			{
				h.DangerousRelease();
			}
		}
	}

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HGDIOBJ SelectObject(HDC hdc, HGDIOBJ h);

	[DllImport("GDI32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL BitBlt(HDC hdc, int x, int y, int cx, int cy, HDC hdcSrc, int x1, int y1, ROP_CODE rop);

	[OverloadResolutionPriority(1)]
	internal unsafe static int GetDIBits(HDC hdc, SafeHandle hbm, uint start, uint cLines, void* lpvBits, BITMAPINFO* lpbmi, DIB_USAGE usage)
	{
		bool success = false;
		try
		{
			if (hbm != null)
			{
				hbm.DangerousAddRef(ref success);
				HBITMAP hbm2 = (HBITMAP)hbm.DangerousGetHandle();
				return GetDIBits(hdc, hbm2, start, cLines, lpvBits, lpbmi, usage);
			}
			throw new ArgumentNullException("hbm");
		}
		finally
		{
			if (success)
			{
				hbm.DangerousRelease();
			}
		}
	}

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern int GetDIBits(HDC hdc, HBITMAP hbm, uint start, uint cLines, [Optional] void* lpvBits, BITMAPINFO* lpbmi, DIB_USAGE usage);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HDC GetDC(HWND hWnd);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int ReleaseDC(HWND hWnd, HDC hDC);
}
