using System;
using System.Windows;

namespace SkrinshoterApp;

public class TwoPointObjectSizingBehavior
{
	public Point InitialStartPoint { get; }

	public Point InitialEndPoint { get; }

	public LineHandle ResizeHandle { get; }

	public Point InitialMousePosition { get; }

	public TwoPointObjectSizingBehavior(Point initialStartPoint, Point initialEndPoint, LineHandle resizeHandle, Point initialMousePosition)
	{
		InitialStartPoint = initialStartPoint;
		InitialEndPoint = initialEndPoint;
		ResizeHandle = resizeHandle;
		InitialMousePosition = initialMousePosition;
	}

	public (Point startPoint, Point endPoint) ProcessMouseMove(Point newMousePosition, bool snapToAxis)
	{
		Vector vector = newMousePosition - InitialMousePosition;
		Point point;
		Point point2;
		if (ResizeHandle == LineHandle.Start)
		{
			point = InitialStartPoint + vector;
			point2 = InitialEndPoint;
		}
		else
		{
			point = InitialEndPoint + vector;
			point2 = InitialStartPoint;
		}
		if (snapToAxis)
		{
			Vector vector2 = point - point2;
			if (Math.Abs(vector2.X) > Math.Abs(vector2.Y))
			{
				point.Y = point2.Y;
			}
			else
			{
				point.X = point2.X;
			}
		}
		if (ResizeHandle != LineHandle.Start)
		{
			return (startPoint: point2, endPoint: point);
		}
		return (startPoint: point, endPoint: point2);
	}
}
