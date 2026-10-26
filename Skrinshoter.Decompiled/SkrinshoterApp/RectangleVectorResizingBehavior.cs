using System.Windows;
using Common;

namespace SkrinshoterApp;

public sealed class RectangleVectorResizingBehavior
{
	private Rect resizingRectangle;

	private readonly Size minSize;

	private readonly Size maxSize;

	public RectangleVectorResizingBehavior(Rect initialResizingRectangle, Rect boundingRectangle, Size minSize)
	{
		resizingRectangle = initialResizingRectangle;
		this.minSize = minSize;
		maxSize = CalculateMaxSize(initialResizingRectangle, boundingRectangle);
	}

	public Rect Resize(Vector vector)
	{
		resizingRectangle.Width = MathEx.Clamp(resizingRectangle.Width + vector.X, minSize.Width, maxSize.Width);
		resizingRectangle.Height = MathEx.Clamp(resizingRectangle.Height + vector.Y, minSize.Height, maxSize.Height);
		return resizingRectangle;
	}

	private static Size CalculateMaxSize(Rect initialResizingRectangle, Rect boundingRectangle)
	{
		double width = boundingRectangle.Right - initialResizingRectangle.Left;
		double height = boundingRectangle.Bottom - initialResizingRectangle.Top;
		return new Size(width, height);
	}
}
