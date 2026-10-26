using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class IconProperties
{
	public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(ImageSource), typeof(IconProperties), new FrameworkPropertyMetadata(null));

	public static void SetIcon(DependencyObject element, ImageSource value)
	{
		element.SetValue(IconProperty, value);
	}

	public static ImageSource GetIcon(DependencyObject element)
	{
		return (ImageSource)element.GetValue(IconProperty);
	}
}
