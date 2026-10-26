using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct BITMAPINFO
{
	internal BITMAPINFOHEADER bmiHeader;

	internal VariableLengthInlineArray<RGBQUAD> bmiColors;

	internal unsafe static int SizeOf(int count)
	{
		int num = sizeof(BITMAPINFO);
		if (count > 1)
		{
			num += checked((count - 1) * sizeof(RGBQUAD));
		}
		else if (count < 0)
		{
			throw new ArgumentOutOfRangeException();
		}
		return num;
	}
}
