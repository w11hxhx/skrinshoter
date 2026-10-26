using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint WM_KEYDOWN = 256u;

	internal const uint WM_KEYUP = 257u;

	internal const uint WM_SYSKEYDOWN = 260u;

	internal const uint WM_SYSKEYUP = 261u;

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL FreeLibrary(HMODULE hLibModule);

	[OverloadResolutionPriority(1)]
	internal unsafe static FreeLibrarySafeHandle LoadLibrary(string lpLibFileName)
	{
		fixed (char* ptr = lpLibFileName)
		{
			return new FreeLibrarySafeHandle(LoadLibrary(ptr));
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "LoadLibraryW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMODULE LoadLibrary(PCWSTR lpLibFileName);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL UnhookWindowsHookEx(HHOOK hhk);

	[OverloadResolutionPriority(1)]
	internal static UnhookWindowsHookExSafeHandle SetWindowsHookEx(WINDOWS_HOOK_ID idHook, HOOKPROC lpfn, SafeHandle hmod, uint dwThreadId)
	{
		bool success = false;
		try
		{
			HINSTANCE hmod2;
			if (hmod != null)
			{
				hmod.DangerousAddRef(ref success);
				hmod2 = (HINSTANCE)hmod.DangerousGetHandle();
			}
			else
			{
				hmod2 = (HINSTANCE)new IntPtr(0L);
			}
			return new UnhookWindowsHookExSafeHandle(SetWindowsHookEx(idHook, lpfn, hmod2, dwThreadId));
		}
		finally
		{
			if (success)
			{
				hmod.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "SetWindowsHookExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HHOOK SetWindowsHookEx(WINDOWS_HOOK_ID idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn, HINSTANCE hmod, uint dwThreadId);

	[OverloadResolutionPriority(1)]
	internal static LRESULT CallNextHookEx(SafeHandle hhk, int nCode, WPARAM wParam, LPARAM lParam)
	{
		bool success = false;
		try
		{
			HHOOK hhk2;
			if (hhk != null)
			{
				hhk.DangerousAddRef(ref success);
				hhk2 = (HHOOK)hhk.DangerousGetHandle();
			}
			else
			{
				hhk2 = (HHOOK)new IntPtr(0L);
			}
			return CallNextHookEx(hhk2, nCode, wParam, lParam);
		}
		finally
		{
			if (success)
			{
				hhk.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern LRESULT CallNextHookEx(HHOOK hhk, int nCode, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern short GetAsyncKeyState(int vKey);
}
