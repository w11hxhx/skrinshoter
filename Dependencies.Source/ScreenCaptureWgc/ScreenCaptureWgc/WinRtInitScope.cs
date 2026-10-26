using System.Threading;
using Common;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.WinRT;

namespace ScreenCaptureWgc;

internal sealed class WinRtInitScope : BaseDisposable
{
	private readonly bool needUninit;

	public WinRtInitScope()
		: this((Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) ? RO_INIT_TYPE.RO_INIT_MULTITHREADED : RO_INIT_TYPE.RO_INIT_SINGLETHREADED)
	{
	}

	public WinRtInitScope(RO_INIT_TYPE initType)
	{
		HRESULT hRESULT = PInvoke.RoInitialize(initType);
		if (hRESULT == HRESULT.S_OK || hRESULT == HRESULT.S_FALSE)
		{
			needUninit = true;
		}
		else if (hRESULT == HRESULT.RPC_E_CHANGED_MODE)
		{
			needUninit = false;
		}
		else
		{
			hRESULT.ThrowOnFailure($"RoInitialize({initType})");
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (needUninit)
		{
			PInvoke.RoUninitialize();
		}
	}
}
