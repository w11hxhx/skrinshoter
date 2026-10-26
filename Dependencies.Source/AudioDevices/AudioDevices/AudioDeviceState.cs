using System;

namespace AudioDevices;

[Flags]
public enum AudioDeviceState
{
	Active = 1,
	Disabled = 2,
	NotPresent = 4,
	Unplugged = 8,
	All = Active | Disabled | NotPresent | Unplugged
}
