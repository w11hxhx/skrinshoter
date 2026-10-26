using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Direct3D11_ID3D11ShaderResourceView_Extensions
{
	internal unsafe static HRESULT GetPrivateData(this ID3D11ShaderResourceView.Interface @this, in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return @this.GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT SetPrivateData(this ID3D11ShaderResourceView.Interface @this, in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateData(guid2, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this ID3D11ShaderResourceView.Interface @this, in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateDataInterface(guid2, pData);
		}
	}

	internal unsafe static void GetDesc(this ID3D11ShaderResourceView.Interface @this, out D3D11_SHADER_RESOURCE_VIEW_DESC pDesc)
	{
		fixed (D3D11_SHADER_RESOURCE_VIEW_DESC* pDesc2 = &pDesc)
		{
			@this.GetDesc(pDesc2);
		}
	}
}
