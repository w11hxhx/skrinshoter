using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IAudioCaptureClient_Extensions
{
	internal unsafe static HRESULT GetBuffer(this IAudioCaptureClient.Interface @this, out byte* ppData, out uint pNumFramesToRead, out uint pdwFlags, ulong* pu64DevicePosition, ulong* pu64QPCPosition)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			fixed (uint* pNumFramesToRead2 = &pNumFramesToRead)
			{
				fixed (byte** ppData2 = &ppData)
				{
					return @this.GetBuffer(ppData2, pNumFramesToRead2, pdwFlags2, pu64DevicePosition, pu64QPCPosition);
				}
			}
		}
	}

	internal unsafe static HRESULT GetNextPacketSize(this IAudioCaptureClient.Interface @this, out uint pNumFramesInNextPacket)
	{
		fixed (uint* pNumFramesInNextPacket2 = &pNumFramesInNextPacket)
		{
			return @this.GetNextPacketSize(pNumFramesInNextPacket2);
		}
	}
}
