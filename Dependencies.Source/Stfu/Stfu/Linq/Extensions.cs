using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stfu.Linq;

public static class Extensions
{
	private sealed class ChunkHelper<T> : IEnumerable<IEnumerable<T>>, IEnumerable
	{
		private readonly IEnumerable<T> m_elements;

		private readonly int m_size;

		private bool m_has_more;

		public ChunkHelper(IEnumerable<T> elements, int size)
		{
			m_elements = elements;
			m_size = size;
		}

		public IEnumerator<IEnumerable<T>> GetEnumerator()
		{
			using IEnumerator<T> enumerator = m_elements.GetEnumerator();
			m_has_more = enumerator.MoveNext();
			while (m_has_more)
			{
				yield return GetNextBatch(enumerator).ToList();
			}
		}

		private IEnumerable<T> GetNextBatch(IEnumerator<T> enumerator)
		{
			int i = 0;
			while (i < m_size)
			{
				yield return enumerator.Current;
				m_has_more = enumerator.MoveNext();
				if (!m_has_more)
				{
					break;
				}
				int num = i + 1;
				i = num;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public static IEnumerable<T> Yield<T>(this T element)
	{
		yield return element;
	}

	public static void ForAll<T>(this IEnumerable<T> elements, Action<T> fn)
	{
		foreach (T element in elements)
		{
			fn(element);
		}
	}

	public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> elements, int size)
	{
		return new ChunkHelper<T>(elements, size);
	}

	public static IEnumerable<(T Previous, T Current, T Next)> WithPreviousAndNext<T>(this IEnumerable<T> elements)
	{
		Queue<T> queue = new Queue<T>(2);
		queue.Enqueue(default);
		foreach (T e in elements)
		{
			if (queue.Count > 1)
			{
				yield return (Previous: queue.Dequeue(), Current: queue.Peek(), Next: e);
			}
			queue.Enqueue(e);
		}
		if (queue.Count > 1)
		{
			yield return (Previous: queue.Dequeue(), Current: queue.Peek(), Next: default);
		}
	}

	public static HashSet<T> ToHashSet<T>(this IEnumerable<T> elements, IEqualityComparer<T> comparer = null)
	{
		return new HashSet<T>(elements, comparer);
	}

	public static bool Contains<T>(this T[] array, T element) where T : IComparable<T>
	{
		return Array.Find(array, (T e) => element.CompareTo(e) == 0) != null;
	}
}
