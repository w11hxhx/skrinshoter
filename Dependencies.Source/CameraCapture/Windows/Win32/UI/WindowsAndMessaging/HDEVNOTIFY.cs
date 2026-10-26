using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.UI.WindowsAndMessaging;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal readonly struct HDEVNOTIFY : IEquatable<HDEVNOTIFY>
{
	internal unsafe readonly void* Value;

	internal static HDEVNOTIFY Null => default;

	internal unsafe bool IsNull => Value == null;

	internal unsafe HDEVNOTIFY(void* value)
	{
		Value = value;
	}

	internal unsafe HDEVNOTIFY(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HDEVNOTIFY value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HDEVNOTIFY(void* value)
	{
		return new HDEVNOTIFY(value);
	}

	public unsafe static bool operator ==(HDEVNOTIFY left, HDEVNOTIFY right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HDEVNOTIFY left, HDEVNOTIFY right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HDEVNOTIFY other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HDEVNOTIFY other)
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

	public unsafe static implicit operator IntPtr(HDEVNOTIFY value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HDEVNOTIFY(IntPtr value)
	{
		return new HDEVNOTIFY(value.ToPointer());
	}

	public unsafe static explicit operator HDEVNOTIFY(UIntPtr value)
	{
		return new HDEVNOTIFY(value.ToPointer());
	}
}
