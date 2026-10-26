using System;
using System.Collections.Generic;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class AudioFilter : BaseDisposable
{
	private readonly ILogger logger;

	private readonly Dictionary<AudioSourceType, AudioFrameConverter> converters = new Dictionary<AudioSourceType, AudioFrameConverter>();

	private readonly AudioFormat outputAudioFormat;

	private unsafe AVFilterGraph* graph;

	private readonly Dictionary<AudioSourceType, UnmanagedPtr<AVFilterContext>> graphInputs = new Dictionary<AudioSourceType, UnmanagedPtr<AVFilterContext>>();

	private unsafe AVFilterContext* graphOutput;

	private unsafe AVFrame* inputFrame;

	private unsafe AVFrame* outputFrame;

	public unsafe AudioFilter(AudioFormat microphoneInputAudioFormat, AudioFormat systemInputAudioFormat, AudioFormat outputAudioFormat, int outputFrameSize, ILogger<AudioFilter> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (microphoneInputAudioFormat == null && systemInputAudioFormat == null)
		{
			throw new ArgumentException("Both input audio formats are null.");
		}
		this.outputAudioFormat = outputAudioFormat ?? throw new ArgumentNullException("outputAudioFormat");
		if (outputFrameSize <= 0)
		{
			throw new ArgumentOutOfRangeException("outputFrameSize");
		}
		try
		{
			if (microphoneInputAudioFormat != null)
			{
				converters[AudioSourceType.Microphone] = new AudioFrameConverter(microphoneInputAudioFormat, outputAudioFormat, this.logger);
			}
			if (systemInputAudioFormat != null)
			{
				converters[AudioSourceType.System] = new AudioFrameConverter(systemInputAudioFormat, outputAudioFormat, this.logger);
			}
			CreateFilterGraph(outputFrameSize);
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

	public List<AudioFrame> ProcessFrame(AudioFrame frame, AudioSourceType audioSourceType)
	{
		Send(frame, audioSourceType);
		List<AudioFrame> list = new List<AudioFrame>();
		AudioFrame frame2;
		while (TryReceive(out frame2))
		{
			list.Add(frame2);
		}
		return list;
	}

	private unsafe void Send(AudioFrame frame, AudioSourceType audioSourceType)
	{
		if (!converters.ContainsKey(audioSourceType))
		{
			throw new ArgumentOutOfRangeException("audioSourceType");
		}
		long timestamp = Stopwatch.GetTimestamp();
		if (frame == null)
		{
			FlushConverter(audioSourceType);
			int num = ffmpeg.av_buffersrc_write_frame(graphInputs[audioSourceType].Ptr, null);
			if (num < 0)
			{
				throw new FFmpegException("av_buffersrc_write_frame(null)", num);
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Sent EOF marker to audio filter: source={source} [time={time}ms]", audioSourceType, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return;
		}
		if (frame.Format != converters[audioSourceType].InputFormat)
		{
			logger.LogInformation("Audio format changed: source={source}, old={oldFormat}, new={newFormat}", audioSourceType, converters[audioSourceType].InputFormat, frame.Format);
			FlushConverter(audioSourceType);
			converters[audioSourceType].Dispose();
			converters[audioSourceType] = new AudioFrameConverter(frame.Format, outputAudioFormat, logger);
		}
		List<AudioFrame> list = converters[audioSourceType].ProcessFrame(frame);
		try
		{
			foreach (AudioFrame item in list)
			{
				WriteFrameToGraph(item, audioSourceType);
			}
		}
		finally
		{
			foreach (AudioFrame item2 in list)
			{
				item2.Dispose();
			}
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Sent {samples} samples to audio filter: source={source}, ts={ts} [time={time}ms]", frame.Samples, audioSourceType, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
	}

	private void FlushConverter(AudioSourceType audioSourceType)
	{
		List<AudioFrame> list = converters[audioSourceType].ProcessFrame(null);
		try
		{
			foreach (AudioFrame item in list)
			{
				WriteFrameToGraph(item, audioSourceType);
			}
		}
		finally
		{
			foreach (AudioFrame item2 in list)
			{
				item2.Dispose();
			}
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Flushed audio frame converter: source={source}, frames={frames}", audioSourceType, list.Count);
		}
	}

	private unsafe void WriteFrameToGraph(AudioFrame frame, AudioSourceType audioSourceType)
	{
		using (frame.FillAVFrame(inputFrame))
		{
			int num = ffmpeg.av_buffersrc_write_frame(graphInputs[audioSourceType].Ptr, inputFrame);
			if (num < 0)
			{
				throw new FFmpegException("av_buffersrc_write_frame()", num);
			}
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
				logger.LogDebug("Received {samples} samples from audio filter: ts={ts} [time={time}ms]", frame.Samples, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return true;
		}
		if (num != ffmpeg.AVERROR(ffmpeg.EAGAIN) && num != ffmpeg.AVERROR_EOF)
		{
			throw new FFmpegException("av_buffersink_get_frame()", num);
		}
		return false;
	}

	private unsafe void CreateFilterGraph(int outputFrameSize)
	{
		graph = ffmpeg.avfilter_graph_alloc();
		if (graph == null)
		{
			throw new FFmpegException("avfilter_graph_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		foreach (AudioSourceType key in converters.Keys)
		{
			graphInputs[key] = new UnmanagedPtr<AVFilterContext>(AudioFilterGraphHelper.CreateInputFilter(graph, outputAudioFormat));
		}
		graphOutput = AudioFilterGraphHelper.CreateOutputFilter(graph);
		List<string> list = new List<string>();
		string text = $"amix=inputs={converters.Count}";
		if (converters.Count > 1)
		{
			list.Add("[mic][sys]" + text);
		}
		else if (converters.ContainsKey(AudioSourceType.Microphone))
		{
			list.Add("[mic]" + text);
		}
		else
		{
			list.Add("[sys]" + text);
		}
		list.Add("volume=10dB");
		list.Add($"aresample={outputAudioFormat.SampleRate}:out_sample_fmt={(int)outputAudioFormat.SampleFormat.ToAVSampleFormat()}:out_chlayout={outputAudioFormat.Channels}c");
		list.Add($"asetnsamples={outputFrameSize}");
		string filtersDescription = string.Join(",", list);
		List<KeyValuePair<string, UnmanagedPtr<AVFilterContext>>> list2 = new List<KeyValuePair<string, UnmanagedPtr<AVFilterContext>>>();
		if (graphInputs.ContainsKey(AudioSourceType.Microphone))
		{
			list2.Add(new KeyValuePair<string, UnmanagedPtr<AVFilterContext>>("mic", graphInputs[AudioSourceType.Microphone]));
		}
		if (graphInputs.ContainsKey(AudioSourceType.System))
		{
			list2.Add(new KeyValuePair<string, UnmanagedPtr<AVFilterContext>>("sys", graphInputs[AudioSourceType.System]));
		}
		AudioFilterGraphHelper.BuildFilterGraph(graph, filtersDescription, list2, graphOutput);
		int num = ffmpeg.avfilter_graph_config(graph, null);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_graph_config()", num);
		}
	}

	protected unsafe override void Dispose(bool disposing)
	{
		foreach (AudioFrameConverter value in converters.Values)
		{
			value.Dispose();
		}
		converters.Clear();
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
