using System;
using AudioVideoCore;
using Windows.Win32;

namespace CameraCapture;

public static class PixelFormatConversions
{
	public static Guid ToGuid(this PixelFormat pixelFormat)
	{
		return pixelFormat switch
		{
			PixelFormat.None => PInvoke.MFVideoFormat_Base, 
			PixelFormat.BGRA => PInvoke.MFVideoFormat_ARGB32, 
			PixelFormat.BGRX => PInvoke.MFVideoFormat_RGB32, 
			PixelFormat.YUYV422 => PInvoke.MFVideoFormat_YUY2, 
			PixelFormat.NV12 => PInvoke.MFVideoFormat_NV12, 
			_ => throw new InvalidOperationException($"Cannot convert {typeof(PixelFormat)}.{pixelFormat} to {typeof(Guid)}."), 
		};
	}

	public static PixelFormat ToPixelFormat(this Guid pixelFormat)
	{
		if (pixelFormat == PInvoke.MFVideoFormat_ARGB32)
		{
			return PixelFormat.BGRA;
		}
		if (pixelFormat == PInvoke.MFVideoFormat_RGB32)
		{
			return PixelFormat.BGRX;
		}
		if (pixelFormat == PInvoke.MFVideoFormat_YUY2)
		{
			return PixelFormat.YUYV422;
		}
		if (pixelFormat == PInvoke.MFVideoFormat_NV12)
		{
			return PixelFormat.NV12;
		}
		return PixelFormat.None;
	}
}
