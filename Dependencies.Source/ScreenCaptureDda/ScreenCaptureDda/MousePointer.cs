using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using MouseHook;
using Windows.Win32;
using Windows.Win32.Graphics.Dxgi;

namespace ScreenCaptureDda;

public class MousePointer : BaseDisposable
{
	private class ClickHighlight
	{
		public Point Position { get; }

		public long ExpirationTimestamp { get; }

		public ClickHighlight(Point position, long expirationTimestamp)
		{
			Position = position;
			ExpirationTimestamp = expirationTimestamp;
		}
	}

	private const int PointerHighlightDiameter = 56;

	private const int ClickHighlightMaxDiameter = 48;

	private static readonly TimeSpan ClickHighlightDuration = TimeSpan.FromMilliseconds(500.0);

	private readonly ILogger logger;

	private readonly MonitorInfo monitorInfo;

	private readonly bool showMousePointer;

	private readonly bool highlightMousePointer;

	private readonly bool highlightMouseClicks;

	private readonly Point captureAreaPosition;

	private readonly StoppableStopwatch stopwatch;

	private bool mouseVisible;

	private Point mouseShapePosition;

	private Point mouseTipPosition;

	private readonly UnmanagedBuffer shapeRawBuffer = new UnmanagedBuffer();

	private readonly UnmanagedBuffer shapeDrawBuffer = new UnmanagedBuffer();

	private readonly UnmanagedBuffer shapeXorBuffer = new UnmanagedBuffer();

	private RawImage shapeDrawImage;

	private RawImage shapeXorImage;

	private Bitmap pointerHighlightBitmap;

	private readonly MouseButtonMonitor mouseButtonMonitor;

	private readonly ConcurrentQueue<ClickHighlight> activeClickHighlights = new ConcurrentQueue<ClickHighlight>();

	public MousePointer(string monitorName, bool showMousePointer, bool highlightMousePointer, bool highlightMouseClicks, Point captureAreaPosition, StoppableStopwatch stopwatch, ILoggerFactory loggerFactory)
	{
		logger = loggerFactory.CreateLogger<MousePointer>();
		monitorInfo = new MonitorInfo(monitorName);
		this.showMousePointer = showMousePointer;
		this.highlightMousePointer = highlightMousePointer;
		this.highlightMouseClicks = highlightMouseClicks;
		this.captureAreaPosition = captureAreaPosition;
		this.stopwatch = stopwatch;
		if (highlightMouseClicks)
		{
			mouseButtonMonitor = new MouseButtonMonitor(loggerFactory.CreateLogger<MouseButtonMonitor>());
			mouseButtonMonitor.MouseButtonEvent += MouseButtonEvent;
		}
	}

	internal unsafe void UpdatePositionAndShape(ComPtr<IDXGIOutputDuplication> outputDuplication, in DXGI_OUTDUPL_FRAME_INFO frameInfo)
	{
		if (outputDuplication.Ptr == null)
		{
			throw new ArgumentNullException("outputDuplication");
		}
		if (frameInfo.LastMouseUpdateTime != 0L)
		{
			UpdatePosition(in frameInfo);
			UpdateShape(outputDuplication, in frameInfo);
		}
	}

	internal void Draw(RawImage image)
	{
		if (!mouseVisible)
		{
			return;
		}
		if (highlightMousePointer)
		{
			DrawPointerHighlight(image);
		}
		if (highlightMouseClicks)
		{
			DrawClickHighlights(image);
		}
		if (showMousePointer)
		{
			if (shapeDrawImage != null)
			{
				image.DrawImage(shapeDrawImage, mouseShapePosition);
			}
			if (shapeXorImage != null)
			{
				image.XorImage(shapeXorImage, mouseShapePosition);
			}
		}
	}

	private void MouseButtonEvent(object sender, MouseButtonEventArgs e)
	{
		if (e.ButtonState == MouseButtonState.Released && stopwatch.IsStarted)
		{
			Point position = e.Position;
			Rectangle rectangle = monitorInfo.GetRectangle();
			position.Offset(-rectangle.X, -rectangle.Y);
			position.Offset(-captureAreaPosition.X, -captureAreaPosition.Y);
			activeClickHighlights.Enqueue(new ClickHighlight(position, stopwatch.GetTimestamp() + (long)(ClickHighlightDuration.TotalSeconds * (double)stopwatch.Frequency)));
		}
	}

	private void UpdatePosition(in DXGI_OUTDUPL_FRAME_INFO frameInfo)
	{
		mouseVisible = frameInfo.PointerPosition.Visible;
		if (mouseVisible)
		{
			mouseShapePosition = frameInfo.PointerPosition.Position;
			mouseShapePosition.Offset(-captureAreaPosition.X, -captureAreaPosition.Y);
			if (!PInvoke.GetCursorPos(out mouseTipPosition))
			{
				throw new WinApiException("GetCursorPos()", new Win32Exception());
			}
			Rectangle rectangle = monitorInfo.GetRectangle();
			mouseTipPosition.Offset(-rectangle.X, -rectangle.Y);
			mouseTipPosition.Offset(-captureAreaPosition.X, -captureAreaPosition.Y);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Mouse position updated: ShapeX={shapeX}, ShapeY={shapeY}, TipX={tipX}, TipY={tipY}", mouseShapePosition.X, mouseShapePosition.Y, mouseTipPosition.X, mouseTipPosition.Y);
			}
		}
	}

	private unsafe void UpdateShape(ComPtr<IDXGIOutputDuplication> outputDuplication, in DXGI_OUTDUPL_FRAME_INFO frameInfo)
	{
		if (frameInfo.PointerShapeBufferSize != 0)
		{
			shapeRawBuffer.Enlarge((int)frameInfo.PointerShapeBufferSize);
			outputDuplication.Ptr->GetFramePointerShape((uint)shapeRawBuffer.Size, shapeRawBuffer.Ptr, out var pPointerShapeBufferSizeRequired, out var pPointerShapeInfo).ThrowOnFailure("outputDuplication.GetFramePointerShape()");
			DXGI_OUTDUPL_POINTER_SHAPE_TYPE type = (DXGI_OUTDUPL_POINTER_SHAPE_TYPE)pPointerShapeInfo.Type;
			switch (type)
			{
			case DXGI_OUTDUPL_POINTER_SHAPE_TYPE.DXGI_OUTDUPL_POINTER_SHAPE_TYPE_MONOCHROME:
				ProcessShapeTypeMonochrome((int)pPointerShapeInfo.Width, (int)pPointerShapeInfo.Height, (int)pPointerShapeInfo.Pitch);
				break;
			case DXGI_OUTDUPL_POINTER_SHAPE_TYPE.DXGI_OUTDUPL_POINTER_SHAPE_TYPE_COLOR:
				ProcessShapeTypeColor((int)pPointerShapeInfo.Width, (int)pPointerShapeInfo.Height, (int)pPointerShapeInfo.Pitch);
				break;
			case DXGI_OUTDUPL_POINTER_SHAPE_TYPE.DXGI_OUTDUPL_POINTER_SHAPE_TYPE_MASKED_COLOR:
				ProcessShapeTypeMaskedColor((int)pPointerShapeInfo.Width, (int)pPointerShapeInfo.Height, (int)pPointerShapeInfo.Pitch);
				break;
			default:
				throw new InvalidOperationException($"Unknown pointer shape type: {type}.");
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Mouse shape updated: BufferSize={bufferSize}, Width={width}, Height={height}, Stride={stride}, Type={type}", pPointerShapeBufferSizeRequired, pPointerShapeInfo.Width, pPointerShapeInfo.Height, pPointerShapeInfo.Pitch, type);
			}
		}
	}

	private unsafe void ProcessShapeTypeMonochrome(int width, int height, int stride)
	{
		height /= 2;
		int num = width * 4;
		int newSize = height * num;
		shapeDrawBuffer.Enlarge(newSize);
		shapeXorBuffer.Enlarge(newSize);
		byte* ptr = shapeRawBuffer.Ptr;
		byte* ptr2 = shapeRawBuffer.Ptr + height * stride;
		byte* ptr3 = shapeDrawBuffer.Ptr;
		byte* ptr4 = shapeXorBuffer.Ptr;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				int num2 = j >> 3;
				int num3 = 7 - (j & 7);
				int num4 = (ptr[num2] >> num3) & 1;
				int num5 = (ptr2[num2] >> num3) & 1;
				if (num4 == 0 && num5 == 0)
				{
					((int*)ptr3)[j] = -16777216;
					((int*)ptr4)[j] = 0;
				}
				else if (num4 != 0 && num5 == 0)
				{
					((int*)ptr3)[j] = 0;
					((int*)ptr4)[j] = 0;
				}
				else if (num4 == 0 && num5 != 0)
				{
					((int*)ptr3)[j] = -1;
					((int*)ptr4)[j] = 0;
				}
				else if (num4 != 0 && num5 != 0)
				{
					((int*)ptr3)[j] = 0;
					((int*)ptr4)[j] = 16777215;
				}
			}
			ptr += stride;
			ptr2 += stride;
			ptr3 += num;
			ptr4 += num;
		}
		shapeDrawImage = new RawImage(new AudioVideoCore.ImageFormat(new Size(width, height), AudioVideoCore.PixelFormat.BGRA), shapeDrawBuffer.Ptr, num);
		shapeXorImage = new RawImage(new AudioVideoCore.ImageFormat(new Size(width, height), AudioVideoCore.PixelFormat.BGRA), shapeXorBuffer.Ptr, num);
	}

	private unsafe void ProcessShapeTypeColor(int width, int height, int stride)
	{
		int num = height * stride;
		shapeDrawBuffer.Enlarge(num);
		Buffer.MemoryCopy(shapeRawBuffer.Ptr, shapeDrawBuffer.Ptr, shapeDrawBuffer.Size, num);
		shapeDrawImage = new RawImage(new AudioVideoCore.ImageFormat(new Size(width, height), AudioVideoCore.PixelFormat.BGRA), shapeDrawBuffer.Ptr, stride);
		shapeXorImage = null;
	}

	private unsafe void ProcessShapeTypeMaskedColor(int width, int height, int stride)
	{
		int newSize = height * stride;
		shapeDrawBuffer.Enlarge(newSize);
		shapeXorBuffer.Enlarge(newSize);
		byte* ptr = shapeRawBuffer.Ptr;
		byte* ptr2 = shapeDrawBuffer.Ptr;
		byte* ptr3 = shapeXorBuffer.Ptr;
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				uint num = ((uint*)ptr)[j];
				if ((num & 0xFF000000u) == 0)
				{
					((int*)ptr2)[j] = (int)num | -16777216;
					((int*)ptr3)[j] = 0;
				}
				else
				{
					((int*)ptr2)[j] = 0;
					((int*)ptr3)[j] = (int)(num & 0xFFFFFF);
				}
			}
			ptr += stride;
			ptr2 += stride;
			ptr3 += stride;
		}
		shapeDrawImage = new RawImage(new AudioVideoCore.ImageFormat(new Size(width, height), AudioVideoCore.PixelFormat.BGRA), shapeDrawBuffer.Ptr, stride);
		shapeXorImage = new RawImage(new AudioVideoCore.ImageFormat(new Size(width, height), AudioVideoCore.PixelFormat.BGRA), shapeXorBuffer.Ptr, stride);
	}

	private void DrawPointerHighlight(RawImage image)
	{
		double scaleFactor = monitorInfo.GetScaleFactor();
		int num = (int)Math.Round(56.0 * scaleFactor);
		if (pointerHighlightBitmap != null && pointerHighlightBitmap.Width != num)
		{
			pointerHighlightBitmap.Dispose();
			pointerHighlightBitmap = null;
		}
		if (pointerHighlightBitmap == null)
		{
			pointerHighlightBitmap = new Bitmap(num, num);
			using Graphics graphics = Graphics.FromImage(pointerHighlightBitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			using SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
			graphics.FillEllipse(brush, 0, 0, num, num);
		}
		image.DrawImage(pointerHighlightBitmap, new Point(mouseTipPosition.X - pointerHighlightBitmap.Width / 2, mouseTipPosition.Y - pointerHighlightBitmap.Height / 2));
	}

	private unsafe void DrawClickHighlights(RawImage image)
	{
		long timestamp = stopwatch.GetTimestamp();
		ClickHighlight result;
		while (activeClickHighlights.TryPeek(out result) && result.ExpirationTimestamp <= timestamp)
		{
			activeClickHighlights.TryDequeue(out var _);
		}
		if (activeClickHighlights.Count <= 0)
		{
			return;
		}
		using MemoryHandle memoryHandle = image.Data[0].Pin();
		using Bitmap image2 = new Bitmap(image.Format.Width, image.Format.Height, image.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb, (IntPtr)memoryHandle.Pointer);
		using Graphics graphics = Graphics.FromImage(image2);
		graphics.SmoothingMode = SmoothingMode.HighQuality;
		double scaleFactor = monitorInfo.GetScaleFactor();
		foreach (ClickHighlight activeClickHighlight in activeClickHighlights)
		{
			long num = activeClickHighlight.ExpirationTimestamp - timestamp;
			long num2 = (long)(ClickHighlightDuration.TotalSeconds * (double)stopwatch.Frequency);
			double num3 = MathEx.Clamp((double)(num2 - num) / (double)num2, 0.0, 1.0);
			float num4 = (float)(48.0 * scaleFactor * num3);
			if ((double)num4 >= scaleFactor)
			{
				using Pen pen = new Pen(Color.FromArgb(MathEx.Clamp((int)(192.0 * (1.0 - ((num3 < 0.75) ? 0.0 : ((num3 - 0.75) * 4.0)))), 0, 255), Color.Red), (float)(4.0 * scaleFactor));
				RectangleF rect = new RectangleF((float)activeClickHighlight.Position.X - num4 / 2f, (float)activeClickHighlight.Position.Y - num4 / 2f, num4, num4);
				graphics.DrawEllipse(pen, rect);
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (mouseButtonMonitor != null)
			{
				mouseButtonMonitor.MouseButtonEvent -= MouseButtonEvent;
				mouseButtonMonitor.Dispose();
			}
			pointerHighlightBitmap?.Dispose();
			shapeRawBuffer.Dispose();
			shapeDrawBuffer.Dispose();
			shapeXorBuffer.Dispose();
			shapeDrawImage = null;
			shapeXorImage = null;
		}
	}
}
