using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIOutput_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIOutput.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIOutput.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIOutput.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIOutput.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static HRESULT GetDesc(this IDXGIOutput.Interface @this, out DXGI_OUTPUT_DESC pDesc)
	{
		fixed (DXGI_OUTPUT_DESC* pDesc2 = &pDesc)
		{
			return @this.GetDesc(pDesc2);
		}
	}

	internal unsafe static HRESULT GetDisplayModeList(this IDXGIOutput.Interface @this, DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, ref uint pNumModes, Span<DXGI_MODE_DESC> pDesc)
	{
		fixed (DXGI_MODE_DESC* pDesc2 = pDesc)
		{
			fixed (uint* pNumModes2 = &pNumModes)
			{
				return @this.GetDisplayModeList(EnumFormat, Flags, pNumModes2, pDesc2);
			}
		}
	}

	internal unsafe static HRESULT FindClosestMatchingMode(this IDXGIOutput.Interface @this, in DXGI_MODE_DESC pModeToMatch, out DXGI_MODE_DESC pClosestMatch, IUnknown* pConcernedDevice)
	{
		fixed (DXGI_MODE_DESC* pClosestMatch2 = &pClosestMatch)
		{
			fixed (DXGI_MODE_DESC* pModeToMatch2 = &pModeToMatch)
			{
				return @this.FindClosestMatchingMode(pModeToMatch2, pClosestMatch2, pConcernedDevice);
			}
		}
	}

	internal unsafe static HRESULT GetGammaControlCapabilities(this IDXGIOutput.Interface @this, out DXGI_GAMMA_CONTROL_CAPABILITIES pGammaCaps)
	{
		fixed (DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps2 = &pGammaCaps)
		{
			return @this.GetGammaControlCapabilities(pGammaCaps2);
		}
	}

	internal unsafe static HRESULT SetGammaControl(this IDXGIOutput.Interface @this, in DXGI_GAMMA_CONTROL pArray)
	{
		fixed (DXGI_GAMMA_CONTROL* gammaControl = &pArray)
		{
			return @this.SetGammaControl(gammaControl);
		}
	}

	internal unsafe static HRESULT GetGammaControl(this IDXGIOutput.Interface @this, out DXGI_GAMMA_CONTROL pArray)
	{
		fixed (DXGI_GAMMA_CONTROL* pArray2 = &pArray)
		{
			return @this.GetGammaControl(pArray2);
		}
	}

	internal unsafe static HRESULT GetFrameStatistics(this IDXGIOutput.Interface @this, out DXGI_FRAME_STATISTICS pStats)
	{
		fixed (DXGI_FRAME_STATISTICS* pStats2 = &pStats)
		{
			return @this.GetFrameStatistics(pStats2);
		}
	}
}
