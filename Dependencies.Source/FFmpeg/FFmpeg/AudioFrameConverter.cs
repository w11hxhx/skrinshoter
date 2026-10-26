using System;
using System.Collections.Generic;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class AudioFrameConverter : BaseDisposable
{
	private readonly ILogger logger;

	private unsafe AVFilterGraph* graph;

	private unsafe AVFilterContext* graphInput;

	private unsafe AVFilterContext* graphOutput;

	private unsafe AVFrame* inputFrame;

	private unsafe AVFrame* outputFrame;

	private readonly int? outputFrameSize;

	public AudioFormat InputFormat { get; }

	public AudioFormat OutputFormat { get; }

	public unsafe AudioFrameConverter(AudioFormat inputFormat, AudioFormat outputFormat, ILogger logger = null, int? outputFrameSize = null)
	{
		this.logger = logger ?? NullLogger.Instance;
		InputFormat = inputFormat ?? throw new ArgumentNullException("inputFormat");
		OutputFormat = outputFormat ?? throw new ArgumentNullException("outputFormat");
		if (outputFrameSize <= 0)
		{
			throw new ArgumentOutOfRangeException("outputFrameSize");
		}
		this.outputFrameSize = outputFrameSize;
		try
		{
			CreateFilterGraph();
			inputFrame = ffmpeg.av_frame_alloc();
			if (inputFrame == null)
			{
				throw new FFmpegException("av_frame_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
			outputFrame = ffmpeg.av_frame_alloc();
			if (outputFrame == null)
			{
				throw new FFmpegException("av_frame_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public List<AudioFrame> ProcessFrame(AudioFrame frame)
	{
		Send(frame);
		List<AudioFrame> list = new List<AudioFrame>();
		AudioFrame frame2;
		while (TryReceive(out frame2))
		{
			list.Add(frame2);
		}
		return list;
	}

	private unsafe void Send(AudioFrame frame)
	{
		long timestamp = Stopwatch.GetTimestamp();
		if (frame == null)
		{
			int num = ffmpeg.av_buffersrc_write_frame(graphInput, null);
			if (num < 0)
			{
				throw new FFmpegException("av_buffersrc_write_frame(null)", num);
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Sent EOF marker to audio frame converter [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return;
		}
		if (frame.Format != InputFormat)
		{
			throw new InvalidOperationException($"Invalid audio frame format, expected: {InputFormat}, actual: {frame.Format}.");
		}
		using (frame.FillAVFrame(inputFrame))
		{
			int num2 = ffmpeg.av_buffersrc_write_frame(graphInput, inputFrame);
			if (num2 < 0)
			{
				throw new FFmpegException("av_buffersrc_write_frame()", num2);
			}
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Sent {samples} samples to audio frame converter: ts={ts} [time={time}ms]", frame.Samples, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
	}

	private unsafe bool TryReceive(out AudioFrame frame)
	{
		frame = null;
		long timestamp = Stopwatch.GetTimestamp();
		int num = ffmpeg.av_buffersink_get_frame(graphOutput, outputFrame);
		if (num >= 0)
		{
			if (outputFrame->ch_layout.nb_channels == 0)
			{
				ffmpeg.av_channel_layout_default(&outputFrame->ch_layout, outputFrame->channels);
			}
			frame = new FFmpegAudioFrame(outputFrame);
			ffmpeg.av_frame_unref(outputFrame);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Received {samples} samples from audio frame converter: ts={ts} [time={time}ms]", frame.Samples, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return true;
		}
		if (num != ffmpeg.AVERROR(ffmpeg.EAGAIN) && num != ffmpeg.AVERROR_EOF)
		{
			throw new FFmpegException("av_buffersink_get_frame()", num);
		}
		return false;
	}

	private unsafe void CreateFilterGraph()
	{
		graph = ffmpeg.avfilter_graph_alloc();
		if (graph == null)
		{
			throw new FFmpegException("avfilter_graph_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		graphInput = AudioFilterGraphHelper.CreateInputFilter(graph, InputFormat);
		graphOutput = AudioFilterGraphHelper.CreateOutputFilter(graph);
		string text = $"[in]aresample={OutputFormat.SampleRate}:out_sample_fmt={(int)OutputFormat.SampleFormat.ToAVSampleFormat()}:out_chlayout={OutputFormat.Channels}c";
		if (outputFrameSize.HasValue)
		{
			text += $",asetnsamples={outputFrameSize}";
		}
		List<KeyValuePair<string, UnmanagedPtr<AVFilterContext>>> graphInputs = new List<KeyValuePair<string, UnmanagedPtr<AVFilterContext>>>
		{
			new KeyValuePair<string, UnmanagedPtr<AVFilterContext>>("in", new UnmanagedPtr<AVFilterContext>(graphInput))
		};
		AudioFilterGraphHelper.BuildFilterGraph(graph, text, graphInputs, graphOutput);
		int num = ffmpeg.avfilter_graph_config(graph, null);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_graph_config()", num);
		}
	}

	protected unsafe override void Dispose(bool disposing)
	{
		fixed (AVFilterGraph** ptr = &graph)
		{
			ffmpeg.avfilter_graph_free(ptr);
		}
		graph = null;
		fixed (AVFrame** frame = &inputFrame)
		{
			ffmpeg.av_frame_free(frame);
		}
		inputFrame = null;
		fixed (AVFrame** frame = &outputFrame)
		{
			ffmpeg.av_frame_free(frame);
		}
		outputFrame = null;
	}
}
