using System;

namespace AudioVideoCore;

internal sealed class VadCore
{
	public const int kNumChannels = 6;

	public const int kNumGaussians = 2;

	public const int kTableSize = 12;

	public const int kMinEnergy = 10;

	private static readonly short[] kSpectrumWeight = new short[6] { 6, 8, 10, 12, 14, 16 };

	private const short kNoiseUpdateConst = 655;

	private const short kSpeechUpdateConst = 6554;

	private const short kBackEta = 154;

	private static readonly short[] kMinimumDifference = new short[6] { 544, 544, 576, 576, 576, 576 };

	private static readonly short[] kMaximumSpeech = new short[6] { 11392, 11392, 11520, 11520, 11520, 11520 };

	private static readonly short[] kMinimumMean = new short[2] { 640, 768 };

	private static readonly short[] kMaximumNoise = new short[6] { 9216, 9088, 8960, 8832, 8704, 8576 };

	private static readonly short[] kNoiseDataWeights = new short[12]
	{
		34, 62, 72, 66, 53, 25, 94, 66, 56, 62,
		75, 103
	};

	private static readonly short[] kSpeechDataWeights = new short[12]
	{
		48, 82, 45, 87, 50, 47, 80, 46, 83, 41,
		78, 81
	};

	private static readonly short[] kNoiseDataMeans = new short[12]
	{
		6738, 4892, 7065, 6715, 6771, 3369, 7646, 3863, 7820, 7266,
		5020, 4362
	};

	private static readonly short[] kSpeechDataMeans = new short[12]
	{
		8306, 10085, 10078, 11823, 11843, 6309, 9473, 9571, 10879, 7581,
		8180, 7483
	};

	private static readonly short[] kNoiseDataStds = new short[12]
	{
		378, 1064, 493, 582, 688, 593, 474, 697, 475, 688,
		421, 455
	};

	private static readonly short[] kSpeechDataStds = new short[12]
	{
		555, 505, 567, 524, 585, 1231, 509, 828, 492, 1540,
		1079, 850
	};

	private const short kMaxSpeechFrames = 6;

	private const short kMinStd = 384;

	private const short kDefaultMode = 0;

	private static readonly short[] kOverHangMax1Q = new short[3] { 8, 4, 3 };

	private static readonly short[] kOverHangMax2Q = new short[3] { 14, 7, 5 };

	private static readonly short[] kLocalThresholdQ = new short[3] { 24, 21, 24 };

	private static readonly short[] kGlobalThresholdQ = new short[3] { 57, 48, 57 };

	private static readonly short[] kOverHangMax1LBR = new short[3] { 8, 4, 3 };

	private static readonly short[] kOverHangMax2LBR = new short[3] { 14, 7, 5 };

	private static readonly short[] kLocalThresholdLBR = new short[3] { 37, 32, 37 };

	private static readonly short[] kGlobalThresholdLBR = new short[3] { 100, 80, 100 };

	private static readonly short[] kOverHangMax1AGG = new short[3] { 6, 3, 2 };

	private static readonly short[] kOverHangMax2AGG = new short[3] { 9, 5, 3 };

	private static readonly short[] kLocalThresholdAGG = new short[3] { 82, 78, 82 };

	private static readonly short[] kGlobalThresholdAGG = new short[3] { 285, 260, 285 };

	private static readonly short[] kOverHangMax1VAG = new short[3] { 6, 3, 2 };

	private static readonly short[] kOverHangMax2VAG = new short[3] { 9, 5, 3 };

	private static readonly short[] kLocalThresholdVAG = new short[3] { 94, 94, 94 };

	private static readonly short[] kGlobalThresholdVAG = new short[3] { 1100, 1050, 1100 };

	public int vad;

	public short[] noise_means = new short[12];

	public short[] speech_means = new short[12];

	public short[] noise_stds = new short[12];

	public short[] speech_stds = new short[12];

	public int frame_counter;

	public short over_hang;

	public short num_of_speech;

	public short[] index_vector = new short[96];

	public short[] low_value_vector = new short[96];

	public short[] mean_value = new short[6];

	public short[] upper_state = new short[5];

	public short[] lower_state = new short[5];

	public short[] hp_filter_state = new short[4];

	public short[] over_hang_max_1 = new short[3];

	public short[] over_hang_max_2 = new short[3];

	public short[] individual = new short[3];

	public short[] total = new short[3];

	public short[] feature_vector = new short[6];

	public short total_power;

	public VadCore()
	{
		Init();
	}

	public void Init()
	{
		vad = 1;
		frame_counter = 0;
		over_hang = 0;
		num_of_speech = 0;
		for (int i = 0; i < 12; i++)
		{
			noise_means[i] = kNoiseDataMeans[i];
			speech_means[i] = kSpeechDataMeans[i];
			noise_stds[i] = kNoiseDataStds[i];
			speech_stds[i] = kSpeechDataStds[i];
		}
		for (int i = 0; i < 96; i++)
		{
			low_value_vector[i] = 10000;
			index_vector[i] = 0;
		}
		Array.Clear(upper_state, 0, upper_state.Length);
		Array.Clear(lower_state, 0, lower_state.Length);
		Array.Clear(hp_filter_state, 0, hp_filter_state.Length);
		for (int i = 0; i < 6; i++)
		{
			mean_value[i] = 1600;
		}
		SetMode(0);
	}

	public int SetMode(int mode)
	{
		int result = 0;
		switch (mode)
		{
		case 0:
			Array.Copy(kOverHangMax1Q, over_hang_max_1, over_hang_max_1.Length);
			Array.Copy(kOverHangMax2Q, over_hang_max_2, over_hang_max_2.Length);
			Array.Copy(kLocalThresholdQ, individual, individual.Length);
			Array.Copy(kGlobalThresholdQ, total, total.Length);
			break;
		case 1:
			Array.Copy(kOverHangMax1LBR, over_hang_max_1, over_hang_max_1.Length);
			Array.Copy(kOverHangMax2LBR, over_hang_max_2, over_hang_max_2.Length);
			Array.Copy(kLocalThresholdLBR, individual, individual.Length);
			Array.Copy(kGlobalThresholdLBR, total, total.Length);
			break;
		case 2:
			Array.Copy(kOverHangMax1AGG, over_hang_max_1, over_hang_max_1.Length);
			Array.Copy(kOverHangMax2AGG, over_hang_max_2, over_hang_max_2.Length);
			Array.Copy(kLocalThresholdAGG, individual, individual.Length);
			Array.Copy(kGlobalThresholdAGG, total, total.Length);
			break;
		case 3:
			Array.Copy(kOverHangMax1VAG, over_hang_max_1, over_hang_max_1.Length);
			Array.Copy(kOverHangMax2VAG, over_hang_max_2, over_hang_max_2.Length);
			Array.Copy(kLocalThresholdVAG, individual, individual.Length);
			Array.Copy(kGlobalThresholdVAG, total, total.Length);
			break;
		default:
			result = -1;
			break;
		}
		return result;
	}

	public int CalcVad8khz(ReadOnlySpan<short> speech_frame)
	{
		total_power = VadFilterbank.CalculateFeatures(this, speech_frame, feature_vector);
		vad = GmmProbability(feature_vector, total_power, speech_frame.Length);
		return vad;
	}

	private static int WeightedAverage(short[] data, int channel, short offset, short[] weights)
	{
		int num = 0;
		for (int i = 0; i < 2; i++)
		{
			data[channel + i * 6] += offset;
			num += data[channel + i * 6] * weights[channel + i * 6];
		}
		return num;
	}

	private short GmmProbability(short[] features, short total_power, int frame_length)
	{
		short num = 0;
		Span<short> span = stackalloc short[12];
		Span<short> span2 = stackalloc short[12];
		Span<short> span3 = stackalloc short[12];
		Span<short> span4 = stackalloc short[12];
		int num2 = 0;
		Span<int> span5 = stackalloc int[2];
		Span<int> span6 = stackalloc int[2];
		short num3;
		short num4;
		short num5;
		short num6;
		switch (frame_length)
		{
		case 80:
			num3 = over_hang_max_1[0];
			num4 = over_hang_max_2[0];
			num5 = individual[0];
			num6 = total[0];
			break;
		case 160:
			num3 = over_hang_max_1[1];
			num4 = over_hang_max_2[1];
			num5 = individual[1];
			num6 = total[1];
			break;
		default:
			num3 = over_hang_max_1[2];
			num4 = over_hang_max_2[2];
			num5 = individual[2];
			num6 = total[2];
			break;
		}
		if (total_power > 10)
		{
			for (int i = 0; i < 6; i++)
			{
				int num7 = 0;
				int num8 = 0;
				for (int j = 0; j < 2; j++)
				{
					int num9 = i + j * 6;
					int num10 = VadGmm.GaussianProbability(features[i], noise_means[num9], noise_stds[num9], out var delta);
					span[num9] = delta;
					span5[j] = kNoiseDataWeights[num9] * num10;
					num7 += span5[j];
					num10 = VadGmm.GaussianProbability(features[i], speech_means[num9], speech_stds[num9], out var delta2);
					span2[num9] = delta2;
					span6[j] = kSpeechDataWeights[num9] * num10;
					num8 += span6[j];
				}
				short num11 = SignalProcessing.NormW32(num7);
				short num12 = SignalProcessing.NormW32(num8);
				if (num7 == 0)
				{
					num11 = 31;
				}
				if (num8 == 0)
				{
					num12 = 31;
				}
				short num13 = (short)(num11 - num12);
				num2 += num13 * kSpectrumWeight[i];
				if (num13 * 4 > num5)
				{
					num = 1;
				}
				short num14 = (short)(num7 >> 12);
				if (num14 > 0)
				{
					int num10 = (span5[0] & -4096) << 2;
					span3[i] = (short)SignalProcessing.DivW32W16(num10, num14);
					span3[i + 6] = (short)(16384 - span3[i]);
				}
				else
				{
					span3[i] = 16384;
				}
				short num15 = (short)(num8 >> 12);
				if (num15 > 0)
				{
					int num10 = (span6[0] & -4096) << 2;
					span4[i] = (short)SignalProcessing.DivW32W16(num10, num15);
					span4[i + 6] = (short)(16384 - span4[i]);
				}
			}
			num |= ((num2 >= num6) ? ((short)1) : ((short)0));
			short num16 = 12800;
			for (int i = 0; i < 6; i++)
			{
				short num17 = VadSp.FindMinimum(this, features[i], i);
				int num18 = WeightedAverage(noise_means, i, 0, kNoiseDataWeights);
				short num19 = (short)(num18 >> 6);
				for (int j = 0; j < 2; j++)
				{
					int num9 = i + j * 6;
					short num20 = noise_means[num9];
					short num21 = speech_means[num9];
					short num22 = noise_stds[num9];
					short num23 = speech_stds[num9];
					short num24 = num20;
					if (num == 0)
					{
						short num25 = (short)(span3[num9] * span[num9] >> 11);
						num24 = (short)(num20 + (short)(num25 * 655 >> 22));
					}
					short num26 = (short)((num17 << 4) - num19);
					short num27 = (short)(num24 + (short)(num26 * 154 >> 9));
					short num28 = (short)(j + 5 << 7);
					if (num27 < num28)
					{
						num27 = num28;
					}
					num28 = (short)(72 + j - i << 7);
					if (num27 > num28)
					{
						num27 = num28;
					}
					noise_means[num9] = num27;
					if (num != 0)
					{
						short num25 = (short)(span4[num9] * span2[num9] >> 11);
						num28 = (short)(num25 * 6554 >> 21);
						short num29 = (short)(num21 + (num28 + 1 >> 1));
						short num30 = (short)(num16 + 640);
						if (num29 < kMinimumMean[j])
						{
							num29 = kMinimumMean[j];
						}
						if (num29 > num30)
						{
							num29 = num30;
						}
						speech_means[num9] = num29;
						num28 = (short)(num21 + 4 >> 3);
						num28 = (short)(features[i] - num28);
						int num10 = span2[num9] * num28 >> 3;
						int num31 = num10 - 4096;
						num28 = (short)(span4[num9] >> 2);
						num10 = num28 * num31;
						num31 = num10 >> 4;
						if (num31 > 0)
						{
							num28 = (short)SignalProcessing.DivW32W16(num31, (short)(num23 * 10));
						}
						else
						{
							num28 = (short)SignalProcessing.DivW32W16(-num31, (short)(num23 * 10));
							num28 = (short)(-num28);
						}
						num28 += 128;
						num23 += (short)(num28 >> 8);
						if (num23 < 384)
						{
							num23 = 384;
						}
						speech_stds[num9] = num23;
					}
					else
					{
						num28 = (short)(features[i] - (num20 >> 3));
						int num10 = span[num9] * num28 >> 3;
						num10 -= 4096;
						num28 = (short)(span3[num9] + 2 >> 2);
						int num31 = num28 * num10;
						num10 = num31 >> 14;
						if (num10 > 0)
						{
							num28 = (short)SignalProcessing.DivW32W16(num10, num22);
						}
						else
						{
							num28 = (short)SignalProcessing.DivW32W16(-num10, num22);
							num28 = (short)(-num28);
						}
						num28 += 32;
						num22 += (short)(num28 >> 6);
						if (num22 < 384)
						{
							num22 = 384;
						}
						noise_stds[num9] = num22;
					}
				}
				num18 = WeightedAverage(noise_means, i, 0, kNoiseDataWeights);
				int num32 = WeightedAverage(speech_means, i, 0, kSpeechDataWeights);
				short num33 = (short)((short)(num32 >> 9) - (short)(num18 >> 9));
				short num34;
				if (num33 < kMinimumDifference[i])
				{
					short num28 = (short)(kMinimumDifference[i] - num33);
					num19 = (short)(13 * num28 >> 2);
					num34 = (short)(3 * num28 >> 2);
					num32 = WeightedAverage(speech_means, i, num19, kSpeechDataWeights);
					num18 = WeightedAverage(noise_means, i, (short)(-num34), kNoiseDataWeights);
				}
				num16 = kMaximumSpeech[i];
				num34 = (short)(num32 >> 7);
				if (num34 > num16)
				{
					num34 -= num16;
					for (int j = 0; j < 2; j++)
					{
						speech_means[i + j * 6] -= num34;
					}
				}
				num34 = (short)(num18 >> 7);
				if (num34 > kMaximumNoise[i])
				{
					num34 -= kMaximumNoise[i];
					for (int j = 0; j < 2; j++)
					{
						noise_means[i + j * 6] -= num34;
					}
				}
			}
			frame_counter++;
		}
		if (num == 0)
		{
			if (over_hang > 0)
			{
				num = (short)(2 + over_hang);
				over_hang--;
			}
			num_of_speech = 0;
		}
		else
		{
			num_of_speech++;
			if (num_of_speech > 6)
			{
				num_of_speech = 6;
				over_hang = num4;
			}
			else
			{
				over_hang = num3;
			}
		}
		return num;
	}
}
