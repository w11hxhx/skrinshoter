using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct BITMAPINFOHEADER
{
	internal uint biSize;

	internal int biWidth;

	internal int biHeight;

	internal ushort biPlanes;

	internal ushort biBitCount;

	internal uint biCompression;

	internal uint biSizeImage;

	internal int biXPelsPerMeter;

	internal int biYPelsPerMeter;

	internal uint biClrUsed;

	internal uint biClrImportant;
}
