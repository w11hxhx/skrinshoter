using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using Common;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class ProgressWindow : Window
{
	private HWND hwnd;

	private bool forceClose;

	public ProgressWindow()
	{
		InitializeComponent();
		Opacity = 0.0;
		SizeChanged += (object _, SizeChangedEventArgs __) =>
		{
			if (IsLoaded)
			{
				PlaceBottomRightOnPrimary();
			}
		};
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		hwnd = new HWND(new WindowInteropHelper(this).Handle);
		int windowLong = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, windowLong & -524289);
	}

	protected override void OnContentRendered(EventArgs e)
	{
		base.OnContentRendered(e);
		PlaceBottomRightOnPrimary();
		Opacity = 1.0;
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

	private void PlaceBottomRightOnPrimary()
	{
		if (PresentationSource.FromVisual(this) != null)
		{
			Rectangle workingArea = new MonitorInfo().GetWorkingArea();
			System.Windows.Size screenPixelSize = this.GetScreenPixelSize();
			System.Windows.Point position = new System.Windows.Point((double)workingArea.Right - ActualWidth / screenPixelSize.Width, (double)workingArea.Bottom - ActualHeight / screenPixelSize.Height);
			this.SetPosition(position);
		}
	}

}
