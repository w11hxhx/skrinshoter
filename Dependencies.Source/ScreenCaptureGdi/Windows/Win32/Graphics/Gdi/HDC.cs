using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal readonly struct HDC : IEquatable<HDC>
{
	internal unsafe readonly void* Value;

	internal static HDC Null => default;

	internal unsafe bool IsNull => Value == null;

	internal unsafe HDC(void* value)
	{
		Value = value;
	}

	internal unsafe HDC(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HDC value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HDC(void* value)
	{
		return new HDC(value);
	}

	public unsafe static bool operator ==(HDC left, HDC right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HDC left, HDC right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HDC other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HDC other)
		{
			return Equals(other);
		}
		return false;
	}

	public unsafe override int GetHashCode()
	{
		return (int)Value;
	}

	public unsafe override string ToString()
	{
		return $"0x{(nuint)Value:x}";
	}

	public unsafe static implicit operator IntPtr(HDC value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HDC(IntPtr value)
	{
		return new HDC(value.ToPointer());
	}

	public unsafe static explicit operator HDC(UIntPtr value)
	{
		return new HDC(value.ToPointer());
	}
}
