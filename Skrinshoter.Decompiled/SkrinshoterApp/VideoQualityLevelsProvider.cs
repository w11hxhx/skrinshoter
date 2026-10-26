using System;
using System.Collections.Generic;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class VideoQualityLevelsProvider
{
	public const VideoQualityLevel Default = VideoQualityLevel.HD;

	public static IReadOnlyList<VideoQualityLevel> SupportedLevels => new _003C_003Ez__ReadOnlyArray<VideoQualityLevel>(new VideoQualityLevel[2]
	{
		VideoQualityLevel.SD,
		VideoQualityLevel.HD
	});

	public string GetDisplayName(VideoQualityLevel level)
	{
		return level switch
		{
			VideoQualityLevel.SD => Resources.VideoQuality_SD, 
			VideoQualityLevel.HD => Resources.VideoQuality_HD, 
			_ => throw new InvalidOperationException($"Unknown video quality level: {level}."), 
		};
	}

	public bool IsAccessible(VideoQualityLevel level)
	{
		return level switch
		{
			VideoQualityLevel.SD => true, 
			VideoQualityLevel.HD => true, 
			_ => false, 
		};
	}
}
