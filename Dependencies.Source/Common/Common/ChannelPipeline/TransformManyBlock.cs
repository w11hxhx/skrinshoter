using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Common.ChannelPipeline;

public sealed class TransformManyBlock<TInput, TOutput>
{
	private readonly ChannelReader<TInput> input;

	private readonly Func<TInput, IEnumerable<TOutput>> transformFunc;

	private readonly Func<Exception, IEnumerable<TOutput>> completionFunc;

	private readonly ChannelPipelineBlockOptions options;

	private readonly ChannelWriter<TOutput> writer;

	public ChannelReader<TOutput> Reader { get; }

	internal TransformManyBlock(ChannelReader<TInput> input, Func<TInput, IEnumerable<TOutput>> transformFunc, Func<Exception, IEnumerable<TOutput>> completionFunc = null, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
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
				if (!input.TryRead(out var item))
				{
					continue;
				}
				foreach (TOutput item2 in transformFunc(item) ?? Array.Empty<TOutput>())
				{
					await writer.WriteAsync(item2, cancellationToken);
				}
			}
		}
		catch (Exception ex)
		{
			exception = ex;
		}
		try
		{
			foreach (TOutput item3 in completionFunc?.Invoke(exception) ?? Array.Empty<TOutput>())
			{
				await writer.WriteAsync(item3, cancellationToken);
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
public static class TransformManyBlock
{
	public static TransformManyBlock<TInput, TOutput> Create<TInput, TOutput>(ChannelReader<TInput> input, Func<TInput, IEnumerable<TOutput>> transformFunc, Func<Exception, IEnumerable<TOutput>> completionFunc = null, Action<ChannelPipelineBlockOptions> optionsBuilder = null)
	{
		return new TransformManyBlock<TInput, TOutput>(input, transformFunc, completionFunc, optionsBuilder);
	}
}
