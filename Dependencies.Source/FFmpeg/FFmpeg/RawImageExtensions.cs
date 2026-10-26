using System;
using System.Buffers;
using System.Collections.Generic;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class RawImageExtensions
{
	public unsafe static IDisposable FillAVFrame(this RawImage image, AVFrame* avFrame)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (avFrame == null)
		{
			throw new ArgumentNullException("avFrame");
		}
		ffmpeg.av_frame_unref(avFrame);
		avFrame->width = image.Format.Width;
		avFrame->height = image.Format.Height;
		avFrame->format = (int)image.Format.PixelFormat.ToAVPixelFormat();
		List<MemoryHandle> list = new List<MemoryHandle>();
		for (int i = 0; i < Math.Min(image.Data.Count, 8); i++)
		{
			MemoryHandle item = image.Data[i].Pin();
			list.Add(item);
			avFrame->data[(uint)i] = (byte*)item.Pointer;
			avFrame->linesize[(uint)i] = image.Stride[i];
			if (image is FFmpegRawImage fFmpegRawImage && i < fFmpegRawImage.buffers.Count)
			{
				avFrame->buf[(uint)i] = ffmpeg.av_buffer_ref(fFmpegRawImage.buffers[i].Ptr);
			}
		}
		return new MemoryHandleCollection(list);
	}
}
