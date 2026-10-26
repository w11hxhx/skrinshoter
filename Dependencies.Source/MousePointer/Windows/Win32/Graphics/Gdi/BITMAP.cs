using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct BITMAP
{
	internal int bmType;

	internal int bmWidth;

	internal int bmHeight;

	internal int bmWidthBytes;

	internal ushort bmPlanes;

	internal ushort bmBitsPixel;

	internal unsafe void* bmBits;
}
