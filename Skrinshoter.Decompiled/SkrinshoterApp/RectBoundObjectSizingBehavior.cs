using System;
using System.Windows;

namespace SkrinshoterApp;

public class RectBoundObjectSizingBehavior
{
	public Rect InitialRect { get; }

	public RectHandle ResizeHandle { get; }

	public Point InitialMousePosition { get; }

	public RectBoundObjectSizingBehavior(Rect initialRect, RectHandle resizeHandle, Point initialMousePosition)
	{
		InitialRect = initialRect;
		ResizeHandle = resizeHandle;
		InitialMousePosition = initialMousePosition;
	}

	public Rect ProcessMouseMove(Point newMousePosition, bool enforceSquare)
	{
		Vector vector = newMousePosition - InitialMousePosition;
		return ResizeHandle switch
		{
			RectHandle.Left => new Rect(InitialRect.TopLeft + new Vector(vector.X, 0.0), InitialRect.BottomRight), 
			RectHandle.Right => new Rect(InitialRect.TopLeft, InitialRect.BottomRight + new Vector(vector.X, 0.0)), 
			RectHandle.Top => new Rect(InitialRect.TopLeft + new Vector(0.0, vector.Y), InitialRect.BottomRight), 
			RectHandle.Bottom => new Rect(InitialRect.TopLeft, InitialRect.BottomRight + new Vector(0.0, vector.Y)), 
			RectHandle.TopLeft => CreateAnchoredRect(InitialRect.BottomRight, InitialRect.TopLeft + vector, enforceSquare), 
			RectHandle.TopRight => CreateAnchoredRect(InitialRect.BottomLeft, InitialRect.TopRight + vector, enforceSquare), 
			RectHandle.BottomLeft => CreateAnchoredRect(InitialRect.TopRight, InitialRect.BottomLeft + vector, enforceSquare), 
			RectHandle.BottomRight => CreateAnchoredRect(InitialRect.TopLeft, InitialRect.BottomRight + vector, enforceSquare), 
			_ => throw new InvalidOperationException($"Unknown resize handle: {ResizeHandle}."), 
		};
	}

	private Rect CreateAnchoredRect(Point anchorPoint, Point dragPoint, bool enforceSquare)
	{
		if (!enforceSquare)
		{
			return new Rect(anchorPoint, dragPoint);
		}
		Vector vector = dragPoint - anchorPoint;
		double num = Math.Min(Math.Abs(vector.X), Math.Abs(vector.Y));
		double x = (double)Math.Sign(vector.X) * num;
		double y = (double)Math.Sign(vector.Y) * num;
		return new Rect(anchorPoint, anchorPoint + new Vector(x, y));
	}
}
