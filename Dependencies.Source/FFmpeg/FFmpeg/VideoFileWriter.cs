using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class VideoFileWriter : BaseDisposable
{
	private readonly ILogger logger;

	private readonly string filename;

	private unsafe AVFormatContext* context;

	private bool fileOpened;

	private readonly List<long> lastTimestamp = new List<long>
	{
		long.MinValue,
		long.MinValue
	};

	private unsafe AVPacket* packet;

	public TimeSpan Duration { get; private set; }

	public unsafe VideoFileWriter(string filename, VideoEncoder videoEncoder, AudioEncoder audioEncoder, ILogger<VideoFileWriter> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.filename = filename ?? throw new ArgumentNullException("filename");
		if (videoEncoder == null)
		{
			throw new ArgumentNullException("videoEncoder");
		}
		try
		{
			CreateContext(videoEncoder, audioEncoder);
			packet = ffmpeg.av_packet_alloc();
			if (packet == null)
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

	public unsafe void WriteFrame(CodedFrame frame, int streamIndex)
	{
		if (frame == null)
		{
			throw new ArgumentNullException("frame");
		}
		switch (streamIndex)
		{
		default:
			throw new ArgumentOutOfRangeException("streamIndex");
		case 1:
			if (context->nb_streams < 2)
			{
				throw new InvalidOperationException("File has no audio stream.");
			}
			break;
		case 0:
			break;
		}
		using (frame.FillAVPacket(packet))
		{
			AVStream* ptr = context->streams[streamIndex];
			long timestamp = Stopwatch.GetTimestamp();
			Duration = TimeSpan.FromMilliseconds(ffmpeg.av_rescale_q(packet->pts, packet->time_base, new AVRational
			{
				num = 1,
				den = 1000
			}));
			ffmpeg.av_packet_rescale_ts(packet, packet->time_base, ptr->time_base);
			packet->time_base = ptr->time_base;
			packet->stream_index = streamIndex;
			if (packet->dts <= lastTimestamp[streamIndex])
			{
				packet->dts = lastTimestamp[streamIndex] + 1;
				packet->pts = packet->dts;
			}
			lastTimestamp[streamIndex] = packet->dts;
			int num = ffmpeg.av_write_frame(context, packet);
			if (num < 0)
			{
				throw new FFmpegException("av_write_frame()", num);
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Wrote {streamType} frame to file: size={size}, key={key}, ts={ts} [time={time}ms]", (ptr->codecpar->codec_type == AVMediaType.AVMEDIA_TYPE_VIDEO) ? "video" : "audio", frame.Data.Length, frame.IsKey, $"{packet->pts}/{packet->time_base.den}", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
		}
	}

	private unsafe void CreateContext(VideoEncoder videoEncoder, AudioEncoder audioEncoder)
	{
		fixed (AVFormatContext** ctx = &context)
		{
			int num = ffmpeg.avformat_alloc_output_context2(ctx, null, "mp4", null);
			if (num < 0)
			{
				throw new FFmpegException("avformat_alloc_output_context2(mp4)", num);
			}
		}
		AddVideoStream(videoEncoder);
		if (audioEncoder != null)
		{
			AddAudioStream(audioEncoder);
		}
		string directoryName = Path.GetDirectoryName(filename);
		if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		int num2 = ffmpeg.avio_open(&context->pb, filename, 2);
		if (num2 < 0)
		{
			throw new FFmpegException("avio_open()", num2);
		}
		AVDictionary* ptr = null;
		num2 = ffmpeg.avformat_write_header(context, &ptr);
		if (num2 < 0)
		{
			throw new FFmpegException("avformat_write_header()", num2);
		}
		fileOpened = true;
		logger.LogInformation("Created file \"{file}\"", filename);
		if (logger.IsEnabled(LogLevel.Debug))
		{
			for (uint num3 = 0u; num3 < context->nb_streams; num3++)
			{
				logger.LogDebug("File {streamType} stream timebase: {num}/{den}", (context->streams[num3]->codecpar->codec_type == AVMediaType.AVMEDIA_TYPE_VIDEO) ? "video" : "audio", context->streams[num3]->time_base.num, context->streams[num3]->time_base.den);
			}
		}
	}

	private unsafe void AddVideoStream(VideoEncoder videoEncoder)
	{
		AVStream* ptr = ffmpeg.avformat_new_stream(context, null);
		if (ptr == null)
		{
			throw new FFmpegException("avformat_new_stream()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		int num = ffmpeg.avcodec_parameters_from_context(ptr->codecpar, videoEncoder.Encoder);
		if (num < 0)
		{
			throw new FFmpegException("avcodec_parameters_from_context(video)", num);
		}
	}

	private unsafe void AddAudioStream(AudioEncoder audioEncoder)
	{
		AVStream* ptr = ffmpeg.avformat_new_stream(context, null);
		if (ptr == null)
		{
			throw new FFmpegException("avformat_new_stream()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		int num = ffmpeg.avcodec_parameters_from_context(ptr->codecpar, audioEncoder.Encoder);
		if (num < 0)
		{
			throw new FFmpegException("avcodec_parameters_from_context(audio)", num);
		}
	}

	private unsafe void CloseFile()
	{
		long num = ((lastTimestamp[0] == long.MinValue) ? 0 : ffmpeg.av_rescale_q(lastTimestamp[0], (*context->streams)->time_base, new AVRational
		{
			num = 1,
			den = 1000
		}));
		int num2 = ffmpeg.av_write_frame(context, null);
		if (num2 < 0)
		{
			throw new FFmpegException("av_write_frame(null)", num2);
		}
		num2 = ffmpeg.av_write_trailer(context);
		if (num2 < 0)
		{
			throw new FFmpegException("av_write_trailer()", num2);
		}
		num2 = ffmpeg.avio_closep(&context->pb);
		if (num2 < 0)
		{
			throw new FFmpegException("avio_closep()", num2);
		}
		fileOpened = false;
		logger.LogInformation("Closed file \"{file}\"", filename);
		try
		{
			logger.LogInformation("File size: {size} bytes", new FileInfo(filename).Length);
		}
		catch
		{
		}
		logger.LogInformation("File duration: {duration}", TimeSpan.FromMilliseconds(num));
	}

	protected unsafe override void Dispose(bool disposing)
	{
		if (context != null)
		{
			if (fileOpened)
			{
				try
				{
					CloseFile();
				}
				catch
				{
				}
			}
			ffmpeg.avformat_free_context(context);
			context = null;
		}
		fixed (AVPacket** pkt = &packet)
		{
			ffmpeg.av_packet_free(pkt);
		}
		packet = null;
	}
}
