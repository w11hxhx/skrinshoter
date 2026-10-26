using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Direct3D11_ID3D11ClassInstance_Extensions
{
	internal unsafe static HRESULT GetPrivateData(this ID3D11ClassInstance.Interface @this, in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return @this.GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT SetPrivateData(this ID3D11ClassInstance.Interface @this, in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateData(guid2, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this ID3D11ClassInstance.Interface @this, in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateDataInterface(guid2, pData);
		}
	}

	internal unsafe static void GetDesc(this ID3D11ClassInstance.Interface @this, out D3D11_CLASS_INSTANCE_DESC pDesc)
	{
		fixed (D3D11_CLASS_INSTANCE_DESC* pDesc2 = &pDesc)
		{
			@this.GetDesc(pDesc2);
		}
	}

	internal unsafe static void GetInstanceName(this ID3D11ClassInstance.Interface @this, PSTR pInstanceName, ref nuint pBufferLength)
	{
		fixed (UIntPtr* pBufferLength2 = &pBufferLength)
		{
			@this.GetInstanceName(pInstanceName, pBufferLength2);
		}
	}

	internal unsafe static void GetTypeName(this ID3D11ClassInstance.Interface @this, PSTR pTypeName, ref nuint pBufferLength)
	{
		fixed (UIntPtr* pBufferLength2 = &pBufferLength)
		{
			@this.GetTypeName(pTypeName, pBufferLength2);
		}
	}
}
