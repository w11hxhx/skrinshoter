using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using SkrinshoterApp.SkrApi;

namespace SkrinshoterApp.BackgroundServices;

public sealed class UserQuotaService : BackgroundService
{
	private readonly QuotaApiClient quotaApiClient;

	private readonly Dispatcher dispatcher;

	private readonly ILogger logger;

	private readonly AsyncAutoResetEvent updateSignal = new AsyncAutoResetEvent(set: true);

	public event EventHandler<UserQuotaUpdatedEventArgs> Updated;

	public UserQuotaService(QuotaApiClient quotaApiClient, Dispatcher dispatcher, ILogger<UserQuotaService> logger)
	{
		this.quotaApiClient = quotaApiClient;
		this.dispatcher = dispatcher;
		this.logger = logger;
	}

	public void Update()
	{
		updateSignal.Set();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		if (AppInfoProvider.IsFloomby)
		{
			return;
		}
		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				await updateSignal.WaitAsync(stoppingToken).ConfigureAwait(continueOnCapturedContext: false);
				QuotaDto quota = await quotaApiClient.GetQuota(stoppingToken).ConfigureAwait(continueOnCapturedContext: false);
				dispatcher.InvokeIfRequired(() =>
				{
					Updated?.Invoke(this, new UserQuotaUpdatedEventArgs(quota));
				});
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to update user quota");
			}
		}
	}
}
