using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFMediaTypeHandler_Extensions
{
	internal unsafe static HRESULT GetMediaTypeCount(this IMFMediaTypeHandler.Interface @this, out uint pdwTypeCount)
	{
		fixed (uint* pdwTypeCount2 = &pdwTypeCount)
		{
			return @this.GetMediaTypeCount(pdwTypeCount2);
		}
	}

	internal unsafe static HRESULT GetMajorType(this IMFMediaTypeHandler.Interface @this, out Guid pguidMajorType)
	{
		fixed (Guid* pguidMajorType2 = &pguidMajorType)
		{
			return @this.GetMajorType(pguidMajorType2);
		}
	}
}
