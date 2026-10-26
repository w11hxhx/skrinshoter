using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Common.ChannelPipeline;

public sealed class TransformBlock<TInput, TOutput>
{
	private readonly ChannelReader<TInput> input;

	private readonly Func<TInput, TOutput> transformFunc;

	private readonly Func<Exception, IEnumerable<TOutput>> completionFunc;

	private readonly ChannelPipelineBlockOptions options;

	private readonly ChannelWriter<TOutput> writer;

	public ChannelReader<TOutput> Reader { get; }

	internal TransformBlock(ChannelReader<TInput> input, Func<TInput, TOutput> transformFunc, Func<Exception, IEnumerable<TOutput>> completionFunc = null, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
	{
		this.input = input ?? throw new ArgumentNullException("input");
		this.transformFunc = transformFunc ?? throw new ArgumentNullException("transformFunc");
		this.completionFunc = completionFunc;
		options = new ChannelPipelineBlockOptions();
		optionsBuilder?.Invoke(options);
		Channel<TOutput> channel = ChannelPipelineHelper.CreateChannel<TOutput>(options);
		writer = channel.Writer;
		Reader = channel.Reader;
	}

	public async Task RunAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		Exception exception = null;
		try
		{
			while (await input.WaitToReadAsync(cancellationToken))
			{
				if (input.TryRead(out var item))
				{
					await writer.WriteAsync(transformFunc(item), cancellationToken);
				}
			}
		}
		catch (Exception ex)
		{
			exception = ex;
		}
		try
		{
			foreach (TOutput item2 in completionFunc?.Invoke(exception) ?? Array.Empty<TOutput>())
			{
				await writer.WriteAsync(item2, cancellationToken);
			}
		}
		catch (Exception ex2)
		{
			if (exception == null)
			{
				exception = ex2;
			}
		}
		writer.TryComplete(exception);
		if (!input.Completion.IsCompleted && !cancellationToken.IsCancellationRequested)
		{
			ChannelPipelineHelper.ReadAllAsync(input, cancellationToken);
		}
	}
}
public static class TransformBlock
{
	public static TransformBlock<TInput, TOutput> Create<TInput, TOutput>(ChannelReader<TInput> input, Func<TInput, TOutput> transformFunc, Func<Exception, IEnumerable<TOutput>> completionFunc = null, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
	{
		return new TransformBlock<TInput, TOutput>(input, transformFunc, completionFunc, optionsBuilder);
	}
}
