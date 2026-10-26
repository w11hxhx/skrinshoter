using System;
using System.Drawing;
using Common;
using ScreenRecorder.Commands;
using ScreenRecorderCommon;

namespace ScreenRecorder;

internal class PipelineConfiguration
{
	public ScreenCaptureApi ScreenCaptureApi { get; }

	public string MonitorName { get; }

	public Size MonitorResolution { get; }

	public Rectangle RecordingArea { get; }

	public int MaxOutputVideoHeight { get; }

	public int VideoFrameRate { get; }

	public string OutputFile { get; }

	public string InputAudioDeviceId { get; }

	public bool IsInputAudioDeviceMuted { get; }

	public string OutputAudioDeviceId { get; }

	public bool IsOutputAudioDeviceMuted { get; }

	public TimeSpan MaxRecordDuration { get; }

	public bool ShowMousePointer { get; }

	public bool HighlightMousePointer { get; }

	public bool HighlightMouseClicks { get; }

	public string VideoScaleAlgorithm { get; } = "SWS_BICUBIC";

	public bool CaptureTest { get; }

	public Size OutputVideoResolution { get; }

	public PipelineConfiguration(StartCommand startCommand)
	{
		if (startCommand == null)
		{
			throw new ArgumentNullException("startCommand");
		}
		ScreenCaptureApi = startCommand.ScreenCaptureApi;
		MonitorName = startCommand.MonitorName;
		RecordingArea = startCommand.RecordingArea;
		MaxOutputVideoHeight = startCommand.MaxOutputVideoHeight;
		VideoFrameRate = startCommand.VideoFrameRate;
		OutputFile = startCommand.OutputFile;
		InputAudioDeviceId = startCommand.InputAudioDeviceId;
		IsInputAudioDeviceMuted = startCommand.IsInputAudioDeviceMuted;
		OutputAudioDeviceId = startCommand.OutputAudioDeviceId;
		IsOutputAudioDeviceMuted = startCommand.IsOutputAudioDeviceMuted;
		MaxRecordDuration = startCommand.MaxRecordDuration;
		ShowMousePointer = startCommand.ShowMousePointer;
		HighlightMousePointer = startCommand.HighlightMousePointer;
		HighlightMouseClicks = startCommand.HighlightMouseClicks;
		MonitorResolution = new MonitorInfo(MonitorName).GetRectangle().Size;
		if (!new Rectangle(Point.Empty, MonitorResolution).Contains(RecordingArea))
		{
			throw new InvalidOperationException("Recording area extends beyond screen.");
		}
		OutputVideoResolution = CalculateOutputVideoResolution();
	}

	private Size CalculateOutputVideoResolution()
	{
		if (MaxOutputVideoHeight >= RecordingArea.Height)
		{
			return RecordingArea.Size;
		}
		double num = (double)MaxOutputVideoHeight / (double)RecordingArea.Height * (double)RecordingArea.Width;
		int num2 = 2 * (int)Math.Round(num / 2.0);
		if (num2 == 0)
		{
			num2 = 2;
		}
		return new Size(num2, MaxOutputVideoHeight);
	}
}
