using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Stfu.Wpf;

public static class Behaviors
{
	public static readonly DependencyProperty SmoothScrollingProperty = DependencyProperty.RegisterAttached("SmoothScrolling", typeof(bool), typeof(Behaviors), new UIPropertyMetadata(false, SmoothScrollingChanged));

	public static bool GetSmoothScrolling(DependencyObject o)
	{
		return (bool)o.GetValue(SmoothScrollingProperty);
	}

	public static void SetSmoothScrolling(DependencyObject o, bool val)
	{
		o.SetValue(SmoothScrollingProperty, val);
	}

	private static void SmoothScrollingChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		if (o is VirtualizingPanel || o is ItemsControl)
		{
			Type type = typeof(Window).Assembly.GetType("System.Windows.Controls.ScrollUnit");
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;
			FieldInfo field = typeof(VirtualizingPanel).GetField("ScrollUnitProperty", bindingAttr);
			if (type != null && field?.GetValue(null) is DependencyProperty dp)
			{
				o.SetValue(dp, Enum.Parse(type, ((bool)e.NewValue) ? "Pixel" : "Item"));
			}
			else if (o is VirtualizingPanel)
			{
				BindingFlags bindingAttr2 = BindingFlags.Instance | BindingFlags.NonPublic;
				o.GetType().GetProperty("IsPixelBased", bindingAttr2)?.SetValue(o, (bool)e.NewValue, null);
			}
		}
	}
}
