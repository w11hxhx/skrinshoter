using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ScreenRecorderCommon;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;

namespace ScreenCaptureDda;

public class VideoCapturerDda : BaseCapturerDda, IVideoCapturer, IDisposable
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

	private Thread captureThread;

	private CancellationTokenSource cts = new CancellationTokenSource();

	private Task<ComPtr<IDXGIOutputDuplication>> createOutputDuplicationTask;

	public ImageFormat ImageFormat { get; }

	public ChannelReader<VideoFrame> Frames { get; }

	public VideoCapturerDda(string monitorName, Rectangle captureArea, int frameRate, MousePointer mousePointer, StoppableStopwatch stopwatch, ILogger<VideoCapturerDda> logger = null)
	{
		base.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (captureArea.Left < 0 || captureArea.Top < 0 || captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		if (frameRate <= 0)
		{
			throw new ArgumentOutOfRangeException("frameRate");
		}
		base.monitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		base.captureArea = captureArea;
		this.frameRate = frameRate;
		base.mousePointer = mousePointer;
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
			CreateCaptureObjects();
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	private protected override void CreateCaptureObjects()
	{
		base.CreateCaptureObjects();
		createOutputDuplicationTask = CreateOutputDuplicationAsync(cts.Token);
		if (createOutputDuplicationTask.IsCompleted)
		{
			outputDuplication = createOutputDuplicationTask.GetAwaiter().GetResult();
			createOutputDuplicationTask = null;
		}
	}

	private async Task<ComPtr<IDXGIOutputDuplication>> CreateOutputDuplicationAsync(CancellationToken cancellationToken)
	{
		HRESULT hRESULT = TryCreateOutputDuplication(out var result);
		if (hRESULT == HRESULT.DXGI_ERROR_UNSUPPORTED)
		{
			logger.LogWarning("IDXGIOutput1.DuplicateOutput(): Unsupported");
			throw new ErrorCodeException(ErrorCode.DuplicateOutputUnsupported, new DirectXException("IDXGIOutput1.DuplicateOutput()", Marshal.GetExceptionForHR(hRESULT)));
		}
		if (hRESULT == HRESULT.E_ACCESSDENIED)
		{
			logger.LogWarning("IDXGIOutput1.DuplicateOutput(): Access denied");
			do
			{
				await Task.Delay(10, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				hRESULT = TryCreateOutputDuplication(out result);
			}
			while (hRESULT == HRESULT.E_ACCESSDENIED);
		}
		hRESULT.ThrowOnFailure("IDXGIOutput1.DuplicateOutput()");
		return result;
	}

	private unsafe VideoFrame CreateVideoFrame(FrameTimestamp timestamp)
	{
		D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
		d3d11DeviceContext.Ptr->Map(stagingTexture, 0u, D3D11_MAP.D3D11_MAP_READ, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(stagingTexture, READ)");
		try
		{
			UnmanagedRawImage image = new UnmanagedRawImage(ImageFormat, d3D11_MAPPED_SUBRESOURCE.pData, (int)d3D11_MAPPED_SUBRESOURCE.RowPitch);
			mousePointer?.Draw(image);
			return new VideoFrame(image, timestamp);
		}
		finally
		{
			d3d11DeviceContext.Ptr->Unmap(stagingTexture, 0u);
		}
	}

	private void CaptureThreadProc()
	{
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
		}
	}

	private unsafe bool TryCaptureFrame(out VideoFrame frame)
	{
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
				if (desktopResource.Ptr != null)
				{
					desktopResource.Dispose();
					outputDuplication.Ptr->ReleaseFrame().ThrowOnFailure("IDXGIOutputDuplication.ReleaseFrame()");
				}
				if (outputDuplication.Ptr == null)
				{
					if (!createOutputDuplicationTask.IsCompleted)
					{
						return false;
					}
					outputDuplication = createOutputDuplicationTask.GetAwaiter().GetResult();
					createOutputDuplicationTask = null;
				}
				IDXGIResource* ptr = default;
				HRESULT hRESULT = outputDuplication.Ptr->AcquireNextFrame(0u, out var pFrameInfo, &ptr);
				if (hRESULT == HRESULT.DXGI_ERROR_WAIT_TIMEOUT)
				{
					return false;
				}
				if (hRESULT.Failed)
				{
					hRESULT.ThrowOnFailure("IDXGIOutputDuplication.AcquireNextFrame()");
				}
				desktopResource = ptr;
				if (logger.IsEnabled(LogLevel.Trace))
				{
					logger.LogTrace("AcquireNextFrame: AccumulatedFrames={accumulatedFrames}, LastPresentTime={lastPresentTime}, LastMouseUpdateTime={lastMouseUpdateTime}, PointerPosition={visible},{x},{y}, PointerShapeBufferSize={pointerShapeBufferSize}, TotalMetadataBufferSize={totalMetadataBufferSize}, RectsCoalesced={rectsCoalesced}, ProtectedContentMaskedOut={protectedContentMaskedOut}", pFrameInfo.AccumulatedFrames, pFrameInfo.LastPresentTime, pFrameInfo.LastMouseUpdateTime, (bool)pFrameInfo.PointerPosition.Visible, pFrameInfo.PointerPosition.Position.X, pFrameInfo.PointerPosition.Position.Y, pFrameInfo.PointerShapeBufferSize, pFrameInfo.TotalMetadataBufferSize, (bool)pFrameInfo.RectsCoalesced, (bool)pFrameInfo.ProtectedContentMaskedOut);
				}
				mousePointer?.UpdatePositionAndShape(outputDuplication, in pFrameInfo);
				if (pFrameInfo.LastPresentTime == 0L)
				{
					return false;
				}
				using (ComPtr<ID3D11Texture2D> capturedImage = RotateIfNeeded(desktopResource))
				{
					CopyCapturedImageToStagingTexture(capturedImage);
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
			catch (DirectXException ex) when (ex.InnerException?.HResult == (int)HRESULT.DXGI_ERROR_ACCESS_LOST)
			{
				logger.LogInformation("Restarting desktop video capturer");
				DisposeOutputDuplication();
				createOutputDuplicationTask = Task.Run(() => CreateOutputDuplicationAsync(cts.Token));
				return false;
			}
		}
	}

	private long GetFrameTimestamp(long frameNumber)
	{
		return (long)Math.Round((double)frameNumber * frameDuration);
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

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			cts.Cancel();
			captureThread?.Join();
			captureThread = null;
			if (createOutputDuplicationTask != null)
			{
				Task.WaitAny(createOutputDuplicationTask);
				if (createOutputDuplicationTask.Status == TaskStatus.RanToCompletion)
				{
					outputDuplication = createOutputDuplicationTask.Result;
				}
				createOutputDuplicationTask = null;
			}
			cts.Dispose();
			cts = null;
		}
		base.Dispose(disposing);
	}
}
