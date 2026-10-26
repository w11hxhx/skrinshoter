using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Priority_Queue;

namespace Common.ChannelPipeline;

public sealed class OrderedJoinBlock<TItem, TKey>
{
	private readonly IReadOnlyCollection<ChannelReader<TItem>> inputs;

	private readonly Func<TItem, TKey> keySelector;

	private readonly ChannelPipelineBlockOptions options;

	private readonly SimplePriorityQueue<(ChannelReader<TItem>, TItem), TKey> orderingQueue;

	private readonly ChannelWriter<TItem> writer;

	public ChannelReader<TItem> Reader { get; }

	internal OrderedJoinBlock(IReadOnlyCollection<ChannelReader<TItem>> inputs, Func<TItem, TKey> keySelector, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
	{
		this.inputs = inputs ?? throw new ArgumentNullException("inputs");
		if (inputs.Contains(null))
		{
			throw new ArgumentException("Collection cannot contain null elements.", "inputs");
		}
		this.keySelector = keySelector ?? throw new ArgumentNullException("keySelector");
		options = new ChannelPipelineBlockOptions();
		optionsBuilder?.Invoke(options);
		orderingQueue = new SimplePriorityQueue<(ChannelReader<TItem>, TItem), TKey>();
		Channel<TItem> channel = ChannelPipelineHelper.CreateChannel<TItem>(options);
		writer = channel.Writer;
		Reader = channel.Reader;
	}

	public async Task RunAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		Exception exception = null;
		try
		{
			foreach (ChannelReader<TItem> input in inputs)
			{
				var (flag, val) = await ReadItemAsync(input, cancellationToken);
				if (flag)
				{
					orderingQueue.Enqueue((input, val), keySelector(val));
				}
			}
			while (orderingQueue.Count > 0)
			{
				ChannelReader<TItem> input;
				TItem item;
				(input, item) = orderingQueue.Dequeue();
				await writer.WriteAsync(item, cancellationToken);
				bool flag2;
				(flag2, item) = await ReadItemAsync(input, cancellationToken);
				if (flag2)
				{
					orderingQueue.Enqueue((input, item), keySelector(item));
				}
			}
		}
		catch (Exception ex)
		{
			exception = ex;
		}
		writer.TryComplete(exception);
		if (exception == null || cancellationToken.IsCancellationRequested)
		{
			return;
		}
		foreach (ChannelReader<TItem> input2 in inputs)
		{
			ChannelPipelineHelper.ReadAllAsync(input2, cancellationToken);
		}
	}

	private async Task<(bool, TItem)> ReadItemAsync(ChannelReader<TItem> input, CancellationToken cancellationToken = default(CancellationToken))
	{
		while (await input.WaitToReadAsync(cancellationToken))
		{
			if (input.TryRead(out var item))
			{
				return (true, item);
			}
		}
		return (false, default);
	}
}
public static class OrderedJoinBlock
{
	public static OrderedJoinBlock<TItem, TKey> Create<TItem, TKey>(IReadOnlyCollection<ChannelReader<TItem>> inputs, Func<TItem, TKey> keySelector, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
	{
		return new OrderedJoinBlock<TItem, TKey>(inputs, keySelector, optionsBuilder);
	}
}
