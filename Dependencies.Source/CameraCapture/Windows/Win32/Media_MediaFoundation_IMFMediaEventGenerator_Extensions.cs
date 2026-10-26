using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFMediaEventGenerator_Extensions
{
	internal unsafe static HRESULT QueueEvent(this IMFMediaEventGenerator.Interface @this, uint met, in Guid guidExtendedType, HRESULT hrStatus, in PROPVARIANT pvValue)
	{
		fixed (PROPVARIANT* pvValue2 = &pvValue)
		{
			fixed (Guid* guidExtendedType2 = &guidExtendedType)
			{
				return @this.QueueEvent(met, guidExtendedType2, hrStatus, pvValue2);
			}
		}
	}
}
