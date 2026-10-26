using System;
using System.Collections.Generic;

namespace MoreLinq.Extensions;

public static class MinimaExtension
{
	public static IExtremaEnumerable<TSource> Minima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
	{
		return MoreEnumerable.Minima(source, selector);
	}

	public static IExtremaEnumerable<TSource> Minima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey>? comparer)
	{
		return MoreEnumerable.Minima(source, selector, comparer);
	}
}
