using System;

namespace AudioVideoCore;

internal static class VadFilterbank
{
	private const short kLogConst = 24660;

	private const short kLogEnergyIntPart = 14336;

	private static readonly short[] kHpZeroCoefs = new short[3] { 6631, -13262, 6631 };

	private static readonly short[] kHpPoleCoefs = new short[3] { 16384, -7756, 5620 };

	private static readonly short[] kAllPassCoefsQ15 = new short[2] { 20972, 5571 };

	private static readonly short[] kOffsetVector = new short[6] { 368, 368, 272, 176, 176, 176 };

	private static void HighPassFilter(ReadOnlySpan<short> data_in, int data_length, short[] filter_state, Span<short> data_out)
	{
		for (int i = 0; i < data_length; i++)
		{
			int num = kHpZeroCoefs[0] * data_in[i];
			num += kHpZeroCoefs[1] * filter_state[0];
			num += kHpZeroCoefs[2] * filter_state[1];
			filter_state[1] = filter_state[0];
			filter_state[0] = data_in[i];
			num -= kHpPoleCoefs[1] * filter_state[2];
			num -= kHpPoleCoefs[2] * filter_state[3];
			filter_state[3] = filter_state[2];
			filter_state[2] = (short)(num >> 14);
			data_out[i] = filter_state[2];
		}
	}

	private static void AllPassFilter(ReadOnlySpan<short> data_in, int data_length, short filter_coefficient, ref short filter_state, Span<short> data_out)
	{
		int num = filter_state * 65536;
		for (int i = 0; i < data_length; i++)
		{
			short num2 = data_in[2 * i];
			short num3 = (short)(num + filter_coefficient * num2 >> 16);
			data_out[i] = num3;
			num = num2 * 16384 - filter_coefficient * num3;
			num *= 2;
		}
		filter_state = (short)(num >> 16);
	}

	private static void SplitFilter(ReadOnlySpan<short> data_in, int data_length, ref short upper_state, ref short lower_state, Span<short> hp_data_out, Span<short> lp_data_out)
	{
		int num = data_length >> 1;
		AllPassFilter(data_in, num, kAllPassCoefsQ15[0], ref upper_state, hp_data_out);
		AllPassFilter(data_in.Slice(1), num, kAllPassCoefsQ15[1], ref lower_state, lp_data_out);
		for (int i = 0; i < num; i++)
		{
			short num2 = hp_data_out[i];
			hp_data_out[i] -= lp_data_out[i];
			lp_data_out[i] += num2;
		}
	}

	private static void LogOfEnergy(ReadOnlySpan<short> data_in, short offset, ref short total_energy, out short log_energy)
	{
		uint num = (uint)SignalProcessing.Energy(data_in, out var scale_factor);
		if (num != 0)
		{
			int num2 = 17 - SignalProcessing.NormU32(num);
			short num3 = 14336;
			scale_factor += num2;
			num = ((num2 >= 0) ? (num >> num2) : (num << -num2));
			num3 += (short)((num & 0x3FFF) >> 4);
			log_energy = (short)((24660 * num3 >> 19) + (scale_factor * 24660 >> 9));
			if (log_energy < 0)
			{
				log_energy = 0;
			}
			log_energy += offset;
			if (total_energy <= 10)
			{
				if (scale_factor >= 0)
				{
					total_energy += 11;
				}
				else
				{
					total_energy += (short)(num >> -scale_factor);
				}
			}
		}
		else
		{
			log_energy = offset;
		}
	}

	public static short CalculateFeatures(VadCore self, ReadOnlySpan<short> data_in, short[] features)
	{
		int length = data_in.Length;
		short total_energy = 0;
		Span<short> span = stackalloc short[120];
		Span<short> span2 = stackalloc short[120];
		Span<short> hp_data_out = stackalloc short[60];
		Span<short> span3 = stackalloc short[60];
		int num2;
		int num = (num2 = length >> 1);
		int num3 = 0;
		SplitFilter(data_in, length, ref self.upper_state[num3], ref self.lower_state[num3], span, span2);
		num3 = 1;
		SplitFilter(span, num2, ref self.upper_state[num3], ref self.lower_state[num3], hp_data_out, span3);
		num2 >>= 1;
		LogOfEnergy(hp_data_out.Slice(0, num2), kOffsetVector[5], ref total_energy, out features[5]);
		LogOfEnergy(span3.Slice(0, num2), kOffsetVector[4], ref total_energy, out features[4]);
		num3 = 2;
		num2 = num;
		SplitFilter(span2, num2, ref self.upper_state[num3], ref self.lower_state[num3], hp_data_out, span3);
		num2 >>= 1;
		LogOfEnergy(hp_data_out.Slice(0, num2), kOffsetVector[3], ref total_energy, out features[3]);
		num3 = 3;
		SplitFilter(span3, num2, ref self.upper_state[num3], ref self.lower_state[num3], span, span2);
		num2 >>= 1;
		LogOfEnergy(span.Slice(0, num2), kOffsetVector[2], ref total_energy, out features[2]);
		num3 = 4;
		SplitFilter(span2, num2, ref self.upper_state[num3], ref self.lower_state[num3], hp_data_out, span3);
		num2 >>= 1;
		LogOfEnergy(hp_data_out.Slice(0, num2), kOffsetVector[1], ref total_energy, out features[1]);
		HighPassFilter(span3, num2, self.hp_filter_state, span);
		LogOfEnergy(span.Slice(0, num2), kOffsetVector[0], ref total_energy, out features[0]);
		return total_energy;
	}
}
