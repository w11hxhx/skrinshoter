using System;

namespace SkrinshoterApp;

internal sealed class ScrollingCaptureImageDebugOverlay
{
	private const uint FragmentBoundaryLineColor = 553582592u;

	private const uint FragmentLabelBackgroundColor = 2684354560u;

	private const uint FragmentLabelTextColor = uint.MaxValue;

	private const int FragmentLabelMargin = 2;

	private const int FragmentLabelPadding = 2;

	private const int FragmentDigitWidth = 3;

	private const int FragmentDigitHeight = 5;

	private const int FragmentDigitSpacing = 1;

	private const int FragmentDigitScale = 1;

	private readonly ScrollingCaptureImage image;

	private readonly int width;

	public ScrollingCaptureImageDebugOverlay(ScrollingCaptureImage image)
	{
		this.image = image ?? throw new ArgumentNullException("image");
		width = image.Width;
	}

	public void DrawAddedFragmentDebugDecorations(int fragmentTopY, int imageHeight, bool drawFragmentBoundaryLine, int? fragmentIndex)
	{
		if (drawFragmentBoundaryLine && fragmentTopY > 0)
		{
			DrawHorizontalLine(fragmentTopY, imageHeight, 553582592u);
		}
		if (fragmentIndex.HasValue)
		{
			DrawFragmentLabel(fragmentTopY, imageHeight, fragmentIndex.Value);
		}
	}

	private unsafe void DrawHorizontalLine(int y, int imageHeight, uint color)
	{
		if ((uint)y >= (uint)imageHeight)
		{
			return;
		}
		byte b = (byte)(color >> 24);
		if (b == 0)
		{
			return;
		}
		uint* rowPointerUnchecked = (uint*)image.GetRowPointerUnchecked(y);
		if (b == byte.MaxValue)
		{
			uint num = 0xFF000000u | (color & 0xFFFFFF);
			for (int i = 0; i < width; i++)
			{
				rowPointerUnchecked[i] = num;
			}
		}
		else
		{
			for (int j = 0; j < width; j++)
			{
				rowPointerUnchecked[j] = BlendColor(rowPointerUnchecked[j], color);
			}
		}
	}

	private void DrawFragmentLabel(int fragmentTopY, int imageHeight, int fragmentIndex)
	{
		int digitCount = GetDigitCount(fragmentIndex);
		int num = digitCount * 3 + (digitCount - 1);
		int num2 = 5;
		int num3 = 2;
		int num4 = fragmentTopY + 2;
		int rectWidth = num + 4;
		int rectHeight = num2 + 4;
		DrawFilledRectangle(num3, num4, rectWidth, rectHeight, imageHeight, 2684354560u);
		int num5 = num3 + 2;
		int y = num4 + 2;
		for (int num6 = GetHighestDecimalDivisor(fragmentIndex); num6 > 0; num6 /= 10)
		{
			int digit = fragmentIndex / num6;
			fragmentIndex %= num6;
			DrawDigit(digit, num5, y, imageHeight, uint.MaxValue);
			num5 += 4;
		}
	}

	private void DrawDigit(int digit, int x, int y, int imageHeight, uint color)
	{
		for (int i = 0; i < 5; i++)
		{
			int digitRowBits = GetDigitRowBits(digit, i);
			for (int j = 0; j < 3; j++)
			{
				int num = 1 << 2 - j;
				if ((digitRowBits & num) != 0)
				{
					DrawFilledRectangle(x + j, y + i, 1, 1, imageHeight, color);
				}
			}
		}
	}

	private unsafe void DrawFilledRectangle(int x, int y, int rectWidth, int rectHeight, int imageHeight, uint color)
	{
		if (rectWidth <= 0 || rectHeight <= 0)
		{
			return;
		}
		byte b = (byte)(color >> 24);
		if (b == 0)
		{
			return;
		}
		int num = Math.Max(0, x);
		int num2 = Math.Max(0, y);
		int num3 = Math.Min(width, x + rectWidth);
		int num4 = Math.Min(imageHeight, y + rectHeight);
		if (num >= num3 || num2 >= num4)
		{
			return;
		}
		if (b == byte.MaxValue)
		{
			uint num5 = 0xFF000000u | (color & 0xFFFFFF);
			for (int i = num2; i < num4; i++)
			{
				uint* rowPointerUnchecked = (uint*)image.GetRowPointerUnchecked(i);
				for (int j = num; j < num3; j++)
				{
					rowPointerUnchecked[j] = num5;
				}
			}
			return;
		}
		for (int k = num2; k < num4; k++)
		{
			uint* rowPointerUnchecked2 = (uint*)image.GetRowPointerUnchecked(k);
			for (int l = num; l < num3; l++)
			{
				rowPointerUnchecked2[l] = BlendColor(rowPointerUnchecked2[l], color);
			}
		}
	}

	private static uint BlendColor(uint dstColor, uint srcColor)
	{
		byte b = (byte)(srcColor >> 24);
		if (b == 0)
		{
			return dstColor;
		}
		byte b2 = (byte)(srcColor >> 16);
		byte b3 = (byte)(srcColor >> 8);
		byte b4 = (byte)srcColor;
		if (b == byte.MaxValue)
		{
			return (uint)(-16777216 | (b2 << 16) | (b3 << 8) | b4);
		}
		int num = 255 - b;
		byte b5 = (byte)dstColor;
		byte b6 = (byte)(dstColor >> 8);
		byte b7 = (byte)(dstColor >> 16);
		byte b8 = (byte)((b5 * num + b4 * b + 127) / 255);
		byte b9 = (byte)((b6 * num + b3 * b + 127) / 255);
		byte b10 = (byte)((b7 * num + b2 * b + 127) / 255);
		return (uint)(-16777216 | (b10 << 16) | (b9 << 8) | b8);
	}

	private static int GetDigitCount(int value)
	{
		int num = 1;
		while (value >= 10)
		{
			value /= 10;
			num++;
		}
		return num;
	}

	private static int GetHighestDecimalDivisor(int value)
	{
		int num = 1;
		while (value >= 10)
		{
			value /= 10;
			num *= 10;
		}
		return num;
	}

	private static int GetDigitRowBits(int digit, int row)
	{
		return digit switch
		{
			0 => row switch
			{
				0 => 7, 
				1 => 5, 
				2 => 5, 
				3 => 5, 
				4 => 7, 
				_ => 0, 
			}, 
			1 => row switch
			{
				0 => 2, 
				1 => 6, 
				2 => 2, 
				3 => 2, 
				4 => 7, 
				_ => 0, 
			}, 
			2 => row switch
			{
				0 => 7, 
				1 => 1, 
				2 => 7, 
				3 => 4, 
				4 => 7, 
				_ => 0, 
			}, 
			3 => row switch
			{
				0 => 7, 
				1 => 1, 
				2 => 7, 
				3 => 1, 
				4 => 7, 
				_ => 0, 
			}, 
			4 => row switch
			{
				0 => 5, 
				1 => 5, 
				2 => 7, 
				3 => 1, 
				4 => 1, 
				_ => 0, 
			}, 
			5 => row switch
			{
				0 => 7, 
				1 => 4, 
				2 => 7, 
				3 => 1, 
				4 => 7, 
				_ => 0, 
			}, 
			6 => row switch
			{
				0 => 7, 
				1 => 4, 
				2 => 7, 
				3 => 5, 
				4 => 7, 
				_ => 0, 
			}, 
			7 => row switch
			{
				0 => 7, 
				1 => 1, 
				2 => 1, 
				3 => 1, 
				4 => 1, 
				_ => 0, 
			}, 
			8 => row switch
			{
				0 => 7, 
				1 => 5, 
				2 => 7, 
				3 => 5, 
				4 => 7, 
				_ => 0, 
			}, 
			9 => row switch
			{
				0 => 7, 
				1 => 5, 
				2 => 7, 
				3 => 1, 
				4 => 7, 
				_ => 0, 
			}, 
			_ => 0, 
		};
	}
}
