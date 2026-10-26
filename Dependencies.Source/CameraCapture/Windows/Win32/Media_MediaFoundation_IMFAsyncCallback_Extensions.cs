using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFAsyncCallback_Extensions
{
	internal unsafe static HRESULT GetParameters(this IMFAsyncCallback.Interface @this, out uint pdwFlags, out uint pdwQueue)
	{
		fixed (uint* pdwQueue2 = &pdwQueue)
		{
			fixed (uint* pdwFlags2 = &pdwFlags)
			{
				return @this.GetParameters(pdwFlags2, pdwQueue2);
			}
		}
	}
}
