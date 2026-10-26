using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using ScreenRecorderCommon;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Common;
using SkrinshoterApp.Properties;
using SkrinshoterApp.Services.Progress;
using SkrinshoterApp.SkrApi.RealTimeFileUpload;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp.Commands;

public class RecordVideoCommandHandler : IRequestHandler<RecordVideoCommand, Unit>
{
	private const ulong StopRecordingDiskSpaceThreshold = 94371840uL;

	private const ulong DoNotStartRecordingDiskSpaceThreshold = 104857600uL;

	private const ulong ShowWindowDiskSpaceThreshold = 314572800uL;

	private readonly Settings settings;

	private readonly AppInfoProvider appInfoProvider;

	private readonly IForegroundWindowInfoProvider foregroundWindowInfoProvider;

	private readonly IProgressWindowService progressWindowService;

	private readonly NotificationService notificationService;

	private readonly ClipboardService clipboardService;

	private readonly VideoModuleAppProxy videoModule;

	private readonly VideoRecordingWindowManager videoRecordingWindowManager;

	private readonly OperationStateService operationStateService;

	private readonly TemplateFileNameResolver templateFileNameResolver;

	private readonly IMediator mediator;

	private readonly TrayIconService trayIconService;

	private readonly IHttpClientFactory httpClientFactory;

	private readonly UserQuotaService userQuotaService;

	private readonly Func<AudioCaptureSession> audioCaptureSessionFactory;

	private readonly ILoggerFactory loggerFactory;

	private readonly ILogger logger;

	private IProgressSession uploadProgressSession;

	private long fileSize;

	public RecordVideoCommandHandler(Settings settings, AppInfoProvider appInfoProvider, IForegroundWindowInfoProvider foregroundWindowInfoProvider, IProgressWindowService progressWindowService, NotificationService notificationService, ClipboardService clipboardService, VideoModuleAppProxy videoModule, VideoRecordingWindowManager videoRecordingWindowManager, OperationStateService operationStateService, TemplateFileNameResolver templateFileNameResolver, IMediator mediator, TrayIconService trayIconService, IHttpClientFactory httpClientFactory, UserQuotaService userQuotaService, Func<AudioCaptureSession> audioCaptureSessionFactory, ILoggerFactory loggerFactory)
	{
		this.settings = settings;
		this.appInfoProvider = appInfoProvider;
		this.foregroundWindowInfoProvider = foregroundWindowInfoProvider;
		this.progressWindowService = progressWindowService;
		this.notificationService = notificationService;
		this.clipboardService = clipboardService;
		this.videoModule = videoModule;
		this.videoRecordingWindowManager = videoRecordingWindowManager;
		this.operationStateService = operationStateService;
		this.templateFileNameResolver = templateFileNameResolver;
		this.mediator = mediator;
		this.trayIconService = trayIconService;
		this.httpClientFactory = httpClientFactory;
		this.userQuotaService = userQuotaService;
		this.audioCaptureSessionFactory = audioCaptureSessionFactory;
		this.loggerFactory = loggerFactory;
		logger = loggerFactory.CreateLogger<RecordVideoCommandHandler>();
	}

	public async Task<Unit> Handle(RecordVideoCommand request, CancellationToken cancellationToken)
	{
		if (operationStateService.TryStartOperation(OperationType.Video))
		{
			try
			{
				await Execute();
			}
			finally
			{
				operationStateService.StopOperation();
			}
		}
		return Unit.Value;
	}

	private async Task Execute()
	{
		settings.VideoSaveType = VideoSaveType.Cloud;
		settings.UseDirectLinkForVideo = false;
		bool audioLevelMonitoringStopRequired = false;
		try
		{
			string windowTitle = foregroundWindowInfoProvider.Title;
			string remoteFileName = GetRemoteFileName();
			string localFileName = GetLocalFileName(windowTitle);
			string savePath = GetVideoFileSavePath(localFileName);
			DiskSpaceMonitor diskSpaceMonitor = new DiskSpaceMonitor(savePath);
			using AudioCaptureSession audioCaptureSession = audioCaptureSessionFactory();
			audioCaptureSession.Begin();
			SendAudioLevelMonitoringCommand(VideoModuleCommand.audioLevelStart, new List<string>(4)
			{
				audioCaptureSession.InputDeviceId,
				audioCaptureSession.IsInputMuted ? "1" : "0",
				audioCaptureSession.OutputDeviceId,
				audioCaptureSession.IsOutputMuted ? "1" : "0"
			});
			audioLevelMonitoringStopRequired = true;
			VideoRecordingAreaSelection videoRecordingAreaSelection = await videoRecordingWindowManager.SelectVideoRecordingArea(diskSpaceMonitor.DiskName, GetMaxFps(), notificationService, videoModule);
			if (videoRecordingAreaSelection == null)
			{
				logger.LogInformation("Video recording has been canceled on region selection");
				return;
			}
			try
			{
				if (diskSpaceMonitor.GetFreeSpace() <= 104857600)
				{
					logger.LogError("Cannot start video recording due to low space on disk {diskName}", diskSpaceMonitor.DiskName);
					notificationService.ShowError(string.Format(Resources.Notify_LowDiskSpace, diskSpaceMonitor.DiskName.TrimEnd('\\')));
					return;
				}
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to check free space on disk {diskName}", diskSpaceMonitor.DiskName);
				notificationService.ShowError(string.Format(Resources.Notify_VideoPathNotAccessible, diskSpaceMonitor.DiskName.TrimEnd('\\')));
				return;
			}
			FileHelpers.DeleteFile(savePath);
			using CancellationTokenSource cts = new CancellationTokenSource();
			RealTimeVideoFileUploader videoFileUploader = null;
			Task videoFileUploaderTask = null;
			bool isMoveFileToDesktopRequired = false;
			try
			{
				if (settings.VideoSaveType != VideoSaveType.File)
				{
					string windowTitle2 = (settings.UploadWindowTitle ? windowTitle : "");
					videoFileUploader = new RealTimeVideoFileUploader(loggerFactory, httpClientFactory, savePath, remoteFileName, windowTitle2, appInfoProvider.AppId, new SynchronousProgress<long>(ProgressHandler));
					videoFileUploaderTask = videoFileUploader.UploadAsync(cts.Token);
				}
				Rectangle recordingArea = videoRecordingAreaSelection.RecordingArea;
				recordingArea = AdjustRegionToFactorOfTwo(recordingArea);
				bool ready = false;
				Icon originalIcon = trayIconService.CurrentIcon;
				try
				{
					trayIconService.ChangeIcon(ProductResources.StopVideo);
					operationStateService.TryStartRecording();
					using CancellationTokenSource recordCts = new CancellationTokenSource();
					Task<bool> recordVideoTask = StartRecording(GetScreenCaptureApi(), videoRecordingAreaSelection.MonitorName, recordingArea, savePath, audioCaptureSession);
					audioLevelMonitoringStopRequired = false;
					Task diskSpaceMonitorTask = diskSpaceMonitor.WaitForLowDiskSpaceAsync(314572800uL, recordCts.Token);
					bool waitingForStopRecordingDiskSpaceThreshold = false;
					List<Task> pendingTasks = new List<Task>(2) { recordVideoTask, diskSpaceMonitorTask };
					if (videoFileUploaderTask != null)
					{
						pendingTasks.Add(videoFileUploaderTask);
					}
					while (true)
					{
						Task task = await Task.WhenAny(pendingTasks);
						if (task == recordVideoTask)
						{
							recordCts.Cancel();
							await Task.WhenAny(diskSpaceMonitorTask);
							ready = await recordVideoTask;
							break;
						}
						if (task == videoFileUploaderTask)
						{
							if (videoFileUploaderTask.Exception?.InnerException is CloudStorageQuotaExceededException)
							{
								await mediator.Send((IRequest<Unit>)new StopVideoRecordingCommand(), default(CancellationToken));
								await Task.WhenAny<bool>(recordVideoTask);
								recordCts.Cancel();
								await Task.WhenAny(diskSpaceMonitorTask);
								await videoFileUploaderTask;
								break;
							}
							pendingTasks.Remove(videoFileUploaderTask);
							continue;
						}
						if (diskSpaceMonitorTask.Status == TaskStatus.RanToCompletion && !waitingForStopRecordingDiskSpaceThreshold && diskSpaceMonitor.GetFreeSpace() > 94371840)
						{
							logger.LogWarning("Detected low space on disk {diskName}", diskSpaceMonitor.DiskName);
							videoRecordingWindowManager.ShowLowDiskSpaceWindow();
							pendingTasks.Remove(diskSpaceMonitorTask);
							diskSpaceMonitorTask = diskSpaceMonitor.WaitForLowDiskSpaceAsync(94371840uL, recordCts.Token);
							pendingTasks.Add(diskSpaceMonitorTask);
							waitingForStopRecordingDiskSpaceThreshold = true;
							continue;
						}
						await mediator.Send((IRequest<Unit>)new StopVideoRecordingCommand(), default(CancellationToken));
						ready = await recordVideoTask;
						await diskSpaceMonitorTask;
						logger.LogWarning("Video recording has been automatically stopped due to low space on disk {diskName}", diskSpaceMonitor.DiskName);
						notificationService.ShowError(string.Format(Resources.Notify_VideoRecordingStoppedDueToLowDiskSpace, diskSpaceMonitor.DiskName));
						break;
					}
				}
				finally
				{
					operationStateService.StopRecording();
					trayIconService.ChangeIcon(originalIcon);
					videoRecordingWindowManager.Hide();
				}
				if (ready)
				{
					if (videoFileUploader != null)
					{
						try
						{
							videoFileUploader.Complete();
							fileSize = videoFileUploader.FileSize;
							Uri fileUrl = null;
							try
							{
								long startTime = Stopwatch.GetTimestamp();
								if (!videoFileUploaderTask.IsCompleted && !settings.SilentMode)
								{
									uploadProgressSession = progressWindowService.TryShow(Resources.Progress_UploadingVideoToCloud, showUi: true, null, cts.Token);
								}
								fileUrl = await videoFileUploader.GetFileUrlAsync(uploadProgressSession?.Token ?? cts.Token);
								if (settings.UseDirectLinkForVideo)
								{
									fileUrl = new Uri(fileUrl.ToString() + ".mp4");
								}
								bool waitingForStopRecordingDiskSpaceThreshold = false;
								try
								{
									clipboardService.SetText(fileUrl.ToString());
									uploadProgressSession?.UpdateMessage(Resources.Progress_LinkToVideoAlreadyInClipboard);
								}
								catch (ClipboardException exception2)
								{
									waitingForStopRecordingDiskSpaceThreshold = true;
									logger.LogError(exception2, "");
								}
								await videoFileUploaderTask.WaitAsync(uploadProgressSession?.Token ?? CancellationToken.None);
								if (waitingForStopRecordingDiskSpaceThreshold)
								{
									notificationService.ShowError(Resources.Notify_FailedToCopyVideoLinkToClipboard + " " + string.Format(Resources.Notify_FailedToCopyToClipboardExplanation, ProductResources.AppNameGenitive, ProductResources.AppName));
								}
								logger.LogInformation("Duration of video uploading after recording is finished: {time} ms", StopwatchHelper.GetDurationInWholeMillisecondsSince(startTime));
							}
							finally
							{
								uploadProgressSession?.Dispose();
							}
							if (fileUrl != null && settings.OpenVideoInBrowser && !settings.SilentMode)
							{
								await mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(fileUrl), default(CancellationToken));
							}
						}
						catch (OperationCanceledException)
						{
							throw;
						}
						catch (Exception exception3)
						{
							logger.LogError(exception3, "Failed to upload video to cloud");
							isMoveFileToDesktopRequired = true;
						}
					}
					else
					{
						CopyVideoToClipboard(savePath);
					}
				}
				else
				{
					logger.LogInformation("Video recording has been canceled");
				}
			}
			finally
			{
				if (videoFileUploaderTask != null && !videoFileUploaderTask.IsCompleted)
				{
					cts.Cancel();
					await Task.WhenAny(videoFileUploaderTask);
				}
				if (videoFileUploaderTask != null)
				{
					userQuotaService.Update();
				}
				videoFileUploader?.Dispose();
				if (isMoveFileToDesktopRequired)
				{
					string desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), localFileName);
					try
					{
						File.Move(savePath, desktopFilePath);
						logger.LogInformation("Video successfully moved to desktop: {path}", desktopFilePath);
						notificationService.ShowError(Resources.Notify_FailedToUploadVideoAndStoredOnDesktop, (NotificationOptions x) =>
						{
							x.WithCommand(new OpenFolderWithFileSelectedCommand(desktopFilePath));
						});
					}
					catch (Exception exception4)
					{
						logger.LogError(exception4, "Failed to move video to desktop");
						notificationService.ShowError(Resources.Notify_FailedToUploadVideo);
					}
				}
				if (settings.VideoSaveType != VideoSaveType.File)
				{
					FileHelpers.DeleteFile(savePath);
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (CloudStorageQuotaExceededException)
		{
			logger.LogError("Video recording has been automatically canceled due to insufficient cloud storage quota");
			notificationService.ShowError(Resources.Notify_CloudStorageQuotaExceeded_DeleteOnly, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenHistoryCommand());
			});
		}
		catch (Exception exception5)
		{
			logger.LogError(exception5, "Failed while running video recording");
			notificationService.ShowError(Resources.Notify_FailedToMakeVideo, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenSupportPageCommand());
			});
		}
		finally
		{
			if (audioLevelMonitoringStopRequired)
			{
				SendAudioLevelMonitoringCommand(VideoModuleCommand.audioLevelStop);
			}
			videoRecordingWindowManager.Hide();
		}
	}

	private async Task SendAudioLevelMonitoringCommand(VideoModuleCommand command, List<string> arguments = null)
	{
		try
		{
			await videoModule.SendCommand(command, arguments);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to send {command} command", command);
		}
	}

	private async Task<bool> StartRecording(ScreenCaptureApi screenCaptureApi, string monitorId, Rectangle recordingArea, string savePath, AudioCaptureSession audioCaptureSession)
	{
		for (int retriesCounter = 0; retriesCounter < 4; retriesCounter++)
		{
			if (retriesCounter > 0)
			{
				await VideoModuleAppProxy.RestartVideoModuleAsync(videoModule, retriesCounter);
			}
			await videoModule.SendCommand(VideoModuleCommand.start, new List<string>(14)
			{
				screenCaptureApi.ToString(),
				monitorId,
				string.Join(",", new _003C_003Ez__ReadOnlyArray<int>(new int[4] { recordingArea.Left, recordingArea.Top, recordingArea.Width, recordingArea.Height })),
				GetMaxResolution(),
				GetMaxFps().ToString(),
				savePath,
				audioCaptureSession.InputDeviceId,
				audioCaptureSession.IsInputMuted ? "1" : "0",
				audioCaptureSession.OutputDeviceId,
				audioCaptureSession.IsOutputMuted ? "1" : "0",
				GetMaxRecordTimeInMinutes().ToString(),
				settings.DrawCursorOnVideo ? "1" : "0",
				settings.HighlightMousePointer ? "1" : "0",
				settings.HighlightMouseClicks ? "1" : "0"
			});
			string[] array = await videoModule.ReceiveCommandResult(VideoModuleCommand.start, TimeSpan.FromMilliseconds(-1.0));
			if (array == null || array.Length < 1)
			{
				throw new Exception("Invalid response from video module for command 'start'");
			}
			switch (array[0].ToLower())
			{
			case "ready":
				return true;
			case "canceled":
				logger.LogInformation("Video recording has been canceled");
				return false;
			case "error":
			{
				if (Enum.TryParse<ErrorCode>(array[1], out var result))
				{
					switch (result)
					{
					default:
						throw new ErrorCodeException(result, null);
					case ErrorCode.General:
						break;
					case ErrorCode.DuplicateOutputUnsupported:
						goto end_IL_031f;
					}
				}
				throw new Exception("Video recording has been failed with code " + array[1] + ": " + array[2]);
			}
			default:
				{
					throw new Exception("StartRecording: Invalid execution path: " + string.Join(";", array));
				}
				end_IL_031f:
				break;
			}
		}
		throw new Exception("Cannot start video recording after 4 tries.");
	}

	private ScreenCaptureApi GetScreenCaptureApi()
	{
		if (!WgcSupportChecker.IsMouseHidingSupported())
		{
			return ScreenCaptureApi.DDA;
		}
		return ScreenCaptureApi.WGC;
	}

	private void ProgressHandler(long progress)
	{
		if (uploadProgressSession != null && fileSize > 0)
		{
			uploadProgressSession.Progress.Report((int)(progress * 100 / fileSize));
		}
	}

	private Rectangle AdjustRegionToFactorOfTwo(Rectangle region)
	{
		if (region.Width % 2 == 0 && region.Height % 2 == 0)
		{
			return region;
		}
		if (region.Width % 2 != 0)
		{
			region.Width--;
		}
		if (region.Height % 2 != 0)
		{
			region.Height--;
		}
		return region;
	}

	private int GetMaxRecordTimeInMinutes()
	{
		return 5;
	}

	private string GetVideoFileSavePath(string localFileName)
	{
		if (settings.VideoSaveType == VideoSaveType.Cloud)
		{
			return Path.GetTempPath() + Guid.NewGuid().ToString() + Path.GetExtension(localFileName);
		}
		return Path.Combine(settings.VideoSavePath, localFileName);
	}

	private string GetLocalFileName(string windowTitle)
	{
		return templateFileNameResolver.Resolve(settings.VideoFileNameTemplate, settings.VideoSavePath, ".mp4", new FileNameTemplateArgs(DateTime.Now, windowTitle));
	}

	private string GetRemoteFileName()
	{
		return string.Format(ProductResources.VideoBaseFileName, DateTime.Now.ToString("dd-MM-yyyy HHmmss", CultureInfo.InvariantCulture)) + ".mp4";
	}

	private int GetMaxFps()
	{
		VideoQualityLevel videoQualityLevel = settings.VideoQualityLevel;
		return videoQualityLevel switch
		{
			VideoQualityLevel.SD => 15, 
			VideoQualityLevel.HD => 25, 
			_ => 25, 
		};
	}

	private string GetMaxResolution()
	{
		VideoQualityLevel videoQualityLevel = settings.VideoQualityLevel;
		switch (videoQualityLevel)
		{
		case VideoQualityLevel.SD:
			return "480";
		case VideoQualityLevel.HD:
			return "720";
		default:
			return "720";
		}
	}

	private void CopyVideoToClipboard(string path)
	{
		try
		{
			long timestamp = Stopwatch.GetTimestamp();
			clipboardService.SetFile(path);
			logger.LogInformation("Video copied to clipboard [time={time}ms]", StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			notificationService.ShowMessage(Resources.Notify_FileSavedOnDiskAndToClipboard, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenFolderWithFileSelectedCommand(path)).WithRespectSilentMode(value: true);
			});
		}
		catch (ClipboardException exception)
		{
			logger.LogError(exception, "");
			notificationService.ShowError(Resources.Notify_FailedToCopyVideoToClipboard + " " + string.Format(Resources.Notify_FailedToCopyToClipboardExplanation, ProductResources.AppNameGenitive, ProductResources.AppName));
			notificationService.ShowMessage(Resources.Notify_FileSavedOnDisk, (NotificationOptions x) =>
			{
				x.WithCommand(new OpenFolderWithFileSelectedCommand(path)).WithRespectSilentMode(value: true);
			});
		}
	}
}
