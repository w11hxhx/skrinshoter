using System.Globalization;
using Windows.Win32;

namespace SkrinshoterApp;

public class UserCountryProvider
{
	public string GetCountryCode()
	{
		string text = GetCountryCodeByGeoName();
		if (string.IsNullOrWhiteSpace(text))
		{
			text = RegionInfo.CurrentRegion.TwoLetterISORegionName;
		}
		return text.ToUpperInvariant();
	}

	private static string GetCountryCodeByGeoName()
	{
		FreeLibrarySafeHandle moduleHandle = PInvoke.GetModuleHandle("kernel32.dll");
		if (moduleHandle.IsInvalid)
		{
			return null;
		}
		if (PInvoke.GetProcAddress(moduleHandle, "GetUserDefaultGeoName").IsNull)
		{
			return null;
		}
		char[] array = new char[16];
		int num = PInvoke.GetUserDefaultGeoName(array);
		if (num <= 0)
		{
			return null;
		}
		if (array[num - 1] == '\0')
		{
			num--;
		}
		if (num <= 0)
		{
			return null;
		}
		return new string(array, 0, num);
	}
}
