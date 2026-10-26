using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;

namespace SkrinshoterApp.BackgroundServices;

public sealed class FreeSettingsEnforcer : IHostedService
{
	private readonly SettingsManager settingsManager;

	private readonly Dispatcher dispatcher;

	private readonly JpegQualityLevelsProvider jpegQualityLevelsProvider;

	private readonly VideoQualityLevelsProvider videoQualityLevelsProvider;

	public FreeSettingsEnforcer(SettingsManager settingsManager, Dispatcher dispatcher, JpegQualityLevelsProvider jpegQualityLevelsProvider, VideoQualityLevelsProvider videoQualityLevelsProvider)
	{
		this.settingsManager = settingsManager;
		this.dispatcher = dispatcher;
		this.jpegQualityLevelsProvider = jpegQualityLevelsProvider;
		this.videoQualityLevelsProvider = videoQualityLevelsProvider;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			Enforce();
			return Task.CompletedTask;
		});
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private void Enforce()
	{
		ApplyForFree(settingsManager.Settings);
		settingsManager.SaveSettings();
	}

	private void ApplyForFree(Settings settings)
	{
		if (!jpegQualityLevelsProvider.IsAccessible(settings.JpegQualityLevel))
		{
			settings.JpegQualityLevel = 80;
		}
		if (!videoQualityLevelsProvider.IsAccessible(settings.VideoQualityLevel))
		{
			settings.VideoQualityLevel = VideoQualityLevel.HD;
		}
		settings.UseDirectLinkForScreenshot = false;
		settings.UseDirectLinkForVideo = false;
		settings.VideoSaveType = VideoSaveType.Cloud;
	}
}
