using System;
using AudioVideoCore;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class PixelFormatConversions
{
	public static AVPixelFormat ToAVPixelFormat(this PixelFormat pixelFormat)
	{
		return pixelFormat switch
		{
			PixelFormat.None => AVPixelFormat.AV_PIX_FMT_NONE, 
			PixelFormat.BGRA => AVPixelFormat.AV_PIX_FMT_BGRA, 
			PixelFormat.BGRX => AVPixelFormat.AV_PIX_FMT_BGR0, 
			PixelFormat.YUYV422 => AVPixelFormat.AV_PIX_FMT_YUYV422, 
			PixelFormat.NV12 => AVPixelFormat.AV_PIX_FMT_NV12, 
			PixelFormat.YUV420P => AVPixelFormat.AV_PIX_FMT_YUV420P, 
			_ => throw new InvalidOperationException($"Cannot convert {typeof(PixelFormat)}.{pixelFormat} to {typeof(AVPixelFormat)}."), 
		};
	}

	public static PixelFormat ToPixelFormat(this AVPixelFormat pixelFormat)
	{
		return pixelFormat switch
		{
			AVPixelFormat.AV_PIX_FMT_NONE => PixelFormat.None, 
			AVPixelFormat.AV_PIX_FMT_BGRA => PixelFormat.BGRA, 
			AVPixelFormat.AV_PIX_FMT_BGR0 => PixelFormat.BGRX, 
			AVPixelFormat.AV_PIX_FMT_YUYV422 => PixelFormat.YUYV422, 
			AVPixelFormat.AV_PIX_FMT_NV12 => PixelFormat.NV12, 
			AVPixelFormat.AV_PIX_FMT_YUV420P => PixelFormat.YUV420P, 
			_ => throw new InvalidOperationException($"Cannot convert {typeof(AVPixelFormat)}.{pixelFormat} to {typeof(PixelFormat)}."), 
		};
	}
}
