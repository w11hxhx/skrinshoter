using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.SkrApi;
using SkrinshoterApp.UseCases;

namespace SkrinshoterApp.BackgroundServices;

public class StatsSender : IHostedService
{
	private readonly ILogger logger;

	private readonly AppInfoProvider appInfoProvider;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly object lockObject = new object();

	private Timer timer;

	private bool isStarted;

	private Task sendStatsTask;

	private CancellationTokenSource cts;

	public StatsSender(AppInfoProvider appInfoProvider, SettingsManager settingsManager, Dispatcher dispatcher, ILogger<StatsSender> logger)
	{
		this.logger = logger;
		this.appInfoProvider = appInfoProvider;
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.dispatcher = dispatcher;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		lock (lockObject)
		{
			if (isStarted)
			{
				return Task.CompletedTask;
			}
			isStarted = true;
			timer = new Timer(TimerCallback, null, TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(1.0));
			cts = new CancellationTokenSource();
			return Task.CompletedTask;
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		lock (lockObject)
		{
			if (!isStarted)
			{
				return Task.CompletedTask;
			}
			isStarted = false;
			timer.Dispose();
			cts.Cancel();
			if (sendStatsTask != null)
			{
				CancellationTokenSource oldCts = cts;
				sendStatsTask.ContinueWith((Task _) =>
				{
					oldCts.Dispose();
				});
				sendStatsTask = null;
			}
			else
			{
				cts.Dispose();
			}
			return Task.CompletedTask;
		}
	}

	private void TimerCallback(object state)
	{
		lock (lockObject)
		{
			try
			{
				if (isStarted && (sendStatsTask == null || sendStatsTask.IsCompleted))
				{
					sendStatsTask = SendStatsAsync(cts.Token);
				}
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error in timer callback");
			}
		}
	}

	private async Task SendStatsAsync(CancellationToken cancellationToken)
	{
		try
		{
			DateTime localDateTime = DateTimeOffset.FromUnixTimeSeconds(dispatcher.InvokeIfRequired(() => settings.LastStatsSendDate)).LocalDateTime;
			if ((DateTime.Now - localDateTime).TotalHours <= 24.0)
			{
				return;
			}
			string tempFolderPath = Path.GetTempPath();
			string path = Guid.NewGuid().ToString();
			tempFolderPath = Path.Combine(tempFolderPath, path);
			string text = new ArchiveAppDataFolderUseCase(appInfoProvider.AppId, tempFolderPath).Execute();
			if (!File.Exists(text))
			{
				logger.LogError("Zip file of App Data not found");
				return;
			}
			Dictionary<string, string> additionalFields = new Dictionary<string, string>
			{
				{
					"appVersion",
					appInfoProvider.Version.ToString()
				}
			};
			await new FilePostUploader(SkrApiHttpClient.StatsHttpClient, "logsPack", text, additionalFields, "/add", cancellationToken, (int _) =>
			{
			}, (HttpResponseMessage _) => Task.CompletedTask).Upload();
			try
			{
				Directory.Delete(tempFolderPath, recursive: true);
			}
			catch (Exception exception)
			{
				logger.LogWarning(exception, "Failed to delete temp folder: {tempFolderPath}", tempFolderPath);
			}
			dispatcher.InvokeIfRequired(() =>
			{
				settings.LastStatsSendDate = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
				settingsManager.SaveSettings();
			});
			logger.LogInformation("Statistics sent successfully");
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception ex2)
		{
			logger.LogWarning("Failed to send statistics: {reason}", ex2.Message);
		}
	}
}
