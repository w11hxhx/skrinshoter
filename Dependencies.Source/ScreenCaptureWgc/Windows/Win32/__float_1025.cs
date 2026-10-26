using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Windows.Win32;

internal struct __float_1025
{
	private const int SpanLength = 1025;

	internal unsafe fixed float Value[1025];

	internal readonly int Length => 1025;

	[UnscopedRef]
	internal unsafe ref float this[int index] => ref Value[index];

	internal unsafe readonly void CopyTo(Span<float> target, int length = 1025)
	{
		fixed (float* value = Value)
		{
			ReadOnlySpan<float> readOnlySpan = new ReadOnlySpan<float>(value, 1025);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	internal unsafe readonly float[] ToArray(int length = 1025)
	{
		fixed (float* value = Value)
		{
			ReadOnlySpan<float> readOnlySpan = new ReadOnlySpan<float>(value, 1025);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	internal unsafe readonly bool Equals(ReadOnlySpan<float> value)
	{
		fixed (float* value2 = Value)
		{
			return new ReadOnlySpan<float>(value2, 1025).SequenceEqual(value);
		}
	}

	public unsafe static implicit operator __float_1025(ReadOnlySpan<float> value)
	{
		Unsafe.SkipInit<__float_1025>(out var value2);
		Span<float> destination = new Span<float>(value2.Value, 1025);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 1025 - length).Clear();
		return value2;
	}
}
