using System;

namespace SkrinshoterApp;

public sealed class FileNameTemplateArgs
{
	public DateTime Timestamp { get; }

	public string WindowTitle { get; }

	public FileNameTemplateArgs(DateTime timestamp, string windowTitle)
	{
		Timestamp = timestamp;
		WindowTitle = windowTitle ?? string.Empty;
	}
}
