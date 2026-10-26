using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace AudioDevices;

[ComImport]
[Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IMMNotificationClient
{
	void OnDeviceStateChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, AudioDeviceState newState);

	void OnDeviceAdded([MarshalAs(UnmanagedType.LPWStr)] string deviceId);

	void OnDeviceRemoved([MarshalAs(UnmanagedType.LPWStr)] string deviceId);

	void OnDefaultDeviceChanged(AudioDataFlow flow, AudioDeviceRole role, [MarshalAs(UnmanagedType.LPWStr)] string defaultDeviceId);

	void OnPropertyValueChanged([MarshalAs(UnmanagedType.LPWStr)] string deviceId, PROPERTYKEY key);
}
