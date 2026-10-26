using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

internal class AutostartHandler
{
	private const string RegistryKeyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

	private static string AppName = Assembly.GetExecutingAssembly().GetName().Name;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly ILogger logger;

	public AutostartHandler(SettingsManager settingsManager, ILogger<AutostartHandler> logger)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.logger = logger;
		UpdateAutostartSetting();
		settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
	}

	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		if (e.Old.Autostart != e.New.Autostart)
		{
			UpdateAutostartSetting();
		}
	}

	private void UpdateAutostartSetting()
	{
		if (settings.Autostart)
		{
			try
			{
				AddToStartup();
				return;
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to enable app autostart");
				MessageBox.Show(Resources.Notify_FailedToChangeAutostartSettings, ProductResources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				settings.Autostart = false;
				settingsManager.SaveSettings();
				return;
			}
		}
		RemoveFromStartup();
	}

	private void AddToStartup()
	{
		using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
		if (registryKey == null)
		{
			throw new InvalidOperationException("Failed to open Software\\Microsoft\\Windows\\CurrentVersion\\Run");
		}
		string executablePath = Application.ExecutablePath;
		registryKey.SetValue(AppName, "\"" + executablePath + "\" -s");
		logger.LogInformation("App autostart enabled");
	}

	private void RemoveFromStartup()
	{
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
			if (registryKey == null)
			{
				throw new InvalidOperationException("Failed to open Software\\Microsoft\\Windows\\CurrentVersion\\Run");
			}
			if (registryKey.GetValue(AppName) != null)
			{
				registryKey.DeleteValue(AppName);
				logger.LogInformation("App autostart disabled");
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to disable app autostart");
		}
	}
}
