using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIDevice_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIDevice.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIDevice.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIDevice.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIDevice.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT CreateSurface(this IDXGIDevice.Interface @this, in DXGI_SURFACE_DESC pDesc, uint NumSurfaces, DXGI_USAGE Usage, DXGI_SHARED_RESOURCE? pSharedResource, IDXGISurface** ppSurface)
	{
		fixed (DXGI_SURFACE_DESC* pDesc2 = &pDesc)
		{
			DXGI_SHARED_RESOURCE valueOrDefault = pSharedResource.GetValueOrDefault();
			return @this.CreateSurface(pDesc2, NumSurfaces, Usage, pSharedResource.HasValue ? (&valueOrDefault) : null, ppSurface);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT QueryResourceResidency(this IDXGIDevice.Interface @this, IUnknown** ppResources, Span<DXGI_RESIDENCY> pResidencyStatus)
	{
		fixed (DXGI_RESIDENCY* pResidencyStatus2 = pResidencyStatus)
		{
			return @this.QueryResourceResidency(ppResources, pResidencyStatus2, (uint)pResidencyStatus.Length);
		}
	}

	internal unsafe static HRESULT GetGPUThreadPriority(this IDXGIDevice.Interface @this, out int pPriority)
	{
		fixed (int* pPriority2 = &pPriority)
		{
			return @this.GetGPUThreadPriority(pPriority2);
		}
	}
}
