using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace ScreenRecorderCommon;

public sealed class CommandArgumentReader
{
	private readonly Queue<string> arguments;

	public CommandArgumentReader(IReadOnlyCollection<string> args, int expectedCount)
	{
		if (args == null)
		{
			throw new ArgumentNullException("args");
		}
		if (args.Count != expectedCount)
		{
			throw new FormatException($"Invalid number of arguments: required {expectedCount}, received {args.Count}.");
		}
		arguments = new Queue<string>(args);
	}

	public static void EnsureNoArguments(IReadOnlyList<string> args)
	{
		if (args == null)
		{
			throw new ArgumentNullException("args");
		}
		if (args.Count != 0)
		{
			throw new FormatException($"Invalid number of arguments: required 0, received {args.Count}.");
		}
	}

	public string ReadString()
	{
		return arguments.Dequeue();
	}

	public string ReadNonEmptyString(string valueName)
	{
		string text = arguments.Dequeue();
		if (string.IsNullOrEmpty(text))
		{
			throw new FormatException("Value of " + valueName + " must not be null or empty.");
		}
		return text;
	}

	public TEnum ReadEnum<TEnum>() where TEnum : struct, Enum
	{
		string text = arguments.Dequeue();
		if (!Enum.TryParse<TEnum>(text, ignoreCase: true, out var result))
		{
			throw new FormatException("Invalid value of " + typeof(TEnum).Name + ": " + text + ".");
		}
		return result;
	}

	public int ReadInt32(string valueName, int minInclusive, int maxInclusive)
	{
		string text = arguments.Dequeue();
		if (!int.TryParse(text, out var result) || result < minInclusive || result > maxInclusive)
		{
			throw new FormatException("Invalid value of " + valueName + ": " + text + ".");
		}
		return result;
	}

	public float ReadSingle(string valueName, float minInclusive, float maxInclusive)
	{
		string text = arguments.Dequeue();
		if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) || result < minInclusive || result > maxInclusive)
		{
			throw new FormatException("Invalid value of " + valueName + ": " + text + ".");
		}
		return result;
	}

	public bool ReadFlag(string flagName)
	{
		string text = arguments.Dequeue();
		if (!int.TryParse(text, out var result) || result < 0 || result > 1)
		{
			throw new FormatException("Invalid value of " + flagName + " flag: " + text + ".");
		}
		return result == 1;
	}

	public Rectangle ReadRectangle(string valueName)
	{
		string text = arguments.Dequeue();
		string[] array = text.Split(',');
		if (array.Length != 4 || !int.TryParse(array[0], out var result) || !int.TryParse(array[1], out var result2) || !int.TryParse(array[2], out var result3) || !int.TryParse(array[3], out var result4) || result3 <= 0 || result4 <= 0)
		{
			throw new FormatException("Invalid format of " + valueName + " coordinates: " + text + ".");
		}
		return new Rectangle(result, result2, result3, result4);
	}
}
