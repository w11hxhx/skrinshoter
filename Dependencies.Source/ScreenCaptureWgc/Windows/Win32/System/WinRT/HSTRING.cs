using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.System.WinRT;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal readonly struct HSTRING : IEquatable<HSTRING>
{
	internal unsafe readonly void* Value;

	internal static HSTRING Null => default;

	internal unsafe bool IsNull => Value == null;

	internal unsafe HSTRING(void* value)
	{
		Value = value;
	}

	internal unsafe HSTRING(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HSTRING value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HSTRING(void* value)
	{
		return new HSTRING(value);
	}

	public unsafe static bool operator ==(HSTRING left, HSTRING right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HSTRING left, HSTRING right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HSTRING other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HSTRING other)
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

	public unsafe static implicit operator IntPtr(HSTRING value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HSTRING(IntPtr value)
	{
		return new HSTRING(value.ToPointer());
	}

	public unsafe static explicit operator HSTRING(UIntPtr value)
	{
		return new HSTRING(value.ToPointer());
	}
}
