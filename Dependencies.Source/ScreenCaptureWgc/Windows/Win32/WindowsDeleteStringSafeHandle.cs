using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.WinRT;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal class WindowsDeleteStringSafeHandle : SafeHandle
{
	private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(0L);

	public override bool IsInvalid => false;

	internal WindowsDeleteStringSafeHandle()
		: base(INVALID_HANDLE_VALUE, ownsHandle: true)
	{
	}

	internal WindowsDeleteStringSafeHandle(IntPtr preexistingHandle, bool ownsHandle = true)
		: base(INVALID_HANDLE_VALUE, ownsHandle)
	{
		SetHandle(preexistingHandle);
	}

	protected override bool ReleaseHandle()
	{
		return PInvoke.WindowsDeleteString((HSTRING)handle) == HRESULT.S_OK;
	}
}
