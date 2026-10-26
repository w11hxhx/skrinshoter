using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Windows.Win32;

internal struct __float_4
{
	private const int SpanLength = 4;

	internal unsafe fixed float Value[4];

	internal readonly int Length => 4;

	[UnscopedRef]
	internal unsafe ref float this[int index] => ref Value[index];

	internal unsafe readonly void CopyTo(Span<float> target, int length = 4)
	{
		fixed (float* value = Value)
		{
			ReadOnlySpan<float> readOnlySpan = new ReadOnlySpan<float>(value, 4);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	internal unsafe readonly float[] ToArray(int length = 4)
	{
		fixed (float* value = Value)
		{
			ReadOnlySpan<float> readOnlySpan = new ReadOnlySpan<float>(value, 4);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	internal unsafe readonly bool Equals(ReadOnlySpan<float> value)
	{
		fixed (float* value2 = Value)
		{
			return new ReadOnlySpan<float>(value2, 4).SequenceEqual(value);
		}
	}

	public unsafe static implicit operator __float_4(ReadOnlySpan<float> value)
	{
		Unsafe.SkipInit<__float_4>(out var value2);
		Span<float> destination = new Span<float>(value2.Value, 4);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 4 - length).Clear();
		return value2;
	}
}
