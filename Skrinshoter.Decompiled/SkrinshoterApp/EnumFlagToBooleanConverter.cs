using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SkrinshoterApp;

public sealed class EnumFlagToBooleanConverter : MarkupExtension, IValueConverter
{
	private object lastEnumValue;

	public object Flag { get; set; }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return this;
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		lastEnumValue = value;
		if (value == null || Flag == null)
		{
			return false;
		}
		Type type = value.GetType();
		ulong num = ToUInt64(value, type);
		ulong num2 = ToUInt64(Flag, type);
		return (num & num2) == num2;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (!(value is bool flag) || Flag == null || lastEnumValue == null)
		{
			return Binding.DoNothing;
		}
		ulong num = ToUInt64(lastEnumValue, targetType);
		ulong num2 = ToUInt64(Flag, targetType);
		ulong value2 = (flag ? (num | num2) : (num & ~num2));
		return lastEnumValue = Enum.ToObject(targetType, value2);
	}

	private static ulong ToUInt64(object enumLike, Type enumType)
	{
		if (enumLike == null)
		{
			return 0uL;
		}
		if (enumLike is string value)
		{
			enumLike = Enum.Parse(enumType, value);
		}
		if (enumLike.GetType().IsEnum && enumLike.GetType() != enumType)
		{
			enumLike = Enum.ToObject(enumType, System.Convert.ToUInt64(enumLike));
		}
		return System.Convert.ToUInt64(enumLike);
	}
}
