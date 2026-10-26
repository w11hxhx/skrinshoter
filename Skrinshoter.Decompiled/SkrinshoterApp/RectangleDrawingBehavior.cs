using System;
using System.Windows;

namespace SkrinshoterApp;

public class RectangleDrawingBehavior
{
	private readonly Rect boundingRectangle;

	private readonly Point initialMousePosition;

	private readonly Size minSize;

	private readonly Size screenPixelSize;

	private Point? finalMousePosition;

	public RectangleDrawingBehavior(Rect boundingRectangle, Point initialMousePosition, Size minSize, Size screenPixelSize)
	{
		this.boundingRectangle = boundingRectangle;
		this.initialMousePosition = initialMousePosition.MoveInsideRect(boundingRectangle.Resize(0.0 - screenPixelSize.Width, 0.0 - screenPixelSize.Height));
		this.minSize = minSize;
		this.screenPixelSize = screenPixelSize;
	}

	public Rect ProcessMouseMove(Point newMousePosition, bool enforceSquare)
	{
		newMousePosition = newMousePosition.MoveInsideRect(boundingRectangle.Resize(0.0 - screenPixelSize.Width, 0.0 - screenPixelSize.Height));
		if (enforceSquare)
		{
			Vector vector = newMousePosition - initialMousePosition;
			if (Math.Abs(vector.X) < Math.Abs(vector.Y))
			{
				vector.Y = (double)Math.Sign(vector.Y) * Math.Abs(vector.X);
			}
			else
			{
				vector.X = (double)Math.Sign(vector.X) * Math.Abs(vector.Y);
			}
			newMousePosition = initialMousePosition + vector;
		}
		if (newMousePosition == initialMousePosition && !finalMousePosition.HasValue)
		{
			return new Rect(initialMousePosition, initialMousePosition);
		}
		finalMousePosition = newMousePosition;
		return new Rect(initialMousePosition, newMousePosition).Resize(screenPixelSize.Width, screenPixelSize.Height);
	}

	public Rect Complete()
	{
		if (!finalMousePosition.HasValue)
		{
			return new Rect(initialMousePosition, initialMousePosition);
		}
		double num = minSize.Width - screenPixelSize.Width;
		double num2 = minSize.Height - screenPixelSize.Height;
		Point point = initialMousePosition;
		Point value = finalMousePosition.Value;
		if (Math.Abs(value.X - point.X) < num)
		{
			if (value.X >= point.X)
			{
				value.X += num + point.X - value.X;
			}
			else
			{
				value.X += 0.0 - num + point.X - value.X;
			}
		}
		if (Math.Abs(value.Y - point.Y) < num2)
		{
			if (value.Y >= point.Y)
			{
				value.Y += num2 + point.Y - value.Y;
			}
			else
			{
				value.Y += 0.0 - num2 + point.Y - value.Y;
			}
		}
		return new Rect(point, value).Resize(screenPixelSize.Width, screenPixelSize.Height).MoveInsideRect(boundingRectangle);
	}
}
