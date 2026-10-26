using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.Foundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DECIMAL
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous1_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
		internal struct _Anonymous_e__Struct
		{
			internal byte scale;

			internal byte sign;
		}

		[FieldOffset(0)]
		internal _Anonymous_e__Struct Anonymous;

		[FieldOffset(0)]
		internal ushort signscale;
	}

	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous2_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
		internal struct _Anonymous_e__Struct
		{
			internal uint Lo32;

			internal uint Mid32;
		}

		[FieldOffset(0)]
		internal _Anonymous_e__Struct Anonymous;

		[FieldOffset(0)]
		internal ulong Lo64;
	}

	internal ushort wReserved;

	internal _Anonymous1_e__Union Anonymous1;

	internal uint Hi32;

	internal _Anonymous2_e__Union Anonymous2;

	public DECIMAL(decimal value)
	{
		int[] bits = decimal.GetBits(value);
		uint lo = (uint)bits[0];
		uint mid = (uint)bits[1];
		uint hi = (uint)bits[2];
		byte scale = (byte)(bits[3] >> 16);
		byte sign = (byte)(((bits[3] & int.MinValue) == int.MinValue) ? 128 : 0);
		Anonymous2 = new _Anonymous2_e__Union
		{
			Anonymous = new _Anonymous2_e__Union._Anonymous_e__Struct
			{
				Lo32 = lo,
				Mid32 = mid
			}
		};
		Hi32 = hi;
		Anonymous1 = new _Anonymous1_e__Union
		{
			Anonymous = new _Anonymous1_e__Union._Anonymous_e__Struct
			{
				scale = scale,
				sign = sign
			}
		};
		wReserved = 0;
	}

	public static implicit operator decimal(DECIMAL value)
	{
		return new decimal((int)value.Anonymous2.Anonymous.Lo32, (int)value.Anonymous2.Anonymous.Mid32, (int)value.Hi32, value.Anonymous1.Anonymous.sign == 128, value.Anonymous1.Anonymous.scale);
	}
}
