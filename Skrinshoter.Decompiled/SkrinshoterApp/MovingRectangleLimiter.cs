using System;
using System.Drawing;

namespace SkrinshoterApp;

public class MovingRectangleLimiter
{
	private Point previousMovingRectangleLocation;

	private readonly Rectangle boundingRectangle;

	private Point state;

	public MovingRectangleLimiter(Point initialMovingRectangleLocation, Rectangle boundingRectangle)
	{
		previousMovingRectangleLocation = initialMovingRectangleLocation;
		this.boundingRectangle = boundingRectangle;
	}

	public Rectangle Limit(Rectangle movingRectangle)
	{
		int x = state.X;
		int y = state.Y;
		movingRectangle.X = MoveInsideRectangleXY(movingRectangle.X, movingRectangle.Right, boundingRectangle.X, boundingRectangle.Right, previousMovingRectangleLocation.X, ref x);
		movingRectangle.Y = MoveInsideRectangleXY(movingRectangle.Y, movingRectangle.Bottom, boundingRectangle.Y, boundingRectangle.Bottom, previousMovingRectangleLocation.Y, ref y);
		state.X = x;
		state.Y = y;
		previousMovingRectangleLocation = movingRectangle.Location;
		return movingRectangle;
	}

	private static int MoveInsideRectangleXY(int movingRectangleXY, int movingRectangleRightBottom, int boundingRectangleXY, int boundingRectangleRightBottom, int previousMovingRectangleXY, ref int state)
	{
		int num = movingRectangleXY - previousMovingRectangleXY;
		if ((state < 0 && num > 0) || (state > 0 && num < 0))
		{
			int num2 = Math.Sign(state) * Math.Min(Math.Abs(state), Math.Abs(num));
			movingRectangleXY += num2;
			movingRectangleRightBottom += num2;
			state -= num2;
		}
		if (movingRectangleXY < boundingRectangleXY)
		{
			int num3 = boundingRectangleXY - movingRectangleXY;
			movingRectangleXY += num3;
			movingRectangleRightBottom += num3;
			state -= num3;
		}
		if (movingRectangleRightBottom > boundingRectangleRightBottom)
		{
			int num4 = boundingRectangleRightBottom - movingRectangleRightBottom;
			movingRectangleXY += num4;
			state -= num4;
		}
		return movingRectangleXY;
	}
}
