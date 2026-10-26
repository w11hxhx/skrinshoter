using System;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public interface IPipeServer
{
	event EventHandler OnConnected;

	event EventHandler OnDisconnected;

	event EventHandler<string> OnResponse;

	string GetPipeName();

	Task StartAsync();

	Task SendAsync(string command);
}
