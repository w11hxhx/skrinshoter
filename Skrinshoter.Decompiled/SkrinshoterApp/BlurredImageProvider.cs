using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkrinshoterApp;

public class BlurredImageProvider
{
	private readonly Dictionary<OverlayWindowVM, Task<BitmapSource>> cache = new Dictionary<OverlayWindowVM, Task<BitmapSource>>();

	private static readonly ThreadLocal<Random> _rng = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

	public Task<BitmapSource> GetBlurredImageAsync(OverlayWindowVM overlayWindowVM)
	{
		lock (cache)
		{
			if (!cache.TryGetValue(overlayWindowVM, out var value))
			{
				if (!overlayWindowVM.BackgroundImage.IsFrozen)
				{
					overlayWindowVM.BackgroundImage.Freeze();
				}
				value = Task.Run((Func<BitmapSource>)(() =>
				{
					WriteableBitmap writeableBitmap = CreateBlurredImage(overlayWindowVM.BackgroundImage, overlayWindowVM.MonitorScaleFactor);
					writeableBitmap.Freeze();
					return writeableBitmap;
				}));
				cache[overlayWindowVM] = value;
			}
			return value;
		}
	}

	private static WriteableBitmap CreateBlurredImage(BitmapSource src, double monitorScaleFactor)
	{
		if (src == null)
		{
			throw new ArgumentNullException("src");
		}
		int num = Math.Max(1, (int)Math.Round(4.0 * monitorScaleFactor));
		int r = Math.Max(1, (int)Math.Ceiling(20.0 * monitorScaleFactor / (double)num));
		WriteableBitmap writeableBitmap = new WriteableBitmap(src);
		if (num > 1)
		{
			writeableBitmap = writeableBitmap.Resize(writeableBitmap.PixelWidth / num, writeableBitmap.PixelHeight / num, WriteableBitmapExtensions.Interpolation.Bilinear);
		}
		GaussianBlur(writeableBitmap, r);
		if (num > 1)
		{
			writeableBitmap = writeableBitmap.Resize(src.PixelWidth, src.PixelHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
		}
		byte[] array = BuildMaskAdapt(writeableBitmap, 14);
		MaskErode(array, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, 1);
		MaskDilate(array, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, 1);
		int r2 = Math.Max(1, (int)Math.Ceiling(4.0 / monitorScaleFactor));
		GaussianBlur(writeableBitmap, r2);
		AddWhiteNoise(writeableBitmap, array, 25.0 * monitorScaleFactor, 0.0 * monitorScaleFactor);
		return writeableBitmap;
	}

	private unsafe static void GaussianBlur(WriteableBitmap wb, int r)
	{
		if (r < 1)
		{
			return;
		}
		int w = wb.PixelWidth;
		int h = wb.PixelHeight;
		int num = w * h;
		float[] k = BuildKernel(r);
		using BitmapContext bitmapContext = wb.GetBitmapContext();
		int* src = bitmapContext.Pixels;
		int[] tmp = new int[num];
		Parallel.For(0, h, (int y) =>
		{
			int num2 = y * w;
			for (int i = 0; i < w; i++)
			{
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				float num6 = 0f;
				for (int j = -r; j <= r; j++)
				{
					int num7 = Clamp(i + j, 0, w - 1);
					int num8 = src[num2 + num7];
					float num9 = k[j + r];
					num3 += (float)(num8 & 0xFF) * num9;
					num4 += (float)((num8 >> 8) & 0xFF) * num9;
					num5 += (float)((num8 >> 16) & 0xFF) * num9;
					num6 += (float)((num8 >> 24) & 0xFF) * num9;
				}
				tmp[num2 + i] = (ClampByte(num6) << 24) | (ClampByte(num5) << 16) | (ClampByte(num4) << 8) | ClampByte(num3);
			}
		});
		Parallel.For(0, w, (int x) =>
		{
			for (int i = 0; i < h; i++)
			{
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				for (int j = -r; j <= r; j++)
				{
					int num6 = Clamp(i + j, 0, h - 1);
					int num7 = tmp[num6 * w + x];
					float num8 = k[j + r];
					num2 += (float)(num7 & 0xFF) * num8;
					num3 += (float)((num7 >> 8) & 0xFF) * num8;
					num4 += (float)((num7 >> 16) & 0xFF) * num8;
					num5 += (float)((num7 >> 24) & 0xFF) * num8;
				}
				src[i * w + x] = (ClampByte(num5) << 24) | (ClampByte(num4) << 16) | (ClampByte(num3) << 8) | ClampByte(num2);
			}
		});
	}

	private static float[] BuildKernel(int r)
	{
		float num = (float)r / 3f;
		float num2 = 0f;
		int num3 = r * 2 + 1;
		float[] array = new float[num3];
		for (int i = 0; i < num3; i++)
		{
			int num4 = i - r;
			array[i] = (float)Math.Exp((float)(-(num4 * num4)) / (2f * num * num));
			num2 += array[i];
		}
		for (int j = 0; j < num3; j++)
		{
			array[j] /= num2;
		}
		return array;
	}

	private unsafe static byte[] BuildMaskAdapt(WriteableBitmap wb, byte thr)
	{
		int w = wb.PixelWidth;
		int h = wb.PixelHeight;
		int num = w * h;
		byte[] mask = new byte[num];
		int win = 9;
		using BitmapContext bitmapContext = wb.GetBitmapContext(ReadWriteMode.ReadOnly);
		int* src = bitmapContext.Pixels;
		int[] tmp = new int[num];
		int[] bg = new int[num];
		Parallel.For(0, h, (int y) =>
		{
			int num2 = y * w;
			for (int i = 0; i < w; i++)
			{
				long num3 = 0L;
				long num4 = 0L;
				long num5 = 0L;
				for (int j = -4; j <= 4; j++)
				{
					int num6 = Clamp(i + j, 0, w - 1);
					int num7 = src[num2 + num6];
					num3 += num7 & 0xFF;
					num4 += (num7 >> 8) & 0xFF;
					num5 += (num7 >> 16) & 0xFF;
				}
				tmp[num2 + i] = ((int)(num5 / win) << 16) | ((int)(num4 / win) << 8) | (int)(num3 / win);
			}
		});
		Parallel.For(0, w, (int x) =>
		{
			for (int i = 0; i < h; i++)
			{
				long num2 = 0L;
				long num3 = 0L;
				long num4 = 0L;
				for (int j = -4; j <= 4; j++)
				{
					int num5 = Clamp(i + j, 0, h - 1);
					int num6 = tmp[num5 * w + x];
					num2 += num6 & 0xFF;
					num3 += (num6 >> 8) & 0xFF;
					num4 += (num6 >> 16) & 0xFF;
				}
				bg[i * w + x] = ((int)(num4 / win) << 16) | ((int)(num3 / win) << 8) | (int)(num2 / win);
			}
		});
		Parallel.For(0, num, (int idx) =>
		{
			int num2 = src[idx];
			int num3 = bg[idx];
			int val = Math.Abs(((num2 >> 16) & 0xFF) - ((num3 >> 16) & 0xFF));
			int val2 = Math.Abs(((num2 >> 8) & 0xFF) - ((num3 >> 8) & 0xFF));
			int val3 = Math.Abs((num2 & 0xFF) - (num3 & 0xFF));
			int num4 = Math.Max(Math.Max(val, val2), val3);
			mask[idx] = (byte)((num4 > thr) ? byte.MaxValue : 0);
		});
		return mask;
	}

	private static void MaskErode(byte[] m, int w, int h, int r)
	{
		if (r < 1)
		{
			return;
		}
		byte[] src = (byte[])m.Clone();
		Parallel.For(r, h - r, (int y) =>
		{
			int num = y * w;
			for (int i = r; i < w - r; i++)
			{
				bool flag = true;
				for (int j = -r; (j <= r) & flag; j++)
				{
					for (int k = -r; k <= r; k++)
					{
						flag &= src[(y + j) * w + (i + k)] != 0;
					}
				}
				m[num + i] = (byte)(flag ? byte.MaxValue : 0);
			}
		});
	}

	private static void MaskDilate(byte[] m, int w, int h, int r)
	{
		if (r < 1)
		{
			return;
		}
		byte[] src = (byte[])m.Clone();
		Parallel.For(r, h - r, (int y) =>
		{
			int num = y * w;
			for (int i = r; i < w - r; i++)
			{
				bool flag = false;
				for (int j = -r; j <= r; j++)
				{
					if (flag)
					{
						break;
					}
					for (int k = -r; k <= r; k++)
					{
						flag |= src[(y + j) * w + (i + k)] != 0;
					}
				}
				m[num + i] = (byte)(flag ? byte.MaxValue : 0);
			}
		});
	}

	private unsafe static void AddWhiteNoise(WriteableBitmap wb, byte[] mask, double sigmaFg, double sigmaBg)
	{
		int pixelWidth = wb.PixelWidth;
		int pixelHeight = wb.PixelHeight;
		int toExclusive = pixelWidth * pixelHeight;
		if (sigmaFg <= 0.0 && sigmaBg <= 0.0)
		{
			return;
		}
		using BitmapContext bitmapContext = wb.GetBitmapContext();
		int* px = bitmapContext.Pixels;
		Parallel.For(0, toExclusive, (int i) =>
		{
			double num = ((mask[i] != 0) ? sigmaFg : sigmaBg);
			if (!(num <= 0.0))
			{
				int num2 = px[i];
				int num3 = num2 & 0xFF;
				int num4 = (num2 >> 8) & 0xFF;
				int num5 = (num2 >> 16) & 0xFF;
				int num6 = (int)Math.Round(GaussRand(_rng.Value) * num);
				px[i] = -16777216 | (ClampByte(num5 + num6) << 16) | (ClampByte(num4 + num6) << 8) | ClampByte(num3 + num6);
			}
		});
	}

	private static int Clamp(int v, int min, int max)
	{
		if (v >= min)
		{
			if (v <= max)
			{
				return v;
			}
			return max;
		}
		return min;
	}

	private static int ClampByte(double v)
	{
		if (!(v < 0.0))
		{
			if (!(v > 255.0))
			{
				return (int)v;
			}
			return 255;
		}
		return 0;
	}

	private static double GaussRand(Random rnd)
	{
		double d = 1.0 - rnd.NextDouble();
		double num = rnd.NextDouble();
		return Math.Sqrt(-2.0 * Math.Log(d)) * Math.Cos(Math.PI * 2.0 * num);
	}
}
