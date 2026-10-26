using System.Collections.Generic;

namespace CalcBinding.Inversion;

internal class Dictionary<TKey1, TKey2, TValue> : Dictionary<TKey1, Dictionary<TKey2, TValue>>
{
	public virtual TValue this[TKey1 key1, TKey2 key2] => base[key1][key2];

	public void Add(TKey1 key1, TKey2 key2, TValue value)
	{
		if (!ContainsKey(key1))
		{
			Add(key1, new Dictionary<TKey2, TValue>());
		}
		base[key1].Add(key2, value);
	}
}
