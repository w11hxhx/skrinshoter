using System;
using System.Threading.Channels;

namespace AudioVideoCore;

public interface IVideoCapturer : IDisposable
{
	ImageFormat ImageFormat { get; }

	ChannelReader<VideoFrame> Frames { get; }
}
