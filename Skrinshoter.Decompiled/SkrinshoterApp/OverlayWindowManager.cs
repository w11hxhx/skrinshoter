using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Common;

namespace SkrinshoterApp;

public class OverlayWindowManager
{
	private readonly Dictionary<OverlayWindowVM, OverlayWindow> windows = new Dictionary<OverlayWindowVM, OverlayWindow>();

	private readonly AreaSelectionManager areaSelectionManager;

	public IReadOnlyCollection<OverlayWindow> Windows => windows.Values;

	public IReadOnlyCollection<OverlayWindowVM> WindowVMs => windows.Keys;

	public OverlayWindowManager(IEnumerable<OverlayWindow> windows, AreaSelectionManager areaSelectionManager)
	{
		foreach (OverlayWindow window in windows)
		{
			this.windows[window.DataContext as OverlayWindowVM] = window;
		}
		this.areaSelectionManager = areaSelectionManager;
	}

	public OverlayWindowVM GetSelectedWindowVM()
	{
		if (!areaSelectionManager.HasArea)
		{
			return null;
		}
		return areaSelectionManager.Owner;
	}

	public OverlayWindow GetSelectedWindow()
	{
		OverlayWindowVM selectedWindowVM = GetSelectedWindowVM();
		if (selectedWindowVM != null)
		{
			return windows[selectedWindowVM];
		}
		return null;
	}

	public OverlayWindow GetWindowForVM(OverlayWindowVM windowVM)
	{
		return windows[windowVM];
	}
}
public sealed class OverlayWindowManager<TContext> : BaseDisposable where TContext : OverlayWindowContext
{
	private readonly Dictionary<OverlayWindowVM, TContext> contexts = new Dictionary<OverlayWindowVM, TContext>();

	private readonly AreaSelectionManager areaSelectionManager;

	public IReadOnlyCollection<TContext> Contexts => contexts.Values;

	public OverlayWindowManager(AreaSelectionManager areaSelectionManager, Func<MonitorInfo, TContext> contextFactory)
	{
		this.areaSelectionManager = areaSelectionManager ?? throw new ArgumentNullException("areaSelectionManager");
		if (contextFactory == null)
		{
			throw new ArgumentNullException("contextFactory");
		}
		foreach (MonitorInfo monitor in VirtualScreen.GetMonitors())
		{
			TContext val = contextFactory(monitor) ?? throw new InvalidOperationException("Overlay window context factory returned null.");
			contexts.Add(val.OverlayWindowVM, val);
		}
	}

	public TContext GetSelectedContext()
	{
		if (!areaSelectionManager.HasArea)
		{
			return null;
		}
		return contexts[areaSelectionManager.Owner];
	}

	public TContext GetContext(OverlayWindowVM overlayWindowVM)
	{
		if (overlayWindowVM == null)
		{
			throw new ArgumentNullException("overlayWindowVM");
		}
		return contexts[overlayWindowVM];
	}

	public TContext GetContextForMonitor(string monitorName)
	{
		if (monitorName == null)
		{
			throw new ArgumentNullException("monitorName");
		}
		return contexts.Values.FirstOrDefault((TContext x) => x.Monitor.MonitorName == monitorName) ?? throw new KeyNotFoundException("Overlay window context for monitor \"" + monitorName + "\" not found");
	}

	public void ShowAll()
	{
		foreach (TContext value in contexts.Values)
		{
			value.OverlayWindow.Show();
			value.OverlayWindow.SetPosition(value.Monitor.GetRectangle().Location);
			value.OverlayWindow.WindowState = WindowState.Maximized;
		}
	}

	public void HideAllExceptSelected()
	{
		OverlayWindow overlayWindow = GetSelectedContext()?.OverlayWindow;
		foreach (TContext value in contexts.Values)
		{
			if (value.OverlayWindow != overlayWindow)
			{
				value.OverlayWindow.Hide();
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}
		foreach (TContext value in contexts.Values)
		{
			value.Dispose();
		}
		contexts.Clear();
	}
}
