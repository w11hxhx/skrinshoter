using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Common;

namespace SkrinshoterApp;

public sealed class FileNameTemplate
{
	private static readonly HashSet<char> InvalidFileNameChars;

	public string Text { get; }

	public bool HasCounter { get; }

	public FileNameTemplate(string template)
	{
		if (template == null)
		{
			throw new ArgumentNullException("template");
		}
		Validate(template);
		Text = template;
		HasCounter = ContainsCounter(template);
	}

	public string Format(FileNameTemplateArgs args, int counter = 0)
	{
		string seriesKey = FormatSeriesKey(args);
		return ApplyCounter(seriesKey, counter);
	}

	public string FormatSeriesKey(FileNameTemplateArgs args)
	{
		if (args == null)
		{
			throw new ArgumentNullException("args");
		}
		DateTime timestamp = args.Timestamp;
		return Text.Replace("{YYYY}", timestamp.ToString("yyyy", CultureInfo.InvariantCulture)).Replace("{MM}", timestamp.ToString("MM", CultureInfo.InvariantCulture)).Replace("{DD}", timestamp.ToString("dd", CultureInfo.InvariantCulture))
			.Replace("{hh}", timestamp.ToString("HH", CultureInfo.InvariantCulture))
			.Replace("{mm}", timestamp.ToString("mm", CultureInfo.InvariantCulture))
			.Replace("{ss}", timestamp.ToString("ss", CultureInfo.InvariantCulture))
			.Replace("{w}", SanitizeFileNamePart(args.WindowTitle));
	}

	public string ApplyCounter(string seriesKey, int counter)
	{
		if (seriesKey == null)
		{
			throw new ArgumentNullException("seriesKey");
		}
		string text = seriesKey;
		for (int i = 1; i <= 9; i++)
		{
			string oldValue = "{" + new string('n', i) + "}";
			string newValue = counter.ToString(new string('0', i), CultureInfo.InvariantCulture);
			text = text.Replace(oldValue, newValue);
		}
		return SanitizeBaseName(text);
	}

	public FileNameTemplate WithImplicitCounterSuffix()
	{
		if (HasCounter)
		{
			return this;
		}
		string text = (Text.EndsWith("_", StringComparison.Ordinal) ? string.Empty : "_");
		return new FileNameTemplate(Text + text + "{nnn}");
	}

	public override string ToString()
	{
		return Text;
	}

	private static void Validate(string template)
	{
		if (string.IsNullOrWhiteSpace(template))
		{
			throw new FileNameTemplateEmptyException();
		}
		foreach (char c in template)
		{
			if (InvalidFileNameChars.Contains(c))
			{
				throw new FileNameTemplateInvalidCharacterException(c);
			}
		}
	}

	private static string SanitizeBaseName(string baseName)
	{
		if (baseName == null)
		{
			return "_";
		}
		baseName = baseName.TrimEnd(' ', '.');
		if (baseName.Length == 0)
		{
			return "_";
		}
		if (IsReservedDeviceName(baseName))
		{
			baseName = "_" + baseName;
		}
		return baseName;
	}

	private static bool IsReservedDeviceName(string baseName)
	{
		baseName = baseName.TrimEnd(' ', '.');
		int num = baseName.IndexOf('.');
		if (num >= 0)
		{
			baseName = baseName.Substring(0, num);
		}
		baseName = baseName.ToUpperInvariant();
		if (baseName.In("CON", "PRN", "AUX", "NUL", "CONIN$", "CONOUT$"))
		{
			return true;
		}
		if (baseName.Length == 4 && (baseName.StartsWith("COM", StringComparison.Ordinal) || baseName.StartsWith("LPT", StringComparison.Ordinal)))
		{
			char c = baseName[3];
			if (c >= '1' && c <= '9')
			{
				return true;
			}
		}
		return false;
	}

	private static bool ContainsCounter(string template)
	{
		for (int i = 1; i <= 9; i++)
		{
			string value = "{" + new string('n', i) + "}";
			if (template.IndexOf(value, StringComparison.Ordinal) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	private static string SanitizeFileNamePart(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(input.Length);
		bool flag = false;
		foreach (char c in input)
		{
			if (char.IsControl(c) || char.IsWhiteSpace(c) || c == '{' || c == '}' || InvalidFileNameChars.Contains(c))
			{
				if (!flag)
				{
					stringBuilder.Append('-');
					flag = true;
				}
			}
			else
			{
				flag = false;
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString().Trim('-').TrimEnd('.');
	}

	static FileNameTemplate()
	{
		HashSet<char> hashSet = new HashSet<char>();
		char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
		foreach (char item in invalidFileNameChars)
		{
			hashSet.Add(item);
		}
		InvalidFileNameChars = hashSet;
	}
}
