using System;

namespace SkrinshoterApp;

public class OverlayWindowStateManager
{
	public OverlayWindowState State { get; private set; }

	public OverlayWindowVM ActiveWindowVM { get; private set; }

	public event EventHandler StateChanged;

	public void SetState(OverlayWindowState state, OverlayWindowVM activeWindowVM = null)
	{
		if (activeWindowVM == null && state.IsProcess())
		{
			throw new ArgumentNullException("activeWindowVM");
		}
		if (state != State || activeWindowVM != ActiveWindowVM)
		{
			State = state;
			ActiveWindowVM = activeWindowVM;
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
