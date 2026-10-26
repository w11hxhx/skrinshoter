using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_SO_DECLARATION_ENTRY
{
	internal uint Stream;

	internal PCSTR SemanticName;

	internal uint SemanticIndex;

	internal byte StartComponent;

	internal byte ComponentCount;

	internal byte OutputSlot;
}
