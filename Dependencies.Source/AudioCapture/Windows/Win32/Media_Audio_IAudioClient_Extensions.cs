using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IAudioClient_Extensions
{
	internal unsafe static HRESULT Initialize(this IAudioClient.Interface @this, AUDCLNT_SHAREMODE ShareMode, uint StreamFlags, long hnsBufferDuration, long hnsPeriodicity, in WAVEFORMATEX pFormat, Guid? AudioSessionGuid)
	{
		fixed (WAVEFORMATEX* pFormat2 = &pFormat)
		{
			Guid valueOrDefault = AudioSessionGuid.GetValueOrDefault();
			return @this.Initialize(ShareMode, StreamFlags, hnsBufferDuration, hnsPeriodicity, pFormat2, AudioSessionGuid.HasValue ? (&valueOrDefault) : null);
		}
	}

	internal unsafe static HRESULT GetBufferSize(this IAudioClient.Interface @this, out uint pNumBufferFrames)
	{
		fixed (uint* pNumBufferFrames2 = &pNumBufferFrames)
		{
			return @this.GetBufferSize(pNumBufferFrames2);
		}
	}

	internal unsafe static HRESULT GetStreamLatency(this IAudioClient.Interface @this, out long phnsLatency)
	{
		fixed (long* phnsLatency2 = &phnsLatency)
		{
			return @this.GetStreamLatency(phnsLatency2);
		}
	}

	internal unsafe static HRESULT GetCurrentPadding(this IAudioClient.Interface @this, out uint pNumPaddingFrames)
	{
		fixed (uint* pNumPaddingFrames2 = &pNumPaddingFrames)
		{
			return @this.GetCurrentPadding(pNumPaddingFrames2);
		}
	}

	internal unsafe static HRESULT IsFormatSupported(this IAudioClient.Interface @this, AUDCLNT_SHAREMODE ShareMode, in WAVEFORMATEX pFormat, WAVEFORMATEX** ppClosestMatch)
	{
		fixed (WAVEFORMATEX* pFormat2 = &pFormat)
		{
			return @this.IsFormatSupported(ShareMode, pFormat2, ppClosestMatch);
		}
	}

	internal unsafe static HRESULT GetMixFormat(this IAudioClient.Interface @this, out WAVEFORMATEX* ppDeviceFormat)
	{
		fixed (WAVEFORMATEX** ppDeviceFormat2 = &ppDeviceFormat)
		{
			return @this.GetMixFormat(ppDeviceFormat2);
		}
	}

	internal static HRESULT SetEventHandle(this IAudioClient.Interface @this, SafeHandle eventHandle)
	{
		bool success = false;
		try
		{
			if (eventHandle != null)
			{
				eventHandle.DangerousAddRef(ref success);
				HANDLE eventHandle2 = (HANDLE)eventHandle.DangerousGetHandle();
				return @this.SetEventHandle(eventHandle2);
			}
			throw new ArgumentNullException("eventHandle");
		}
		finally
		{
			if (success)
			{
				eventHandle.DangerousRelease();
			}
		}
	}

	internal unsafe static HRESULT GetService(this IAudioClient.Interface @this, in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetService(riid2, ppv2);
			}
		}
	}
}
