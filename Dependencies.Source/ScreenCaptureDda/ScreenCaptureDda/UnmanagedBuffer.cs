using System;
using System.Runtime.InteropServices;
using Common;

namespace ScreenCaptureDda;

internal class UnmanagedBuffer : BaseDisposable
{
	public unsafe byte* Ptr { get; private set; }

	public int Size { get; private set; }

	public unsafe void Enlarge(int newSize)
	{
		if (newSize <= 0)
		{
			throw new ArgumentOutOfRangeException("newSize", newSize, "New size must be positive.");
		}
		if (newSize > Size)
		{
			Free();
			Ptr = (byte*)(void*)Marshal.AllocHGlobal(newSize);
			Size = newSize;
		}
	}

	public unsafe void Free()
	{
		if (Ptr == null)
		{
			return;
		}
		try
		{
			Marshal.FreeHGlobal((IntPtr)Ptr);
		}
		catch
		{
		}
		finally
		{
			Ptr = null;
			Size = 0;
		}
	}

	protected override void Dispose(bool disposing)
	{
		Free();
	}
}
