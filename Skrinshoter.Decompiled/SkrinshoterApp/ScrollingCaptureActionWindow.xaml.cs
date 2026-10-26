using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class ScrollingCaptureActionWindow : PanelWindow
{
	protected override bool ExcludeFromCapture => true;

	public ScrollingCaptureActionWindow()
	{
		InitializeComponent();
	}

}
