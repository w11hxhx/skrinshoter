using System.Windows;

namespace SkrinshoterApp;

public class OnePointObjectMovingBehavior
{
	public Point InitialPoint { get; }

	public Point InitialMousePosition { get; }

	public OnePointObjectMovingBehavior(Point initialPoint, Point initialMousePosition)
	{
		InitialPoint = initialPoint;
		InitialMousePosition = initialMousePosition;
	}

	public Point ProcessMouseMove(Point newMousePosition)
	{
		Vector vector = newMousePosition - InitialMousePosition;
		return InitialPoint + vector;
	}
}
