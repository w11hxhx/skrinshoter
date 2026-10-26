using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common;

public static class ThreadEx
{
	public static void RunInMTA(Action action)
	{
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
		{
			action();
		}
		else
		{
			Task.Run(action).GetAwaiter().GetResult();
		}
	}

	public static TResult RunInMTA<TResult>(Func<TResult> function)
	{
		if (function == null)
		{
			throw new ArgumentNullException("function");
		}
		if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
		{
			return function();
		}
		return Task.Run(function).GetAwaiter().GetResult();
	}
}
