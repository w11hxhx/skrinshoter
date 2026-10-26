using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common;

namespace AudioVideoCore;

public class UnmanagedRawImage : RawImage
{
	public unsafe UnmanagedRawImage(ImageFormat format)
		: base(format)
	{
		if (!format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {format.PixelFormat}.", "format");
		}
		try
		{
			int num = format.Width * 4;
			int num2 = format.Height * num;
			byte* pointer = (byte*)(void*)Marshal.AllocHGlobal(num2);
			Data = new List<Memory<byte>> { new UnmanagedMemoryManager<byte>(pointer, num2).Memory };
			Stride = new List<int> { num };
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe UnmanagedRawImage(ImageFormat format, void* srcData, int stride)
		: base(format)
	{
		if (!format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {format.PixelFormat}.", "format");
		}
		if (srcData == null)
		{
			throw new ArgumentNullException("srcData");
		}
		if (stride <= 0)
		{
			throw new ArgumentException("Stride value must be positive.", "stride");
		}
		try
		{
			int num = format.Height * stride;
			byte* ptr = (byte*)(void*)Marshal.AllocHGlobal(num);
			Buffer.MemoryCopy(srcData, ptr, num, num);
			Data = new List<Memory<byte>> { new UnmanagedMemoryManager<byte>(ptr, num).Memory };
			Stride = new List<int> { stride };
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	protected unsafe override void Dispose(bool disposing)
	{
		if (Data != null)
		{
			using (MemoryHandle memoryHandle = Data[0].Pin())
			{
				Marshal.FreeHGlobal((IntPtr)memoryHandle.Pointer);
			}
		}
	}
}
