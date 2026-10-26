using System;
using System.Windows;

namespace SkrinshoterApp;

public class RectangleMovingBehavior
{
	private Rect movingRectangle;

	private readonly Rect boundingRectangle;

	private Point mousePosition;

	private Point state;

	public RectangleMovingBehavior(Rect movingRectangle, Rect boundingRectangle, Point mousePosition)
	{
		this.movingRectangle = movingRectangle;
		this.boundingRectangle = boundingRectangle;
		this.mousePosition = mousePosition;
	}

	public Rect ProcessMouseMove(Point newMousePosition)
	{
		Rect rect = movingRectangle;
		rect.Offset(newMousePosition.X - mousePosition.X, newMousePosition.Y - mousePosition.Y);
		mousePosition = newMousePosition;
		double x = state.X;
		double y = state.Y;
		rect.X = MoveInsideRectangleXY(rect.X, rect.Right, boundingRectangle.X, boundingRectangle.Right, movingRectangle.X, ref x);
		rect.Y = MoveInsideRectangleXY(rect.Y, rect.Bottom, boundingRectangle.Y, boundingRectangle.Bottom, movingRectangle.Y, ref y);
		state.X = x;
		state.Y = y;
		movingRectangle = rect;
		return movingRectangle;
	}

	private static double MoveInsideRectangleXY(double movingRectangleXY, double movingRectangleRightBottom, double boundingRectangleXY, double boundingRectangleRightBottom, double previousMovingRectangleXY, ref double state)
	{
		double num = movingRectangleXY - previousMovingRectangleXY;
		if ((state < 0.0 && num > 0.0) || (state > 0.0 && num < 0.0))
		{
			double num2 = (double)Math.Sign(state) * Math.Min(Math.Abs(state), Math.Abs(num));
			movingRectangleXY += num2;
			movingRectangleRightBottom += num2;
			state -= num2;
		}
		if (movingRectangleXY < boundingRectangleXY)
		{
			double num3 = boundingRectangleXY - movingRectangleXY;
			movingRectangleXY += num3;
			movingRectangleRightBottom += num3;
			state -= num3;
		}
		if (movingRectangleRightBottom > boundingRectangleRightBottom)
		{
			double num4 = boundingRectangleRightBottom - movingRectangleRightBottom;
			movingRectangleXY += num4;
			state -= num4;
		}
		return movingRectangleXY;
	}
}
