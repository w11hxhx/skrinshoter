using System;
using System.Collections.Generic;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

public class FFmpegRawImage : RawImage
{
	internal readonly List<UnmanagedPtr<AVBufferRef>> buffers = new List<UnmanagedPtr<AVBufferRef>>();

	public unsafe FFmpegRawImage(AVFrame* frame)
		: base(GetImageFormat(frame), GetData(frame), GetStride(frame))
	{
		for (uint num = 0u; num < 8 && frame->buf[num] != null; num++)
		{
			buffers.Add(new UnmanagedPtr<AVBufferRef>(ffmpeg.av_buffer_ref(frame->buf[num])));
		}
	}

	private unsafe static ImageFormat GetImageFormat(AVFrame* frame)
	{
		if (frame == null)
		{
			throw new ArgumentNullException("frame");
		}
		return new ImageFormat(frame->width, frame->height, ((AVPixelFormat)frame->format).ToPixelFormat());
	}

	private unsafe static IReadOnlyList<Memory<byte>> GetData(AVFrame* frame)
	{
		List<Memory<byte>> list = new List<Memory<byte>>();
		for (uint num = 0u; num < 8 && frame->data[num] != null; num++)
		{
			list.Add(new UnmanagedMemoryManager<byte>(frame->data[num], frame->height * frame->linesize[num]).Memory);
		}
		return list;
	}

	private unsafe static IReadOnlyList<int> GetStride(AVFrame* frame)
	{
		List<int> list = new List<int>();
		for (uint num = 0u; num < 8 && frame->data[num] != null; num++)
		{
			list.Add(frame->linesize[num]);
		}
		return list;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		foreach (UnmanagedPtr<AVBufferRef> buffer in buffers)
		{
			AVBufferRef* ptr = buffer.Ptr;
			ffmpeg.av_buffer_unref(&ptr);
		}
		buffers.Clear();
		base.Dispose(disposing);
	}
}
