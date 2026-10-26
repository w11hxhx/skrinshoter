using System.Threading.Channels;
using ScreenRecorder.OutgoingMessages;

namespace ScreenRecorder;

internal interface IEventProducer
{
	ChannelWriter<OutgoingMessage> Events { get; }
}
