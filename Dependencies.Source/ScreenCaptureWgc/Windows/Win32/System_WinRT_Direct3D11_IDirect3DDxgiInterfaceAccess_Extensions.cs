using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.WinRT.Direct3D11;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_WinRT_Direct3D11_IDirect3DDxgiInterfaceAccess_Extensions
{
	internal unsafe static HRESULT GetInterface(this IDirect3DDxgiInterfaceAccess.Interface @this, in Guid iid, out void* p)
	{
		fixed (void** p2 = &p)
		{
			fixed (Guid* iid2 = &iid)
			{
				return @this.GetInterface(iid2, p2);
			}
		}
	}
}
