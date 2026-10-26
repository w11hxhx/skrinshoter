using System;
using System.ComponentModel;
using System.Reflection;

namespace Common;

public static class EnumExtensions
{
	public static bool AreFlagsDefined(this Enum value)
	{
		char c = value.ToString()[0];
		if (char.IsDigit(c) || c == '-')
		{
			return false;
		}
		return true;
	}

	public static string GetDescription(this Enum value)
	{
		return (value.GetType().GetField(value.ToString())?.GetCustomAttribute<DescriptionAttribute>())?.Description ?? value.ToString();
	}
}
