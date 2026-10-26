using System;
using System.Buffers;
using AudioVideoCore;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class CodedFrameExtensions
{
	public unsafe static IDisposable FillAVPacket(this CodedFrame codedFrame, AVPacket* avPacket)
	{
		if (codedFrame == null)
		{
			throw new ArgumentNullException("codedFrame");
		}
		if (avPacket == null)
		{
			throw new ArgumentNullException("avPacket");
		}
		ffmpeg.av_packet_unref(avPacket);
		avPacket->pts = codedFrame.Timestamp.Value;
		avPacket->dts = avPacket->pts;
		avPacket->time_base = new AVRational
		{
			num = 1,
			den = (int)codedFrame.Timestamp.TimeBase
		};
		if (codedFrame.IsKey)
		{
			avPacket->flags |= 1;
		}
		MemoryHandle memoryHandle = codedFrame.Data.Pin();
		avPacket->data = (byte*)memoryHandle.Pointer;
		avPacket->size = codedFrame.Data.Length;
		if (codedFrame is FFmpegCodedFrame fFmpegCodedFrame && fFmpegCodedFrame.buffer != null)
		{
			avPacket->buf = ffmpeg.av_buffer_ref(fFmpegCodedFrame.buffer);
		}
		return memoryHandle;
	}
}
