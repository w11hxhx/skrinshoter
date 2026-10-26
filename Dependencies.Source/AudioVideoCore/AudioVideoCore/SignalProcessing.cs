using System;

namespace AudioVideoCore;

internal static class SignalProcessing
{
	private static readonly sbyte[] kWebRtcSpl_CountLeadingZeros32_Table = new sbyte[64]
	{
		32, 8, 17, -1, -1, 14, -1, -1, -1, 20,
		-1, -1, -1, 28, -1, 18, -1, -1, -1, -1,
		-1, -1, -1, -1, -1, -1, -1, -1, 0, 26,
		25, 24, 4, 11, 23, 31, 3, 7, 10, 16,
		22, 30, -1, -1, 2, 6, 13, 9, -1, 15,
		-1, 21, -1, 29, 19, -1, -1, -1, -1, -1,
		1, 27, 5, 12
	};

	public static int CountLeadingZeros32(uint n)
	{
		n |= n >> 1;
		n |= n >> 2;
		n |= n >> 4;
		n |= n >> 8;
		n |= n >> 16;
		return kWebRtcSpl_CountLeadingZeros32_Table[(int)n * -1945425775 >>> 26];
	}

	public static short GetSizeInBits(uint n)
	{
		return (short)(32 - CountLeadingZeros32(n));
	}

	public static short NormW32(int a)
	{
		return (short)((a != 0) ? (CountLeadingZeros32((uint)((a < 0) ? (~a) : a)) - 1) : 0);
	}

	public static short NormU32(uint a)
	{
		return (short)((a != 0) ? CountLeadingZeros32(a) : 0);
	}

	public static int DivW32W16(int num, short den)
	{
		if (den != 0)
		{
			return num / den;
		}
		return int.MaxValue;
	}

	public static short GetScalingSquare(ReadOnlySpan<short> in_vector, int times)
	{
		short sizeInBits = GetSizeInBits((uint)times);
		short num = -1;
		for (int i = 0; i < in_vector.Length; i++)
		{
			short num2 = ((in_vector[i] > 0) ? in_vector[i] : ((short)(-in_vector[i])));
			num = ((num2 > num) ? num2 : num);
		}
		short num3 = NormW32(num * num);
		if (num == 0)
		{
			return 0;
		}
		if (num3 <= sizeInBits)
		{
			return (short)(sizeInBits - num3);
		}
		return 0;
	}

	public static int Energy(ReadOnlySpan<short> vector, out int scale_factor)
	{
		int num = 0;
		int scalingSquare = GetScalingSquare(vector, vector.Length);
		for (int i = 0; i < vector.Length; i++)
		{
			num += vector[i] * vector[i] >> scalingSquare;
		}
		scale_factor = scalingSquare;
		return num;
	}
}
