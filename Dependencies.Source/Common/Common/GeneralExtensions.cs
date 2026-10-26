using System.Linq;

namespace Common;

public static class GeneralExtensions
{
	public static bool In<T>(this T item, params T[] items)
	{
		return items.Contains(item);
	}
}
