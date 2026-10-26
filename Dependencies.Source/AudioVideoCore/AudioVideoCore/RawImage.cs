using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AudioVideoCore;

public class RawImage : BaseDisposable
{
	public ImageFormat Format { get; }

	public IReadOnlyList<Memory<byte>> Data { get; protected set; }

	public IReadOnlyList<int> Stride { get; protected set; }

	public RawImage(ImageFormat format, IReadOnlyList<Memory<byte>> data, IReadOnlyList<int> stride)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (stride == null)
		{
			throw new ArgumentNullException("stride");
		}
		int planeCount = format.PixelFormat.GetPlaneCount();
		if (data.Count != planeCount)
		{
			throw new ArgumentException($"Invalid number of data planes for pixel format {format.PixelFormat}, expected: {planeCount}, actual: {data.Count}.", "data");
		}
		if (data.Any((Memory<byte> x) => x.Length == 0))
		{
			throw new ArgumentException("All data planes must have a non-zero size.", "data");
		}
		if (data.Count != stride.Count)
		{
			throw new ArgumentException("Data and stride lists must have the same size.", "stride");
		}
		if (stride.Any((int x) => x <= 0))
		{
			throw new ArgumentException("All stride values must be positive.", "stride");
		}
		for (int num = 0; num < data.Count; num++)
		{
			if (format.Height * stride[num] != data[num].Length)
			{
				throw new ArgumentException("The sizes of all data planes must be equal to the image height multiplied by the corresponding stride value.", "stride");
			}
		}
		Format = format;
		Data = data;
		Stride = stride;
	}

	public unsafe RawImage(ImageFormat format, void* data, int stride)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		int planeCount = format.PixelFormat.GetPlaneCount();
		if (planeCount != 1)
		{
			throw new ArgumentException($"Pixel format {format.PixelFormat} expects {planeCount} data planes but this constructor accepts only one data plane.", "format");
		}
		if (stride <= 0)
		{
			throw new ArgumentOutOfRangeException("stride", stride, "Stride must be positive.");
		}
		Format = format;
		Data = new List<Memory<byte>> { new UnmanagedMemoryManager<byte>((byte*)data, format.Height * stride).Memory };
		Stride = new List<int> { stride };
	}

	protected RawImage(ImageFormat format)
	{
		Format = format ?? throw new ArgumentNullException("format");
	}

	public unsafe static void CopyRegion(byte* srcData, int srcStride, byte* dstData, int dstStride, int rowSize, int rowCount)
	{
		if (srcData == null)
		{
			throw new ArgumentNullException("srcData");
		}
		if (dstData == null)
		{
			throw new ArgumentNullException("dstData");
		}
		if (srcStride == 0)
		{
			throw new ArgumentOutOfRangeException("srcStride", "Source stride cannot be zero.");
		}
		if (dstStride == 0)
		{
			throw new ArgumentOutOfRangeException("dstStride", "Destination stride cannot be zero.");
		}
		if (rowSize < 0)
		{
			throw new ArgumentOutOfRangeException("rowSize", "Row size cannot be negative.");
		}
		if (rowSize > Math.Abs(srcStride))
		{
			throw new ArgumentOutOfRangeException("rowSize", "Row size cannot be greater than source stride.");
		}
		if (rowSize > Math.Abs(dstStride))
		{
			throw new ArgumentOutOfRangeException("rowSize", "Row size cannot be greater than destination stride.");
		}
		if (rowCount < 0)
		{
			throw new ArgumentOutOfRangeException("rowCount", "Row count cannot be negative.");
		}
		if (rowSize != 0)
		{
			while (--rowCount >= 0)
			{
				Buffer.MemoryCopy(srcData, dstData, rowSize, rowSize);
				srcData += srcStride;
				dstData += dstStride;
			}
		}
	}
}
