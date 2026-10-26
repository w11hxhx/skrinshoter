using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SkrinshoterApp;

internal sealed class DesignTimeHost : IHost, IDisposable
{
	private readonly IServiceProvider services;

	public IServiceProvider Services => services;

	public DesignTimeHost(IServiceProvider services)
	{
		this.services = services;
	}

	public Task StartAsync(CancellationToken ct = default(CancellationToken))
	{
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken ct = default(CancellationToken))
	{
		return Task.CompletedTask;
	}

	public void Dispose()
	{
	}
}
