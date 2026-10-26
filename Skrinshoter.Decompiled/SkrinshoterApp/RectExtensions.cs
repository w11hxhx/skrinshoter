using System;
using System.Drawing;
using System.Windows;

namespace SkrinshoterApp;

public static class RectExtensions
{
	public static Rect Resize(this Rect rect, double deltaWidth, double deltaHeight)
	{
		return new Rect(rect.Location, new System.Windows.Size(rect.Size.Width + deltaWidth, rect.Size.Height + deltaHeight));
	}

	public static Rect MoveInsideRect(this Rect movingRect, Rect boundingRect)
	{
		if (movingRect.Left < boundingRect.Left)
		{
			movingRect.X += boundingRect.Left - movingRect.Left;
		}
		if (movingRect.Right > boundingRect.Right)
		{
			movingRect.X += boundingRect.Right - movingRect.Right;
		}
		if (movingRect.Top < boundingRect.Top)
		{
			movingRect.Y += boundingRect.Top - movingRect.Top;
		}
		if (movingRect.Bottom > boundingRect.Bottom)
		{
			movingRect.Y += boundingRect.Bottom - movingRect.Bottom;
		}
		return movingRect;
	}

	public static Rectangle ToRectangle(this Rect rect)
	{
		System.Drawing.Point point = new System.Drawing.Point((int)Math.Round(rect.X), (int)Math.Round(rect.Y));
		System.Drawing.Point point2 = new System.Drawing.Point((int)Math.Round(rect.Right), (int)Math.Round(rect.Bottom));
		return new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
	}

	public static bool HasNonZeroIntersectionWith(this Rect rect1, Rect rect2)
	{
		rect1.Intersect(rect2);
		if (rect1.Width > 0.0)
		{
			return rect1.Height > 0.0;
		}
		return false;
	}
}
