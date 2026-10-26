using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIFactory1_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIFactory1.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIFactory1.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIFactory1.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIFactory1.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetWindowAssociation(this IDXGIFactory1.Interface @this, out HWND pWindowHandle)
	{
		fixed (HWND* pWindowHandle2 = &pWindowHandle)
		{
			return @this.GetWindowAssociation(pWindowHandle2);
		}
	}

	internal unsafe static HRESULT CreateSwapChain(this IDXGIFactory1.Interface @this, IUnknown* pDevice, in DXGI_SWAP_CHAIN_DESC pDesc, IDXGISwapChain** ppSwapChain)
	{
		fixed (DXGI_SWAP_CHAIN_DESC* pDesc2 = &pDesc)
		{
			return @this.CreateSwapChain(pDevice, pDesc2, ppSwapChain);
		}
	}

	internal unsafe static HRESULT CreateSoftwareAdapter(this IDXGIFactory1.Interface @this, SafeHandle Module, IDXGIAdapter** ppAdapter)
	{
		bool success = false;
		try
		{
			if (Module != null)
			{
				Module.DangerousAddRef(ref success);
				HMODULE module = (HMODULE)Module.DangerousGetHandle();
				return @this.CreateSoftwareAdapter(module, ppAdapter);
			}
			throw new ArgumentNullException("Module");
		}
		finally
		{
			if (success)
			{
				Module.DangerousRelease();
			}
		}
	}
}
