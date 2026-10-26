using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

[AnyThread]
public sealed class AppShutdownService
{
	private readonly IHost host;

	private readonly ILogger logger;

	private int shuttingDown;

	public AppShutdownService(IHost host, ILogger<AppShutdownService> logger)
	{
		this.host = host;
		this.logger = logger;
	}

	public async Task ShutdownAsync()
	{
		if (Interlocked.Exchange(ref shuttingDown, 1) != 1)
		{
			try
			{
				await host.StopAsync();
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to stop hosted services");
			}
			try
			{
				host.Dispose();
			}
			catch
			{
			}
			Application current = Application.Current;
			current?.Dispatcher.InvokeIfRequired(current.Shutdown);
		}
	}
}
