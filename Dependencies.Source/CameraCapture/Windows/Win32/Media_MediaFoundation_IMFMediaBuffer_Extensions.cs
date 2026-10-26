using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFMediaBuffer_Extensions
{
	internal unsafe static HRESULT Lock(this IMFMediaBuffer.Interface @this, out byte* ppbBuffer, uint* pcbMaxLength, uint* pcbCurrentLength)
	{
		fixed (byte** ppbBuffer2 = &ppbBuffer)
		{
			return @this.Lock(ppbBuffer2, pcbMaxLength, pcbCurrentLength);
		}
	}

	internal unsafe static HRESULT GetCurrentLength(this IMFMediaBuffer.Interface @this, out uint pcbCurrentLength)
	{
		fixed (uint* pcbCurrentLength2 = &pcbCurrentLength)
		{
			return @this.GetCurrentLength(pcbCurrentLength2);
		}
	}

	internal unsafe static HRESULT GetMaxLength(this IMFMediaBuffer.Interface @this, out uint pcbMaxLength)
	{
		fixed (uint* pcbMaxLength2 = &pcbMaxLength)
		{
			return @this.GetMaxLength(pcbMaxLength2);
		}
	}
}
