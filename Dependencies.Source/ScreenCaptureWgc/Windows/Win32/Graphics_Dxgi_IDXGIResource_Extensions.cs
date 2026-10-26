using System;
using System.CodeDom.Compiler;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIResource_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIResource.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIResource.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIResource.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIResource.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetDevice(this IDXGIResource.Interface @this, in Guid riid, out void* ppDevice)
	{
		fixed (void** ppDevice2 = &ppDevice)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetDevice(riid2, ppDevice2);
			}
		}
	}

	internal unsafe static HRESULT GetSharedHandle(this IDXGIResource.Interface @this, out SafeFileHandle pSharedHandle)
	{
		HANDLE hANDLE = default;
		HRESULT sharedHandle = @this.GetSharedHandle(&hANDLE);
		pSharedHandle = new SafeFileHandle(hANDLE, ownsHandle: true);
		return sharedHandle;
	}

	internal unsafe static HRESULT GetUsage(this IDXGIResource.Interface @this, out DXGI_USAGE pUsage)
	{
		fixed (DXGI_USAGE* pUsage2 = &pUsage)
		{
			return @this.GetUsage(pUsage2);
		}
	}

	internal unsafe static HRESULT GetEvictionPriority(this IDXGIResource.Interface @this, out DXGI_RESOURCE_PRIORITY pEvictionPriority)
	{
		fixed (DXGI_RESOURCE_PRIORITY* pEvictionPriority2 = &pEvictionPriority)
		{
			return @this.GetEvictionPriority(pEvictionPriority2);
		}
	}
}
