using System;

namespace SkrinshoterApp;

[Flags]
public enum ScreenshotUploadDestination
{
	None = 0,
	Skrinshoter = 1,
	KappaLol = 2,
	Xyli = 4,
	ZeroXl1r = 8,
	NoikCloud = 16,
	All = Skrinshoter | KappaLol | Xyli | ZeroXl1r | NoikCloud
}
