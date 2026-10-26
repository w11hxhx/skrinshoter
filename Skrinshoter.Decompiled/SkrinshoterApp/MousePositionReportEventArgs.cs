using System;

namespace SkrinshoterApp;

public sealed class MousePositionReportEventArgs : EventArgs
{
	public bool IsMouseInsideCaptureArea { get; }

	public bool CanInject { get; set; } = true;

	public MousePositionReportEventArgs(bool isMouseInsideCaptureArea)
	{
		IsMouseInsideCaptureArea = isMouseInsideCaptureArea;
	}
}
