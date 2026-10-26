using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoreLinq;

internal static class MoreEnumerable
{
	private sealed class ExtremaEnumerable<T, TKey>(IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, TKey, int> comparer) : IExtremaEnumerable<T>, IEnumerable<T>, IEnumerable
	{
		private static class Extrema
		{
			private sealed class FirstExtrema : Extrema<List<T>?, T>
			{
				public override List<T>? New()
				{
					return null;
				}

				public override void Restart(ref List<T>? store)
				{
					store = null;
				}

				public override IEnumerable<T> GetEnumerable(List<T>? store)
				{
					return store ?? Enumerable.Empty<T>();
				}

				public override void Add(ref List<T>? store, int? limit, T item)
				{
					if (!limit.HasValue || store == null || store.Count < limit)
					{
						(store ?? (store = new List<T>())).Add(item);
					}
				}
			}

			private sealed class LastExtrema : Extrema<Queue<T>?, T>
			{
				public override Queue<T>? New()
				{
					return null;
				}

				public override void Restart(ref Queue<T>? store)
				{
					store = null;
				}

				public override IEnumerable<T> GetEnumerable(Queue<T>? store)
				{
					return store ?? Enumerable.Empty<T>();
				}

				public override void Add(ref Queue<T>? store, int? limit, T item)
				{
					if (limit.HasValue)
					{
						int valueOrDefault = limit.GetValueOrDefault();
						Queue<T> queue = store;
						if (queue != null && queue.Count == valueOrDefault)
						{
							queue.Dequeue();
						}
					}
					(store ?? (store = new Queue<T>())).Enqueue(item);
				}
			}

			public static readonly Extrema<List<T>?, T> First = new FirstExtrema();

			public static readonly Extrema<Queue<T>?, T> Last = new LastExtrema();
		}

		private sealed class Extremum : Extrema<(bool, T), T>
		{
			public static readonly Extrema<(bool, T), T> First = new Extremum(poppable: false);

			public static readonly Extrema<(bool, T), T> Last = new Extremum(poppable: true);

			private readonly bool poppable;

			private Extremum(bool poppable)
			{
				this.poppable = poppable;
			}

			public override (bool, T) New()
			{
				return default;
			}

			public override void Restart(ref (bool, T) store)
			{
				store = default;
			}

			public override IEnumerable<T> GetEnumerable((bool, T) store)
			{
				if (store.Item1)
				{
					T item = store.Item2;
					return new _003C_003Ez__ReadOnlySingleElementList<T>(item);
				}
				return Array.Empty<T>();
			}

			public override void Add(ref (bool, T) store, int? limit, T item)
			{
				if (poppable || !store.Item1)
				{
					store = (true, item);
				}
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return source.ExtremaBy(Extrema.First, null, selector, comparer).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<T> Take(int count)
		{
			return count switch
			{
				0 => Array.Empty<T>(), 
				1 => source.ExtremaBy(Extremum.First, 1, selector, comparer), 
				_ => source.ExtremaBy(Extrema.First, count, selector, comparer), 
			};
		}

		public IEnumerable<T> TakeLast(int count)
		{
			return count switch
			{
				0 => Array.Empty<T>(), 
				1 => source.ExtremaBy(Extremum.Last, 1, selector, comparer), 
				_ => source.ExtremaBy(Extrema.Last, count, selector, comparer), 
			};
		}
	}

	private abstract class Extrema<TStore, T>
	{
		public abstract TStore New();

		public abstract void Restart(ref TStore store);

		public abstract IEnumerable<T> GetEnumerable(TStore store);

		public abstract void Add(ref TStore store, int? limit, T item);
	}

	public static T First<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.Take(1).AsEnumerable().First();
	}

	public static T? FirstOrDefault<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.Take(1).AsEnumerable().FirstOrDefault();
	}

	public static T Last<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.TakeLast(1).AsEnumerable().Last();
	}

	public static T? LastOrDefault<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.TakeLast(1).AsEnumerable().LastOrDefault();
	}

	public static T Single<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.Take(2).AsEnumerable().Single();
	}

	public static T? SingleOrDefault<T>(this IExtremaEnumerable<T> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		return source.Take(2).AsEnumerable().SingleOrDefault();
	}

	public static IExtremaEnumerable<TSource> Maxima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
	{
		return source.Maxima(selector, null);
	}

	public static IExtremaEnumerable<TSource> Maxima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey>? comparer)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		if (selector == null)
		{
			throw new ArgumentNullException("selector");
		}
		if (comparer == null)
		{
			comparer = Comparer<TKey>.Default;
		}
		return new ExtremaEnumerable<TSource, TKey>(source, selector, comparer.Compare);
	}

	private static IEnumerable<TSource> ExtremaBy<TSource, TKey, TStore>(this IEnumerable<TSource> source, Extrema<TStore, TSource> extrema, int? limit, Func<TSource, TKey> selector, Func<TKey, TKey, int> comparer)
	{
		foreach (TSource item in Extrema(source, extrema, limit, selector, comparer))
		{
			yield return item;
		}
		static IEnumerable<TSource> Extrema(IEnumerable<TSource> enumerable, Extrema<TStore, TSource> extrema2, int? limit2, Func<TSource, TKey> func, Func<TKey, TKey, int> func2)
		{
			using IEnumerator<TSource> enumerator2 = enumerable.GetEnumerator();
			if (!enumerator2.MoveNext())
			{
				return Array.Empty<TSource>();
			}
			TStore store = extrema2.New();
			extrema2.Add(ref store, limit2, enumerator2.Current);
			TKey arg = func(enumerator2.Current);
			while (enumerator2.MoveNext())
			{
				TSource current = enumerator2.Current;
				TKey val = func(current);
				int num = func2(val, arg);
				if (num <= 0)
				{
					if (num == 0)
					{
						extrema2.Add(ref store, limit2, current);
					}
				}
				else
				{
					extrema2.Restart(ref store);
					extrema2.Add(ref store, limit2, current);
					arg = val;
				}
			}
			return extrema2.GetEnumerable(store);
		}
	}

	public static IExtremaEnumerable<TSource> Minima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
	{
		return source.Minima(selector, null);
	}

	public static IExtremaEnumerable<TSource> Minima<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey>? comparer)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		if (selector == null)
		{
			throw new ArgumentNullException("selector");
		}
		if (comparer == null)
		{
			comparer = Comparer<TKey>.Default;
		}
		return new ExtremaEnumerable<TSource, TKey>(source, selector, (TKey x, TKey y) => -Math.Sign(comparer.Compare(x, y)));
	}
}
