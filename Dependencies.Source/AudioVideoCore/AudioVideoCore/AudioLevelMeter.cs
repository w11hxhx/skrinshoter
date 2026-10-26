using System;
using System.Runtime.InteropServices;

namespace AudioVideoCore;

public static class AudioLevelMeter
{
	public static float ComputeRms(ReadOnlySpan<byte> data, AudioFormat format)
	{
		if (data.IsEmpty)
		{
			return 0f;
		}
		double num = 0.0;
		long num3;
		switch (format.SampleFormat)
		{
		case SampleFormat.U8:
		{
			for (int m = 0; m < data.Length; m++)
			{
				double num7 = (double)(data[m] - 128) / 128.0;
				num += num7 * num7;
			}
			num3 = data.Length;
			break;
		}
		case SampleFormat.S16:
		{
			ReadOnlySpan<short> readOnlySpan3 = MemoryMarshal.Cast<byte, short>(data);
			for (int k = 0; k < readOnlySpan3.Length; k++)
			{
				double num5 = (double)readOnlySpan3[k] / 32768.0;
				num += num5 * num5;
			}
			num3 = readOnlySpan3.Length;
			break;
		}
		case SampleFormat.S32:
		{
			ReadOnlySpan<int> readOnlySpan5 = MemoryMarshal.Cast<byte, int>(data);
			for (int n = 0; n < readOnlySpan5.Length; n++)
			{
				double num8 = (double)readOnlySpan5[n] / 2147483648.0;
				num += num8 * num8;
			}
			num3 = readOnlySpan5.Length;
			break;
		}
		case SampleFormat.S64:
		{
			ReadOnlySpan<long> readOnlySpan2 = MemoryMarshal.Cast<byte, long>(data);
			for (int j = 0; j < readOnlySpan2.Length; j++)
			{
				double num4 = (double)readOnlySpan2[j] / 9.223372036854776E+18;
				num += num4 * num4;
			}
			num3 = readOnlySpan2.Length;
			break;
		}
		case SampleFormat.Float:
		{
			ReadOnlySpan<float> readOnlySpan4 = MemoryMarshal.Cast<byte, float>(data);
			for (int l = 0; l < readOnlySpan4.Length; l++)
			{
				double num6 = readOnlySpan4[l];
				num += num6 * num6;
			}
			num3 = readOnlySpan4.Length;
			break;
		}
		case SampleFormat.Double:
		{
			ReadOnlySpan<double> readOnlySpan = MemoryMarshal.Cast<byte, double>(data);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				double num2 = readOnlySpan[i];
				num += num2 * num2;
			}
			num3 = readOnlySpan.Length;
			break;
		}
		default:
			throw new ArgumentException($"Unsupported sample format {format.SampleFormat}.", "format");
		}
		if (num3 == 0L)
		{
			return 0f;
		}
		double num9 = Math.Sqrt(num / (double)num3);
		if (double.IsNaN(num9) || double.IsInfinity(num9))
		{
			return 0f;
		}
		if (num9 > 1.0)
		{
			num9 = 1.0;
		}
		return (float)num9;
	}
}
