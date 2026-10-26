using System;
using System.Globalization;
using System.Windows.Data;

namespace SkrinshoterApp;

public class IsLessThanConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		IComparable obj = value as IComparable;
		IComparable comparable = parameter as IComparable;
		if (obj == null || comparable == null)
		{
			throw new FormatException("To use this converter, the value and parameter must inherit from IComparable.");
		}
		return obj.CompareTo(comparable) < 0;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
