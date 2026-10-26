using System;

namespace AudioVideoCore;

public static class PixelFormatExtensions
{
	public static int GetPlaneCount(this PixelFormat format)
	{
		switch (format)
		{
		case PixelFormat.BGRA:
		case PixelFormat.BGRX:
		case PixelFormat.YUYV422:
			return 1;
		case PixelFormat.NV12:
			return 2;
		case PixelFormat.YUV420P:
			return 3;
		default:
			throw new InvalidOperationException($"Unknown pixel format {typeof(PixelFormat)}.{format}.");
		}
	}
}
