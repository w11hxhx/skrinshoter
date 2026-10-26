using System;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

public class FFmpegCodedFrame : CodedFrame
{
	internal unsafe AVBufferRef* buffer;

	public unsafe FFmpegCodedFrame(AVPacket* packet)
		: base(GetData(packet), GetTimestamp(packet), GetKeyFlag(packet))
	{
		if (packet->buf != null)
		{
			buffer = ffmpeg.av_buffer_ref(packet->buf);
		}
	}

	private unsafe static Memory<byte> GetData(AVPacket* packet)
	{
		if (packet == null)
		{
			throw new ArgumentNullException("packet");
		}
		return new UnmanagedMemoryManager<byte>(packet->data, packet->size).Memory;
	}

	private unsafe static FrameTimestamp GetTimestamp(AVPacket* packet)
	{
		return new FrameTimestamp(packet->pts, packet->time_base.den);
	}

	private unsafe static bool GetKeyFlag(AVPacket* packet)
	{
		return (packet->flags & 1) != 0;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		fixed (AVBufferRef** buf = &buffer)
		{
			ffmpeg.av_buffer_unref(buf);
		}
		buffer = null;
		base.Dispose(disposing);
	}
}
