using System;
using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Common;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public abstract class MakeScreenshotCommandHandler
{
	protected readonly IIPCProxy videoModule;

	protected readonly SettingsManager settingsManager;

	protected readonly Settings settings;

	private readonly AppInfoProvider appInfoProvider;

	protected readonly IForegroundWindowInfoProvider foregroundWindowInfoProvider;

	protected readonly NotificationService notificationService;

	private readonly ClipboardService clipboardService;

	protected readonly AudioPlayer audioPlayer;

	protected readonly OperationStateService operationStateService;

	private readonly TemplateFileNameResolver templateFileNameResolver;

	private readonly ScreenshotFileFormatsProvider screenshotFileFormatsProvider;

	private readonly IMediator mediator;

	private readonly ScreenshotUploadCoordinator screenshotUploadCoordinator;

	protected readonly ILogger logger;

	public MakeScreenshotCommandHandler(VideoModuleAppProxy videoModule, SettingsManager settingsManager, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, NotificationService notificationService, ClipboardService clipboardService, AudioPlayer audioPlayer, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, ScreenshotFileFormatsProvider screenshotFileFormatsProvider, IMediator mediator, ScreenshotUploadCoordinator screenshotUploadCoordinator, ILogger logger)
	{
		this.videoModule = videoModule;
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.appInfoProvider = appInfoProvider;
		this.foregroundWindowInfoProvider = foregroundWindowInfoProvider;
		this.notificationService = notificationService;
		this.clipboardService = clipboardService;
		this.audioPlayer = audioPlayer;
		this.operationStateService = operationStateService;
		this.templateFileNameResolver = templateFileNameResolver;
		this.screenshotFileFormatsProvider = screenshotFileFormatsProvider;
		this.mediator = mediator;
		this.screenshotUploadCoordinator = screenshotUploadCoordinator;
		this.logger = logger;
	}

	protected async Task PerformScreenshotEditingAction(ScreenshotEditingResult result, string windowTitle, bool isScrollingCapture = false)
	{
		using (result)
		{
			string remoteBaseFileName = GetRemoteBaseFileName();
			string localFileName = GetLocalFileName(windowTitle);
			string extension = Path.GetExtension(localFileName);
			string path = null;
			if (settings.PlaySound && !settings.SilentMode)
			{
				audioPlayer.PlayAsync(ProductResources.ShutterClick);
			}
			if (result.Action == ScreenshotEditingAction.SaveToFile)
			{
				SaveScreenshotToFile(result.Image, result.Path);
				return;
			}
			if (settings.ScreenshotSaveType.HasFlag(ScreenshotSaveType.File) && result.Action.In(ScreenshotEditingAction.Default, ScreenshotEditingAction.CopyToClipboard, ScreenshotEditingAction.UploadToCloud, ScreenshotEditingAction.UploadToAll))
			{
				string screenshotSavePath = settings.ScreenshotSavePath;
				path = Path.Combine(screenshotSavePath, localFileName);
				try
				{
					Directory.CreateDirectory(screenshotSavePath);
				}
				catch (Exception exception)
				{
					logger.LogError(exception, "Failed to create directory \"{dir}\" for saving screenshot", screenshotSavePath);
					notificationService.ShowError(string.Format(Resources.Notify_ScreenshotPathNotAccessible, Path.GetPathRoot(path).TrimEnd('\\')));
					return;
				}
				SaveScreenshotToFile(result.Image, path);
			}
			if (result.Action == ScreenshotEditingAction.CopyToClipboard || (result.Action == ScreenshotEditingAction.Default && settings.ScreenshotSaveType.HasFlag(ScreenshotSaveType.Clipboard)))
			{
				CopyScreenshotToClipboard(result.Image);
			}
			if (!result.Action.In(ScreenshotEditingAction.UploadToCloud, ScreenshotEditingAction.UploadToAll) && (result.Action != ScreenshotEditingAction.Default || !settings.ScreenshotSaveType.HasFlag(ScreenshotSaveType.Cloud)))
			{
				return;
			}
			bool isFileInTempFolder = false;
			try
			{
				if (path == null)
				{
					path = Path.GetTempPath() + Guid.NewGuid().ToString() + extension;
					isFileInTempFolder = true;
					SaveScreenshotToFile(result.Image, path);
				}
				try
				{
					ScreenshotUploadDestination destinations = result.Action switch
					{
						ScreenshotEditingAction.UploadToCloud => ScreenshotUploadDestination.Skrinshoter,
						ScreenshotEditingAction.UploadToAll => settings.ScreenshotUploadDestinations,
						_ => settings.ScreenshotUploadDestinations
					};
					await UploadScreenshotAsync(path, remoteBaseFileName, windowTitle, isScrollingCapture, destinations);
				}
				catch (OperationCanceledException)
				{
				}
				catch (Exception exception2)
				{
					logger.LogError(exception2, "Failed to upload screenshot to cloud");
					if (isFileInTempFolder)
					{
						string desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), localFileName);
						try
						{
							File.Move(path, desktopFilePath);
							logger.LogInformation("Screenshot successfully moved to desktop: {path}", desktopFilePath);
							notificationService.ShowError(Resources.Notify_FailedToUploadScreenshotAndStoredOnDesktop, (NotificationOptions x) =>
							{
								x.WithCommand(new OpenFolderWithFileSelectedCommand(desktopFilePath));
							});
						}
						catch (Exception exception3)
						{
							logger.LogError(exception3, "Failed to move screenshot to desktop");
							notificationService.ShowError(Resources.Notify_FailedToUploadScreenshot);
						}
					}
					else
					{
						notificationService.ShowError(Resources.Notify_FailedToUploadScreenshotAndStoredInUserFolder, (NotificationOptions x) =>
						{
							x.WithCommand(new OpenFolderWithFileSelectedCommand(path));
						});
					}
				}
			}
			finally
			{
				if (isFileInTempFolder)
				{
					try
					{
						File.Delete(path);
					}
					catch
					{
					}
				}
			}
		}
	}

	protected string GetLocalFileName(string windowTitle)
	{
		string fileExtension = screenshotFileFormatsProvider.GetFileExtension(settings.ScreenshotFileFormat);
		return templateFileNameResolver.Resolve(settings.ScreenshotFileNameTemplate, settings.ScreenshotSavePath, fileExtension, new FileNameTemplateArgs(DateTime.Now, windowTitle));
	}

	protected string GetRemoteBaseFileName()
	{
		return string.Format(ProductResources.ScreenshotBaseFileName, DateTime.Now.ToString("dd-MM-yyyy HHmmss", CultureInfo.InvariantCulture));
	}

	private unsafe void SaveScreenshotToFile(RawImage image, string path)
	{
		long timestamp = Stopwatch.GetTimestamp();
		using MemoryHandle memoryHandle = image.Data[0].Pin();
		using Bitmap image2 = new Bitmap(image.Format.Width, image.Format.Height, image.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppArgb, (IntPtr)memoryHandle.Pointer);
		string text = Path.GetExtension(path).ToLowerInvariant();
		if (!(text == ".png"))
		{
			if (!(text == ".jpg"))
			{
				throw new InvalidOperationException("Unsupported screenshot file format: " + text + ".");
			}
			image2.SaveAsJpeg(path, Math.Min(settings.JpegQualityLevel, JpegQualityLevelsProvider.Default));
		}
		else
		{
			image2.SaveAsPng(path);
		}
		logger.LogInformation("Screenshot saved to file \"{path}\" [time={time}ms]", path, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
	}

	private void CopyScreenshotToClipboard(RawImage image)
	{
		try
		{
			long timestamp = Stopwatch.GetTimestamp();
			clipboardService.SetImage(image);
			logger.LogInformation("Screenshot copied to clipboard [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
		catch (ClipboardException exception)
		{
			logger.LogError(exception, "");
			notificationService.ShowError(Resources.Notify_FailedToCopyScreenshotToClipboard + " " + string.Format(Resources.Notify_FailedToCopyToClipboardExplanation, ProductResources.AppNameGenitive, ProductResources.AppName));
		}
	}

	private async Task UploadScreenshotAsync(string path, string friendlyBaseFileName, string windowTitle, bool isScrollingCapture, ScreenshotUploadDestination destinations)
	{
		long startTime = Stopwatch.GetTimestamp();
		if (!settings.UploadWindowTitle)
		{
			windowTitle = "";
		}
		var results = await screenshotUploadCoordinator.UploadAsync(path, friendlyBaseFileName, windowTitle, isScrollingCapture, destinations);
		string links = ScreenshotUploadCoordinator.FormatLinks(results);
		logger.LogInformation("Screenshot uploaded to {count} services [time={time}ms]", results.Count, StopwatchHelper.GetDurationInWholeMillisecondsSince(startTime));
		if (settings.CopyScreenshotLinksToClipboard)
		{
			try
			{
				clipboardService.SetText(links);
			}
			catch (ClipboardException exception)
			{
				logger.LogError(exception, "Failed to copy screenshot links to clipboard");
				notificationService.ShowError(Resources.Notify_FailedToCopyFileLinkToClipboard + " " + string.Format(Resources.Notify_FailedToCopyToClipboardExplanation, ProductResources.AppNameGenitive, ProductResources.AppName));
			}
		}
		if (settings.OpenScreenshotInBrowser && !settings.SilentMode)
		{
			foreach (ScreenshotUploadResult result in results)
			{
				await mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(result.Url), default(CancellationToken));
			}
		}
	}
}
