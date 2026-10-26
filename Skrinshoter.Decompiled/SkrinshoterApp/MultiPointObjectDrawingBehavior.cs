using System;
using System.Windows;

namespace SkrinshoterApp;

public class MultiPointObjectDrawingBehavior
{
	private enum MouseDirection
	{
		Any,
		Horizontal,
		Vertical
	}

	private const double AxisSnapThreshold = 2.0;

	private MouseDirection direction;

	private Point axisSnapStartPoint;

	public MultiPointObjectDrawingBehavior(Point initialMousePosition)
	{
		axisSnapStartPoint = initialMousePosition;
	}

	public Point ProcessMouseMove(Point newMousePosition, bool snapToAxis)
	{
		if (!snapToAxis)
		{
			direction = MouseDirection.Any;
			axisSnapStartPoint = newMousePosition;
			return newMousePosition;
		}
		if (direction == MouseDirection.Any)
		{
			if (Math.Abs(newMousePosition.X - axisSnapStartPoint.X) >= 2.0)
			{
				direction = MouseDirection.Horizontal;
			}
			else
			{
				if (!(Math.Abs(newMousePosition.Y - axisSnapStartPoint.Y) >= 2.0))
				{
					return axisSnapStartPoint;
				}
				direction = MouseDirection.Vertical;
			}
		}
		if (direction != MouseDirection.Horizontal)
		{
			return new Point(axisSnapStartPoint.X, newMousePosition.Y);
		}
		return new Point(newMousePosition.X, axisSnapStartPoint.Y);
	}
}
