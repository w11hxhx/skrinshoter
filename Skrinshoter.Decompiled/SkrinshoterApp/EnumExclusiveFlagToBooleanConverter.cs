using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SkrinshoterApp;

public class EnumExclusiveFlagToBooleanConverter : MarkupExtension, IValueConverter
{
	private object lastEnumValue;

	public object Mask { get; set; }

	public object Flag { get; set; }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return this;
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		lastEnumValue = value;
		if (value == null || Mask == null || Flag == null)
		{
			return false;
		}
		Type type = value.GetType();
		ulong num = ToUInt64(value, type);
		ulong num2 = ToUInt64(Mask, type);
		ulong num3 = ToUInt64(Flag, type);
		return (num & num2) == num3;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (!(value is bool) || !(bool)value || lastEnumValue == null || Mask == null || Flag == null)
		{
			return Binding.DoNothing;
		}
		ulong num = ToUInt64(lastEnumValue, targetType);
		ulong num2 = ToUInt64(Mask, targetType);
		ulong num3 = ToUInt64(Flag, targetType);
		ulong value2 = (num & ~num2) | num3;
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
