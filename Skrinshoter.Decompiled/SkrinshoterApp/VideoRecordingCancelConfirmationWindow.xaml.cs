using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class VideoRecordingCancelConfirmationWindow : FramelessWindow
{
	protected override bool ExcludeFromCapture => true;

	public VideoRecordingCancelConfirmationWindow()
	{
		InitializeComponent();
	}

	private void AbortButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
	}

	private void CloseButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		DragMove();
	}

}
