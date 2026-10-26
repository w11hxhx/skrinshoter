using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal readonly struct HBITMAP : IEquatable<HBITMAP>
{
	internal unsafe readonly void* Value;

	internal static HBITMAP Null => default;

	internal unsafe bool IsNull => Value == null;

	internal unsafe HBITMAP(void* value)
	{
		Value = value;
	}

	internal unsafe HBITMAP(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HBITMAP value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HBITMAP(void* value)
	{
		return new HBITMAP(value);
	}

	public unsafe static bool operator ==(HBITMAP left, HBITMAP right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HBITMAP left, HBITMAP right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HBITMAP other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HBITMAP other)
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

	public unsafe static implicit operator IntPtr(HBITMAP value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HBITMAP(IntPtr value)
	{
		return new HBITMAP(value.ToPointer());
	}

	public unsafe static explicit operator HBITMAP(UIntPtr value)
	{
		return new HBITMAP(value.ToPointer());
	}

	public unsafe static implicit operator HGDIOBJ(HBITMAP value)
	{
		return new HGDIOBJ(value.Value);
	}
}
