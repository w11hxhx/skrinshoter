using System;
using System.Globalization;
using System.Windows.Data;

namespace SkrinshoterApp;

public sealed class EnumToBooleanConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null || parameter == null)
		{
			return false;
		}
		Type type = value.GetType();
		object obj = ((parameter is string value2) ? Enum.Parse(type, value2) : parameter);
		return value.Equals(obj);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = default;
		int num;
		if (value is bool)
		{
			flag = (bool)value;
			num = 1;
		}
		else
		{
			num = 0;
		}
		if (((uint)num & (flag ? 1u : 0u)) != 0)
		{
			if (parameter == null)
			{
				return Binding.DoNothing;
			}
			if (parameter is string value2)
			{
				return Enum.Parse(targetType, value2);
			}
			return parameter;
		}
		return Binding.DoNothing;
	}
}
