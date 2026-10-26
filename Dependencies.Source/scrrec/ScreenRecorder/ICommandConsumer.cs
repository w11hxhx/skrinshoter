using System.Threading.Channels;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages;

namespace ScreenRecorder;

internal interface ICommandConsumer : IEventProducer
{
	ChannelReader<Command> Commands { get; }

	ChannelWriter<OutgoingMessage> Responses { get; }
}
