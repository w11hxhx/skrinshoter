using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IMMDeviceCollection_Extensions
{
	internal unsafe static HRESULT GetCount(this IMMDeviceCollection.Interface @this, out uint pcDevices)
	{
		fixed (uint* pcDevices2 = &pcDevices)
		{
			return @this.GetCount(pcDevices2);
		}
	}
}
