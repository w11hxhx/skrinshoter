using System.Threading.Channels;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages;

namespace ScreenRecorder;

internal interface ICommandProducer
{
	ChannelWriter<Command> Commands { get; }

	Channel<OutgoingMessage> OutgoingMessages { get; }
}
