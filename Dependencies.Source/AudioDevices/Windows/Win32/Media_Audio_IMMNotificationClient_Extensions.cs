using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IMMNotificationClient_Extensions
{
	internal unsafe static HRESULT OnDeviceStateChanged(this IMMNotificationClient.Interface @this, string pwstrDeviceId, DEVICE_STATE dwNewState)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return @this.OnDeviceStateChanged(ptr, dwNewState);
		}
	}

	internal unsafe static HRESULT OnDeviceAdded(this IMMNotificationClient.Interface @this, string pwstrDeviceId)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return @this.OnDeviceAdded(ptr);
		}
	}

	internal unsafe static HRESULT OnDeviceRemoved(this IMMNotificationClient.Interface @this, string pwstrDeviceId)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return @this.OnDeviceRemoved(ptr);
		}
	}

	internal unsafe static HRESULT OnDefaultDeviceChanged(this IMMNotificationClient.Interface @this, EDataFlow flow, ERole role, string pwstrDefaultDeviceId)
	{
		fixed (char* ptr = pwstrDefaultDeviceId)
		{
			return @this.OnDefaultDeviceChanged(flow, role, ptr);
		}
	}

	internal unsafe static HRESULT OnPropertyValueChanged(this IMMNotificationClient.Interface @this, string pwstrDeviceId, PROPERTYKEY key)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return @this.OnPropertyValueChanged(ptr, key);
		}
	}
}
