using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFMediaSource_Extensions
{
	internal unsafe static HRESULT QueueEvent(this IMFMediaSource.Interface @this, uint met, in Guid guidExtendedType, HRESULT hrStatus, in PROPVARIANT pvValue)
	{
		fixed (PROPVARIANT* pvValue2 = &pvValue)
		{
			fixed (Guid* guidExtendedType2 = &guidExtendedType)
			{
				return @this.QueueEvent(met, guidExtendedType2, hrStatus, pvValue2);
			}
		}
	}

	internal unsafe static HRESULT GetCharacteristics(this IMFMediaSource.Interface @this, out uint pdwCharacteristics)
	{
		fixed (uint* pdwCharacteristics2 = &pdwCharacteristics)
		{
			return @this.GetCharacteristics(pdwCharacteristics2);
		}
	}

	internal unsafe static HRESULT Start(this IMFMediaSource.Interface @this, IMFPresentationDescriptor* pPresentationDescriptor, in Guid pguidTimeFormat, in PROPVARIANT pvarStartPosition)
	{
		fixed (PROPVARIANT* pvarStartPosition2 = &pvarStartPosition)
		{
			fixed (Guid* pguidTimeFormat2 = &pguidTimeFormat)
			{
				return @this.Start(pPresentationDescriptor, pguidTimeFormat2, pvarStartPosition2);
			}
		}
	}
}
