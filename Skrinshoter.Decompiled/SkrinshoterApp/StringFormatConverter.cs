using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SkrinshoterApp;

public class StringFormatConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length >= 2 && values[0] is string format)
		{
			return string.Format(format, values.Skip(1).ToArray());
		}
		return string.Empty;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
