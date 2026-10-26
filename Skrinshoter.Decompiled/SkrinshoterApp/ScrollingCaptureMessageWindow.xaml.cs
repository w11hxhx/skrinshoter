using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class ScrollingCaptureMessageWindow : PanelWindow
{
	protected override bool ExcludeFromCapture => true;

	public ScrollingCaptureMessageWindow()
	{
		InitializeComponent();
	}

	public void SetMessage(string message)
	{
		Message.Text = message;
	}

}
