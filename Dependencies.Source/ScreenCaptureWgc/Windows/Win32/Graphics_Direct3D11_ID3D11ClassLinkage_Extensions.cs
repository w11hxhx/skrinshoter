using System;
using System.CodeDom.Compiler;
using System.Text;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Direct3D11_ID3D11ClassLinkage_Extensions
{
	internal unsafe static HRESULT GetPrivateData(this ID3D11ClassLinkage.Interface @this, in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return @this.GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT SetPrivateData(this ID3D11ClassLinkage.Interface @this, in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateData(guid2, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this ID3D11ClassLinkage.Interface @this, in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateDataInterface(guid2, pData);
		}
	}

	internal unsafe static HRESULT GetClassInstance(this ID3D11ClassLinkage.Interface @this, string pClassInstanceName, uint InstanceIndex, ID3D11ClassInstance** ppInstance)
	{
		fixed (byte* value = ((pClassInstanceName != null) ? Encoding.Default.GetBytes(pClassInstanceName) : null))
		{
			return @this.GetClassInstance(new PCSTR(value), InstanceIndex, ppInstance);
		}
	}

	internal unsafe static HRESULT CreateClassInstance(this ID3D11ClassLinkage.Interface @this, string pClassTypeName, uint ConstantBufferOffset, uint ConstantVectorOffset, uint TextureOffset, uint SamplerOffset, ID3D11ClassInstance** ppInstance)
	{
		fixed (byte* value = ((pClassTypeName != null) ? Encoding.Default.GetBytes(pClassTypeName) : null))
		{
			return @this.CreateClassInstance(new PCSTR(value), ConstantBufferOffset, ConstantVectorOffset, TextureOffset, SamplerOffset, ppInstance);
		}
	}
}
