using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class SettingsWindow : Window
{
	private bool forceClose;

	public SettingsWindow()
	{
		InitializeComponent();
	}

	public void ForceClose()
	{
		forceClose = true;
		Close();
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		if (!forceClose)
		{
			e.Cancel = true;
			Hide();
		}
	}

	protected override void OnClosed(EventArgs e)
	{
		(DataContext as IDisposable)?.Dispose();
		base.OnClosed(e);
	}

	private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.OriginalSource == sender)
		{
			TabControl tabControl = (TabControl)sender;
			(tabControl.Template.FindName("ContentScroll", tabControl) as ScrollViewer)?.ScrollToHome();
		}
	}

}
