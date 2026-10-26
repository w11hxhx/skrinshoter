using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CalcBinding;

public class BoolToVisibilityConverter : IValueConverter
{
	public FalseToVisibility FalseToVisibility { get; set; }

	public BoolToVisibilityConverter()
	{
		FalseToVisibility = FalseToVisibility.Collapsed;
	}

	public BoolToVisibilityConverter(FalseToVisibility falseToVisibility)
	{
		FalseToVisibility = falseToVisibility;
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if ((value is bool) ? ((bool)value) : ((bool)(dynamic)value))
		{
			return Visibility.Visible;
		}
		return (FalseToVisibility != FalseToVisibility.Collapsed) ? Visibility.Hidden : Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (Visibility)value == Visibility.Visible;
	}
}
