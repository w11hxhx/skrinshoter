using System.Windows;

namespace SkrinshoterApp;

public static class ComboBoxHelper
{
	public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.RegisterAttached("PlaceholderText", typeof(string), typeof(ComboBoxHelper), new PropertyMetadata(null));

	public static string GetPlaceholderText(DependencyObject obj)
	{
		return (string)obj.GetValue(PlaceholderTextProperty);
	}

	public static void SetPlaceholderText(DependencyObject obj, string value)
	{
		obj.SetValue(PlaceholderTextProperty, value);
	}
}
