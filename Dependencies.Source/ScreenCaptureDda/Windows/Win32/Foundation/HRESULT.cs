using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal readonly struct HRESULT : IEquatable<HRESULT>
{
	internal readonly int Value;

	internal static readonly HRESULT DXGI_ERROR_INVALID_CALL = (HRESULT)(-2005270527);

	internal static readonly HRESULT DXGI_ERROR_NOT_FOUND = (HRESULT)(-2005270526);

	internal static readonly HRESULT DXGI_ERROR_MORE_DATA = (HRESULT)(-2005270525);

	internal static readonly HRESULT DXGI_ERROR_UNSUPPORTED = (HRESULT)(-2005270524);

	internal static readonly HRESULT DXGI_ERROR_DEVICE_REMOVED = (HRESULT)(-2005270523);

	internal static readonly HRESULT DXGI_ERROR_DEVICE_HUNG = (HRESULT)(-2005270522);

	internal static readonly HRESULT DXGI_ERROR_DEVICE_RESET = (HRESULT)(-2005270521);

	internal static readonly HRESULT DXGI_ERROR_WAS_STILL_DRAWING = (HRESULT)(-2005270518);

	internal static readonly HRESULT DXGI_ERROR_FRAME_STATISTICS_DISJOINT = (HRESULT)(-2005270517);

	internal static readonly HRESULT DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE = (HRESULT)(-2005270516);

	internal static readonly HRESULT DXGI_ERROR_DRIVER_INTERNAL_ERROR = (HRESULT)(-2005270496);

	internal static readonly HRESULT DXGI_ERROR_NONEXCLUSIVE = (HRESULT)(-2005270495);

	internal static readonly HRESULT DXGI_ERROR_NOT_CURRENTLY_AVAILABLE = (HRESULT)(-2005270494);

	internal static readonly HRESULT DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED = (HRESULT)(-2005270493);

	internal static readonly HRESULT DXGI_ERROR_REMOTE_OUTOFMEMORY = (HRESULT)(-2005270492);

	internal static readonly HRESULT DXGI_ERROR_ACCESS_LOST = (HRESULT)(-2005270490);

	internal static readonly HRESULT DXGI_ERROR_WAIT_TIMEOUT = (HRESULT)(-2005270489);

	internal static readonly HRESULT DXGI_ERROR_SESSION_DISCONNECTED = (HRESULT)(-2005270488);

	internal static readonly HRESULT DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE = (HRESULT)(-2005270487);

	internal static readonly HRESULT DXGI_ERROR_CANNOT_PROTECT_CONTENT = (HRESULT)(-2005270486);

	internal static readonly HRESULT DXGI_ERROR_ACCESS_DENIED = (HRESULT)(-2005270485);

	internal static readonly HRESULT DXGI_ERROR_NAME_ALREADY_EXISTS = (HRESULT)(-2005270484);

	internal static readonly HRESULT DXGI_ERROR_SDK_COMPONENT_MISSING = (HRESULT)(-2005270483);

	internal static readonly HRESULT DXGI_ERROR_NOT_CURRENT = (HRESULT)(-2005270482);

	internal static readonly HRESULT DXGI_ERROR_HW_PROTECTION_OUTOFMEMORY = (HRESULT)(-2005270480);

	internal static readonly HRESULT DXGI_ERROR_DYNAMIC_CODE_POLICY_VIOLATION = (HRESULT)(-2005270479);

	internal static readonly HRESULT DXGI_ERROR_NON_COMPOSITED_UI = (HRESULT)(-2005270478);

	internal static readonly HRESULT DXGI_ERROR_MODE_CHANGE_IN_PROGRESS = (HRESULT)(-2005270491);

	internal static readonly HRESULT DXGI_ERROR_CACHE_CORRUPT = (HRESULT)(-2005270477);

	internal static readonly HRESULT DXGI_ERROR_CACHE_FULL = (HRESULT)(-2005270476);

	internal static readonly HRESULT DXGI_ERROR_CACHE_HASH_COLLISION = (HRESULT)(-2005270475);

	internal static readonly HRESULT DXGI_ERROR_ALREADY_EXISTS = (HRESULT)(-2005270474);

	internal static readonly HRESULT DXGI_ERROR_MPO_UNPINNED = (HRESULT)(-2005270428);

	internal static readonly HRESULT DXGI_ERROR_SETDISPLAYMODE_REQUIRED = (HRESULT)(-2005270427);

	internal static readonly HRESULT E_ACCESSDENIED = (HRESULT)(-2147024891);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	internal bool Succeeded => Value >= 0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	internal bool Failed => Value < 0;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private string DebuggerDisplay => $"{ToString()} {new Win32Exception(Value).Message}";

	internal HRESULT(int value)
	{
		Value = value;
	}

	public static implicit operator int(HRESULT value)
	{
		return value.Value;
	}

	public static explicit operator HRESULT(int value)
	{
		return new HRESULT(value);
	}

	public static bool operator ==(HRESULT left, HRESULT right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HRESULT left, HRESULT right)
	{
		return !(left == right);
	}

	public bool Equals(HRESULT other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HRESULT other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", Value);
	}

	public static implicit operator uint(HRESULT value)
	{
		return (uint)value.Value;
	}

	public static explicit operator HRESULT(uint value)
	{
		return new HRESULT((int)value);
	}

	internal HRESULT ThrowOnFailure(IntPtr errorInfo = default(IntPtr))
	{
		Marshal.ThrowExceptionForHR(Value, errorInfo);
		return this;
	}

	internal string ToString(string format, IFormatProvider formatProvider)
	{
		return ((uint)Value).ToString(format, formatProvider);
	}
}
