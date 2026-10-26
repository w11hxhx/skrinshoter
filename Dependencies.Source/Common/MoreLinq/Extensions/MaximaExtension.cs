using System;
using System.Collections.Generic;

namespace MoreLinq.Extensions;

public static class MaximaExtension
{
	public static IExtremaEnumerable<TSource> Maxima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
	{
		return MoreEnumerable.Maxima(source, selector);
	}

	public static IExtremaEnumerable<TSource> Maxima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey>? comparer)
	{
		return MoreEnumerable.Maxima(source, selector, comparer);
	}
}
