using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SkrinshoterApp;

public class ColorToBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is Color color)
		{
			if (parameter != null && double.TryParse(parameter.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
			{
				color = Color.FromArgb((byte)(result * 255.0), color.R, color.G, color.B);
			}
			return new SolidColorBrush(color);
		}
		return Brushes.Transparent;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is SolidColorBrush solidColorBrush)
		{
			return solidColorBrush.Color;
		}
		return Colors.Transparent;
	}
}
