using System;

namespace AudioVideoCore;

public static class SampleFormatExtensions
{
	public static bool IsPacked(this SampleFormat format)
	{
		switch (format)
		{
		case SampleFormat.U8:
		case SampleFormat.S16:
		case SampleFormat.S32:
		case SampleFormat.S64:
		case SampleFormat.Float:
		case SampleFormat.Double:
			return true;
		case SampleFormat.U8P:
		case SampleFormat.S16P:
		case SampleFormat.S32P:
		case SampleFormat.S64P:
		case SampleFormat.FloatP:
		case SampleFormat.DoubleP:
			return false;
		default:
			throw new InvalidOperationException($"Unknown sample format {typeof(SampleFormat)}.{format}.");
		}
	}

	public static bool IsPlanar(this SampleFormat format)
	{
		return !format.IsPacked();
	}

	public static int GetBytesPerSample(this SampleFormat format)
	{
		switch (format)
		{
		case SampleFormat.U8:
		case SampleFormat.U8P:
			return 1;
		case SampleFormat.S16:
		case SampleFormat.S16P:
			return 2;
		case SampleFormat.S32:
		case SampleFormat.Float:
		case SampleFormat.S32P:
		case SampleFormat.FloatP:
			return 4;
		case SampleFormat.S64:
		case SampleFormat.Double:
		case SampleFormat.S64P:
		case SampleFormat.DoubleP:
			return 8;
		default:
			throw new InvalidOperationException($"Unknown sample format {typeof(SampleFormat)}.{format}.");
		}
	}
}
