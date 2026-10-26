using System.Windows;

namespace SkrinshoterApp;

public sealed class RectangleVectorMovingBehavior
{
	private Rect movingRectangle;

	private readonly Rect boundingRectangle;

	public RectangleVectorMovingBehavior(Rect initialMovingRectangle, Rect boundingRectangle)
	{
		movingRectangle = initialMovingRectangle;
		this.boundingRectangle = boundingRectangle;
	}

	public Rect Move(Vector vector)
	{
		Rect movingRect = new Rect(movingRectangle.TopLeft + vector, movingRectangle.BottomRight + vector);
		movingRectangle = movingRect.MoveInsideRect(boundingRectangle);
		return movingRectangle;
	}
}
