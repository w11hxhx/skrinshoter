using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SkrinshoterApp;

public sealed class NullToUnsetValueConverter : IValueConverter
{
	public object Convert(object value, Type t, object p, CultureInfo c)
	{
		if (value != null)
		{
			return value;
		}
		return DependencyProperty.UnsetValue;
	}

	public object ConvertBack(object v, Type t, object p, CultureInfo c)
	{
		return Binding.DoNothing;
	}
}
