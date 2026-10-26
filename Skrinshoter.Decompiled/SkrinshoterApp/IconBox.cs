using System.Windows;

namespace SkrinshoterApp;

public static class IconBox
{
	public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached("Width", typeof(double?), typeof(IconBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

	public static void SetWidth(DependencyObject obj, double? value)
	{
		obj.SetValue(WidthProperty, value);
	}

	public static double? GetWidth(DependencyObject obj)
	{
		return (double?)obj.GetValue(WidthProperty);
	}
}
