using System;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FFmpeg;

public class ImageResizerAndConverter : BaseDisposable
{
	private readonly ILogger logger;

	private ImageFormat inputImageFormat;

	private readonly ImageFormat outputImageFormat;

	private readonly int scaleFlag;

	private unsafe SwsContext* context;

	private unsafe AVFrame* inputFrame;

	private unsafe AVFrame* outputFrame;

	public unsafe ImageResizerAndConverter(ImageFormat inputImageFormat, ImageFormat outputImageFormat, string scaleAlgorithm, ILogger<ImageResizerAndConverter> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.inputImageFormat = inputImageFormat ?? throw new ArgumentNullException("inputImageFormat");
		this.outputImageFormat = outputImageFormat ?? throw new ArgumentNullException("outputImageFormat");
		if (scaleAlgorithm == null)
		{
			throw new ArgumentNullException("scaleAlgorithm");
		}
		scaleFlag = (int)(typeof(ffmpeg).GetField(scaleAlgorithm)?.GetValue(null) ?? throw new ArgumentException("Unknown image scale algorithm: " + scaleAlgorithm + ".", "scaleAlgorithm"));
		try
		{
			CreateContext();
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

	public unsafe RawImage ResizeAndConvert(RawImage inputImage)
	{
		if (inputImage == null)
		{
			throw new ArgumentNullException("inputImage");
		}
		if (inputImage.Format != inputImageFormat)
		{
			logger.LogInformation("Input image format changed from {oldInputImageFormat} to {newInputImageFormat}", inputImageFormat, inputImage.Format);
			inputImageFormat = inputImage.Format;
			DisposeContext();
			CreateContext();
		}
		long timestamp = Stopwatch.GetTimestamp();
		using (inputImage.FillAVFrame(inputFrame))
		{
			int num = ffmpeg.sws_scale_frame(context, outputFrame, inputFrame);
			if (num < 0)
			{
				throw new FFmpegException("sws_scale_frame()", num);
			}
			FFmpegRawImage fFmpegRawImage = new FFmpegRawImage(outputFrame);
			ffmpeg.av_frame_unref(outputFrame);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Converted image from {inputImageFormat} to {outputImageFormat} [time={time}ms]", inputImage.Format, fFmpegRawImage.Format, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return fFmpegRawImage;
		}
	}

	private unsafe void CreateContext()
	{
		context = ffmpeg.sws_getContext(inputImageFormat.Width, inputImageFormat.Height, inputImageFormat.PixelFormat.ToAVPixelFormat(), outputImageFormat.Width, outputImageFormat.Height, outputImageFormat.PixelFormat.ToAVPixelFormat(), scaleFlag, null, null, null);
		if (context == null)
		{
			throw new FFmpegException("sws_getContext()", ffmpeg.AVERROR_UNKNOWN);
		}
	}

	private unsafe void DisposeContext()
	{
		ffmpeg.sws_freeContext(context);
		context = null;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		DisposeContext();
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
