using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using AudioVideoCore;
using Common;

namespace MousePointer;

public class MousePointerHighlighter : SafeDisposable, IMouseOverlay, IDisposable
{
	private const int HighlightDiameter = 56;

	private Bitmap highlightBitmap;

	public void Draw(RawImage image, Point imagePosition, MousePointerInfo mousePointerInfo)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (mousePointerInfo == null)
		{
			throw new ArgumentNullException("mousePointerInfo");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed || !mousePointerInfo.IsVisible)
			{
				return;
			}
			int num = (int)Math.Round(56.0 * mousePointerInfo.ScaleFactor);
			if (highlightBitmap != null && highlightBitmap.Width != num)
			{
				highlightBitmap.Dispose();
				highlightBitmap = null;
			}
			if (highlightBitmap == null)
			{
				highlightBitmap = new Bitmap(num, num);
				using Graphics graphics = Graphics.FromImage(highlightBitmap);
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				using SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
				graphics.FillEllipse(brush, 0, 0, num, num);
			}
			Point position = mousePointerInfo.Position;
			position.Offset(-imagePosition.X, -imagePosition.Y);
			image.DrawImage(highlightBitmap, new Point(position.X - highlightBitmap.Width / 2, position.Y - highlightBitmap.Height / 2));
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			highlightBitmap?.Dispose();
			highlightBitmap = null;
		}
	}
}
