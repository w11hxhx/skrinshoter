using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFSourceReaderEx_Extensions
{
	internal unsafe static HRESULT GetStreamSelection(this IMFSourceReaderEx.Interface @this, uint dwStreamIndex, out BOOL pfSelected)
	{
		fixed (BOOL* pfSelected2 = &pfSelected)
		{
			return @this.GetStreamSelection(dwStreamIndex, pfSelected2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT SetCurrentMediaType(this IMFSourceReaderEx.Interface @this, uint dwStreamIndex, IMFMediaType* pMediaType)
	{
		return @this.SetCurrentMediaType(dwStreamIndex, null, pMediaType);
	}

	internal unsafe static HRESULT SetCurrentPosition(this IMFSourceReaderEx.Interface @this, in Guid guidTimeFormat, in PROPVARIANT varPosition)
	{
		fixed (PROPVARIANT* varPosition2 = &varPosition)
		{
			fixed (Guid* guidTimeFormat2 = &guidTimeFormat)
			{
				return @this.SetCurrentPosition(guidTimeFormat2, varPosition2);
			}
		}
	}

	internal unsafe static HRESULT GetServiceForStream(this IMFSourceReaderEx.Interface @this, uint dwStreamIndex, in Guid guidService, in Guid riid, out void* ppvObject)
	{
		fixed (void** ppvObject2 = &ppvObject)
		{
			fixed (Guid* riid2 = &riid)
			{
				fixed (Guid* guidService2 = &guidService)
				{
					return @this.GetServiceForStream(dwStreamIndex, guidService2, riid2, ppvObject2);
				}
			}
		}
	}

	internal unsafe static HRESULT GetPresentationAttribute(this IMFSourceReaderEx.Interface @this, uint dwStreamIndex, in Guid guidAttribute, out PROPVARIANT pvarAttribute)
	{
		fixed (PROPVARIANT* pvarAttribute2 = &pvarAttribute)
		{
			fixed (Guid* guidAttribute2 = &guidAttribute)
			{
				return @this.GetPresentationAttribute(dwStreamIndex, guidAttribute2, pvarAttribute2);
			}
		}
	}

	internal unsafe static HRESULT SetNativeMediaType(this IMFSourceReaderEx.Interface @this, uint dwStreamIndex, IMFMediaType* pMediaType, out uint pdwStreamFlags)
	{
		fixed (uint* pdwStreamFlags2 = &pdwStreamFlags)
		{
			return @this.SetNativeMediaType(dwStreamIndex, pMediaType, pdwStreamFlags2);
		}
	}
}
