using System;
using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ScreenRecorderCommon;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;

namespace ScreenCaptureDda;

public class ScreenCapturerDda : BaseCapturerDda, IScreenCapturer, IDisposable
{
	private int capturedFrameCounter;

	private long logStatisticsNextTimestamp;

	public ScreenCapturerDda(string monitorName, Rectangle captureArea, MousePointer mousePointer, ILogger<ScreenCapturerDda> logger = null)
	{
		base.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (captureArea.Left < 0 || captureArea.Top < 0 || captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		base.monitorName = monitorName ?? throw new ArgumentNullException("monitorName");
		base.captureArea = captureArea;
		base.mousePointer = mousePointer;
		try
		{
			CreateCaptureObjects();
			logStatisticsNextTimestamp = Stopwatch.GetTimestamp() + Stopwatch.Frequency;
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
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			long timestamp = Stopwatch.GetTimestamp();
			while (true)
			{
				LogStatisticsIfTimeIsRight();
				if (TryCaptureFrame(image))
				{
					capturedFrameCounter++;
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
	}

	private protected override void CreateCaptureObjects()
	{
		base.CreateCaptureObjects();
		CreateOutputDuplication();
	}

	private void CreateOutputDuplication()
	{
		HRESULT hRESULT = TryCreateOutputDuplication(out outputDuplication);
		if (hRESULT == HRESULT.DXGI_ERROR_UNSUPPORTED)
		{
			logger.LogWarning("IDXGIOutput1.DuplicateOutput(): Unsupported");
			throw new ErrorCodeException(ErrorCode.DuplicateOutputUnsupported, new DirectXException("IDXGIOutput1.DuplicateOutput()", Marshal.GetExceptionForHR(hRESULT)));
		}
		hRESULT.ThrowOnFailure("IDXGIOutput1.DuplicateOutput()");
	}

	private unsafe void CopyStagingTextureToImage(RawImage image)
	{
		D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
		d3d11DeviceContext.Ptr->Map(stagingTexture, 0u, D3D11_MAP.D3D11_MAP_READ, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(stagingTexture, READ)");
		try
		{
			using (MemoryHandle memoryHandle = image.Data[0].Pin())
			{
				RawImage.CopyRegion((byte*)d3D11_MAPPED_SUBRESOURCE.pData, (int)d3D11_MAPPED_SUBRESOURCE.RowPitch, (byte*)memoryHandle.Pointer, image.Stride[0], captureArea.Width * 4, captureArea.Height);
			}
			mousePointer?.Draw(image);
		}
		finally
		{
			d3d11DeviceContext.Ptr->Unmap(stagingTexture, 0u);
		}
	}

	private unsafe bool TryCaptureFrame(RawImage image)
	{
		if (desktopResource.Ptr != null)
		{
			desktopResource.Dispose();
			outputDuplication.Ptr->ReleaseFrame().ThrowOnFailure("IDXGIOutputDuplication.ReleaseFrame()");
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
		CopyStagingTextureToImage(image);
		return true;
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
