using System;

namespace SkrinshoterApp.Common;

public static class StringExtensions
{
	public static int CountSubstring(this string str, string subStr, StringComparison comparison = StringComparison.Ordinal)
	{
		if (str == null)
		{
			throw new ArgumentNullException("str");
		}
		if (subStr == null)
		{
			throw new ArgumentNullException("subStr");
		}
		if (subStr.Length == 0)
		{
			return 0;
		}
		int num = 0;
		int startIndex = 0;
		while (true)
		{
			startIndex = str.IndexOf(subStr, startIndex, comparison);
			if (startIndex < 0)
			{
				break;
			}
			num++;
			startIndex += subStr.Length;
		}
		return num;
	}
}
