using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class VisualExtensions
{
	public static Size GetScreenPixelSize(this Visual visual)
	{
		Point point = new Point(0.0, 0.0);
		Point point2 = visual.PointToScreen(point);
		point2.Offset(1.0, 1.0);
		point2 = visual.PointFromScreen(point2);
		return new Rect(point, point2).Size;
	}

	public static Rect RectToScreen(this Visual visual, Rect rect)
	{
		return new Rect(visual.PointToScreen(rect.TopLeft), visual.PointToScreen(rect.BottomRight));
	}

	public static Rect RectFromScreen(this Visual visual, Rect rect)
	{
		return new Rect(visual.PointFromScreen(rect.TopLeft), visual.PointFromScreen(rect.BottomRight));
	}

	public static double ValueToScreen(this Visual visual, double value)
	{
		return visual.PointToScreen(new Point(value, 0.0)).X - visual.PointToScreen(new Point(0.0, 0.0)).X;
	}
}
