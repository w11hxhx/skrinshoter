using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace SkrinshoterApp;

public partial class AboutTabView : UserControl
{
	private readonly DispatcherTimer toolTipTimer = new DispatcherTimer
	{
		Interval = TimeSpan.FromSeconds(2.0)
	};

	public AboutTabView()
	{
		InitializeComponent();
		toolTipTimer.Tick += (object s, EventArgs e) =>
		{
			toolTipTimer.Stop();
			if (AppId.ToolTip is ToolTip toolTip)
			{
				toolTip.IsOpen = false;
			}
		};
	}

	private void CopyToClipboardButton_Click(object sender, RoutedEventArgs e)
	{
		if (AppId.ToolTip is ToolTip toolTip)
		{
			toolTip.PlacementTarget = AppId;
			if (!toolTip.IsOpen)
			{
				toolTip.IsOpen = true;
			}
			toolTipTimer.Stop();
			toolTipTimer.Start();
		}
	}
}
