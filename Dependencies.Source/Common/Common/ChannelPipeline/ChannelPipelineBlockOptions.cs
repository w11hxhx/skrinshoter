using System.Threading.Channels;

namespace Common.ChannelPipeline;

public class ChannelPipelineBlockOptions
{
	public int Capacity { get; set; }

	public BoundedChannelFullMode FullMode { get; set; }
}
