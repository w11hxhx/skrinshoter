using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IMMDeviceEnumerator_Extensions
{
	internal unsafe static HRESULT GetDevice(this IMMDeviceEnumerator.Interface @this, string pwstrId, IMMDevice** ppDevice)
	{
		fixed (char* ptr = pwstrId)
		{
			return @this.GetDevice(ptr, ppDevice);
		}
	}
}
