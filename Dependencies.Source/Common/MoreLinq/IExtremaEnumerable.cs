using System.Collections;
using System.Collections.Generic;

namespace MoreLinq;

public interface IExtremaEnumerable<out T> : IEnumerable<T>, IEnumerable
{
	IEnumerable<T> Take(int count);

	IEnumerable<T> TakeLast(int count);
}
