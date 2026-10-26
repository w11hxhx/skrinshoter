using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MouseHook;

namespace MousePointer;

public class MouseClickHighlighter : SafeDisposable, IMouseOverlay, IDisposable
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

	private const int HighlightMaxDiameter = 48;

	private static readonly TimeSpan HighlightDuration = TimeSpan.FromMilliseconds(500.0);

	private Point? imagePosition;

	private readonly StoppableStopwatch stopwatch;

	private readonly MouseButtonMonitor mouseButtonMonitor;

	private readonly ConcurrentQueue<ClickHighlight> activeHighlights = new ConcurrentQueue<ClickHighlight>();

	public MouseClickHighlighter(StoppableStopwatch stopwatch, ILoggerFactory loggerFactory)
	{
		this.stopwatch = stopwatch ?? throw new ArgumentNullException("stopwatch");
		try
		{
			mouseButtonMonitor = new MouseButtonMonitor((loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<MouseButtonMonitor>());
			mouseButtonMonitor.MouseButtonEvent += MouseButtonEvent;
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe void Draw(RawImage image, Point imagePosition, MousePointerInfo mousePointerInfo)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			this.imagePosition = imagePosition;
			long timestamp = stopwatch.GetTimestamp();
			ClickHighlight result;
			while (activeHighlights.TryPeek(out result) && result.ExpirationTimestamp <= timestamp)
			{
				activeHighlights.TryDequeue(out var _);
			}
			if (activeHighlights.IsEmpty)
			{
				return;
			}
			using MemoryHandle memoryHandle = image.Data[0].Pin();
			using Bitmap image2 = new Bitmap(image.Format.Width, image.Format.Height, image.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb, (IntPtr)memoryHandle.Pointer);
			using Graphics graphics = Graphics.FromImage(image2);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			double scaleFactor = mousePointerInfo.ScaleFactor;
			foreach (ClickHighlight activeHighlight in activeHighlights)
			{
				long num = activeHighlight.ExpirationTimestamp - timestamp;
				long num2 = (long)(HighlightDuration.TotalSeconds * (double)stopwatch.Frequency);
				double num3 = MathEx.Clamp((double)(num2 - num) / (double)num2, 0.0, 1.0);
				float num4 = (float)(48.0 * scaleFactor * num3);
				if ((double)num4 >= scaleFactor)
				{
					using Pen pen = new Pen(Color.FromArgb(MathEx.Clamp((int)(192.0 * (1.0 - ((num3 < 0.75) ? 0.0 : ((num3 - 0.75) * 4.0)))), 0, 255), Color.Red), (float)(4.0 * scaleFactor));
					RectangleF rect = new RectangleF((float)activeHighlight.Position.X - num4 / 2f, (float)activeHighlight.Position.Y - num4 / 2f, num4, num4);
					graphics.DrawEllipse(pen, rect);
				}
			}
		}
	}

	private void MouseButtonEvent(object sender, MouseButtonEventArgs e)
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed && imagePosition.HasValue && e.ButtonState == MouseButtonState.Released && stopwatch.IsStarted)
			{
				Point position = e.Position;
				position.Offset(-imagePosition.Value.X, -imagePosition.Value.Y);
				activeHighlights.Enqueue(new ClickHighlight(position, stopwatch.GetTimestamp() + (long)(HighlightDuration.TotalSeconds * (double)stopwatch.Frequency)));
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && mouseButtonMonitor != null)
		{
			mouseButtonMonitor.MouseButtonEvent -= MouseButtonEvent;
			mouseButtonMonitor.Dispose();
		}
	}
}
