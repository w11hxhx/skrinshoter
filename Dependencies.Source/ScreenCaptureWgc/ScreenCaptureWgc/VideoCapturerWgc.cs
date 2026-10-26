using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Channels;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MousePointer;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Win32.Graphics.Direct3D11;

namespace ScreenCaptureWgc;

public class VideoCapturerWgc : BaseCapturerWgc, IVideoCapturer, IDisposable
{
	private const int FrameTimeBase = 1000000;

	private readonly int frameRate;

	private readonly StoppableStopwatch stopwatch;

	private readonly double frameDuration;

	private readonly double frameTimeBaseFactor;

	private int capturedFrameCounter;

	private int duplicatedFrameCounter;

	private int skippedFrameCounter;

	private long logStatisticsNextFrameNumber;

	private long lastStopwatchTimestamp;

	private long nextFrameNumber;

	private long lastTriedFrameNumber = -1L;

	private readonly ChannelWriter<VideoFrame> frameWriter;

	private bool frameWriterHasFrames;

	public ImageFormat ImageFormat { get; }

	public ChannelReader<VideoFrame> Frames { get; }

	public VideoCapturerWgc(string monitorName, Rectangle captureArea, int frameRate, bool showMousePointer, IMouseOverlay mouseOverlay, StoppableStopwatch stopwatch, ILoggerFactory loggerFactory)
	{
		base.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
		logger = base.loggerFactory.CreateLogger<VideoCapturerWgc>();
		if (captureArea.Left < 0 || captureArea.Top < 0 || captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		if (frameRate <= 0)
		{
			throw new ArgumentOutOfRangeException("frameRate");
		}
		base.monitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		monitorInfo = new MonitorInfo(monitorName);
		base.captureArea = captureArea;
		this.frameRate = frameRate;
		base.showMousePointer = showMousePointer;
		base.mouseOverlay = mouseOverlay;
		this.stopwatch = stopwatch ?? throw new ArgumentNullException("stopwatch");
		frameDuration = 1000000.0 / (double)frameRate;
		frameTimeBaseFactor = 1000000.0 / (double)stopwatch.Frequency;
		ImageFormat = new ImageFormat(captureArea.Size, PixelFormat.BGRA);
		logStatisticsNextFrameNumber = frameRate;
		try
		{
			Channel<VideoFrame> channel = Channel.CreateBounded<VideoFrame>(new BoundedChannelOptions(1)
			{
				SingleWriter = true
			});
			frameWriter = channel.Writer;
			Frames = channel.Reader;
			captureThread = new Thread(CaptureThreadProc)
			{
				Name = "desktop video capture",
				Priority = ThreadPriority.Highest,
				IsBackground = true
			};
			captureThread.Start();
			captureThreadStartupCompletion.Task.GetAwaiter().GetResult();
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	private void CaptureThreadProc()
	{
		WinRtInitScope winRtInitScope = null;
		try
		{
			winRtInitScope = new WinRtInitScope();
		}
		catch (Exception exception)
		{
			captureThreadStartupCompletion.SetException(exception);
			return;
		}
		using (winRtInitScope)
		{
			try
			{
				CreateCaptureObjects();
				captureThreadStartupCompletion.SetResult(result: true);
			}
			catch (Exception exception2)
			{
				DisposeCaptureObjects();
				captureThreadStartupCompletion.SetException(exception2);
				return;
			}
			try
			{
				while (!IsDisposed)
				{
					if (TryCaptureFrame(out var frame))
					{
						try
						{
							frameWriter.WriteAsync(frame, cts.Token).AsTask().GetAwaiter()
								.GetResult();
							frameWriterHasFrames = true;
						}
						catch
						{
							frame.Dispose();
							throw;
						}
					}
					sleeper.Sleep(1);
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception ex2)
			{
				logger.LogError(ex2, "Desktop video capture thread exited unexpectedly");
				if (!frameWriterHasFrames)
				{
					frameWriter.Complete(ex2);
				}
			}
			finally
			{
				frameWriter.TryComplete();
				DisposeCaptureObjects();
			}
		}
	}

	private bool TryCaptureFrame(out VideoFrame frame)
	{
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		frame = null;
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return false;
			}
			long timestamp = stopwatch.GetTimestamp();
			if (timestamp == lastStopwatchTimestamp)
			{
				return false;
			}
			lastStopwatchTimestamp = timestamp;
			long num = (long)Math.Round((double)timestamp * frameTimeBaseFactor);
			long num2 = GetFrameTimestamp(nextFrameNumber);
			if (num < num2)
			{
				return false;
			}
			while (true)
			{
				long frameTimestamp = GetFrameTimestamp(nextFrameNumber + 1);
				if (num < frameTimestamp)
				{
					break;
				}
				if (nextFrameNumber == lastTriedFrameNumber)
				{
					long timestamp2 = Stopwatch.GetTimestamp();
					frame = CreateVideoFrame(new FrameTimestamp(num2, 1000000L));
					if (logger.IsEnabled(LogLevel.Debug))
					{
						logger.LogDebug("Duplicated video frame {frameNumber}: ts={ts}, {width}x{height} [time={time}ms]", nextFrameNumber, frame.Timestamp.Value, frame.Image.Format.Width, frame.Image.Format.Height, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp2));
					}
					nextFrameNumber++;
					duplicatedFrameCounter++;
					LogStatisticsIfTimeIsRight();
					return true;
				}
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Skipped    video frame {frameNumber}: ts={ts}", nextFrameNumber, num2);
				}
				nextFrameNumber++;
				skippedFrameCounter++;
				LogStatisticsIfTimeIsRight();
				num2 = frameTimestamp;
			}
			try
			{
				long timestamp3 = Stopwatch.GetTimestamp();
				lastTriedFrameNumber = nextFrameNumber;
				Direct3D11CaptureFrame val = captureFramePool.TryGetNextFrame();
				try
				{
					if (val == null)
					{
						return false;
					}
					if (logger.IsEnabled(LogLevel.Trace))
					{
						logger.LogTrace("TryGetNextFrame: ContentSize={contentWidth}x{contentHeight}, SystemRelativeTime={systemRelativeTime}", val.ContentSize.Width, val.ContentSize.Height, val.SystemRelativeTime.Ticks);
					}
					IDirect3DSurface surface = val.Surface;
					try
					{
						using ComPtr<ID3D11Texture2D> capturedImage = GetTextureFromSurface(surface);
						CopyCapturedImageToStagingTexture(capturedImage, new Size(val.ContentSize.Width, val.ContentSize.Height));
					}
					finally
					{
						((IDisposable)surface)?.Dispose();
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				frame = CreateVideoFrame(new FrameTimestamp(num2, 1000000L));
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Captured   video frame {frameNumber}: ts={ts}, {width}x{height} [time={time}ms]", nextFrameNumber, frame.Timestamp.Value, frame.Image.Format.Width, frame.Image.Format.Height, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp3));
				}
				nextFrameNumber++;
				capturedFrameCounter++;
				LogStatisticsIfTimeIsRight();
				return true;
			}
			catch
			{
				throw;
			}
		}
	}

	private long GetFrameTimestamp(long frameNumber)
	{
		return (long)Math.Round((double)frameNumber * frameDuration);
	}

	private unsafe VideoFrame CreateVideoFrame(FrameTimestamp timestamp)
	{
		D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
		d3d11DeviceContext.Ptr->Map(stagingTexture, 0u, D3D11_MAP.D3D11_MAP_READ, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(stagingTexture, READ)");
		try
		{
			UnmanagedRawImage image = new UnmanagedRawImage(ImageFormat, d3D11_MAPPED_SUBRESOURCE.pData, (int)d3D11_MAPPED_SUBRESOURCE.RowPitch);
			if (mouseOverlay != null)
			{
				Rectangle rectangle = monitorInfo.GetRectangle();
				Point location = captureArea.Location;
				location.Offset(rectangle.X, rectangle.Y);
				using MousePointerInfo mousePointerInfo = new MousePointerInfo(loggerFactory.CreateLogger<MousePointerInfo>());
				mouseOverlay.Draw(image, location, mousePointerInfo);
			}
			return new VideoFrame(image, timestamp);
		}
		finally
		{
			d3d11DeviceContext.Ptr->Unmap(stagingTexture, 0u);
		}
	}

	private void LogStatisticsIfTimeIsRight()
	{
		if (nextFrameNumber >= logStatisticsNextFrameNumber)
		{
			logger.LogInformation("Captured {actualFrames}/{expectedFrames} video frames per second (unique={capturedFrames}, duplicated={duplicatedFrames}, skipped={skippedFrames})", capturedFrameCounter + duplicatedFrameCounter, frameRate, capturedFrameCounter, duplicatedFrameCounter, skippedFrameCounter);
			logStatisticsNextFrameNumber += frameRate;
			capturedFrameCounter = 0;
			duplicatedFrameCounter = 0;
			skippedFrameCounter = 0;
		}
	}
}
