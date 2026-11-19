using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

public sealed class ScreenshotUploadCoordinator
{
	private static readonly ScreenshotUploadDestination[] ExternalDestinations =
	{
		ScreenshotUploadDestination.KappaLol,
		ScreenshotUploadDestination.Xyli,
		ScreenshotUploadDestination.ZeroXl1r,
		ScreenshotUploadDestination.NoikCloud,
		ScreenshotUploadDestination.Nuuls
	};

	private readonly CloudFileUploadService cloudFileUploadService;

	private readonly ExternalScreenshotUploadService externalUploadService;

	private readonly NotificationService notificationService;

	private readonly ILogger logger;
	private readonly SettingsManager settingsManager;

	public ScreenshotUploadCoordinator(CloudFileUploadService cloudFileUploadService, ExternalScreenshotUploadService externalUploadService, NotificationService notificationService, ILogger<ScreenshotUploadCoordinator> logger, SettingsManager settingsManager)
	{
		this.cloudFileUploadService = cloudFileUploadService;
		this.externalUploadService = externalUploadService;
		this.notificationService = notificationService;
		this.logger = logger;
		this.settingsManager = settingsManager;
	}

	public async Task<IReadOnlyList<ScreenshotUploadResult>> UploadAsync(string filePath, string friendlyBaseFileName, string windowTitle, bool isScrollingCapture, ScreenshotUploadDestination destinations, CancellationToken cancellationToken = default)
	{
		destinations &= ScreenshotUploadDestination.All;
		if (destinations == ScreenshotUploadDestination.None)
		{
			destinations = ScreenshotUploadDestination.Skrinshoter;
		}
		cancellationToken.ThrowIfCancellationRequested();
		var pending = new List<Task<(ScreenshotUploadResult Result, string Failure)>>();

		async Task<(ScreenshotUploadResult, string)> UploadOneAsync(string name, Func<Task<ScreenshotUploadResult>> upload)
		{
			try
			{
				return (await upload(), null);
			}
			catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
			{
				// Cancelling one provider must not discard links from the others.
				return (null, name);
			}
			catch (Exception exception) when (!(exception is OperationCanceledException))
			{
				logger.LogError(exception, "Failed to upload screenshot to {destination}", name);
				return (null, name);
			}
		}

		foreach (ScreenshotUploadDestination destination in ExternalDestinations)
		{
			if (destinations.HasFlag(destination))
			{
				pending.Add(UploadOneAsync(destination.ToString(),
					() => externalUploadService.UploadAsync(destination, filePath, cancellationToken)));
			}
		}
		if (destinations.HasFlag(ScreenshotUploadDestination.Custom))
		{
			foreach (var provider in settingsManager.Settings.CustomUploadProviders.Where(x => x.Enabled).ToArray())
			{
				pending.Add(UploadOneAsync(provider.Name,
					() => externalUploadService.UploadAsync(provider, filePath, cancellationToken)));
			}
		}

		// WhenAll waits for every reader before the caller can delete the temporary screenshot.
		// Its result order follows the provider list, regardless of completion order.
		var completed = await Task.WhenAll(pending);
		cancellationToken.ThrowIfCancellationRequested();
		var results = completed.Where(x => x.Result != null).Select(x => x.Result).ToList();
		var failures = completed.Where(x => x.Failure != null).Select(x => x.Failure).ToList();

		if (destinations.HasFlag(ScreenshotUploadDestination.Skrinshoter))
		{
			try
			{
				string titleWithLinks = FormatLinks(results.Where((ScreenshotUploadResult result) => result.Destination != ScreenshotUploadDestination.Skrinshoter));
				string url = await cloudFileUploadService.UploadFile(filePath, CloudFileUploadType.Screenshot, friendlyBaseFileName, Path.GetExtension(filePath), titleWithLinks, isScrollingCapture, copyLinkToClipboard: false);
				results.Add(new ScreenshotUploadResult(ScreenshotUploadDestination.Skrinshoter, "Скриншотер", url));
			}
			catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
			{
				failures.Add("Скриншотер");
			}
			catch (Exception exception) when (!(exception is OperationCanceledException))
			{
				logger.LogError(exception, "Failed to upload screenshot to Skrinshoter");
				failures.Add("Скриншотер");
			}
		}

		if (failures.Count > 0 && results.Count > 0)
		{
			notificationService.ShowError("Не удалось загрузить скриншот в: " + string.Join(", ", failures) + ". Остальные ссылки готовы.");
		}
		if (results.Count == 0)
		{
			throw new InvalidOperationException("Не удалось загрузить скриншот ни в один выбранный сервис.");
		}
		return results;
	}

	public static string FormatLinks(IEnumerable<ScreenshotUploadResult> results, bool includeServiceNames = true)
	{
		return string.Join(Environment.NewLine, results.Select((ScreenshotUploadResult result) => (includeServiceNames ? result.ServiceName + ": " : "") + result.Url));
	}

}
