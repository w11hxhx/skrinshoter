using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint LLMHF_INJECTED = 1u;

	internal const uint WM_LBUTTONDOWN = 513u;

	internal const uint WM_LBUTTONUP = 514u;

	internal const uint WM_LBUTTONDBLCLK = 515u;

	internal const uint WM_RBUTTONDOWN = 516u;

	internal const uint WM_RBUTTONUP = 517u;

	internal const uint WM_RBUTTONDBLCLK = 518u;

	internal const uint WM_MBUTTONDOWN = 519u;

	internal const uint WM_MBUTTONUP = 520u;

	internal const uint WM_MBUTTONDBLCLK = 521u;

	internal const uint WM_XBUTTONDOWN = 523u;

	internal const uint WM_XBUTTONUP = 524u;

	internal const uint WM_XBUTTONDBLCLK = 525u;

	internal const uint WM_MOUSEWHEEL = 522u;

	internal const uint WM_MOUSEHWHEEL = 526u;

	internal const uint WM_QUIT = 18u;

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

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("KERNEL32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern uint GetCurrentThreadId();

	[DllImport("KERNEL32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern uint GetCurrentProcessId();

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

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL PeekMessage(out MSG lpMsg, HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax, PEEK_MESSAGE_REMOVE_TYPE wRemoveMsg)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return PeekMessage(lpMsg2, hWnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "PeekMessageW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL PeekMessage(MSG* lpMsg, HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax, PEEK_MESSAGE_REMOVE_TYPE wRemoveMsg);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetMessage(out MSG lpMsg, HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return GetMessage(lpMsg2, hWnd, wMsgFilterMin, wMsgFilterMax);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetMessageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetMessage(MSG* lpMsg, HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL TranslateMessage(in MSG lpMsg)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return TranslateMessage(lpMsg2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL TranslateMessage(MSG* lpMsg);

	[OverloadResolutionPriority(1)]
	internal unsafe static LRESULT DispatchMessage(in MSG lpMsg)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return DispatchMessage(lpMsg2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "DispatchMessageW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern LRESULT DispatchMessage(MSG* lpMsg);

	[DllImport("USER32.dll", EntryPoint = "PostThreadMessageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL PostThreadMessage(uint idThread, uint Msg, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern short GetAsyncKeyState(int vKey);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWND WindowFromPoint(Point Point);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern uint GetWindowThreadProcessId(HWND hWnd, [Optional] uint* lpdwProcessId);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMONITOR MonitorFromPoint(Point pt, MONITOR_FROM_FLAGS dwFlags);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetMonitorInfo(HMONITOR hMonitor, ref MONITORINFO lpmi)
	{
		fixed (MONITORINFO* lpmi2 = &lpmi)
		{
			return GetMonitorInfo(hMonitor, lpmi2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetMonitorInfoW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetMonitorInfo(HMONITOR hMonitor, MONITORINFO* lpmi);
}
