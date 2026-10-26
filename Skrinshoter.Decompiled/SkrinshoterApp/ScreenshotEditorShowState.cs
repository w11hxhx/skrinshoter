using System;

namespace SkrinshoterApp;

public class ScreenshotEditorShowState
{
	public bool ShouldShow { get; private set; }

	public event EventHandler ShouldShowChanged;

	public ScreenshotEditorShowState(bool shouldShow)
	{
		ShouldShow = shouldShow;
	}

	public void SetShouldShow(bool shouldShow)
	{
		if (ShouldShow != shouldShow)
		{
			ShouldShow = shouldShow;
			ShouldShowChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public void ToggleShouldShow()
	{
		ShouldShow = !ShouldShow;
		ShouldShowChanged?.Invoke(this, EventArgs.Empty);
	}
}
