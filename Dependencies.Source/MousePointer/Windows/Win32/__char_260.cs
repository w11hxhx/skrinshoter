using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct __char_260
{
	private const int SpanLength = 260;

	internal unsafe fixed char Value[260];

	internal readonly int Length => 260;

	[UnscopedRef]
	internal unsafe ref char this[int index] => ref Value[index];

	internal unsafe readonly void CopyTo(Span<char> target, int length = 260)
	{
		fixed (char* value = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 260);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	internal unsafe readonly char[] ToArray(int length = 260)
	{
		fixed (char* value = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 260);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	internal unsafe readonly bool Equals(ReadOnlySpan<char> value)
	{
		fixed (char* value2 = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value2, 260);
			if (value.Length != 260)
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
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(value, 260);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToString();
		}
	}

	public unsafe override readonly string ToString()
	{
		fixed (char* value = Value)
		{
			return new ReadOnlySpan<char>(value, 260).SliceAtNull().ToString();
		}
	}

	public static implicit operator __char_260(string value)
	{
		return (ReadOnlySpan<char>)value;
	}

	public unsafe static implicit operator __char_260(ReadOnlySpan<char> value)
	{
		Unsafe.SkipInit<__char_260>(out var value2);
		Span<char> destination = new Span<char>(value2.Value, 260);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 260 - length).Clear();
		return value2;
	}
}
