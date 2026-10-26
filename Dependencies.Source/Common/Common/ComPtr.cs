using System;
using System.Runtime.InteropServices;

namespace Common;

public struct ComPtr<T> : IDisposable where T : unmanaged
{
	public unsafe T* Ptr { get; private set; }

	public unsafe ComPtr(T* ptr)
	{
		if (ptr == null)
		{
			throw new ArgumentNullException("ptr");
		}
		Ptr = ptr;
	}

	public unsafe static implicit operator ComPtr<T>(T* ptr)
	{
		return new ComPtr<T>(ptr);
	}

	public unsafe static implicit operator T*(ComPtr<T> comPtr)
	{
		return comPtr.Ptr;
	}

	public unsafe void Dispose()
	{
		if (Ptr != null)
		{
			Marshal.Release((IntPtr)Ptr);
			Ptr = null;
		}
		GC.SuppressFinalize(this);
	}
}
