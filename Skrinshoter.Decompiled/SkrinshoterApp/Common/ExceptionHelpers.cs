using System;
using System.Collections.Generic;
using System.Linq;

namespace SkrinshoterApp.Common;

public static class ExceptionHelpers
{
	public static string AggregateMessages(this Exception ex)
	{
		return string.Join(" --> ", from e in ex.GetInnerExceptions()
			select e.GetType()?.ToString() + ": " + e.Message);
	}

	public static IEnumerable<Exception> GetInnerExceptions(this Exception ex, int maxDepth = 10)
	{
		if (ex == null || maxDepth <= 0)
		{
			yield break;
		}
		yield return ex;
		if (ex is AggregateException { InnerExceptions: var innerExceptions })
		{
			foreach (Exception item in innerExceptions.SelectMany((Exception ie) => ie.GetInnerExceptions(maxDepth - 1)))
			{
				yield return item;
			}
		}
		foreach (Exception innerException in ex.InnerException.GetInnerExceptions(maxDepth - 1))
		{
			yield return innerException;
		}
	}
}
