using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScreenRecorderCommon;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp;

public interface IIPCProxy
{
	string Name { get; }

	IChildProcess Process { get; }

	event EventHandler OnChildDisconnected;

	event EventHandler OnChildConnected;

	event Action OnFailedToRunChildProcess;

	void RunAsync();

	Task SendCommand(VideoModuleCommand command, List<string> arguments = null);

	Task<string[]> ReceiveCommandResult(VideoModuleCommand command, TimeSpan maxExecutionTime);

	void RegisterHandler(VideoModuleEvent incomingEvent, Action<string[]> handler);

	Task WaitForProcessDisconnected(TimeSpan timeout);
}
