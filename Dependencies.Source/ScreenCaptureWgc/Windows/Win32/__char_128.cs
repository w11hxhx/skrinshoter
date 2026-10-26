using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct __char_128
{
	private const int SpanLength = 128;

	internal unsafe fixed char Value[128];

	internal readonly int Length => 128;

	[UnscopedRef]
	internal unsafe ref char this[int index] => ref Value[index];

	internal unsafe readonly void CopyTo(Span<char> target, int length = 128)
	{
		fixed (char* value = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	internal unsafe readonly char[] ToArray(int length = 128)
	{
		fixed (char* value = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	internal unsafe readonly bool Equals(ReadOnlySpan<char> value)
	{
		fixed (char* value2 = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value2, 128);
			if (value.Length != 128)
			{
				return readOnlySpan.SliceAtNull().SequenceEqual(value);
			}
			return readOnlySpan.SequenceEqual(value);
		}
	}

	internal readonly bool Equals(string value)
	{
		return Equals((ReadOnlySpan<char>)value);
	}

	internal unsafe readonly string ToString(int length)
	{
		fixed (char* value = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToString();
		}
	}

	public unsafe override readonly string ToString()
	{
		fixed (char* value = Value)
		{
			return new ReadOnlySpan<char>(value, 128).SliceAtNull().ToString();
		}
	}

	public static implicit operator __char_128(string value)
	{
		return (ReadOnlySpan<char>)value;
	}

	public unsafe static implicit operator __char_128(ReadOnlySpan<char> value)
	{
		Unsafe.SkipInit<__char_128>(out var value2);
		Span<char> destination = new Span<char>(value2.Value, 128);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 128 - length).Clear();
		return value2;
	}
}
