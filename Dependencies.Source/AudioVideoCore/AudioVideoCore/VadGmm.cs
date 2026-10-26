namespace AudioVideoCore;

internal static class VadGmm
{
	private const int kCompVar = 22005;

	private const short kLog2Exp = 5909;

	public static int GaussianProbability(short input, short mean, short std, out short delta)
	{
		short num = 0;
		int num2 = 131072 + (std >> 1);
		short num3 = (short)SignalProcessing.DivW32W16(num2, std);
		short num4 = (short)(num3 >> 2);
		short num5 = (short)(num4 * num4 >> 2);
		num4 = (short)(input << 3);
		num4 -= mean;
		delta = (short)(num5 * num4 >> 10);
		num2 = delta * num4 >> 9;
		if (num2 < 22005)
		{
			num4 = (short)(5909 * num2 >> 12);
			num4 = (short)(-num4);
			num = (short)(0x400 | (num4 & 0x3FF));
			num4 = (short)(num4 ^ 0xFFFF);
			num4 >>= 10;
			num4++;
			num = (short)(num >> (int)num4);
		}
		return num3 * num;
	}
}
