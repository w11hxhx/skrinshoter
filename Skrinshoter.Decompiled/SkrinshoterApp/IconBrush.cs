using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class IconBrush
{
	public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Brush), typeof(IconBrush), new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.Inherits));

	public static void SetValue(DependencyObject obj, Brush value)
	{
		obj.SetValue(ValueProperty, value);
	}

	public static Brush GetValue(DependencyObject obj)
	{
		return (Brush)obj.GetValue(ValueProperty);
	}
}
