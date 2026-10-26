using System.Threading.Channels;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages;

namespace ScreenRecorder;

internal class MessageChannel : ICommandProducer, ICommandConsumer, IEventProducer
{
	private readonly Channel<Command> commands = Channel.CreateUnbounded<Command>();

	private readonly Channel<OutgoingMessage> outgoingMessages = Channel.CreateUnbounded<OutgoingMessage>();

	ChannelWriter<Command> ICommandProducer.Commands => commands.Writer;

	Channel<OutgoingMessage> ICommandProducer.OutgoingMessages => outgoingMessages;

	ChannelReader<Command> ICommandConsumer.Commands => commands.Reader;

	ChannelWriter<OutgoingMessage> ICommandConsumer.Responses => outgoingMessages.Writer;

	ChannelWriter<OutgoingMessage> IEventProducer.Events => outgoingMessages.Writer;
}
