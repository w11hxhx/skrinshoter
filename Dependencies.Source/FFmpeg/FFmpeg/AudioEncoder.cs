using System;
using System.Collections.Generic;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class AudioEncoder : BaseDisposable
{
	public static readonly IReadOnlyList<int> SupportedSampleRates = new List<int>
	{
		7350, 8000, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000,
		64000, 88200, 96000
	};

	private readonly ILogger logger;

	private readonly AudioFormat audioFormat;

	private unsafe AVCodecContext* encoder;

	private unsafe AVFrame* inputFrame;

	private unsafe AVPacket* outputPacket;

	public unsafe int InputFrameSize => encoder->frame_size;

	internal unsafe AVCodecContext* Encoder => encoder;

	public unsafe AudioEncoder(AudioFormat audioFormat, ILogger<AudioEncoder> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.audioFormat = audioFormat ?? throw new ArgumentNullException("audioFormat");
		if (audioFormat.SampleFormat != SampleFormat.FloatP)
		{
			throw new NotSupportedException($"AAC encoder only supports {SampleFormat.FloatP} sample format.");
		}
		try
		{
			CreateEncoder();
			inputFrame = ffmpeg.av_frame_alloc();
			if (inputFrame == null)
			{
				throw new FFmpegException("av_frame_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
			outputPacket = ffmpeg.av_packet_alloc();
			if (outputPacket == null)
			{
				throw new FFmpegException("av_packet_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public List<CodedFrame> EncodeFrame(AudioFrame frame)
	{
		Send(frame);
		List<CodedFrame> list = new List<CodedFrame>();
		CodedFrame frame2;
		while (TryReceive(out frame2))
		{
			list.Add(frame2);
		}
		return list;
	}

	private unsafe void CreateEncoder()
	{
		AVCodec* ptr = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_AAC);
		if (ptr == null)
		{
			throw new FFmpegException("avcodec_find_encoder(AAC)", ffmpeg.AVERROR_ENCODER_NOT_FOUND);
		}
		encoder = ffmpeg.avcodec_alloc_context3(ptr);
		if (encoder == null)
		{
			throw new FFmpegException("avcodec_alloc_context3()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		encoder->sample_rate = audioFormat.SampleRate;
		ffmpeg.av_channel_layout_default(&encoder->ch_layout, audioFormat.Channels);
		encoder->sample_fmt = audioFormat.SampleFormat.ToAVSampleFormat();
		encoder->time_base = new AVRational
		{
			num = 1,
			den = audioFormat.SampleRate
		};
		int num = ffmpeg.avcodec_open2(encoder, ptr, null);
		if (num < 0)
		{
			throw new FFmpegException("avcodec_open2(AAC)", num);
		}
	}

	private unsafe void Send(AudioFrame frame)
	{
		long timestamp = Stopwatch.GetTimestamp();
		if (frame == null)
		{
			int num = ffmpeg.avcodec_send_frame(encoder, null);
			if (num < 0)
			{
				throw new FFmpegException("avcodec_send_frame(null)", num);
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Sent EOF marker to audio encoder [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return;
		}
		if (frame.Format != audioFormat)
		{
			throw new InvalidOperationException($"Invalid audio frame format, expected: {audioFormat}, actual: {frame.Format}.");
		}
		using (frame.FillAVFrame(inputFrame))
		{
			int num2 = ffmpeg.avcodec_send_frame(encoder, inputFrame);
			if (num2 < 0)
			{
				throw new FFmpegException("avcodec_send_frame()", num2);
			}
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Sent {samples} samples to audio encoder: ts={ts} [time={time}ms]", frame.Samples, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
	}

	private unsafe bool TryReceive(out CodedFrame frame)
	{
		frame = null;
		long timestamp = Stopwatch.GetTimestamp();
		int num = ffmpeg.avcodec_receive_packet(encoder, outputPacket);
		if (num >= 0)
		{
			outputPacket->time_base = encoder->time_base;
			frame = new FFmpegCodedFrame(outputPacket);
			ffmpeg.av_packet_unref(outputPacket);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Received coded frame from audio encoder: size={size}, ts={ts} [time={time}ms]", frame.Data.Length, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			if (frame.Timestamp.Value == ffmpeg.AV_NOPTS_VALUE)
			{
				logger.LogWarning("Ignoring coded frame without timestamp from audio encoder: size={size}", frame.Data.Length);
				frame.Dispose();
				frame = null;
				return false;
			}
			return true;
		}
		if (num != ffmpeg.AVERROR(ffmpeg.EAGAIN) && num != ffmpeg.AVERROR_EOF)
		{
			throw new FFmpegException("avcodec_receive_packet()", num);
		}
		return false;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		fixed (AVCodecContext** avctx = &encoder)
		{
			ffmpeg.avcodec_free_context(avctx);
		}
		encoder = null;
		fixed (AVFrame** frame = &inputFrame)
		{
			ffmpeg.av_frame_free(frame);
		}
		inputFrame = null;
		fixed (AVPacket** pkt = &outputPacket)
		{
			ffmpeg.av_packet_free(pkt);
		}
		outputPacket = null;
	}
}
