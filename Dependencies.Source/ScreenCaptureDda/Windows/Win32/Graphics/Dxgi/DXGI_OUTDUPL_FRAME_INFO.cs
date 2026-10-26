using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_OUTDUPL_FRAME_INFO
{
	internal long LastPresentTime;

	internal long LastMouseUpdateTime;

	internal uint AccumulatedFrames;

	internal BOOL RectsCoalesced;

	internal BOOL ProtectedContentMaskedOut;

	internal DXGI_OUTDUPL_POINTER_POSITION PointerPosition;

	internal uint TotalMetadataBufferSize;

	internal uint PointerShapeBufferSize;
}
