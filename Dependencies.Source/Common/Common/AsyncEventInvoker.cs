using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Common;

public class AsyncEventInvoker<TEventArgs>
{
	private class SubscriberDescriptor
	{
		public Task Queue;

		public int QueueSizeField;

		public bool Unsubscribed;

		public DateTime QueueOverflowLogTimeUtc = DateTime.UtcNow;

		public long ProcessedEventCount;

		public long DroppedEventCount;

		public int QueueSize => Interlocked.CompareExchange(ref QueueSizeField, 0, 0);
	}

	public const int DefaultMaxQueueSize = 30;

	private readonly ILogger logger;

	private readonly string eventName;

	private readonly int maxQueueSize;

	private readonly TimeSpan queueOverflowLogInterval;

	private readonly ConcurrentDictionary<EventHandler<TEventArgs>, SubscriberDescriptor> subscribers = new ConcurrentDictionary<EventHandler<TEventArgs>, SubscriberDescriptor>();

	public ICollection<EventHandler<TEventArgs>> Subscribers => subscribers.Keys;

	public int TotalQueueSize => subscribers.Values.Sum((SubscriberDescriptor x) => x.QueueSize);

	public AsyncEventInvoker(ILogger logger, string eventName, int maxQueueSize = 30, TimeSpan queueOverflowLogInterval = default(TimeSpan))
	{
		if (maxQueueSize <= 0)
		{
			throw new ArgumentOutOfRangeException("maxQueueSize");
		}
		if (queueOverflowLogInterval < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("queueOverflowLogInterval");
		}
		this.logger = logger ?? NullLogger.Instance;
		this.eventName = eventName;
		this.maxQueueSize = maxQueueSize;
		this.queueOverflowLogInterval = queueOverflowLogInterval;
		if (this.logger.IsEnabled(LogLevel.Debug))
		{
			this.logger.LogDebug("Created {Object} instance for event {Event}", "AsyncEventInvoker", eventName);
		}
	}

	public void Subscribe(EventHandler<TEventArgs> subscriber)
	{
		subscribers.TryAdd(subscriber, new SubscriberDescriptor());
	}

	public void Unsubscribe(EventHandler<TEventArgs> subscriber)
	{
		if (subscriber == null || !subscribers.TryRemove(subscriber, out var value))
		{
			return;
		}
		lock (value)
		{
			value.Unsubscribed = true;
		}
	}

	public void InvokeAsync(object sender, TEventArgs e)
	{
		foreach (KeyValuePair<EventHandler<TEventArgs>, SubscriberDescriptor> subscriber in subscribers)
		{
			InvokeAsync(sender, e, subscriber.Key, subscriber.Value);
		}
	}

	public void InvokeAsync(object sender, TEventArgs e, EventHandler<TEventArgs> subscriber)
	{
		if (subscribers.TryGetValue(subscriber, out var value))
		{
			InvokeAsync(sender, e, subscriber, value);
		}
	}

	private void InvokeAsync(object sender, TEventArgs e, EventHandler<TEventArgs> subscriber, SubscriberDescriptor descriptor)
	{
		lock (descriptor)
		{
			if (descriptor.Unsubscribed)
			{
				return;
			}
			if (descriptor.QueueSize == maxQueueSize)
			{
				if (queueOverflowLogInterval == default(TimeSpan))
				{
					logger.LogWarning("Event queue overflow for {Event}. Event source: {Sender}. Subscriber: {Subscriber}. Queue size: {CurrentQueueSize}/{MaxQueueSize}.", eventName, sender, subscriber.Target ?? subscriber.Method.DeclaringType, descriptor.QueueSize, maxQueueSize);
				}
				else
				{
					descriptor.DroppedEventCount++;
					LogQueueOverlowIfNeeded(sender, subscriber, descriptor);
				}
				return;
			}
			Interlocked.Increment(ref descriptor.QueueSizeField);
			if (descriptor.QueueSize == 1)
			{
				descriptor.Queue = Task.Run(() =>
				{
					Invoke(sender, e, subscriber, descriptor);
				});
			}
			else
			{
				descriptor.Queue = descriptor.Queue.ContinueWith((Task _) =>
				{
					Invoke(sender, e, subscriber, descriptor);
				});
			}
			if (descriptor.DroppedEventCount > 0)
			{
				LogQueueOverlowIfNeeded(sender, subscriber, descriptor);
			}
		}
	}

	private void Invoke(object sender, TEventArgs e, EventHandler<TEventArgs> subscriber, SubscriberDescriptor descriptor)
	{
		bool flag = false;
		lock (descriptor)
		{
			flag = descriptor.Unsubscribed;
		}
		if (!flag)
		{
			try
			{
				subscriber(sender, e);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error while processing event {Event}. Event source: {Sender}. Subscriber: {Subscriber}.", eventName, sender, subscriber.Target ?? subscriber.Method.DeclaringType);
			}
		}
		lock (descriptor)
		{
			descriptor.ProcessedEventCount++;
			Interlocked.Decrement(ref descriptor.QueueSizeField);
			if (descriptor.QueueSize == 0)
			{
				descriptor.Queue = null;
			}
		}
	}

	private void LogQueueOverlowIfNeeded(object sender, EventHandler<TEventArgs> subscriber, SubscriberDescriptor descriptor)
	{
		TimeSpan timeSpan = DateTime.UtcNow - descriptor.QueueOverflowLogTimeUtc;
		if (!(timeSpan < queueOverflowLogInterval))
		{
			logger.LogWarning("Event queue overflow for {Event}. Event source: {Sender}. Subscriber: {Subscriber}. Queue size: {CurrentQueueSize}/{MaxQueueSize}. During {TimeInterval}, events processed by subscriber: {ProcessedEventCount}, events dropped: {DroppedEventCount}.", eventName, sender, subscriber.Target ?? subscriber.Method.DeclaringType, descriptor.QueueSize, maxQueueSize, ClearMilliseconds(timeSpan), descriptor.ProcessedEventCount, descriptor.DroppedEventCount);
			descriptor.QueueOverflowLogTimeUtc = DateTime.UtcNow;
			descriptor.ProcessedEventCount = 0L;
			descriptor.DroppedEventCount = 0L;
		}
	}

	private static TimeSpan ClearMilliseconds(TimeSpan ts)
	{
		return new TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
	}
}
