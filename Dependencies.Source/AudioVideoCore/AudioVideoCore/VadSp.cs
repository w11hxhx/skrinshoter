namespace AudioVideoCore;

internal static class VadSp
{
	private const short kSmoothingDown = 6553;

	private const short kSmoothingUp = 32439;

	public static short FindMinimum(VadCore self, short feature_value, int channel)
	{
		int num = -1;
		int num2 = channel << 4;
		short num3 = 1600;
		short num4 = 0;
		short[] index_vector = self.index_vector;
		short[] low_value_vector = self.low_value_vector;
		for (int i = 0; i < 16; i++)
		{
			if (index_vector[num2 + i] != 100)
			{
				index_vector[num2 + i]++;
				continue;
			}
			for (int j = i; j < 15; j++)
			{
				low_value_vector[num2 + j] = low_value_vector[num2 + j + 1];
				index_vector[num2 + j] = index_vector[num2 + j + 1];
			}
			index_vector[num2 + 15] = 101;
			low_value_vector[num2 + 15] = 10000;
		}
		if (feature_value < low_value_vector[num2 + 7])
		{
			if (feature_value < low_value_vector[num2 + 3])
			{
				if (feature_value < low_value_vector[num2 + 1])
				{
					num = ((feature_value >= low_value_vector[num2]) ? 1 : 0);
				}
				else
				{
					num = ((feature_value >= low_value_vector[num2 + 2]) ? 3 : 2);
				}
			}
			else if (feature_value < low_value_vector[num2 + 5])
			{
				num = ((feature_value >= low_value_vector[num2 + 4]) ? 5 : 4);
			}
			else
			{
				num = ((feature_value >= low_value_vector[num2 + 6]) ? 7 : 6);
			}
		}
		else if (feature_value < low_value_vector[num2 + 15])
		{
			if (feature_value < low_value_vector[num2 + 11])
			{
				if (feature_value < low_value_vector[num2 + 9])
				{
					num = ((feature_value >= low_value_vector[num2 + 8]) ? 9 : 8);
				}
				else
				{
					num = ((feature_value >= low_value_vector[num2 + 10]) ? 11 : 10);
				}
			}
			else if (feature_value < low_value_vector[num2 + 13])
			{
				num = ((feature_value >= low_value_vector[num2 + 12]) ? 13 : 12);
			}
			else
			{
				num = ((feature_value >= low_value_vector[num2 + 14]) ? 15 : 14);
			}
		}
		if (num > -1)
		{
			for (int i = 15; i > num; i--)
			{
				low_value_vector[num2 + i] = low_value_vector[num2 + i - 1];
				index_vector[num2 + i] = index_vector[num2 + i - 1];
			}
			low_value_vector[num2 + num] = feature_value;
			index_vector[num2 + num] = 1;
		}
		if (self.frame_counter > 2)
		{
			num3 = low_value_vector[num2 + 2];
		}
		else if (self.frame_counter > 0)
		{
			num3 = low_value_vector[num2];
		}
		if (self.frame_counter > 0)
		{
			num4 = (short)((num3 >= self.mean_value[channel]) ? 32439 : 6553);
		}
		int num5 = (num4 + 1) * self.mean_value[channel];
		num5 += (32767 - num4) * num3;
		num5 += 16384;
		self.mean_value[channel] = (short)(num5 >> 15);
		return self.mean_value[channel];
	}
}
