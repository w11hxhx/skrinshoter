using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Common;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dwm;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public static class WindowExtensions
{
	public static void SetPosition(this Window window, System.Windows.Point position, bool activate = true)
	{
		window.SetPosition(new System.Drawing.Point((int)Math.Round(position.X), (int)Math.Round(position.Y)), activate);
	}

	public static void SetPosition(this Window window, System.Drawing.Point position, bool activate = true)
	{
		IntPtr handle = new WindowInteropHelper(window).Handle;
		SET_WINDOW_POS_FLAGS sET_WINDOW_POS_FLAGS = SET_WINDOW_POS_FLAGS.SWP_NOSIZE | SET_WINDOW_POS_FLAGS.SWP_NOZORDER;
		if (!activate)
		{
			sET_WINDOW_POS_FLAGS |= SET_WINDOW_POS_FLAGS.SWP_NOACTIVATE;
		}
		if (!PInvoke.SetWindowPos(new HWND(handle), HWND.Null, position.X, position.Y, 0, 0, sET_WINDOW_POS_FLAGS))
		{
			throw new WinApiException("SetWindowPos()", new Win32Exception());
		}
	}

	public static bool? ShowDialogCentered(this Window window, Rect screenRect)
	{
		MonitorInfo monitor = new MonitorInfo(PInvoke.MonitorFromRect((RECT)screenRect.ToRectangle(), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST));
		window.SourceInitialized += (object _, EventArgs _) =>
		{
			window.SetCloaked(cloaked: true);
		};
		window.ContentRendered += (object _, EventArgs _) =>
		{
			if ((int)Math.Round(VisualTreeHelper.GetDpi(window).PixelsPerInchX) != monitor.GetDpi())
			{
				window.SetPosition(monitor.GetRectangle().Location, activate: false);
			}
			Rect rect = window.RectToScreen(new Rect(0.0, 0.0, window.ActualWidth, window.ActualHeight));
			Rect rect2 = new Rect(screenRect.X + (screenRect.Width - rect.Width) / 2.0, screenRect.Y + (screenRect.Height - rect.Height) / 2.0, rect.Width, rect.Height).MoveInsideRect(monitor.GetWorkingArea().ToRect());
			window.SetPosition(rect2.Location);
			window.Dispatcher.InvokeAsync(() =>
			{
				window.SetCloaked(cloaked: false);
			}, DispatcherPriority.ContextIdle);
		};
		return window.ShowDialog();
	}

	private unsafe static void SetCloaked(this Window window, bool cloaked)
	{
		HWND hwnd = new HWND(new WindowInteropHelper(window).Handle);
		BOOL bOOL = cloaked;
		PInvoke.DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_CLOAK, &bOOL, (uint)sizeof(BOOL));
	}

	public static MonitorInfo GetMonitor(this Window window)
	{
		return new MonitorInfo(PInvoke.MonitorFromWindow(new HWND(new WindowInteropHelper(window).Handle), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST));
	}

	public static void EnsureLayoutUpdated(this Window window)
	{
		if (window == null)
		{
			throw new ArgumentNullException("window");
		}
		if (window.IsVisible)
		{
			window.UpdateLayout();
			return;
		}
		double left = window.Left;
		double top = window.Top;
		try
		{
			window.Left = 1000000.0;
			window.Top = 1000000.0;
			window.Show();
			window.UpdateLayout();
		}
		finally
		{
			window.Hide();
			window.Left = left;
			window.Top = top;
		}
	}
}
