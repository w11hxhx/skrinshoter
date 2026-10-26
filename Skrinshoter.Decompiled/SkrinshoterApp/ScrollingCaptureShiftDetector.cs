using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SkrinshoterApp;

public class ScrollingCaptureShiftDetector
{
	private const int StripesMin = 6;

	private const int StripesMinNarrow = 3;

	private const int StripesMax = 16;

	private const int StripeTargetWidth = 64;

	private const int HashBits = 32;

	private const int HashSamples = 33;

	private const int KeyDelta = 3;

	private const ulong InvalidKey = ulong.MaxValue;

	private const int MaxShiftPercent = 75;

	private const int FineRowStep = 2;

	private const int CoarseRowStep = 8;

	private const int CoarseShiftStep = 4;

	private const int CoarsePeakCount = 15;

	private const int CoarsePeakMinSep = 8;

	private const int FineHalfWindow = 5;

	private const double MinChangedCellsFraction = 0.05;

	private const int MinAbsoluteDiffScore = 100;

	private const double MinPeakSharpness = 1.1;

	private const int PeakNeighborDistance = 6;

	private const int CoarseHd0RowStep = 4;

	private readonly int width;

	private readonly int height;

	private readonly int stripes;

	private readonly int maxShift;

	private readonly ILogger logger;

	private readonly int[] hd0;

	private readonly int hd0RowCount;

	private readonly int[] fineScores;

	private readonly int[] coarsePeakShifts;

	private readonly int[] coarsePeakScores;

	private long startTimestamp;

	private string logPrefix;

	public ScrollingCaptureShiftDetector(int width, int height, ILogger<ScrollingCaptureShiftDetector> logger)
	{
		if (width < 1)
		{
			throw new ArgumentOutOfRangeException("width");
		}
		if (height < 1)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		this.width = width;
		this.height = height;
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		stripes = GetStripesForWidth(width);
		maxShift = height * 75 / 100;
		if (maxShift < 1)
		{
			maxShift = 1;
		}
		hd0RowCount = (height + 2 - 1) / 2;
		hd0 = new int[hd0RowCount * stripes];
		fineScores = new int[maxShift + 1];
		for (int i = 0; i <= maxShift; i++)
		{
			fineScores[i] = -1;
		}
		coarsePeakShifts = new int[15];
		coarsePeakScores = new int[15];
	}

	public unsafe static PreprocessedFrame Preprocess(IntPtr bgra, int width, int height)
	{
		if (bgra == IntPtr.Zero)
		{
			throw new ArgumentNullException("bgra");
		}
		if (width < 1)
		{
			throw new ArgumentOutOfRangeException("width");
		}
		if (height < 1)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		return PreprocessInternal((byte*)(void*)bgra, width, height);
	}

	public int? FindVerticalShift(PreprocessedFrame first, PreprocessedFrame second, out int movingCenter, int? frameIndex = null)
	{
		if (first == null)
		{
			throw new ArgumentNullException("first");
		}
		if (second == null)
		{
			throw new ArgumentNullException("second");
		}
		if (first.Width != width || first.Height != height)
		{
			throw new ArgumentException("First frame dimensions do not match the detector.");
		}
		if (second.Width != width || second.Height != height)
		{
			throw new ArgumentException("Second frame dimensions do not match the detector.");
		}
		if (first.Stripes != stripes)
		{
			throw new ArgumentException("First frame stripes count does not match the detector.");
		}
		if (second.Stripes != stripes)
		{
			throw new ArgumentException("Second frame stripes count does not match the detector.");
		}
		startTimestamp = Stopwatch.GetTimestamp();
		logPrefix = (frameIndex.HasValue ? $"ShiftDetector #{frameIndex.Value} " : "ShiftDetector ");
		movingCenter = height / 2;
		for (int i = 0; i < fineScores.Length; i++)
		{
			fineScores[i] = -1;
		}
		PrecomputeHd0(first, second);
		int num = hd0RowCount * stripes;
		int num2 = CountChangedCells(num);
		if (logger.IsEnabled(LogLevel.Debug))
		{
			LogHd0Stats(num, num2);
		}
		if ((double)num2 < (double)num * 0.05)
		{
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("{Prefix}[{Elapsed}ms] Shift=null (insufficient changes: {Changed}/{Total})", logPrefix, StopwatchHelper.GetDurationInWholeMillisecondsSince(startTimestamp), num2, num);
			}
			return null;
		}
		int num3 = CoarsePass(first, second);
		if (logger.IsEnabled(LogLevel.Debug))
		{
			LogCoarsePass(num3);
		}
		FinePass(first, second, num3);
		Span<int> span = stackalloc int[15];
		Span<int> span2 = stackalloc int[15];
		Span<int> span3 = stackalloc int[15];
		Span<long> span4 = stackalloc long[15];
		int num4 = 0;
		int num5 = (height - 1 - 3 + 2 - 1) / 2;
		for (int j = 0; j < num3; j++)
		{
			int center = coarsePeakShifts[j];
			FindBestFineScore(center, out var bestShift, out var bestScore);
			if (bestShift != 0 && bestScore >= 100)
			{
				int num6 = CountSupportingStripes(first, second, bestShift);
				int num7 = (height - bestShift - 3 + 2 - 1) / 2;
				long num8 = ((num7 > 0) ? ((long)bestScore * (long)num5 / num7) : 0);
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("{Prefix}Candidate: shift={Shift} score={Score} stripeSupport={Support}/{Total}", logPrefix, bestShift, bestScore, num6, stripes);
				}
				span[num4] = bestShift;
				span2[num4] = bestScore;
				span3[num4] = num6;
				span4[num4] = num8;
				num4++;
			}
		}
		for (int k = 0; k < num4 - 1; k++)
		{
			for (int l = k + 1; l < num4; l++)
			{
				if (span3[l] > span3[k] || (span3[l] == span3[k] && span4[l] > span4[k]))
				{
					ref int reference = ref span[k];
					ref int reference2 = ref span[l];
					int num9 = span[l];
					int num10 = span[k];
					reference = num9;
					reference2 = num10;
					reference = ref span2[k];
					ref int reference3 = ref span2[l];
					num10 = span2[l];
					num9 = span2[k];
					reference = num10;
					reference3 = num9;
					reference = ref span3[k];
					ref int reference4 = ref span3[l];
					num9 = span3[l];
					num10 = span3[k];
					reference = num9;
					reference4 = num10;
					ref long reference5 = ref span4[k];
					ref long reference6 = ref span4[l];
					long num11 = span4[l];
					long num12 = span4[k];
					reference5 = num11;
					reference6 = num12;
				}
			}
		}
		for (int m = 0; m < num4; m++)
		{
			int num13 = span[m];
			int bestScore2 = span2[m];
			int num14 = span3[m];
			if (num14 > 0)
			{
				int num15 = RefineShift(first, second, num13);
				if (logger.IsEnabled(LogLevel.Debug) && num15 != num13)
				{
					logger.LogDebug("{Prefix}Refine: {Old} -> {New}", logPrefix, num13, num15);
				}
				num13 = num15;
			}
			bool flag = IsPeakSharp(first, second, num13, bestScore2, out var neighborAvg);
			if (logger.IsEnabled(LogLevel.Debug))
			{
				LogFinalDecision(num13, bestScore2, neighborAvg, flag, num14);
			}
			if (flag)
			{
				movingCenter = ComputeMovingCenter(first, second, num13);
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("{Prefix}[{Elapsed}ms] Shift={Shift}, MovingCenter={MovingCenter}", logPrefix, StopwatchHelper.GetDurationInWholeMillisecondsSince(startTimestamp), num13, movingCenter);
				}
				return num13;
			}
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("{Prefix}[{Elapsed}ms] Shift=null (no candidate passed sharpness, count={Count})", logPrefix, StopwatchHelper.GetDurationInWholeMillisecondsSince(startTimestamp), num4);
		}
		return null;
	}

	private void PrecomputeHd0(PreprocessedFrame first, PreprocessedFrame second)
	{
		int num = 0;
		for (int i = 0; i < height; i += 2)
		{
			int num2 = num * stripes;
			for (int j = 0; j < stripes; j++)
			{
				int num3 = CellIndex(j, i, height);
				hd0[num2 + j] = CellHd(first, second, num3, num3);
			}
			num++;
		}
	}

	private int CountChangedCells(int totalCells)
	{
		int num = 0;
		for (int i = 0; i < totalCells; i++)
		{
			if (hd0[i] > 0)
			{
				num++;
			}
		}
		return num;
	}

	private int CoarsePass(PreprocessedFrame first, PreprocessedFrame second)
	{
		int count = 0;
		for (int i = 0; i < coarsePeakShifts.Length; i++)
		{
			coarsePeakShifts[i] = 0;
			coarsePeakScores[i] = 0;
		}
		for (int j = 1; j <= maxShift; j += 4)
		{
			int score = DiffScore(first, second, j, 8, 4);
			InsertCoarsePeak(ref count, j, score);
		}
		return count;
	}

	private void InsertCoarsePeak(ref int count, int shift, int score)
	{
		if (score <= 0)
		{
			return;
		}
		for (int i = 0; i < count; i++)
		{
			int num = coarsePeakShifts[i] - shift;
			if (num < 0)
			{
				num = -num;
			}
			if (num < 8)
			{
				if (score > coarsePeakScores[i])
				{
					coarsePeakShifts[i] = shift;
					coarsePeakScores[i] = score;
				}
				return;
			}
		}
		if (count < coarsePeakShifts.Length)
		{
			coarsePeakShifts[count] = shift;
			coarsePeakScores[count] = score;
			count++;
			return;
		}
		int num2 = 0;
		for (int j = 1; j < coarsePeakShifts.Length; j++)
		{
			if (coarsePeakScores[j] < coarsePeakScores[num2])
			{
				num2 = j;
			}
		}
		if (score > coarsePeakScores[num2])
		{
			coarsePeakShifts[num2] = shift;
			coarsePeakScores[num2] = score;
		}
	}

	private void FinePass(PreprocessedFrame first, PreprocessedFrame second, int coarsePeakCount)
	{
		for (int i = 0; i < coarsePeakCount; i++)
		{
			int num = coarsePeakShifts[i];
			int num2 = num - 5;
			if (num2 < 1)
			{
				num2 = 1;
			}
			int num3 = num + 5;
			if (num3 > maxShift)
			{
				num3 = maxShift;
			}
			for (int j = num2; j <= num3; j++)
			{
				if (fineScores[j] < 0)
				{
					fineScores[j] = DiffScore(first, second, j, 2, 1);
				}
			}
		}
	}

	private void FindBestFineScore(int center, out int bestShift, out int bestScore)
	{
		bestShift = 0;
		bestScore = 0;
		int num = center - 5;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = center + 5;
		if (num2 > maxShift)
		{
			num2 = maxShift;
		}
		for (int i = num; i <= num2; i++)
		{
			int num3 = fineScores[i];
			if (num3 > bestScore)
			{
				bestScore = num3;
				bestShift = i;
			}
		}
	}

	private int DiffScore(PreprocessedFrame first, PreprocessedFrame second, int shift, int rowStep, int hd0RowStep)
	{
		int num = height - shift - 3;
		if (num <= 0)
		{
			return 0;
		}
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < num; i += rowStep)
		{
			int num4 = num3 * stripes;
			for (int j = 0; j < stripes; j++)
			{
				int num5 = hd0[num4 + j];
				int idx = CellIndex(j, i + shift, height);
				int idx2 = CellIndex(j, i, height);
				int num6 = CellHd(first, second, idx, idx2);
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					num2 += num7;
				}
			}
			num3 += hd0RowStep;
		}
		return num2;
	}

	private void ComputeStripeDiffs(PreprocessedFrame first, PreprocessedFrame second, int shift, Span<int> stripeDiffs)
	{
		for (int i = 0; i < stripes; i++)
		{
			stripeDiffs[i] = 0;
		}
		int num = height - shift - 3;
		if (num <= 0)
		{
			return;
		}
		int num2 = 0;
		for (int j = 0; j < num; j += 2)
		{
			int num3 = num2 * stripes;
			for (int k = 0; k < stripes; k++)
			{
				int num4 = hd0[num3 + k];
				int idx = CellIndex(k, j + shift, height);
				int idx2 = CellIndex(k, j, height);
				int num5 = CellHd(first, second, idx, idx2);
				stripeDiffs[k] += num4 - num5;
			}
			num2++;
		}
	}

	private int CountSupportingStripes(PreprocessedFrame first, PreprocessedFrame second, int shift)
	{
		Span<int> stripeDiffs = stackalloc int[stripes];
		ComputeStripeDiffs(first, second, shift, stripeDiffs);
		int num = 0;
		for (int i = 0; i < stripes; i++)
		{
			if (stripeDiffs[i] > 0)
			{
				num++;
			}
		}
		return num;
	}

	private int RefineShift(PreprocessedFrame first, PreprocessedFrame second, int bestShift)
	{
		Span<int> stripeDiffs = stackalloc int[stripes];
		ComputeStripeDiffs(first, second, bestShift, stripeDiffs);
		Span<bool> span = stackalloc bool[stripes];
		for (int i = 0; i < stripes; i++)
		{
			span[i] = stripeDiffs[i] > 0;
		}
		int num = bestShift - 5;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = bestShift + 5;
		if (num2 > maxShift)
		{
			num2 = maxShift;
		}
		int result = bestShift;
		int num3 = int.MinValue;
		for (int j = num; j <= num2; j++)
		{
			int num4 = height - j - 3;
			if (num4 <= 0)
			{
				continue;
			}
			int num5 = 0;
			int num6 = 0;
			for (int k = 0; k < num4; k += 2)
			{
				int num7 = num6 * stripes;
				for (int l = 0; l < stripes; l++)
				{
					if (span[l])
					{
						int num8 = hd0[num7 + l];
						int idx = CellIndex(l, k + j, height);
						int idx2 = CellIndex(l, k, height);
						int num9 = CellHd(first, second, idx, idx2);
						num5 += num8 - num9;
					}
				}
				num6++;
			}
			if (num5 > num3)
			{
				num3 = num5;
				result = j;
			}
		}
		return result;
	}

	private bool IsPeakSharp(PreprocessedFrame first, PreprocessedFrame second, int bestShift, int bestScore, out double neighborAvg)
	{
		int num = 0;
		int num2 = 0;
		for (int i = -6; i <= 6; i++)
		{
			if (i == 0)
			{
				continue;
			}
			int num3 = bestShift + i;
			if (num3 >= 1 && num3 <= maxShift)
			{
				int num4 = fineScores[num3];
				if (num4 < 0)
				{
					num4 = DiffScore(first, second, num3, 2, 1);
					fineScores[num3] = num4;
				}
				num += num4;
				num2++;
			}
		}
		neighborAvg = ((num2 > 0) ? ((double)num / (double)num2) : 0.0);
		if (neighborAvg < 1.0)
		{
			return true;
		}
		return (double)bestScore >= neighborAvg * 1.1;
	}

	private int ComputeMovingCenter(PreprocessedFrame first, PreprocessedFrame second, int shift)
	{
		int num = height - shift - 3;
		int num2 = -1;
		int num3 = -1;
		int num4 = 0;
		for (int i = 0; i < num; i += 2)
		{
			int num5 = num4 * stripes;
			bool flag = false;
			for (int j = 0; j < stripes; j++)
			{
				int num6 = hd0[num5 + j];
				int idx = CellIndex(j, i + shift, height);
				int idx2 = CellIndex(j, i, height);
				if (CellHd(first, second, idx, idx2) < num6)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				if (num2 < 0)
				{
					num2 = i;
				}
				num3 = i;
			}
			num4++;
		}
		if (num2 < 0)
		{
			return height / 2;
		}
		int num7 = num2;
		int num8 = num3 + shift;
		if (num8 >= height)
		{
			num8 = height - 1;
		}
		return (num7 + num8) / 2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int CellHd(PreprocessedFrame first, PreprocessedFrame second, int idx1, int idx2)
	{
		ulong num = first.Key[idx1];
		ulong num2 = second.Key[idx2];
		if (num != ulong.MaxValue && num2 != ulong.MaxValue)
		{
			return PopCount64(num ^ num2);
		}
		return PopCount32(first.RowHash[idx1] ^ second.RowHash[idx2]) * 2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int CellIndex(int stripe, int y, int height)
	{
		return stripe * height + y;
	}

	private void LogHd0Stats(int totalCells, int changedCells)
	{
		long num = 0L;
		int num2 = 0;
		for (int i = 0; i < totalCells; i++)
		{
			int num3 = hd0[i];
			if (num3 > 0)
			{
				num += num3;
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
		}
		double num4 = ((changedCells > 0) ? ((double)num / (double)changedCells) : 0.0);
		double num5 = ((totalCells > 0) ? ((double)changedCells / (double)totalCells * 100.0) : 0.0);
		logger.LogDebug("{Prefix}HD0: N={Total} changed={Changed}({Pct}%) avg={Avg} max={Max}", logPrefix, totalCells, changedCells, num5.ToString("F1"), num4.ToString("F1"), num2);
	}

	private void LogCoarsePass(int count)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Coarse: [");
		for (int i = 0; i < count; i++)
		{
			if (i > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(coarsePeakShifts[i]).Append(':').Append(coarsePeakScores[i]);
		}
		stringBuilder.Append(']');
		logger.LogDebug("{Prefix}{Message}", logPrefix, stringBuilder.ToString());
	}

	private void LogFinalDecision(int bestShift, int bestScore, double neighborAvg, bool peakSharp, int bestStripeSupport)
	{
		string value = ((neighborAvg >= 1.0) ? ((double)bestScore / neighborAvg).ToString("F2") : "inf");
		Span<int> shifts = stackalloc int[5];
		Span<int> scores = stackalloc int[5];
		int count = 0;
		for (int i = 1; i <= maxShift; i++)
		{
			int num = fineScores[i];
			if (num >= 0)
			{
				InsertTopN(shifts, scores, ref count, i, num);
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Final: s=").Append(bestShift);
		stringBuilder.Append(" sc=").Append(bestScore);
		stringBuilder.Append(" ss=").Append(bestStripeSupport).Append('/')
			.Append(stripes);
		stringBuilder.Append(" navg=").Append(neighborAvg.ToString("F1"));
		stringBuilder.Append(" shrp=").Append(value);
		stringBuilder.Append(" ok=").Append(peakSharp);
		stringBuilder.Append(" top=[");
		for (int j = 0; j < count; j++)
		{
			if (j > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(shifts[j]).Append(':').Append(scores[j]);
		}
		stringBuilder.Append(']');
		logger.LogDebug("{Prefix}{Message}", logPrefix, stringBuilder.ToString());
	}

	private static void InsertTopN(Span<int> shifts, Span<int> scores, ref int count, int shift, int score)
	{
		int length = shifts.Length;
		if (count < length)
		{
			shifts[count] = shift;
			scores[count] = score;
			count++;
		}
		else
		{
			if (score <= scores[length - 1])
			{
				return;
			}
			shifts[length - 1] = shift;
			scores[length - 1] = score;
		}
		int num = ((count < length) ? count : length) - 1;
		while (num > 0 && scores[num] > scores[num - 1])
		{
			int num2 = shifts[num];
			shifts[num] = shifts[num - 1];
			shifts[num - 1] = num2;
			int num3 = scores[num];
			scores[num] = scores[num - 1];
			scores[num - 1] = num3;
			num--;
		}
	}

	private unsafe static PreprocessedFrame PreprocessInternal(byte* bgra, int width, int height)
	{
		int stripesForWidth = GetStripesForWidth(width);
		int num = stripesForWidth * height;
		uint[] array = new uint[num];
		ulong[] array2 = new ulong[num];
		int num2 = width * 4;
		for (int i = 0; i < height; i++)
		{
			byte* rowPtr = bgra + i * num2;
			for (int j = 0; j < stripesForWidth; j++)
			{
				int num3 = j * width / stripesForWidth;
				int num4 = (j + 1) * width / stripesForWidth;
				int num5 = num4 - num3;
				if (num5 <= 0)
				{
					continue;
				}
				int num6 = CellIndex(j, i, height);
				int num7 = -1;
				uint num8 = 0u;
				for (int k = 0; k < 33; k++)
				{
					int num9 = num3 + k * (num5 - 1) / 32;
					int num10 = num9 + 1;
					if (num10 >= num4)
					{
						num10 = num9;
					}
					int num11 = LumaAt(rowPtr, num9) + LumaAt(rowPtr, num10) >> 1;
					if (k > 0 && num11 > num7)
					{
						num8 |= (uint)(1 << k - 1);
					}
					num7 = num11;
				}
				array[num6] = num8;
			}
		}
		for (int l = 0; l < stripesForWidth; l++)
		{
			int num12 = l * height;
			for (int m = 0; m < height; m++)
			{
				int num13 = num12 + m;
				int num14 = m + 3;
				if (num14 >= height)
				{
					array2[num13] = ulong.MaxValue;
				}
				else
				{
					array2[num13] = array[num13] | ((ulong)array[num12 + num14] << 32);
				}
			}
		}
		return new PreprocessedFrame(width, height, stripesForWidth, array, array2);
	}

	private static int GetStripesForWidth(int width)
	{
		int num = width / 64;
		if (num < 6)
		{
			num = Math.Max(3, width / 64);
		}
		if (num > 16)
		{
			num = 16;
		}
		return num;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static int LumaAt(byte* rowPtr, int x)
	{
		byte b = rowPtr[x * 4];
		byte b2 = rowPtr[x * 4 + 1];
		return rowPtr[x * 4 + 2] * 77 + b2 * 150 + b * 29 >> 8;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int PopCount64(ulong v)
	{
		v -= (v >> 1) & 0x5555555555555555L;
		v = (v & 0x3333333333333333L) + ((v >> 2) & 0x3333333333333333L);
		v = (v + (v >> 4)) & 0xF0F0F0F0F0F0F0FL;
		return (int)(v * 72340172838076673L >> 56);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int PopCount32(uint v)
	{
		v -= (v >> 1) & 0x55555555;
		v = (v & 0x33333333) + ((v >> 2) & 0x33333333);
		v = (v + (v >> 4)) & 0xF0F0F0F;
		return (int)(v * 16843009 >> 24);
	}
}
