using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIAdapter1_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIAdapter1.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIAdapter1.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIAdapter1.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIAdapter1.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetDesc(this IDXGIAdapter1.Interface @this, out DXGI_ADAPTER_DESC pDesc)
	{
		fixed (DXGI_ADAPTER_DESC* pDesc2 = &pDesc)
		{
			return @this.GetDesc(pDesc2);
		}
	}

	internal unsafe static HRESULT CheckInterfaceSupport(this IDXGIAdapter1.Interface @this, in Guid InterfaceName, out long pUMDVersion)
	{
		fixed (long* pUMDVersion2 = &pUMDVersion)
		{
			fixed (Guid* interfaceName = &InterfaceName)
			{
				return @this.CheckInterfaceSupport(interfaceName, pUMDVersion2);
			}
		}
	}

	internal unsafe static HRESULT GetDesc1(this IDXGIAdapter1.Interface @this, out DXGI_ADAPTER_DESC1 pDesc)
	{
		fixed (DXGI_ADAPTER_DESC1* pDesc2 = &pDesc)
		{
			return @this.GetDesc1(pDesc2);
		}
	}
}
