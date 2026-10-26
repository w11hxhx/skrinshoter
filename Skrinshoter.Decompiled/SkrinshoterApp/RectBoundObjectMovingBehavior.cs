using System.Windows;

namespace SkrinshoterApp;

public class RectBoundObjectMovingBehavior
{
	public Rect InitialRect { get; }

	public Point InitialMousePosition { get; }

	public RectBoundObjectMovingBehavior(Rect initialRect, Point initialMousePosition)
	{
		InitialRect = initialRect;
		InitialMousePosition = initialMousePosition;
	}

	public Rect ProcessMouseMove(Point newMousePosition)
	{
		Vector vector = newMousePosition - InitialMousePosition;
		return new Rect(InitialRect.TopLeft + vector, InitialRect.BottomRight + vector);
	}
}
