using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_Audio_IMMDevice_Extensions
{
	internal unsafe static HRESULT Activate(this IMMDevice.Interface @this, in Guid iid, CLSCTX dwClsCtx, PROPVARIANT? pActivationParams, out void* ppInterface)
	{
		fixed (void** ppInterface2 = &ppInterface)
		{
			fixed (Guid* iid2 = &iid)
			{
				PROPVARIANT valueOrDefault = pActivationParams.GetValueOrDefault();
				return @this.Activate(iid2, dwClsCtx, pActivationParams.HasValue ? (&valueOrDefault) : null, ppInterface2);
			}
		}
	}

	internal unsafe static HRESULT GetId(this IMMDevice.Interface @this, out PWSTR ppstrId)
	{
		fixed (PWSTR* ppstrId2 = &ppstrId)
		{
			return @this.GetId(ppstrId2);
		}
	}

	internal unsafe static HRESULT GetState(this IMMDevice.Interface @this, out DEVICE_STATE pdwState)
	{
		fixed (DEVICE_STATE* pdwState2 = &pdwState)
		{
			return @this.GetState(pdwState2);
		}
	}
}
