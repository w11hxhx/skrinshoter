using System;
using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Win32.Graphics.Direct3D11;

namespace ScreenCaptureWgc;

public class ScreenCapturerWgc : BaseCapturerWgc, IScreenCapturer, IDisposable
{
	private class GetFrameRequest
	{
		public RawImage Image { get; }

		public TimeSpan Timeout { get; }

		public TaskCompletionSource<bool> Completion { get; } = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

		public GetFrameRequest(RawImage image, TimeSpan timeout)
		{
			Image = image;
			Timeout = timeout;
		}
	}

	private readonly Channel<GetFrameRequest> requestQueue;

	private int capturedFrameCounter;

	private long logStatisticsNextTimestamp;

	public ScreenCapturerWgc(string monitorName, Rectangle captureArea, bool showMousePointer, ILogger<ScreenCapturerWgc> logger = null)
	{
		base.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (captureArea.Left < 0 || captureArea.Top < 0 || captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		base.monitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		base.captureArea = captureArea;
		base.showMousePointer = showMousePointer;
		try
		{
			requestQueue = Channel.CreateBounded<GetFrameRequest>(new BoundedChannelOptions(1)
			{
				SingleReader = true
			});
			captureThread = new Thread(CaptureThreadProc)
			{
				Name = "desktop image capture",
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

	public bool GetFrame(RawImage image, TimeSpan timeout)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (image.Format.Width != captureArea.Width || image.Format.Height != captureArea.Height)
		{
			throw new ArgumentException("Image size does not match capture area size.", "image");
		}
		if (image.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Unsupported pixel format: {image.Format.PixelFormat}.", "image");
		}
		if (timeout < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("timeout");
		}
		GetFrameRequest getFrameRequest = new GetFrameRequest(image, timeout);
		if (!requestQueue.Writer.TryWrite(getFrameRequest))
		{
			throw new InvalidOperationException("Failed to enqueue request.");
		}
		return getFrameRequest.Completion.Task.GetAwaiter().GetResult();
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
				logStatisticsNextTimestamp = Stopwatch.GetTimestamp() + Stopwatch.Frequency;
				while (true)
				{
					cts.Token.ThrowIfCancellationRequested();
					LogStatisticsIfTimeIsRight();
					if (requestQueue.Reader.TryRead(out var item))
					{
						try
						{
							bool flag = CaptureFrame(item.Image, item.Timeout, cts.Token);
							if (flag)
							{
								capturedFrameCounter++;
							}
							item.Completion.TrySetResult(flag);
						}
						catch (Exception exception3)
						{
							item.Completion.TrySetException(exception3);
						}
					}
					else
					{
						sleeper.Sleep(1);
					}
				}
			}
			catch (Exception ex)
			{
				if (!(ex is OperationCanceledException))
				{
					logger.LogError(ex, "Desktop image capture thread exited unexpectedly");
				}
				requestQueue.Writer.TryComplete(ex);
				GetFrameRequest item2;
				while (requestQueue.Reader.TryRead(out item2))
				{
					item2.Completion.TrySetException(ex);
				}
			}
			finally
			{
				DisposeCaptureObjects();
			}
		}
	}

	private bool CaptureFrame(RawImage image, TimeSpan timeout, CancellationToken cancellationToken)
	{
		long timestamp = Stopwatch.GetTimestamp();
		while (true)
		{
			cancellationToken.ThrowIfCancellationRequested();
			LogStatisticsIfTimeIsRight();
			if (TryCaptureFrame(image))
			{
				return true;
			}
			if ((double)StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp) >= timeout.TotalMilliseconds)
			{
				break;
			}
			sleeper.Sleep(1);
		}
		return false;
	}

	private bool TryCaptureFrame(RawImage image)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
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
			CopyStagingTextureToImage(image);
			return true;
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private unsafe void CopyStagingTextureToImage(RawImage image)
	{
		D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
		d3d11DeviceContext.Ptr->Map(stagingTexture, 0u, D3D11_MAP.D3D11_MAP_READ, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(stagingTexture, READ)");
		try
		{
			using MemoryHandle memoryHandle = image.Data[0].Pin();
			RawImage.CopyRegion((byte*)d3D11_MAPPED_SUBRESOURCE.pData, (int)d3D11_MAPPED_SUBRESOURCE.RowPitch, (byte*)memoryHandle.Pointer, image.Stride[0], captureArea.Width * 4, captureArea.Height);
		}
		finally
		{
			d3d11DeviceContext.Ptr->Unmap(stagingTexture, 0u);
		}
	}

	private void LogStatisticsIfTimeIsRight()
	{
		if (Stopwatch.GetTimestamp() >= logStatisticsNextTimestamp)
		{
			logger.LogInformation("Captured {capturedFrameCounter} frames per second", capturedFrameCounter);
			logStatisticsNextTimestamp += Stopwatch.Frequency;
			capturedFrameCounter = 0;
		}
	}
}
