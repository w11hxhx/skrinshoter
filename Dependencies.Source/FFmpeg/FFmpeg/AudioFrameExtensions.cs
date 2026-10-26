using System;
using System.Buffers;
using System.Collections.Generic;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class AudioFrameExtensions
{
	public unsafe static IDisposable FillAVFrame(this AudioFrame audioFrame, AVFrame* avFrame)
	{
		if (audioFrame == null)
		{
			throw new ArgumentNullException("audioFrame");
		}
		if (avFrame == null)
		{
			throw new ArgumentNullException("avFrame");
		}
		ffmpeg.av_frame_unref(avFrame);
		avFrame->sample_rate = audioFrame.Format.SampleRate;
		avFrame->ch_layout.nb_channels = audioFrame.Format.Channels;
		avFrame->format = (int)audioFrame.Format.SampleFormat.ToAVSampleFormat();
		avFrame->nb_samples = audioFrame.Samples;
		avFrame->pts = audioFrame.Timestamp.Value;
		avFrame->time_base = new AVRational
		{
			num = 1,
			den = (int)audioFrame.Timestamp.TimeBase
		};
		List<MemoryHandle> list = new List<MemoryHandle>();
		for (int i = 0; i < Math.Min(audioFrame.Data.Count, 8); i++)
		{
			MemoryHandle item = audioFrame.Data[i].Pin();
			list.Add(item);
			avFrame->data[(uint)i] = (byte*)item.Pointer;
			if (audioFrame is FFmpegAudioFrame fFmpegAudioFrame && i < fFmpegAudioFrame.buffers.Count)
			{
				avFrame->buf[(uint)i] = ffmpeg.av_buffer_ref(fFmpegAudioFrame.buffers[i].Ptr);
			}
		}
		return new MemoryHandleCollection(list);
	}
}
