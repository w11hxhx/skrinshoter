using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Windows.Win32;
using Windows.Win32.System.SystemInformation;

namespace SkrinshoterApp;

public class SystemInfoLogger
{
	private readonly ILogger logger;

	public SystemInfoLogger(ILogger<SystemInfoLogger> logger)
	{
		this.logger = logger;
	}

	public void LogSystemInfo()
	{
		LogWindowsVersion();
		LogSmbiosInfo();
		LogCpuModel();
		LogRamInfo();
		LogMonitorInfo();
		LogUtcOffset();
	}

	private void LogWindowsVersion()
	{
		try
		{
			using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", writable: false) ?? throw new InvalidOperationException("Registry key 'SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion' not found.");
			int build = Environment.OSVersion.Version.Build;
			int num = Convert.ToInt32(registryKey.GetValue("UBR") ?? ((object)0));
			string text = ((num > 0) ? $"{build}.{num}" : build.ToString());
			string text2 = ((build >= 22000) ? "Windows 11" : "Windows 10");
			string text3 = registryKey.GetValue("EditionID") as string;
			if (string.IsNullOrWhiteSpace(text3))
			{
				text3 = "Unknown";
			}
			string text4 = registryKey.GetValue("DisplayVersion") as string;
			if (string.IsNullOrWhiteSpace(text4))
			{
				text4 = registryKey.GetValue("ReleaseId") as string;
			}
			if (string.IsNullOrWhiteSpace(text4))
			{
				text4 = "Unknown";
			}
			logger.LogInformation("OS: {family} {editionId} {marketingVersion} (OS build {osBuild})", text2, text3, text4, text);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to get Windows version");
		}
	}

	private void LogSmbiosInfo()
	{
		try
		{
			using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS", writable: false) ?? throw new InvalidOperationException("Registry key 'HARDWARE\\DESCRIPTION\\System\\BIOS' not found.");
			string text = (registryKey.GetValue("SystemManufacturer") as string) ?? "";
			string text2 = (registryKey.GetValue("SystemProductName") as string) ?? "";
			logger.LogInformation("SMBIOS: SystemManufacturer='{systemManufacturer}', SystemProductName='{systemProductName}'", text, text2);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to get SMBIOS information");
		}
	}

	private void LogCpuModel()
	{
		try
		{
			using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", writable: false) ?? throw new InvalidOperationException("Registry key 'HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0' not found.");
			string text = registryKey.GetValue("ProcessorNameString") as string;
			text = ((!string.IsNullOrWhiteSpace(text)) ? text.Trim() : "Unknown");
			int num = Convert.ToInt32(registryKey.GetValue("~MHz") ?? ((object)0));
			string text2 = ((num > 0) ? (((double)num / 1000.0).ToString("0.00", CultureInfo.InvariantCulture) + " GHz") : "Unknown");
			int processorCount = Environment.ProcessorCount;
			logger.LogInformation("CPU: {cpuModel} ({baseGHzText}), LogicalProcessors={logical}", text, text2, processorCount);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to get CPU information");
		}
	}

	private void LogRamInfo()
	{
		try
		{
			if (!PInvoke.GetPhysicallyInstalledSystemMemory(out var TotalMemoryInKilobytes))
			{
				throw new WinApiException("GetPhysicallyInstalledSystemMemory()", new Win32Exception());
			}
			MEMORYSTATUSEX lpBuffer = new MEMORYSTATUSEX
			{
				dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX))
			};
			if (!PInvoke.GlobalMemoryStatusEx(ref lpBuffer))
			{
				throw new WinApiException("GlobalMemoryStatusEx()", new Win32Exception());
			}
			double num = Math.Round((double)TotalMemoryInKilobytes / 1024.0 / 1024.0, 1, MidpointRounding.AwayFromZero);
			double num2 = Math.Round((double)lpBuffer.ullTotalPhys / 1024.0 / 1024.0 / 1024.0, 1, MidpointRounding.AwayFromZero);
			string text = num.ToString("N1", CultureInfo.CurrentCulture) + " GB";
			string text2 = num2.ToString("N1", CultureInfo.CurrentCulture) + " GB";
			logger.LogInformation("RAM: {installed} ({usable} usable)", text, text2);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to get RAM information");
		}
	}

	private void LogMonitorInfo()
	{
		try
		{
			List<string> list = new List<string>();
			foreach (MonitorInfo monitor in VirtualScreen.GetMonitors())
			{
				Rectangle rectangle = monitor.GetRectangle();
				list.Add($"({rectangle.X},{rectangle.Y})[{rectangle.Width}x{rectangle.Height}]{{{Math.Round(monitor.GetScaleFactor() * 100.0)}%}}");
			}
			logger.LogInformation("Monitors: {monitors}", string.Join(", ", list));
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to get monitor information");
		}
	}

	private void LogUtcOffset()
	{
		TimeSpan offset = DateTimeOffset.Now.Offset;
		string text = ((offset < TimeSpan.Zero) ? "-" : "+") + offset.Duration().ToString("hh\\:mm");
		logger.LogInformation("Local time UTC offset: {UtcOffset}", text);
	}
}
