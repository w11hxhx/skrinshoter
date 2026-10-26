using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGISurface_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGISurface.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGISurface.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGISurface.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGISurface.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetDevice(this IDXGISurface.Interface @this, in Guid riid, out void* ppDevice)
	{
		fixed (void** ppDevice2 = &ppDevice)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetDevice(riid2, ppDevice2);
			}
		}
	}

	internal unsafe static HRESULT GetDesc(this IDXGISurface.Interface @this, out DXGI_SURFACE_DESC pDesc)
	{
		fixed (DXGI_SURFACE_DESC* pDesc2 = &pDesc)
		{
			return @this.GetDesc(pDesc2);
		}
	}

	internal unsafe static HRESULT Map(this IDXGISurface.Interface @this, out DXGI_MAPPED_RECT pLockedRect, DXGI_MAP_FLAGS MapFlags)
	{
		fixed (DXGI_MAPPED_RECT* pLockedRect2 = &pLockedRect)
		{
			return @this.Map(pLockedRect2, MapFlags);
		}
	}
}
