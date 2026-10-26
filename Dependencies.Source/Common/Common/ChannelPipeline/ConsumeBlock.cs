using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Common.ChannelPipeline;

public sealed class ConsumeBlock<TInput>
{
	private readonly ChannelReader<TInput> input;

	private readonly Action<TInput> consumeAction;

	internal ConsumeBlock(ChannelReader<TInput> input, Action<TInput> consumeAction)
	{
		this.input = input ?? throw new ArgumentNullException("input");
		this.consumeAction = consumeAction ?? throw new ArgumentNullException("consumeAction");
	}

	public async Task RunAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		try
		{
			while (await input.WaitToReadAsync(cancellationToken))
			{
				if (input.TryRead(out var item))
				{
					consumeAction(item);
				}
			}
		}
		finally
		{
			if (!input.Completion.IsCompleted && !cancellationToken.IsCancellationRequested)
			{
				ChannelPipelineHelper.ReadAllAsync(input, cancellationToken);
			}
		}
	}
}
public static class ConsumeBlock
{
	public static ConsumeBlock<TInput> Create<TInput>(ChannelReader<TInput> input, Action<TInput> consumeAction)
	{
		return new ConsumeBlock<TInput>(input, consumeAction);
	}
}
