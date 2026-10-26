using System;

namespace SkrinshoterApp;

public class ScreenshotEditingActionSelectedEventArgs : EventArgs
{
	public ScreenshotEditingAction Action { get; }

	public ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction action)
	{
		Action = action;
	}
}
