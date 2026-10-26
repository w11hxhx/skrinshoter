using System;
using System.Collections.Generic;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

public class FFmpegAudioFrame : AudioFrame
{
	internal readonly List<UnmanagedPtr<AVBufferRef>> buffers = new List<UnmanagedPtr<AVBufferRef>>();

	public unsafe FFmpegAudioFrame(AVFrame* frame)
		: base(GetAudioFormat(frame), GetData(frame), GetTimestamp(frame))
	{
		for (uint num = 0u; num < 8 && frame->buf[num] != null; num++)
		{
			buffers.Add(new UnmanagedPtr<AVBufferRef>(ffmpeg.av_buffer_ref(frame->buf[num])));
		}
	}

	private unsafe static AudioFormat GetAudioFormat(AVFrame* frame)
	{
		if (frame == null)
		{
			throw new ArgumentNullException("frame");
		}
		return new AudioFormat(frame->sample_rate, frame->ch_layout.nb_channels, ((AVSampleFormat)frame->format).ToSampleFormat());
	}

	private unsafe static IReadOnlyList<Memory<byte>> GetData(AVFrame* frame)
	{
		List<Memory<byte>> list = new List<Memory<byte>>();
		int num = ffmpeg.av_get_bytes_per_sample((AVSampleFormat)frame->format);
		if (ffmpeg.av_sample_fmt_is_planar((AVSampleFormat)frame->format) == 0)
		{
			list.Add(new UnmanagedMemoryManager<byte>(frame->data[0u], frame->nb_samples * num * frame->ch_layout.nb_channels).Memory);
		}
		else
		{
			for (uint num2 = 0u; num2 < 8 && frame->data[num2] != null; num2++)
			{
				list.Add(new UnmanagedMemoryManager<byte>(frame->data[num2], frame->nb_samples * num).Memory);
			}
		}
		return list;
	}

	private unsafe static FrameTimestamp GetTimestamp(AVFrame* frame)
	{
		return new FrameTimestamp(frame->pts, frame->sample_rate);
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
