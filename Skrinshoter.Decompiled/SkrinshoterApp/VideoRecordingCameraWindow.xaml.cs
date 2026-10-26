using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class VideoRecordingCameraWindow : Window
{
	private HWND hwnd;

	private MovingRectangleLimiter movingRectangleLimiter;

	private ResizingRectangleLimiter resizingRectangleLimiter;

	private bool forceClose;

	public event EventHandler<EventArgs> WindowActivated;

	public VideoRecordingCameraWindow()
	{
		InitializeComponent();
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
		hwndSource.AddHook(WndProc);
		hwnd = (HWND)hwndSource.Handle;
		int windowLong = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, windowLong & -524289);
		int windowLong2 = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, windowLong2 | 0x80);
	}

	private unsafe IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
	{
		switch ((uint)msg)
		{
		case 33u:
			handled = true;
			return (IntPtr)3L;
		case 534u:
		{
			Rectangle movingRectangle = *(RECT*)(void*)lParam;
			movingRectangle = movingRectangleLimiter.Limit(movingRectangle);
			*(RECT*)(void*)lParam = movingRectangle;
			return (IntPtr)1;
		}
		case 532u:
		{
			_ = (int)wParam;
			Rectangle resizingRectangle = *(RECT*)(void*)lParam;
			resizingRectangle = resizingRectangleLimiter.Limit(resizingRectangle);
			*(RECT*)(void*)lParam = resizingRectangle;
			return (IntPtr)1;
		}
		default:
			return IntPtr.Zero;
		}
	}

	protected override void OnActivated(EventArgs e)
	{
		WindowActivated?.Invoke(this, EventArgs.Empty);
	}

	public void ForceClose()
	{
		forceClose = true;
		Close();
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		e.Cancel = !forceClose;
	}

	private void Window_MouseDown_Inside(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton == MouseButton.Left && DataContext is VideoRecordingCameraWindowVM videoRecordingCameraWindowVM)
		{
			PInvoke.GetWindowRect(hwnd, out var lpRect);
			movingRectangleLimiter = new MovingRectangleLimiter(((Rectangle)lpRect).Location, videoRecordingCameraWindowVM.SelectionRectangle);
			DragMove();
			PInvoke.GetWindowRect(hwnd, out lpRect);
			videoRecordingCameraWindowVM.SaveWindowRelativeRectToSettings(lpRect);
		}
	}

	private void Window_MouseDown_BorderTopLeft(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 4u);
	}

	private void Window_MouseDown_BorderTop(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 3u);
	}

	private void Window_MouseDown_BorderTopRight(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 5u);
	}

	private void Window_MouseDown_BorderRight(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 2u);
	}

	private void Window_MouseDown_BorderBottomRight(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 8u);
	}

	private void Window_MouseDown_BorderBottom(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 6u);
	}

	private void Window_MouseDown_BorderBottomLeft(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 7u);
	}

	private void Window_MouseDown_BorderLeft(object sender, MouseButtonEventArgs e)
	{
		ResizeWindow(sender, e, 1u);
	}

	private void ResizeWindow(object sender, MouseButtonEventArgs e, uint resizingDirection)
	{
		if (e.ChangedButton == MouseButton.Left && DataContext is VideoRecordingCameraWindowVM videoRecordingCameraWindowVM)
		{
			PInvoke.GetWindowRect(hwnd, out var lpRect);
			resizingRectangleLimiter = new ResizingRectangleLimiter(lpRect, videoRecordingCameraWindowVM.WindowAspectRatio, videoRecordingCameraWindowVM.SelectionRectangle, resizingDirection, (int)Math.Round(this.ValueToScreen(videoRecordingCameraWindowVM.WindowMinWidth)));
			Mouse.OverrideCursor = (sender as FrameworkElement)?.Cursor;
			videoRecordingCameraWindowVM.CanUpdateCameraCapturer = false;
			try
			{
				PInvoke.SendMessage(hwnd, 274u, 0xF000 | resizingDirection, 0);
			}
			finally
			{
				Mouse.OverrideCursor = null;
				videoRecordingCameraWindowVM.CanUpdateCameraCapturer = true;
			}
			PInvoke.GetWindowRect(hwnd, out lpRect);
			videoRecordingCameraWindowVM.SaveWindowRelativeRectToSettings(lpRect);
		}
	}

}
