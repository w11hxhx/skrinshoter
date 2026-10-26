using System;
using System.Windows;
using Common;

namespace SkrinshoterApp;

public class RectangleResizingBehavior
{
	private readonly Rect initialResizingRectangle;

	private readonly RectHandle resizeHandle;

	private readonly Point initialMousePosition;

	private readonly Size minSize;

	private readonly Size maxSize;

	public RectangleResizingBehavior(Rect initialResizingRectangle, Rect boundingRectangle, RectHandle resizeHandle, Point initialMousePosition, Size minSize)
	{
		this.initialResizingRectangle = initialResizingRectangle;
		this.resizeHandle = resizeHandle;
		this.initialMousePosition = initialMousePosition;
		this.minSize = minSize;
		maxSize = CalculateMaxSize(initialResizingRectangle, boundingRectangle, resizeHandle);
	}

	public Rect ProcessMouseMove(Point newMousePosition, bool enforceSquare)
	{
		Vector vector = newMousePosition - initialMousePosition;
		double width;
		double height;
		double x;
		double y;
		switch (resizeHandle)
		{
		case RectHandle.Left:
			width = MathEx.Clamp(initialResizingRectangle.Width - vector.X, minSize.Width, maxSize.Width);
			height = initialResizingRectangle.Height;
			x = initialResizingRectangle.Right - width;
			y = initialResizingRectangle.Y;
			break;
		case RectHandle.Right:
			width = MathEx.Clamp(initialResizingRectangle.Width + vector.X, minSize.Width, maxSize.Width);
			height = initialResizingRectangle.Height;
			x = initialResizingRectangle.X;
			y = initialResizingRectangle.Y;
			break;
		case RectHandle.Top:
			width = initialResizingRectangle.Width;
			height = MathEx.Clamp(initialResizingRectangle.Height - vector.Y, minSize.Height, maxSize.Height);
			x = initialResizingRectangle.X;
			y = initialResizingRectangle.Bottom - height;
			break;
		case RectHandle.Bottom:
			width = initialResizingRectangle.Width;
			height = MathEx.Clamp(initialResizingRectangle.Height + vector.Y, minSize.Height, maxSize.Height);
			x = initialResizingRectangle.X;
			y = initialResizingRectangle.Y;
			break;
		case RectHandle.TopLeft:
			width = MathEx.Clamp(initialResizingRectangle.Width - vector.X, minSize.Width, maxSize.Width);
			height = MathEx.Clamp(initialResizingRectangle.Height - vector.Y, minSize.Height, maxSize.Height);
			if (enforceSquare)
			{
				EnforceSquare(ref width, ref height);
			}
			x = initialResizingRectangle.Right - width;
			y = initialResizingRectangle.Bottom - height;
			break;
		case RectHandle.TopRight:
			width = MathEx.Clamp(initialResizingRectangle.Width + vector.X, minSize.Width, maxSize.Width);
			height = MathEx.Clamp(initialResizingRectangle.Height - vector.Y, minSize.Height, maxSize.Height);
			if (enforceSquare)
			{
				EnforceSquare(ref width, ref height);
			}
			x = initialResizingRectangle.X;
			y = initialResizingRectangle.Bottom - height;
			break;
		case RectHandle.BottomLeft:
			width = MathEx.Clamp(initialResizingRectangle.Width - vector.X, minSize.Width, maxSize.Width);
			height = MathEx.Clamp(initialResizingRectangle.Height + vector.Y, minSize.Height, maxSize.Height);
			if (enforceSquare)
			{
				EnforceSquare(ref width, ref height);
			}
			x = initialResizingRectangle.Right - width;
			y = initialResizingRectangle.Y;
			break;
		case RectHandle.BottomRight:
			width = MathEx.Clamp(initialResizingRectangle.Width + vector.X, minSize.Width, maxSize.Width);
			height = MathEx.Clamp(initialResizingRectangle.Height + vector.Y, minSize.Height, maxSize.Height);
			if (enforceSquare)
			{
				EnforceSquare(ref width, ref height);
			}
			x = initialResizingRectangle.X;
			y = initialResizingRectangle.Y;
			break;
		default:
			throw new InvalidOperationException($"Unknown resize handle: {resizeHandle}.");
		}
		return new Rect(x, y, width, height);
	}

	private static Size CalculateMaxSize(Rect initialResizingRectangle, Rect boundingRectangle, RectHandle resizeHandle)
	{
		double width = 0.0;
		double height = 0.0;
		switch (resizeHandle)
		{
		case RectHandle.Left:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			break;
		case RectHandle.Right:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			break;
		case RectHandle.Top:
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case RectHandle.Bottom:
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		case RectHandle.TopLeft:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case RectHandle.TopRight:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case RectHandle.BottomLeft:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		case RectHandle.BottomRight:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		default:
			throw new InvalidOperationException($"Unknown resize handle: {resizeHandle}.");
		}
		return new Size(width, height);
	}

	private static void EnforceSquare(ref double width, ref double height)
	{
		if (width < height)
		{
			height = width;
		}
		else
		{
			width = height;
		}
	}
}
