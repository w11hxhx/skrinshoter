using System.Drawing;
using System.Windows;

namespace SkrinshoterApp;

public static class RectangleExtensions
{
	public static Rect ToRect(this Rectangle rectangle)
	{
		return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
	}
}
