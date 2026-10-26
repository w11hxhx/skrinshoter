using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Hardcodet.Wpf.TaskbarNotification;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public partial class NotifyIcon : TaskbarIcon
{
	public NotifyIcon(NotifyIconVM notifyIconVM)
	{
		InitializeComponent();
		TrayPopupResolved.Tag = this;
		Icon = ProductResources.AppIcon;
		DataContext = notifyIconVM;
	}

	private void TaskbarIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
	{
		(DataContext as NotifyIconVM)?.MouseUpCommand.Execute((e, MouseButton.Left));
		if (!e.Handled && TrayPopupResolved.IsOpen)
		{
			TrayPopupResolved.IsOpen = false;
			e.Handled = true;
		}
	}

	private void TaskbarIcon_TrayRightMouseUp(object sender, RoutedEventArgs e)
	{
		(DataContext as NotifyIconVM)?.MouseUpCommand.Execute((e, MouseButton.Right));
	}

	private void TaskbarIcon_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
	{
		(DataContext as NotifyIconVM)?.TrayBalloonTipClickedCommand.Execute(Tag);
	}

	public Rect GetIconRect()
	{
		Hardcodet.Wpf.TaskbarNotification.Interop.Point position = GetPopupTrayPosition();
		return new Rect(position.X - 16.0, position.Y - 16.0, 32.0, 32.0);
	}

}
