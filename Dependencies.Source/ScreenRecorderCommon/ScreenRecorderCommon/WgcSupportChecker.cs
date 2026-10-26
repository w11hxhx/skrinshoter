using System;

namespace ScreenRecorderCommon;

public static class WgcSupportChecker
{
	public static bool IsApiSupported()
	{
		return Environment.OSVersion.Version.Build >= 18362;
	}

	public static bool IsMouseHidingSupported()
	{
		return Environment.OSVersion.Version.Build >= 19041;
	}
}
