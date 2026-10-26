using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class VideoRecordingMutedMicrophoneWindow : PanelWindow
{
	protected override bool ExcludeFromCapture => true;

	public VideoRecordingMutedMicrophoneWindow()
	{
		InitializeComponent();
	}

}
