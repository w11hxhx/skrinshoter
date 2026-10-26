using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_INPUT_ELEMENT_DESC
{
	internal PCSTR SemanticName;

	internal uint SemanticIndex;

	internal DXGI_FORMAT Format;

	internal uint InputSlot;

	internal uint AlignedByteOffset;

	internal D3D11_INPUT_CLASSIFICATION InputSlotClass;

	internal uint InstanceDataStepRate;
}
