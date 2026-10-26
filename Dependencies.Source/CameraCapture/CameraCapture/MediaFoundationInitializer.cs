using System;
using Common;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace CameraCapture;

public class MediaFoundationInitializer : BaseDisposable
{
	public MediaFoundationInitializer()
	{
		ThreadEx.RunInMTA(() =>
		{
			PInvoke.MFStartup(131184u, 1u).ThrowOnFailure("MFStartup()");
		});
	}

	protected override void Dispose(bool disposing)
	{
		ThreadEx.RunInMTA((Func<HRESULT>)PInvoke.MFShutdown);
	}
}
