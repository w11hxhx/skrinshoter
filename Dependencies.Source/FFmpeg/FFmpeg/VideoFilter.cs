using System;
using System.Collections.Generic;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class VideoFilter : BaseDisposable
{
	private readonly ILogger logger;

	private readonly ImageFormat initialInputImageFormat;

	private ImageFormat currentInputImageFormat;

	private readonly ImageFormat outputImageFormat;

	private readonly int scaleFlag;

	private unsafe AVFilterGraph* graph;

	private unsafe AVFilterContext* graphInput;

	private unsafe AVFilterContext* graphOutput;

	private unsafe AVFrame* inputFrame;

	private unsafe AVFrame* outputFrame;

	public unsafe VideoFilter(ImageFormat inputImageFormat, ImageFormat outputImageFormat, string scaleAlgorithm, ILogger<VideoFilter> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		initialInputImageFormat = inputImageFormat ?? throw new ArgumentNullException("inputImageFormat");
		currentInputImageFormat = inputImageFormat;
		this.outputImageFormat = outputImageFormat ?? throw new ArgumentNullException("outputImageFormat");
		if (scaleAlgorithm == null)
		{
			throw new ArgumentNullException("scaleAlgorithm");
		}
		scaleFlag = (int)(typeof(ffmpeg).GetField(scaleAlgorithm)?.GetValue(null) ?? throw new ArgumentException("Unknown image scale algorithm: " + scaleAlgorithm + ".", "scaleAlgorithm"));
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

	public List<VideoFrame> ProcessFrame(VideoFrame frame)
	{
		Send(frame);
		List<VideoFrame> list = new List<VideoFrame>();
		VideoFrame frame2;
		while (TryReceive(out frame2))
		{
			list.Add(frame2);
		}
		return list;
	}

	private unsafe void Send(VideoFrame frame)
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
				logger.LogDebug("Sent EOF marker to video filter [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return;
		}
		if (frame.Image.Format != currentInputImageFormat)
		{
			logger.LogInformation("Input image format changed from {oldInputImageFormat} to {newInputImageFormat}", currentInputImageFormat, frame.Image.Format);
			currentInputImageFormat = frame.Image.Format;
			DisposeFilterGraph();
			CreateFilterGraph();
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
			logger.LogDebug("Sent frame to video filter: format={format}, ts={ts} [time={time}ms]", frame.Image.Format, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
	}

	private unsafe bool TryReceive(out VideoFrame frame)
	{
		frame = null;
		long timestamp = Stopwatch.GetTimestamp();
		int num = ffmpeg.av_buffersink_get_frame(graphOutput, outputFrame);
		if (num >= 0)
		{
			frame = new FFmpegVideoFrame(outputFrame);
			ffmpeg.av_frame_unref(outputFrame);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Received frame from video filter: format={format}, ts={ts} [time={time}ms]", frame.Image.Format, frame.Timestamp, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
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
		graphInput = CreateInputFilter();
		graphOutput = CreateOutputFilter();
		List<string> values = new List<string>
		{
			$"scale=w={outputImageFormat.Width}:h={outputImageFormat.Height}:flags={scaleFlag}",
			$"format={(int)outputImageFormat.PixelFormat.ToAVPixelFormat()}"
		};
		string filtersDescription = string.Join(",", values);
		BuildFilterGraph(filtersDescription);
		int num = ffmpeg.avfilter_graph_config(graph, null);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_graph_config()", num);
		}
	}

	private unsafe AVFilterContext* CreateInputFilter()
	{
		AVFilter* ptr = ffmpeg.avfilter_get_by_name("buffer");
		if (ptr == null)
		{
			throw new FFmpegException("avfilter_get_by_name(buffer)", ffmpeg.AVERROR_FILTER_NOT_FOUND);
		}
		AVFilterContext* ptr2 = ffmpeg.avfilter_graph_alloc_filter(graph, ptr, "buffer");
		if (ptr2 == null)
		{
			throw new FFmpegException("avfilter_graph_alloc_filter(buffer)", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		string args = $"width={currentInputImageFormat.Width}:height={currentInputImageFormat.Height}:pix_fmt={(int)currentInputImageFormat.PixelFormat.ToAVPixelFormat()}:time_base=1/{1000000}";
		int num = ffmpeg.avfilter_init_str(ptr2, args);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_init_str(buffer)", num);
		}
		return ptr2;
	}

	private unsafe AVFilterContext* CreateOutputFilter()
	{
		AVFilter* ptr = ffmpeg.avfilter_get_by_name("buffersink");
		if (ptr == null)
		{
			throw new FFmpegException("avfilter_get_by_name(buffersink)", ffmpeg.AVERROR_FILTER_NOT_FOUND);
		}
		AVFilterContext* ptr2 = ffmpeg.avfilter_graph_alloc_filter(graph, ptr, "buffersink");
		if (ptr2 == null)
		{
			throw new FFmpegException("avfilter_graph_alloc_filter(buffersink)", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		int num = ffmpeg.avfilter_init_str(ptr2, null);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_init_str(buffersink)", num);
		}
		return ptr2;
	}

	private unsafe void BuildFilterGraph(string filtersDescription)
	{
		AVFilterInOut* ptr = ffmpeg.avfilter_inout_alloc();
		AVFilterInOut* ptr2 = ffmpeg.avfilter_inout_alloc();
		try
		{
			if (ptr == null || ptr2 == null)
			{
				throw new FFmpegException("avfilter_inout_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
			ptr2->name = ffmpeg.av_strdup("in");
			ptr2->filter_ctx = graphInput;
			ptr->name = ffmpeg.av_strdup("out");
			ptr->filter_ctx = graphOutput;
			int num = ffmpeg.avfilter_graph_parse_ptr(graph, filtersDescription, &ptr, &ptr2, null);
			if (num < 0)
			{
				throw new FFmpegException("avfilter_graph_parse_ptr()", num);
			}
		}
		finally
		{
			ffmpeg.avfilter_inout_free(&ptr);
			ffmpeg.avfilter_inout_free(&ptr2);
		}
	}

	private unsafe void DisposeFilterGraph()
	{
		fixed (AVFilterGraph** ptr = &graph)
		{
			ffmpeg.avfilter_graph_free(ptr);
		}
		graph = null;
		graphInput = null;
		graphOutput = null;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		DisposeFilterGraph();
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
