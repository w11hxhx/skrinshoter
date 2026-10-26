using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class ScreenshotEditingDrawingWindow : Window
{
	private HWND hwnd;

	private bool forceClose;

	public ScreenshotEditingDrawingWindow()
	{
		InitializeComponent();
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		hwnd = new HWND(new WindowInteropHelper(this).Handle);
		int windowLong = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, windowLong & -524289);
		int windowLong2 = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, windowLong2 | 0x80);
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

}
