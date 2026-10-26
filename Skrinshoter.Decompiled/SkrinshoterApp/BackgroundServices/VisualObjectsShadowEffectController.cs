using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Hosting;

namespace SkrinshoterApp.BackgroundServices;

public sealed class VisualObjectsShadowEffectController : IHostedService
{
	private const string VisualDefaultKey = "ScreenshotEditingVisualObjectsDropShadowEffectDefault";

	private const string VisualKey = "ScreenshotEditingVisualObjectsDropShadowEffect";

	private const string TextDefaultKey = "ScreenshotEditingTextDropShadowEffectDefault";

	private const string TextKey = "ScreenshotEditingTextDropShadowEffect";

	private readonly SettingsManager settingsManager;

	private readonly Dispatcher dispatcher;

	public VisualObjectsShadowEffectController(SettingsManager settingsManager, Dispatcher dispatcher)
	{
		this.settingsManager = settingsManager;
		this.dispatcher = dispatcher;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
			Apply();
			return Task.CompletedTask;
		});
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return dispatcher.InvokeIfRequiredAsync(() =>
		{
			settingsManager.SettingsChanged -= SettingsManager_SettingsChanged;
			return Task.CompletedTask;
		});
	}

	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		if (e.New.DrawObjectShadows != e.Old.DrawObjectShadows)
		{
			Apply();
		}
	}

	private void Apply()
	{
		bool drawObjectShadows = settingsManager.Settings.DrawObjectShadows;
		Application.Current.Resources["ScreenshotEditingVisualObjectsDropShadowEffect"] = (drawObjectShadows ? Application.Current.Resources["ScreenshotEditingVisualObjectsDropShadowEffectDefault"] : null);
		Application.Current.Resources["ScreenshotEditingTextDropShadowEffect"] = (drawObjectShadows ? Application.Current.Resources["ScreenshotEditingTextDropShadowEffectDefault"] : null);
	}
}
