using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IMMEndpoint_Extensions
{
	internal unsafe static HRESULT GetDataFlow(this IMMEndpoint.Interface @this, out EDataFlow pDataFlow)
	{
		fixed (EDataFlow* pDataFlow2 = &pDataFlow)
		{
			return @this.GetDataFlow(pDataFlow2);
		}
	}
}
