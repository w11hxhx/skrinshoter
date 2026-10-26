using System;
using System.Numerics;

namespace AudioVideoCore;

public readonly struct FrameTimestamp : IEquatable<FrameTimestamp>, IComparable<FrameTimestamp>
{
	public long Value { get; }

	public long TimeBase { get; }

	public FrameTimestamp(long value, long timeBase)
	{
		if (timeBase <= 0)
		{
			throw new ArgumentOutOfRangeException("timeBase");
		}
		Value = value;
		TimeBase = timeBase;
	}

	public bool Equals(FrameTimestamp other)
	{
		return CompareTo(other) == 0;
	}

	public override bool Equals(object obj)
	{
		if (obj is FrameTimestamp other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return 0;
	}

	public static bool operator ==(FrameTimestamp left, FrameTimestamp right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(FrameTimestamp left, FrameTimestamp right)
	{
		return !(left == right);
	}

	public int CompareTo(FrameTimestamp other)
	{
		if (TimeBase == other.TimeBase)
		{
			return Value.CompareTo(other.Value);
		}
		BigInteger bigInteger = (BigInteger)Value * (BigInteger)other.TimeBase;
		BigInteger other2 = (BigInteger)other.Value * (BigInteger)TimeBase;
		return bigInteger.CompareTo(other2);
	}

	public static bool operator <(FrameTimestamp left, FrameTimestamp right)
	{
		return left.CompareTo(right) < 0;
	}

	public static bool operator <=(FrameTimestamp left, FrameTimestamp right)
	{
		return left.CompareTo(right) <= 0;
	}

	public static bool operator >(FrameTimestamp left, FrameTimestamp right)
	{
		return left.CompareTo(right) > 0;
	}

	public static bool operator >=(FrameTimestamp left, FrameTimestamp right)
	{
		return left.CompareTo(right) >= 0;
	}

	public long Rescale(long timeBase)
	{
		if (timeBase <= 0)
		{
			throw new ArgumentOutOfRangeException("timeBase");
		}
		return (long)((BigInteger)Value * (BigInteger)timeBase / TimeBase);
	}

	public TimeSpan ToTimeSpan()
	{
		return new TimeSpan(Rescale(10000000L));
	}

	public override string ToString()
	{
		return $"{Value}/{TimeBase}";
	}
}
