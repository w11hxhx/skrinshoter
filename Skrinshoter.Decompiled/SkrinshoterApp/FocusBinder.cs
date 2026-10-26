using System;
using System.Windows;
using System.Windows.Threading;

namespace SkrinshoterApp;

public static class FocusBinder
{
	public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(FocusBinder), new PropertyMetadata(false, OnIsFocusedChanged));

	public static void SetIsFocused(DependencyObject obj, bool value)
	{
		obj.SetValue(IsFocusedProperty, value);
	}

	public static bool GetIsFocused(DependencyObject obj)
	{
		return (bool)obj.GetValue(IsFocusedProperty);
	}

	private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		UIElement element = d as UIElement;
		if (element == null || !(bool)e.NewValue)
		{
			return;
		}
		element.Dispatcher.BeginInvoke((Action)(() =>
		{
			if (element.Focusable && !element.IsKeyboardFocusWithin)
			{
				element.Focus();
			}
		}), DispatcherPriority.Input);
	}
}
