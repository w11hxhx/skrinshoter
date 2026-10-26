using System;
using Common;

namespace SkrinshoterApp;

public class OverlayWindowContext : BaseDisposable
{
	public MonitorInfo Monitor { get; }

	public OverlayWindow OverlayWindow { get; }

	public OverlayWindowVM OverlayWindowVM { get; }

	public OverlayWindowContext(MonitorInfo monitor, OverlayWindow overlayWindow, OverlayWindowVM overlayWindowVM)
	{
		Monitor = monitor ?? throw new ArgumentNullException("monitor");
		OverlayWindow = overlayWindow ?? throw new ArgumentNullException("overlayWindow");
		OverlayWindowVM = overlayWindowVM ?? throw new ArgumentNullException("overlayWindowVM");
		if (overlayWindow.DataContext != null && overlayWindow.DataContext != overlayWindowVM)
		{
			throw new InvalidOperationException("OverlayWindow already has another DataContext.");
		}
		overlayWindow.DataContext = overlayWindowVM;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			OverlayWindow.ForceClose();
		}
	}
}
