using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class VideoRecordingLowDiskSpaceWindow : Window
{
	public VideoRecordingLowDiskSpaceWindow()
	{
		InitializeComponent();
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		PInvoke.SetWindowDisplayAffinity(new HWND(new WindowInteropHelper(this).Handle), WINDOW_DISPLAY_AFFINITY.WDA_EXCLUDEFROMCAPTURE);
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}
