using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com.StructuredStorage;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct PROPVARIANT
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
				[FieldOffset(0)]
				internal CHAR cVal;

				[FieldOffset(0)]
				internal byte bVal;

				[FieldOffset(0)]
				internal short iVal;

				[FieldOffset(0)]
				internal ushort uiVal;

				[FieldOffset(0)]
				internal int lVal;

				[FieldOffset(0)]
				internal uint ulVal;

				[FieldOffset(0)]
				internal int intVal;

				[FieldOffset(0)]
				internal uint uintVal;

				[FieldOffset(0)]
				internal long hVal;

				[FieldOffset(0)]
				internal ulong uhVal;

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
				internal global::System.Runtime.InteropServices.ComTypes.FILETIME filetime;

				[FieldOffset(0)]
				internal unsafe Guid* puuid;

				[FieldOffset(0)]
				internal unsafe CLIPDATA* pclipdata;

				[FieldOffset(0)]
				internal BSTR bstrVal;

				[FieldOffset(0)]
				internal BSTRBLOB bstrblobVal;

				[FieldOffset(0)]
				internal BLOB blob;

				[FieldOffset(0)]
				internal PSTR pszVal;

				[FieldOffset(0)]
				internal PWSTR pwszVal;

				[FieldOffset(0)]
				internal unsafe IUnknown* punkVal;

				[FieldOffset(0)]
				internal unsafe IDispatch* pdispVal;

				[FieldOffset(0)]
				internal unsafe IStream* pStream;

				[FieldOffset(0)]
				internal unsafe IStorage* pStorage;

				[FieldOffset(0)]
				internal unsafe VERSIONEDSTREAM* pVersionedStream;

				[FieldOffset(0)]
				internal unsafe SAFEARRAY* parray;

				[FieldOffset(0)]
				internal CAC cac;

				[FieldOffset(0)]
				internal CAUB caub;

				[FieldOffset(0)]
				internal CAI cai;

				[FieldOffset(0)]
				internal CAUI caui;

				[FieldOffset(0)]
				internal CAL cal;

				[FieldOffset(0)]
				internal CAUL caul;

				[FieldOffset(0)]
				internal CAH cah;

				[FieldOffset(0)]
				internal CAUH cauh;

				[FieldOffset(0)]
				internal CAFLT caflt;

				[FieldOffset(0)]
				internal CADBL cadbl;

				[FieldOffset(0)]
				internal CABOOL cabool;

				[FieldOffset(0)]
				internal CASCODE cascode;

				[FieldOffset(0)]
				internal CACY cacy;

				[FieldOffset(0)]
				internal CADATE cadate;

				[FieldOffset(0)]
				internal CAFILETIME cafiletime;

				[FieldOffset(0)]
				internal CACLSID cauuid;

				[FieldOffset(0)]
				internal CACLIPDATA caclipdata;

				[FieldOffset(0)]
				internal CABSTR cabstr;

				[FieldOffset(0)]
				internal CABSTRBLOB cabstrblob;

				[FieldOffset(0)]
				internal CALPSTR calpstr;

				[FieldOffset(0)]
				internal CALPWSTR calpwstr;

				[FieldOffset(0)]
				internal CAPROPVARIANT capropvar;

				[FieldOffset(0)]
				internal PSTR pcVal;

				[FieldOffset(0)]
				internal unsafe byte* pbVal;

				[FieldOffset(0)]
				internal unsafe short* piVal;

				[FieldOffset(0)]
				internal unsafe ushort* puiVal;

				[FieldOffset(0)]
				internal unsafe int* plVal;

				[FieldOffset(0)]
				internal unsafe uint* pulVal;

				[FieldOffset(0)]
				internal unsafe int* pintVal;

				[FieldOffset(0)]
				internal unsafe uint* puintVal;

				[FieldOffset(0)]
				internal unsafe float* pfltVal;

				[FieldOffset(0)]
				internal unsafe double* pdblVal;

				[FieldOffset(0)]
				internal unsafe VARIANT_BOOL* pboolVal;

				[FieldOffset(0)]
				internal unsafe DECIMAL* pdecVal;

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
				internal unsafe PROPVARIANT* pvarVal;
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
