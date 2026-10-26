using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using Windows.Win32;

namespace SkrinshoterApp;

public partial class NotifyIconPopup : Popup
{
	public NotifyIconPopup()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		IsOpen = false;
	}

	private void Popup_Opened(object sender, EventArgs e)
	{
		(DataContext as NotifyIconVM)?.PopupOpenedCommand.Execute(null);
	}

	private void Popup_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Escape && e.KeyboardDevice.Modifiers == ModifierKeys.None)
		{
			IsOpen = false;
		}
	}

	private void Popup_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		Rect iconRect = (Tag as NotifyIcon).GetIconRect();
		PInvoke.GetCursorPos(out var lpPoint);
		Point point = new Point(lpPoint.X, lpPoint.Y);
		bool flag = (PInvoke.GetAsyncKeyState(1) & 0x8000) != 0;
		bool flag2 = (PInvoke.GetAsyncKeyState(4) & 0x8000) != 0;
		if (!iconRect.Contains(point) || !(flag | flag2))
		{
			IsOpen = false;
		}
	}
}
