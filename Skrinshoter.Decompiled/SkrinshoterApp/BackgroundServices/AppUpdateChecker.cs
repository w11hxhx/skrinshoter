using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.BackgroundServices;

public class AppUpdateChecker : IHostedService
{
	private class VersionResponseDto
	{
		[JsonRequired]
		public string Version { get; set; }
	}

	private readonly ILogger logger;

	private readonly AppInfoProvider appInfoProvider;

	private readonly Settings settings;

	private readonly NotificationService notificationService;

	private readonly IHttpClientFactory httpClientFactory;

	private readonly Dispatcher dispatcher;

	private readonly object lockObject = new object();

	private Timer timer;

	private bool isStarted;

	private Task checkForUpdatesTask;

	private CancellationTokenSource cts;

	public AppUpdateChecker(AppInfoProvider appInfoProvider, SettingsManager settingsManager, NotificationService notificationService, IHttpClientFactory httpClientFactory, Dispatcher dispatcher, ILogger<AppUpdateChecker> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.appInfoProvider = appInfoProvider;
		settings = settingsManager.Settings;
		this.notificationService = notificationService;
		this.httpClientFactory = httpClientFactory;
		this.dispatcher = dispatcher;
		settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
	}

	private async void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		if (e.Old.CheckForUpdates != e.New.CheckForUpdates)
		{
			if (e.New.CheckForUpdates)
			{
				await StartAsync(CancellationToken.None);
			}
			else
			{
				await StopAsync(CancellationToken.None);
			}
		}
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		lock (lockObject)
		{
			if (isStarted || !dispatcher.InvokeIfRequired(() => settings.CheckForUpdates))
			{
				return Task.CompletedTask;
			}
			isStarted = true;
			timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(10.0), Timeout.InfiniteTimeSpan);
			cts = new CancellationTokenSource();
			logger.LogInformation("App update checker started");
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
			if (checkForUpdatesTask != null)
			{
				CancellationTokenSource oldCts = cts;
				checkForUpdatesTask.ContinueWith((Task _) =>
				{
					oldCts.Dispose();
				});
				checkForUpdatesTask = null;
			}
			else
			{
				cts.Dispose();
			}
			logger.LogInformation("App update checker stopped");
			return Task.CompletedTask;
		}
	}

	private void TimerCallback(object state)
	{
		lock (lockObject)
		{
			try
			{
				if (isStarted)
				{
					checkForUpdatesTask = CheckForUpdatesAsync(cts.Token);
					TimeSpan dueTime = TimeSpan.FromDays(1.0) + TimeSpan.FromSeconds(new Random().Next((int)TimeSpan.FromDays(1.0).TotalSeconds));
					timer.Change(dueTime, Timeout.InfiniteTimeSpan);
				}
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error in timer callback");
			}
		}
	}

	private async Task CheckForUpdatesAsync(CancellationToken cancellationToken)
	{
		try
		{
			Version version = await GetLatestVersionAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (appInfoProvider.Version < version)
			{
				logger.LogInformation("New app version is available: {latestVersion}", version);
				notificationService.ShowMessage(string.Format(Resources.Notify_AppUpdateAvailable, ProductResources.AppNameGenitive), (NotificationOptions x) =>
				{
					x.WithCommand(new ShowAppUpdateCheckWindowCommand()).WithRespectSilentMode(value: true);
				});
			}
			else
			{
				logger.LogInformation("No new app versions are available");
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to check for updates");
		}
	}

	private async Task<Version> GetLatestVersionAsync(CancellationToken cancellationToken)
	{
		string text = DateTime.UtcNow.Ticks.ToString();
		using HttpClient httpClient = httpClientFactory.CreateClient("AppUpdateChecker");
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "windows/latest_version.json?rnd=" + text);
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		response.EnsureSuccessStatusCode();
		return Version.Parse(JsonConvert.DeserializeObject<VersionResponseDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)).Version);
	}
}
