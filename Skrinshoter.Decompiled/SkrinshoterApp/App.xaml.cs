using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

public partial class App : System.Windows.Application
{
	private void Application_Startup(object sender, StartupEventArgs e)
	{
		WpfGlobalSettings.Apply();
		System.Windows.Forms.Application.EnableVisualStyles();
		System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		ILogger<App> requiredService = Program.Services.GetRequiredService<ILogger<App>>();
		int num = RenderCapability.Tier >> 16;
		requiredService.LogInformation("WPF rendering tier: {tier}", num);
		Program.Services.GetRequiredService<NotifyIcon>();
		try
		{
			Program.Services.GetRequiredService<AutostartHandler>();
		}
		catch (Exception exception)
		{
			requiredService.LogError(exception, "Failed to create autostart handler");
		}
		SettingsWindow requiredService2 = Program.Services.GetRequiredService<SettingsWindow>();
		requiredService2.DataContext = Program.Services.GetRequiredService<SettingsWindowVM>();
		if (Program.Services.GetRequiredService<CommandLineArguments>().Action == CommandLineAction.SilentRun)
		{
			new WindowInteropHelper(requiredService2).EnsureHandle();
		}
		else
		{
			requiredService2.Show();
		}
		try
		{
			new WpfWarmer();
		}
		catch (Exception exception2)
		{
			requiredService.LogError(exception2, "Failed to run WPF warmer");
		}
	}
}
