using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

[AnyThread]
public class AppInfoProvider
{
	public static bool IsFloomby => false;

	public static bool IsSkrinshoter => true;

	public Version Version { get; }

	public string AppId { get; }

	public AppInfoProvider(Settings settings, ILogger<AppInfoProvider> logger)
	{
		string input = (from x in Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyInformationalVersionAttribute>()
			select x.InformationalVersion).FirstOrDefault();
		Version = Version.Parse(input);
		AppId = settings.AppId;
		logger?.LogInformation("AppId: {appId}", AppId);
	}
}
