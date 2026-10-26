using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DeleteObject(HGDIOBJ ho);

	[OverloadResolutionPriority(1)]
	internal unsafe static int GetObject(SafeHandle h, int c, void* pv)
	{
		bool success = false;
		try
		{
			if (h != null)
			{
				h.DangerousAddRef(ref success);
				HGDIOBJ h2 = (HGDIOBJ)h.DangerousGetHandle();
				return GetObject(h2, c, pv);
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

	[DllImport("GDI32.dll", EntryPoint = "GetObjectW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern int GetObject(HGDIOBJ h, int c, [Optional] void* pv);

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

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL FreeLibrary(HMODULE hLibModule);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyCursor(HCURSOR hCursor);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyIcon(HICON hIcon);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetCursorInfo(ref CURSORINFO pci)
	{
		fixed (CURSORINFO* pci2 = &pci)
		{
			return GetCursorInfo(pci2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetCursorInfo(CURSORINFO* pci);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetIconInfo(SafeHandle hIcon, out ICONINFO piconinfo)
	{
		bool success = false;
		try
		{
			fixed (ICONINFO* piconinfo2 = &piconinfo)
			{
				if (hIcon != null)
				{
					hIcon.DangerousAddRef(ref success);
					HICON hIcon2 = (HICON)hIcon.DangerousGetHandle();
					return GetIconInfo(hIcon2, piconinfo2);
				}
				throw new ArgumentNullException("hIcon");
			}
		}
		finally
		{
			if (success)
			{
				hIcon.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetIconInfo(HICON hIcon, ICONINFO* piconinfo);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetIconInfoEx(SafeHandle hicon, ref ICONINFOEXW piconinfo)
	{
		bool success = false;
		try
		{
			fixed (ICONINFOEXW* piconinfo2 = &piconinfo)
			{
				if (hicon != null)
				{
					hicon.DangerousAddRef(ref success);
					HICON hicon2 = (HICON)hicon.DangerousGetHandle();
					return GetIconInfoEx(hicon2, piconinfo2);
				}
				throw new ArgumentNullException("hicon");
			}
		}
		finally
		{
			if (success)
			{
				hicon.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetIconInfoExW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetIconInfoEx(HICON hicon, ICONINFOEXW* piconinfo);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMONITOR MonitorFromPoint(Point pt, MONITOR_FROM_FLAGS dwFlags);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HDC GetDC(HWND hWnd);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int ReleaseDC(HWND hWnd, HDC hDC);

	[OverloadResolutionPriority(1)]
	internal unsafe static SafeFileHandle LoadImage(SafeHandle hInst, string name, GDI_IMAGE_TYPE type, int cx, int cy, IMAGE_FLAGS fuLoad)
	{
		bool success = false;
		try
		{
			fixed (char* ptr = name)
			{
				HINSTANCE hInst2;
				if (hInst != null)
				{
					hInst.DangerousAddRef(ref success);
					hInst2 = (HINSTANCE)hInst.DangerousGetHandle();
				}
				else
				{
					hInst2 = (HINSTANCE)new IntPtr(0L);
				}
				return new SafeFileHandle(LoadImage(hInst2, ptr, type, cx, cy, fuLoad), ownsHandle: true);
			}
		}
		finally
		{
			if (success)
			{
				hInst.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "LoadImageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HANDLE LoadImage(HINSTANCE hInst, PCWSTR name, GDI_IMAGE_TYPE type, int cx, int cy, IMAGE_FLAGS fuLoad);

	[OverloadResolutionPriority(1)]
	internal static SafeFileHandle CopyImage(SafeHandle h, GDI_IMAGE_TYPE type, int cx, int cy, IMAGE_FLAGS flags)
	{
		bool success = false;
		try
		{
			if (h != null)
			{
				h.DangerousAddRef(ref success);
				HANDLE h2 = (HANDLE)h.DangerousGetHandle();
				return new SafeFileHandle(CopyImage(h2, type, cx, cy, flags), ownsHandle: true);
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

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HANDLE CopyImage(HANDLE h, GDI_IMAGE_TYPE type, int cx, int cy, IMAGE_FLAGS flags);

	[OverloadResolutionPriority(1)]
	internal static DestroyIconSafeHandle CopyIcon(SafeHandle hIcon)
	{
		bool success = false;
		try
		{
			if (hIcon != null)
			{
				hIcon.DangerousAddRef(ref success);
				HICON hIcon2 = (HICON)hIcon.DangerousGetHandle();
				return new DestroyIconSafeHandle(CopyIcon(hIcon2));
			}
			throw new ArgumentNullException("hIcon");
		}
		finally
		{
			if (success)
			{
				hIcon.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HICON CopyIcon(HICON hIcon);
}
