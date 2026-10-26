using System;
using System.IO;
using SkrinshoterApp.Common;

namespace SkrinshoterApp;

public sealed class TemplateFileNameResolver
{
	private const int MaxFullPathLength = 259;

	private string cachedKey;

	private int cachedLastCounter;

	public string Resolve(FileNameTemplate template, string directory, string extension, FileNameTemplateArgs args)
	{
		if (template == null)
		{
			throw new ArgumentNullException("template");
		}
		if (directory == null)
		{
			throw new ArgumentNullException("directory");
		}
		if (extension == null)
		{
			throw new ArgumentNullException("extension");
		}
		if (args == null)
		{
			throw new ArgumentNullException("args");
		}
		if (!template.HasCounter)
		{
			FileNameTemplateArgs args2 = AdjustArgsToFitMaxPath(template, directory, extension, args, 0);
			string text = template.Format(args2) + extension;
			if (!File.Exists(Path.Combine(directory, text)))
			{
				cachedKey = null;
				cachedLastCounter = 0;
				return text;
			}
			template = template.WithImplicitCounterSuffix();
		}
		FileNameTemplateArgs args3 = AdjustArgsToFitMaxPath(template, directory, extension, args, int.MaxValue);
		string seriesKey = template.FormatSeriesKey(args3);
		string b = BuildCacheKey(directory, extension, seriesKey);
		if (!string.Equals(cachedKey, b, StringComparison.Ordinal))
		{
			cachedKey = b;
			cachedLastCounter = 0;
		}
		int startCounter = ((cachedLastCounter <= 0) ? 1 : cachedLastCounter);
		return BuildFileName(template, extension, seriesKey, cachedLastCounter = FindFirstFreeCounter(template, directory, extension, seriesKey, startCounter));
	}

	private static string BuildCacheKey(string directory, string extension, string seriesKey)
	{
		return TryGetFullPath(directory) + "\n" + extension + "\n" + seriesKey;
	}

	private static string BuildFileName(FileNameTemplate template, string extension, string seriesKey, int counter)
	{
		return template.ApplyCounter(seriesKey, counter) + extension;
	}

	private static bool FileNameExists(FileNameTemplate template, string directory, string extension, string seriesKey, int counter)
	{
		string path = BuildFileName(template, extension, seriesKey, counter);
		return File.Exists(Path.Combine(directory, path));
	}

	private static int FindFirstFreeCounter(FileNameTemplate template, string directory, string extension, string seriesKey, int startCounter)
	{
		if (startCounter <= 0)
		{
			startCounter = 1;
		}
		if (!FileNameExists(template, directory, extension, seriesKey, startCounter))
		{
			return startCounter;
		}
		if (startCounter == int.MaxValue)
		{
			ThrowMaxCounterReached();
		}
		int num = startCounter;
		long num2 = 1L;
		long num3 = startCounter + num2;
		while (num3 < int.MaxValue && FileNameExists(template, directory, extension, seriesKey, (int)num3))
		{
			num = (int)num3;
			num2 <<= 1;
			num3 = startCounter + num2;
		}
		int num4;
		if (num3 >= int.MaxValue)
		{
			num4 = int.MaxValue;
			if (FileNameExists(template, directory, extension, seriesKey, num4))
			{
				ThrowMaxCounterReached();
			}
		}
		else
		{
			num4 = (int)num3;
		}
		while (num + 1 < num4)
		{
			int num5 = num + (num4 - num) / 2;
			if (FileNameExists(template, directory, extension, seriesKey, num5))
			{
				num = num5;
			}
			else
			{
				num4 = num5;
			}
		}
		return num4;
	}

	private static void ThrowMaxCounterReached()
	{
		throw new IOException("Failed to resolve a unique file name: maximum counter value has been reached.");
	}

	private static FileNameTemplateArgs AdjustArgsToFitMaxPath(FileNameTemplate template, string directory, string extension, FileNameTemplateArgs args, int longestCounter)
	{
		int num = template.Text.CountSubstring("{w}");
		if (num == 0)
		{
			return args;
		}
		int maxBaseNameLength = GetMaxBaseNameLength(directory, extension);
		if (maxBaseNameLength <= 0)
		{
			throw new PathTooLongException("The target directory path is too long.");
		}
		FileNameTemplateArgs args2 = new FileNameTemplateArgs(args.Timestamp, string.Empty);
		string text = template.Format(args2, longestCounter);
		int num2 = maxBaseNameLength - text.Length;
		if (num2 <= 0)
		{
			throw new PathTooLongException("The resulting file name is too long. Shorten the template or the target directory path.");
		}
		int num3 = num2 / num;
		if (num3 <= 0)
		{
			return new FileNameTemplateArgs(args.Timestamp, string.Empty);
		}
		string text2 = args.WindowTitle ?? string.Empty;
		if (text2.Length <= num3)
		{
			return args;
		}
		return new FileNameTemplateArgs(args.Timestamp, text2.Substring(0, num3));
	}

	private static int GetMaxBaseNameLength(string directory, string extension)
	{
		directory = TryGetFullPath(directory);
		directory = directory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		return 259 - (directory.Length + 1) - extension.Length;
	}

	private static string TryGetFullPath(string path)
	{
		try
		{
			return Path.GetFullPath(path);
		}
		catch
		{
			return path;
		}
	}
}
