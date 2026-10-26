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
		ScreenshotUploadDestination.NoikCloud
	};

	private readonly CloudFileUploadService cloudFileUploadService;

	private readonly ExternalScreenshotUploadService externalUploadService;

	private readonly NotificationService notificationService;

	private readonly ILogger logger;

	public ScreenshotUploadCoordinator(CloudFileUploadService cloudFileUploadService, ExternalScreenshotUploadService externalUploadService, NotificationService notificationService, ILogger<ScreenshotUploadCoordinator> logger)
	{
		this.cloudFileUploadService = cloudFileUploadService;
		this.externalUploadService = externalUploadService;
		this.notificationService = notificationService;
		this.logger = logger;
	}

	public async Task<IReadOnlyList<ScreenshotUploadResult>> UploadAsync(string filePath, string friendlyBaseFileName, string windowTitle, bool isScrollingCapture, ScreenshotUploadDestination destinations, CancellationToken cancellationToken = default)
	{
		destinations &= ScreenshotUploadDestination.All;
		if (destinations == ScreenshotUploadDestination.None)
		{
			destinations = ScreenshotUploadDestination.Skrinshoter;
		}
		List<ScreenshotUploadResult> results = new List<ScreenshotUploadResult>();
		List<string> failures = new List<string>();

		foreach (ScreenshotUploadDestination destination in ExternalDestinations)
		{
			if (!destinations.HasFlag(destination))
			{
				continue;
			}

			try
			{
				results.Add(await externalUploadService.UploadAsync(destination, filePath, cancellationToken));
			}
			catch (Exception exception) when (!(exception is OperationCanceledException))
			{
				logger.LogError(exception, "Failed to upload screenshot to {destination}", destination);
				failures.Add(destination.ToString());
			}
		}

		if (destinations.HasFlag(ScreenshotUploadDestination.Skrinshoter))
		{
			try
			{
				string titleWithLinks = FormatLinks(results.Where((ScreenshotUploadResult result) => result.Destination != ScreenshotUploadDestination.Skrinshoter));
				string url = await cloudFileUploadService.UploadFile(filePath, CloudFileUploadType.Screenshot, friendlyBaseFileName, Path.GetExtension(filePath), titleWithLinks, isScrollingCapture, copyLinkToClipboard: false);
				results.Add(new ScreenshotUploadResult(ScreenshotUploadDestination.Skrinshoter, "Скриншотер", url));
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

	public static string FormatLinks(IEnumerable<ScreenshotUploadResult> results)
	{
		return string.Join(Environment.NewLine, results.Select((ScreenshotUploadResult result) => result.ServiceName + ": " + result.Url));
	}

}
