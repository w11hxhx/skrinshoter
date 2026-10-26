using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Variant;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace AudioDevices;

internal static class PropVariantHelper
{
	public unsafe static bool TryReadStringProperty(IPropertyStore* propertyStore, PROPERTYKEY key, out string value)
	{
		value = null;
		if (propertyStore->GetValue(in key, out var pv).Failed)
		{
			return false;
		}
		try
		{
			if (pv.Anonymous.Anonymous.vt != VARENUM.VT_LPWSTR)
			{
				return false;
			}
			PWSTR pwszVal = pv.Anonymous.Anonymous.Anonymous.pwszVal;
			if (pwszVal.Value == null)
			{
				return false;
			}
			value = pwszVal.ToString();
			return true;
		}
		finally
		{
			PInvoke.PropVariantClear(ref pv);
		}
	}
}
