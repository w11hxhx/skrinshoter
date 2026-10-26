using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.Media.Audio;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct WAVEFORMATEX
{
	internal ushort wFormatTag;

	internal ushort nChannels;

	internal uint nSamplesPerSec;

	internal uint nAvgBytesPerSec;

	internal ushort nBlockAlign;

	internal ushort wBitsPerSample;

	internal ushort cbSize;
}
