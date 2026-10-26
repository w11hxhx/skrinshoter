using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Ole;

namespace Windows.Win32.System.Variant;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct VARIANT
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
		internal struct _Anonymous_e__Struct
		{
			[StructLayout(LayoutKind.Explicit)]
			[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
			internal struct _Anonymous_e__Union
			{
				[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
				internal struct _Anonymous_e__Struct
				{
					internal unsafe void* pvRecord;

					internal unsafe IRecordInfo* pRecInfo;
				}

				[FieldOffset(0)]
				internal long llVal;

				[FieldOffset(0)]
				internal int lVal;

				[FieldOffset(0)]
				internal byte bVal;

				[FieldOffset(0)]
				internal short iVal;

				[FieldOffset(0)]
				internal float fltVal;

				[FieldOffset(0)]
				internal double dblVal;

				[FieldOffset(0)]
				internal VARIANT_BOOL boolVal;

				[FieldOffset(0)]
				internal VARIANT_BOOL __OBSOLETE__VARIANT_BOOL;

				[FieldOffset(0)]
				internal int scode;

				[FieldOffset(0)]
				internal CY cyVal;

				[FieldOffset(0)]
				internal double date;

				[FieldOffset(0)]
				internal BSTR bstrVal;

				[FieldOffset(0)]
				internal unsafe IUnknown* punkVal;

				[FieldOffset(0)]
				internal unsafe IDispatch* pdispVal;

				[FieldOffset(0)]
				internal unsafe SAFEARRAY* parray;

				[FieldOffset(0)]
				internal unsafe byte* pbVal;

				[FieldOffset(0)]
				internal unsafe short* piVal;

				[FieldOffset(0)]
				internal unsafe int* plVal;

				[FieldOffset(0)]
				internal unsafe long* pllVal;

				[FieldOffset(0)]
				internal unsafe float* pfltVal;

				[FieldOffset(0)]
				internal unsafe double* pdblVal;

				[FieldOffset(0)]
				internal unsafe VARIANT_BOOL* pboolVal;

				[FieldOffset(0)]
				internal unsafe VARIANT_BOOL* __OBSOLETE__VARIANT_PBOOL;

				[FieldOffset(0)]
				internal unsafe int* pscode;

				[FieldOffset(0)]
				internal unsafe CY* pcyVal;

				[FieldOffset(0)]
				internal unsafe double* pdate;

				[FieldOffset(0)]
				internal unsafe BSTR* pbstrVal;

				[FieldOffset(0)]
				internal unsafe IUnknown** ppunkVal;

				[FieldOffset(0)]
				internal unsafe IDispatch** ppdispVal;

				[FieldOffset(0)]
				internal unsafe SAFEARRAY** pparray;

				[FieldOffset(0)]
				internal unsafe VARIANT* pvarVal;

				[FieldOffset(0)]
				internal unsafe void* byref;

				[FieldOffset(0)]
				internal CHAR cVal;

				[FieldOffset(0)]
				internal ushort uiVal;

				[FieldOffset(0)]
				internal uint ulVal;

				[FieldOffset(0)]
				internal ulong ullVal;

				[FieldOffset(0)]
				internal int intVal;

				[FieldOffset(0)]
				internal uint uintVal;

				[FieldOffset(0)]
				internal unsafe DECIMAL* pdecVal;

				[FieldOffset(0)]
				internal PSTR pcVal;

				[FieldOffset(0)]
				internal unsafe ushort* puiVal;

				[FieldOffset(0)]
				internal unsafe uint* pulVal;

				[FieldOffset(0)]
				internal unsafe ulong* pullVal;

				[FieldOffset(0)]
				internal unsafe int* pintVal;

				[FieldOffset(0)]
				internal unsafe uint* puintVal;

				[FieldOffset(0)]
				internal _Anonymous_e__Struct Anonymous;
			}

			internal VARENUM vt;

			internal ushort wReserved1;

			internal ushort wReserved2;

			internal ushort wReserved3;

			internal _Anonymous_e__Union Anonymous;
		}

		[FieldOffset(0)]
		internal _Anonymous_e__Struct Anonymous;

		[FieldOffset(0)]
		internal DECIMAL decVal;
	}

	internal _Anonymous_e__Union Anonymous;
}
