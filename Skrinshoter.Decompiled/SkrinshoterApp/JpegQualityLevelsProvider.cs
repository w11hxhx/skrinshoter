using System;
using System.Collections.Generic;

namespace SkrinshoterApp;

public class JpegQualityLevelsProvider
{
	public const int Default = 80;

	public static IReadOnlyList<int> SupportedLevels => new _003C_003Ez__ReadOnlyArray<int>(new int[4] { 50, 60, 70, 80 });

	public string GetDisplayName(int level)
	{
		if (level < 0 || level > 100)
		{
			throw new ArgumentOutOfRangeException("level");
		}
		return level.ToString();
	}

	public bool IsAccessible(int level)
	{
		if (level < 0 || level > 100)
		{
			throw new ArgumentOutOfRangeException("level");
		}
		return level <= 80;
	}
}
