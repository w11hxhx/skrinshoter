using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DEVMODEW
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous1_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
		internal struct _Anonymous1_e__Struct
		{
			internal short dmOrientation;

			internal short dmPaperSize;

			internal short dmPaperLength;

			internal short dmPaperWidth;

			internal short dmScale;

			internal short dmCopies;

			internal short dmDefaultSource;

			internal short dmPrintQuality;
		}

		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
		internal struct _Anonymous2_e__Struct
		{
			internal POINTL dmPosition;

			internal DEVMODE_DISPLAY_ORIENTATION dmDisplayOrientation;

			internal DEVMODE_DISPLAY_FIXED_OUTPUT dmDisplayFixedOutput;
		}

		[FieldOffset(0)]
		internal _Anonymous1_e__Struct Anonymous1;

		[FieldOffset(0)]
		internal _Anonymous2_e__Struct Anonymous2;
	}

	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
	internal struct _Anonymous2_e__Union
	{
		[FieldOffset(0)]
		internal uint dmDisplayFlags;

		[FieldOffset(0)]
		internal uint dmNup;
	}

	internal __char_32 dmDeviceName;

	internal ushort dmSpecVersion;

	internal ushort dmDriverVersion;

	internal ushort dmSize;

	internal ushort dmDriverExtra;

	internal DEVMODE_FIELD_FLAGS dmFields;

	internal _Anonymous1_e__Union Anonymous1;

	internal DEVMODE_COLOR dmColor;

	internal DEVMODE_DUPLEX dmDuplex;

	internal short dmYResolution;

	internal DEVMODE_TRUETYPE_OPTION dmTTOption;

	internal DEVMODE_COLLATE dmCollate;

	internal __char_32 dmFormName;

	internal ushort dmLogPixels;

	internal uint dmBitsPerPel;

	internal uint dmPelsWidth;

	internal uint dmPelsHeight;

	internal _Anonymous2_e__Union Anonymous2;

	internal uint dmDisplayFrequency;

	internal uint dmICMMethod;

	internal uint dmICMIntent;

	internal uint dmMediaType;

	internal uint dmDitherType;

	internal uint dmReserved1;

	internal uint dmReserved2;

	internal uint dmPanningWidth;

	internal uint dmPanningHeight;
}
