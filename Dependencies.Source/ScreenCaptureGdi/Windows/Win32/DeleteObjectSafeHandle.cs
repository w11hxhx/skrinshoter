using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal class DeleteObjectSafeHandle : SafeHandle
{
	private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1L);

	public override bool IsInvalid
	{
		get
		{
			if (handle.ToInt64() != -1)
			{
				return handle.ToInt64() == 0;
			}
			return true;
		}
	}

	internal DeleteObjectSafeHandle()
		: base(INVALID_HANDLE_VALUE, ownsHandle: true)
	{
	}

	internal DeleteObjectSafeHandle(IntPtr preexistingHandle, bool ownsHandle = true)
		: base(INVALID_HANDLE_VALUE, ownsHandle)
	{
		SetHandle(preexistingHandle);
	}

	protected override bool ReleaseHandle()
	{
		return PInvoke.DeleteObject((HGDIOBJ)handle);
	}
}
