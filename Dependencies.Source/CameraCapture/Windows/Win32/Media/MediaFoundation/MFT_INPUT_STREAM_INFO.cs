using System.CodeDom.Compiler;

namespace Windows.Win32.Media.MediaFoundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct MFT_INPUT_STREAM_INFO
{
	internal long hnsMaxLatency;

	internal uint dwFlags;

	internal uint cbSize;

	internal uint cbMaxLookahead;

	internal uint cbAlignment;
}
