using System.Windows;

namespace SkrinshoterApp;

public static class MouseOverProxy
{
	public static readonly DependencyProperty ExternalMouseOverProperty = DependencyProperty.RegisterAttached("ExternalMouseOver", typeof(bool), typeof(MouseOverProxy), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

	public static void SetExternalMouseOver(DependencyObject element, bool value)
	{
		element.SetValue(ExternalMouseOverProperty, value);
	}

	public static bool GetExternalMouseOver(DependencyObject element)
	{
		return (bool)element.GetValue(ExternalMouseOverProperty);
	}
}
