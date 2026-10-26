using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;
using SkrinshoterApp.Services.Progress;

namespace SkrinshoterApp;

public class CloudFileUploadService
{
	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly AppInfoProvider appInfoProvider;

	private readonly NotificationService notificationService;

	private readonly ClipboardService clipboardService;

	private readonly IProgressWindowService progressWindowService;

	private readonly UserQuotaService userQuotaService;

	private readonly ILogger logger;

	public CloudFileUploadService(SettingsManager settingsManager, AppInfoProvider appInfoProvider, NotificationService notificationService, ClipboardService clipboardService, IProgressWindowService progressWindowService, UserQuotaService userQuotaService, ILogger<CloudFileUploadService> logger)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.appInfoProvider = appInfoProvider;
		this.notificationService = notificationService;
		this.clipboardService = clipboardService;
		this.progressWindowService = progressWindowService;
		this.userQuotaService = userQuotaService;
		this.logger = logger;
	}

	public async Task<string> UploadFile(string filePath, CloudFileUploadType fileType, string baseFileName, string fileExtension, string windowTitle, bool isScrollingCapture = false, bool respectSilentMode = true, bool copyLinkToClipboard = true)
	{
		string text = await GetFileUID();
		string fileUid = GetDatedUID(text);
		string url = GetFileSaveShortUrl(text, fileType);
		SkrApiFileUpload fileUpload = new SkrApiFileUpload(appInfoProvider.AppId, baseFileName, filePath, fileUid, (int)fileType, url, fileExtension, windowTitle, isScrollingCapture);
		IProgressSession session = progressWindowService.TryShow(Resources.Progress_UploadingFileToCloud, !respectSilentMode || !settings.SilentMode);
		try
		{
			try
			{
				fileUpload.OnProgress += (object s, int e) =>
				{
					session.Progress.Report(e);
				};
				if (isScrollingCapture)
				{
					url += "?scroll_capture=1";
				}
				bool clipboardError = false;
				if (copyLinkToClipboard)
				{
					try
					{
						clipboardService.SetText(url);
						session?.UpdateMessage(Resources.Progress_LinkToFileAlreadyInClipboard);
					}
					catch (ClipboardException exception)
					{
						clipboardError = true;
						logger.LogError(exception, "");
					}
				}
				await fileUpload.StartAsync(session.Token);
				if (!(await new SkrApiCheckCloudFileUploadedOk(fileUid).StartAsync()))
				{
					throw new Exception("Upload verification for id " + fileUid + " has been failed.");
				}
				await new SkrFileUids(settings).Drop(fileUpload.FileUidWithNoDate);
				settingsManager.SaveSettings();
				if (clipboardError)
				{
					notificationService.ShowError(Resources.Notify_FailedToCopyFileLinkToClipboard + " " + string.Format(Resources.Notify_FailedToCopyToClipboardExplanation, ProductResources.AppNameGenitive, ProductResources.AppName));
				}
			}
			catch (OperationCanceledException)
			{
				logger.LogInformation("File upload to cloud canceled");
				throw;
			}
			catch (CloudStorageQuotaExceededException)
			{
				notificationService.ShowError(Resources.Notify_CloudStorageQuotaExceeded_DeleteOnly, (NotificationOptions x) =>
				{
					x.WithCommand(new OpenHistoryCommand());
				});
				throw;
			}
			finally
			{
				userQuotaService.Update();
			}
			return url;
		}
		finally
		{
			if (session != null)
			{
				session.Dispose();
			}
		}
	}

	private async Task<string> GetFileUID()
	{
		string result = await new SkrFileUids(settings).PopNext();
		settingsManager.SaveSettings();
		return result;
	}

	private string GetDatedUID(string fileUID)
	{
		return DateTime.Now.ToString("ddMMyy", CultureInfo.InvariantCulture) + "/" + fileUID;
	}

	private string GetFileSaveShortUrl(string uid, CloudFileUploadType fileType)
	{
		return new SkrShortUrl(uid, fileType, settings).Generate();
	}
}
