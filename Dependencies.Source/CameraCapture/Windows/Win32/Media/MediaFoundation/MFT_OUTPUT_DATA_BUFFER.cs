using System.CodeDom.Compiler;

namespace Windows.Win32.Media.MediaFoundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct MFT_OUTPUT_DATA_BUFFER
{
	internal uint dwStreamID;

	internal unsafe IMFSample* pSample;

	internal uint dwStatus;

	internal unsafe IMFCollection* pEvents;
}
