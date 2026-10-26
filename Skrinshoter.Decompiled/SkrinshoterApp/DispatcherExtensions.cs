using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SkrinshoterApp;

public static class DispatcherExtensions
{
	public static void InvokeIfRequired(this Dispatcher dispatcher, Action action)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		if (dispatcher.CheckAccess())
		{
			action();
		}
		else
		{
			dispatcher.Invoke(action);
		}
	}

	public static T InvokeIfRequired<T>(this Dispatcher dispatcher, Func<T> func)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		if (func == null)
		{
			throw new ArgumentNullException("func");
		}
		if (dispatcher.CheckAccess())
		{
			return func();
		}
		return dispatcher.Invoke(func);
	}

	public static Task InvokeIfRequiredAsync(this Dispatcher dispatcher, Func<Task> action)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		if (dispatcher.CheckAccess())
		{
			return action();
		}
		return dispatcher.InvokeAsync(action).Task.Unwrap();
	}

	public static Task<T> InvokeIfRequiredAsync<T>(this Dispatcher dispatcher, Func<Task<T>> func)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		if (func == null)
		{
			throw new ArgumentNullException("func");
		}
		if (dispatcher.CheckAccess())
		{
			return func();
		}
		return dispatcher.InvokeAsync(func).Task.Unwrap();
	}
}
