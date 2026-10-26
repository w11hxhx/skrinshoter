using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class OverlayWindow : Window
{
	private readonly bool createForVideo;

	private HWND hwnd;

	private bool forceClose;

	private bool darkAreaFadeOutFinished;

	public OverlayWindow(bool createForVideo)
	{
		this.createForVideo = createForVideo;
		AllowsTransparency = createForVideo;
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
		if (createForVideo)
		{
			PInvoke.SetWindowDisplayAffinity(hwnd, WINDOW_DISPLAY_AFFINITY.WDA_EXCLUDEFROMCAPTURE);
		}
		(Resources["DarkAreaFadeIn"] as Storyboard).Begin();
	}

	public void MakeTransparentForMouseEvents()
	{
		int windowLong = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
		PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, windowLong | 0x20);
	}

	public void ForceClose()
	{
		forceClose = true;
		Close();
	}

	private async void Window_Closing(object sender, CancelEventArgs e)
	{
		if (!forceClose)
		{
			e.Cancel = true;
		}
		else if (!darkAreaFadeOutFinished)
		{
			e.Cancel = true;
			(Resources["DarkAreaFadeOut"] as Storyboard).Begin();
			await Task.Delay(120);
			darkAreaFadeOutFinished = true;
			ForceClose();
		}
	}

	private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
	{
		DrawingCanvas.Focus();
	}

	private void Window_MouseEnter(object sender, MouseEventArgs e)
	{
		(DataContext as OverlayWindowVM).IsMouseOverWindow = true;
	}

	private void Window_MouseLeave(object sender, MouseEventArgs e)
	{
		(DataContext as OverlayWindowVM).IsMouseOverWindow = false;
	}

}
