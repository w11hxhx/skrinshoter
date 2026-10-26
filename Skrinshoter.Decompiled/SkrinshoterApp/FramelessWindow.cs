using System;
using System.Windows;
using System.Windows.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public class FramelessWindow : Window
{
	public static readonly DependencyProperty UseSystemShadowProperty = DependencyProperty.Register("UseSystemShadow", typeof(bool), typeof(FramelessWindow), new PropertyMetadata(false));

	public bool UseSystemShadow
	{
		get
		{
			return (bool)GetValue(UseSystemShadowProperty);
		}
		set
		{
			SetValue(UseSystemShadowProperty, value);
		}
	}

	protected virtual bool ExcludeFromCapture => false;

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		HWND hWnd = new HWND(new WindowInteropHelper(this).Handle);
		int num = PInvoke.GetWindowLong(hWnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE) & -524289;
		if (UseSystemShadow)
		{
			num |= 0x40000;
		}
		PInvoke.SetWindowLong(hWnd, WINDOW_LONG_PTR_INDEX.GWL_STYLE, num);
		if (ExcludeFromCapture)
		{
			PInvoke.SetWindowDisplayAffinity(hWnd, WINDOW_DISPLAY_AFFINITY.WDA_EXCLUDEFROMCAPTURE);
		}
	}
}
