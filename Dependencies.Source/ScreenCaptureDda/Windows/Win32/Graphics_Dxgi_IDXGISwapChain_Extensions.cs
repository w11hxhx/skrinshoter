using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGISwapChain_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGISwapChain.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGISwapChain.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGISwapChain.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGISwapChain.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetDevice(this IDXGISwapChain.Interface @this, in Guid riid, out void* ppDevice)
	{
		fixed (void** ppDevice2 = &ppDevice)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetDevice(riid2, ppDevice2);
			}
		}
	}

	internal unsafe static HRESULT GetBuffer(this IDXGISwapChain.Interface @this, uint Buffer, in Guid riid, out void* ppSurface)
	{
		fixed (void** ppSurface2 = &ppSurface)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetBuffer(Buffer, riid2, ppSurface2);
			}
		}
	}

	internal unsafe static HRESULT GetDesc(this IDXGISwapChain.Interface @this, out DXGI_SWAP_CHAIN_DESC pDesc)
	{
		fixed (DXGI_SWAP_CHAIN_DESC* pDesc2 = &pDesc)
		{
			return @this.GetDesc(pDesc2);
		}
	}

	internal unsafe static HRESULT ResizeTarget(this IDXGISwapChain.Interface @this, in DXGI_MODE_DESC pNewTargetParameters)
	{
		fixed (DXGI_MODE_DESC* pNewTargetParameters2 = &pNewTargetParameters)
		{
			return @this.ResizeTarget(pNewTargetParameters2);
		}
	}

	internal unsafe static HRESULT GetFrameStatistics(this IDXGISwapChain.Interface @this, out DXGI_FRAME_STATISTICS pStats)
	{
		fixed (DXGI_FRAME_STATISTICS* pStats2 = &pStats)
		{
			return @this.GetFrameStatistics(pStats2);
		}
	}

	internal unsafe static HRESULT GetLastPresentCount(this IDXGISwapChain.Interface @this, out uint pLastPresentCount)
	{
		fixed (uint* pLastPresentCount2 = &pLastPresentCount)
		{
			return @this.GetLastPresentCount(pLastPresentCount2);
		}
	}
}
