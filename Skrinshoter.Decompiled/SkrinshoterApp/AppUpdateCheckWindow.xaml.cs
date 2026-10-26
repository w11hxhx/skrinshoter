using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class AppUpdateCheckWindow : Window
{
	public AppUpdateCheckWindow(AppUpdateCheckWindowVM viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		Top -= 190.0;
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		(DataContext as IDisposable)?.Dispose();
	}

}
