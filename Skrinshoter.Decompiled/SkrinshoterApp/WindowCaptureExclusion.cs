using System;

namespace SkrinshoterApp;

public static class WindowCaptureExclusion
{
	public static bool CanHideWindowsFromCapture()
	{
		return Environment.OSVersion.Version.Build >= 22000;
	}
}
