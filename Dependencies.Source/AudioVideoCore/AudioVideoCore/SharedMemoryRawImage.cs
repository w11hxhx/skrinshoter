using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using Common;

namespace AudioVideoCore;

public class SharedMemoryRawImage : RawImage
{
	private readonly MemoryMappedFile memoryMappedFile;

	private readonly MemoryMappedViewAccessor memoryMappedViewAccessor;

	public string SharedMemoryName { get; }

	public unsafe SharedMemoryRawImage(ImageFormat format)
		: base(format)
	{
		if (!format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {format.PixelFormat}.", "format");
		}
		try
		{
			SharedMemoryName = "SharedMemoryRawImage_" + Guid.NewGuid().ToString();
			int num = format.Width * 4;
			int num2 = num * format.Height;
			memoryMappedFile = MemoryMappedFile.CreateNew(SharedMemoryName, num2);
			memoryMappedViewAccessor = memoryMappedFile.CreateViewAccessor();
			byte* pointer = null;
			memoryMappedViewAccessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
			Data = new List<Memory<byte>> { new UnmanagedMemoryManager<byte>(pointer, num2).Memory };
			Stride = new List<int> { num };
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe SharedMemoryRawImage(ImageFormat format, string sharedMemoryName)
		: base(format)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (!format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {format.PixelFormat}.", "format");
		}
		if (sharedMemoryName == null)
		{
			throw new ArgumentNullException("sharedMemoryName");
		}
		try
		{
			SharedMemoryName = sharedMemoryName;
			int num = format.Width * 4;
			int num2 = num * format.Height;
			memoryMappedFile = MemoryMappedFile.OpenExisting(SharedMemoryName);
			memoryMappedViewAccessor = memoryMappedFile.CreateViewAccessor();
			if (memoryMappedViewAccessor.Capacity < num2)
			{
				throw new InvalidOperationException("Shared memory size is less than image size.");
			}
			byte* pointer = null;
			memoryMappedViewAccessor.SafeMemoryMappedViewHandle.AcquirePointer(ref pointer);
			Data = new List<Memory<byte>> { new UnmanagedMemoryManager<byte>(pointer, num2).Memory };
			Stride = new List<int> { num };
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public IntPtr GetHandle()
	{
		return memoryMappedFile.SafeMemoryMappedFileHandle.DangerousGetHandle();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (Data != null)
			{
				memoryMappedViewAccessor.SafeMemoryMappedViewHandle.ReleasePointer();
			}
			memoryMappedViewAccessor?.Dispose();
			memoryMappedFile?.Dispose();
		}
	}
}
