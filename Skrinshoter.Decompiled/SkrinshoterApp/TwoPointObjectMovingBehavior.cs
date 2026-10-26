using System.Windows;

namespace SkrinshoterApp;

public class TwoPointObjectMovingBehavior
{
	public Point InitialStartPoint { get; }

	public Point InitialEndPoint { get; }

	public Point InitialMousePosition { get; }

	public TwoPointObjectMovingBehavior(Point initialStartPoint, Point initialEndPoint, Point initialMousePosition)
	{
		InitialStartPoint = initialStartPoint;
		InitialEndPoint = initialEndPoint;
		InitialMousePosition = initialMousePosition;
	}

	public (Point startPoint, Point endPoint) ProcessMouseMove(Point newMousePosition)
	{
		Vector vector = newMousePosition - InitialMousePosition;
		return (startPoint: InitialStartPoint + vector, endPoint: InitialEndPoint + vector);
	}
}
