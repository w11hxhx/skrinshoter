using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFCollection_Extensions
{
	internal unsafe static HRESULT GetElementCount(this IMFCollection.Interface @this, out uint pcElements)
	{
		fixed (uint* pcElements2 = &pcElements)
		{
			return @this.GetElementCount(pcElements2);
		}
	}
}
