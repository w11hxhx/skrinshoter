using System;
using System.Collections.Generic;

namespace SkrinshoterApp;

public class ScreenshotFileFormatsProvider
{
	public const ScreenshotFileFormat Default = ScreenshotFileFormat.Jpeg;

	public static IReadOnlyList<ScreenshotFileFormat> SupportedFormats => new _003C_003Ez__ReadOnlyArray<ScreenshotFileFormat>(new ScreenshotFileFormat[2]
	{
		ScreenshotFileFormat.Png,
		ScreenshotFileFormat.Jpeg
	});

	public string GetDisplayName(ScreenshotFileFormat format)
	{
		return format switch
		{
			ScreenshotFileFormat.Png => "PNG", 
			ScreenshotFileFormat.Jpeg => "JPEG", 
			_ => throw new InvalidOperationException($"Unknown screenshot file format: {format}."), 
		};
	}

	public string GetFileExtension(ScreenshotFileFormat format)
	{
		return format switch
		{
			ScreenshotFileFormat.Png => ".png", 
			ScreenshotFileFormat.Jpeg => ".jpg", 
			_ => throw new InvalidOperationException($"Unknown screenshot file format: {format}."), 
		};
	}
}
