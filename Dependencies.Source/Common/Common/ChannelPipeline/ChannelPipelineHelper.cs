using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Common.ChannelPipeline;

internal static class ChannelPipelineHelper
{
	public static Channel<TItem> CreateChannel<TItem>(ChannelPipelineBlockOptions options)
	{
		if (options.Capacity == 0)
		{
			return Channel.CreateUnbounded<TItem>();
		}
		return Channel.CreateBounded<TItem>(new BoundedChannelOptions(options.Capacity)
		{
			FullMode = options.FullMode
		});
	}

	public static async Task ReadAllAsync<TItem>(ChannelReader<TItem> channelReader, CancellationToken cancellationToken = default(CancellationToken))
	{
		while (await channelReader.WaitToReadAsync(cancellationToken))
		{
			channelReader.TryRead(out var _);
		}
	}
}
