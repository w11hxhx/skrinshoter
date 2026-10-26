using System.Drawing;

namespace SkrinshoterApp;

public class VideoRecordingAreaSelection
{
	public string MonitorName { get; }

	public Rectangle RecordingArea { get; }

	public VideoRecordingAreaSelection(string monitorName, Rectangle recordingArea)
	{
		MonitorName = monitorName;
		RecordingArea = recordingArea;
	}
}
