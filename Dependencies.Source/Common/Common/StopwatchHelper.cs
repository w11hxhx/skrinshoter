using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Common;

public static class StopwatchHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long GetDurationInWholeMillisecondsSince(long startTime)
	{
		return (long)Math.Round(GetDurationInMillisecondsSince(startTime));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double GetDurationInMillisecondsSince(long startTime)
	{
		return GetDurationInMilliseconds(startTime, Stopwatch.GetTimestamp());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double GetDurationInSecondsSince(long startTime)
	{
		return GetDurationInSeconds(startTime, Stopwatch.GetTimestamp());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TimeSpan GetDurationSince(long startTime)
	{
		return GetDuration(startTime, Stopwatch.GetTimestamp());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TimeSpan GetDuration(long from, long to)
	{
		return TimeSpan.FromMilliseconds(GetDurationInMilliseconds(from, to));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long GetDurationInWholeMicroseconds(long from, long to)
	{
		return (long)Math.Round(GetDurationInMicroseconds(from, to));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double GetDurationInMicroseconds(long from, long to)
	{
		return GetDurationInSeconds(from, to) * 1000000.0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double GetDurationInMilliseconds(long from, long to)
	{
		return GetDurationInSeconds(from, to) * 1000.0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double GetDurationInSeconds(long from, long to)
	{
		return ((double)to - (double)from) / (double)Stopwatch.Frequency;
	}
}
