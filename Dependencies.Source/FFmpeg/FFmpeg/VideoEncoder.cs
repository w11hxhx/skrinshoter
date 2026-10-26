using System;
using System.Collections.Generic;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class VideoEncoder : BaseDisposable
{
	private readonly ILogger logger;

	private readonly ImageFormat imageFormat;

	private unsafe AVCodecContext* encoder;

	private unsafe AVFrame* inputFrame;

	private unsafe AVPacket* outputPacket;

	internal unsafe AVCodecContext* Encoder => encoder;

	public unsafe VideoEncoder(ImageFormat imageFormat, int frameRate, ILogger<VideoEncoder> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.imageFormat = imageFormat ?? throw new ArgumentNullException("imageFormat");
		if (frameRate <= 0)
		{
			throw new ArgumentOutOfRangeException("frameRate");
		}
		try
		{
			CreateEncoder(frameRate);
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

	public List<CodedFrame> EncodeFrame(VideoFrame frame)
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

	private unsafe void CreateEncoder(int frameRate)
	{
		AVCodec* ptr = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_H264);
		if (ptr == null)
		{
			throw new FFmpegException("avcodec_find_encoder(H264)", ffmpeg.AVERROR_ENCODER_NOT_FOUND);
		}
		encoder = ffmpeg.avcodec_alloc_context3(ptr);
		if (encoder == null)
		{
			throw new FFmpegException("avcodec_alloc_context3()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		encoder->width = imageFormat.Width;
		encoder->height = imageFormat.Height;
		encoder->pix_fmt = imageFormat.PixelFormat.ToAVPixelFormat();
		encoder->gop_size = frameRate;
		encoder->time_base = new AVRational
		{
			num = 1,
			den = frameRate
		};
		encoder->profile = 66;
		int num = ffmpeg.av_opt_set(encoder->priv_data, "tune", "zerolatency", 0);
		if (num < 0)
		{
			throw new FFmpegException("av_opt_set(tune)", num);
		}
		num = ffmpeg.av_opt_set(encoder->priv_data, "preset", "superfast", 0);
		if (num < 0)
		{
			throw new FFmpegException("av_opt_set(preset)", num);
		}
		int num2 = ffmpeg.avcodec_open2(encoder, ptr, null);
		if (num2 < 0)
		{
			throw new FFmpegException("avcodec_open2(H264)", num2);
		}
	}

	private unsafe void Send(VideoFrame frame)
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
				logger.LogDebug("Sent EOF marker to video encoder [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return;
		}
		if (frame.Image.Format != imageFormat)
		{
			throw new InvalidOperationException($"Invalid image format, expected: {imageFormat}, actual: {frame.Image.Format}.");
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
			logger.LogDebug("Sent frame {width}x{height} to video encoder: ts={ts} [time={time}ms]", frame.Image.Format.Width, frame.Image.Format.Height, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
	}

	private unsafe bool TryReceive(out CodedFrame frame)
	{
		frame = null;
		long timestamp = Stopwatch.GetTimestamp();
		int num = ffmpeg.avcodec_receive_packet(encoder, outputPacket);
		if (num >= 0)
		{
			outputPacket->time_base = new AVRational
			{
				num = 1,
				den = 1000000
			};
			frame = new FFmpegCodedFrame(outputPacket);
			ffmpeg.av_packet_unref(outputPacket);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Received coded frame from video encoder: size={size}, key={key}, ts={ts} [time={time}ms]", frame.Data.Length, frame.IsKey ? '1' : '0', frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
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
