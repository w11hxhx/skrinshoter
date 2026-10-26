using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Nito.AsyncEx;

namespace SkrinshoterApp;

public sealed class ActionRateLimiter : IDisposable
{
	private readonly Dispatcher dispatcher;

	private readonly DispatcherPriority priority;

	private readonly Func<bool> action;

	private readonly bool executeImmediately;

	private readonly long intervalStopwatchTicks;

	private readonly AsyncAutoResetEvent signal;

	private readonly CancellationTokenSource cancelSource;

	private readonly Task workerTask;

	private volatile int disposed;

	public ActionRateLimiter(Dispatcher dispatcher, TimeSpan minInterval, Func<bool> action, DispatcherPriority priority = DispatcherPriority.Background, bool executeImmediately = true)
	{
		if (dispatcher == null)
		{
			throw new ArgumentNullException("dispatcher");
		}
		if (action == null)
		{
			throw new ArgumentNullException("action");
		}
		if (minInterval <= TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("minInterval");
		}
		this.dispatcher = dispatcher;
		this.action = action;
		this.priority = priority;
		this.executeImmediately = executeImmediately;
		intervalStopwatchTicks = (long)Math.Ceiling(minInterval.TotalSeconds * (double)Stopwatch.Frequency);
		signal = new AsyncAutoResetEvent(set: false);
		cancelSource = new CancellationTokenSource();
		workerTask = Task.Run((Func<Task>)WorkerAsync);
	}

	public void Run()
	{
		if (disposed == 0)
		{
			signal.Set();
		}
	}

	private async Task WorkerAsync()
	{
		CancellationToken token = cancelSource.Token;
		long nextAllowedTimestamp = 0L;
		while (true)
		{
			try
			{
				await signal.WaitAsync(token).ConfigureAwait(continueOnCapturedContext: false);
				if (disposed != 0)
				{
					break;
				}
				if (nextAllowedTimestamp == 0L)
				{
					long timestamp = Stopwatch.GetTimestamp();
					if (executeImmediately)
					{
						nextAllowedTimestamp = timestamp;
					}
					else
					{
						nextAllowedTimestamp = timestamp + intervalStopwatchTicks;
					}
				}
				while (true)
				{
					long timestamp2 = Stopwatch.GetTimestamp();
					long num = nextAllowedTimestamp - timestamp2;
					if (num <= 0)
					{
						break;
					}
					TimeSpan timeSpan = TimeSpan.FromSeconds((double)num / (double)Stopwatch.Frequency);
					if (timeSpan <= TimeSpan.Zero)
					{
						timeSpan = TimeSpan.FromMilliseconds(1.0);
					}
					await Task.Delay(timeSpan, token).ConfigureAwait(continueOnCapturedContext: false);
				}
				await dispatcher.InvokeAsync(() =>
				{
					if (disposed == 0)
					{
						long timestamp3 = Stopwatch.GetTimestamp();
						bool flag = false;
						try
						{
							flag = action();
						}
						catch
						{
							flag = false;
						}
						if (flag)
						{
							nextAllowedTimestamp = timestamp3 + intervalStopwatchTicks;
						}
					}
				}, priority, token).Task.ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (OperationCanceledException) when (token.IsCancellationRequested)
			{
				break;
			}
			catch (InvalidOperationException) when (dispatcher.HasShutdownStarted || dispatcher.HasShutdownFinished)
			{
				break;
			}
			catch
			{
			}
		}
	}

	public void Dispose()
	{
		if (Interlocked.Exchange(ref disposed, 1) != 1)
		{
			cancelSource.Cancel();
			signal.Set();
			workerTask.ContinueWith((Task _) =>
			{
				cancelSource.Dispose();
			}, TaskScheduler.Default);
		}
	}
}
