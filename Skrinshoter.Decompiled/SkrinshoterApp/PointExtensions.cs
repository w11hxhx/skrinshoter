using System;
using System.Drawing;
using System.Windows;

namespace SkrinshoterApp;

public static class PointExtensions
{
	public static System.Windows.Point MoveInsideRect(this System.Windows.Point movingPoint, Rect boundingRect)
	{
		if (movingPoint.X < boundingRect.Left)
		{
			movingPoint.X = boundingRect.Left;
		}
		if (movingPoint.X > boundingRect.Right)
		{
			movingPoint.X = boundingRect.Right;
		}
		if (movingPoint.Y < boundingRect.Top)
		{
			movingPoint.Y = boundingRect.Top;
		}
		if (movingPoint.Y > boundingRect.Bottom)
		{
			movingPoint.Y = boundingRect.Bottom;
		}
		return movingPoint;
	}

	public static System.Drawing.Point ToIntPoint(this System.Windows.Point point)
	{
		return new System.Drawing.Point((int)Math.Round(point.X), (int)Math.Round(point.Y));
	}

	public static bool AlmostEquals(this System.Windows.Point a, System.Windows.Point b)
	{
		if (Math.Abs(a.X - b.X) < 0.05)
		{
			return Math.Abs(a.Y - b.Y) < 0.05;
		}
		return false;
	}
}
