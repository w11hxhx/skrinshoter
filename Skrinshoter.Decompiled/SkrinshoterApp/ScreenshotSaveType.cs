using System;

namespace SkrinshoterApp;

[Flags]
public enum ScreenshotSaveType
{
	Clipboard = 1,
	Cloud = 2,
	File = 4,
	ExclusiveMask = Clipboard | Cloud
}
