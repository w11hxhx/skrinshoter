using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFPresentationDescriptor_Extensions
{
	internal unsafe static HRESULT GetItem(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, PROPVARIANT* pValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.GetItem(guidKey2, pValue);
		}
	}

	internal unsafe static HRESULT GetItemType(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out MF_ATTRIBUTE_TYPE pType)
	{
		fixed (MF_ATTRIBUTE_TYPE* pType2 = &pType)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetItemType(guidKey2, pType2);
			}
		}
	}

	internal unsafe static HRESULT CompareItem(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, in PROPVARIANT Value, out BOOL pbResult)
	{
		fixed (BOOL* pbResult2 = &pbResult)
		{
			fixed (PROPVARIANT* value = &Value)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return @this.CompareItem(guidKey2, value, pbResult2);
				}
			}
		}
	}

	internal unsafe static HRESULT Compare(this IMFPresentationDescriptor.Interface @this, IMFAttributes* pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out BOOL pbResult)
	{
		fixed (BOOL* pbResult2 = &pbResult)
		{
			return @this.Compare(pTheirs, MatchType, pbResult2);
		}
	}

	internal unsafe static HRESULT GetUINT32(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out uint punValue)
	{
		fixed (uint* punValue2 = &punValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetUINT32(guidKey2, punValue2);
			}
		}
	}

	internal unsafe static HRESULT GetUINT64(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out ulong punValue)
	{
		fixed (ulong* punValue2 = &punValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetUINT64(guidKey2, punValue2);
			}
		}
	}

	internal unsafe static HRESULT GetDouble(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out double pfValue)
	{
		fixed (double* pfValue2 = &pfValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetDouble(guidKey2, pfValue2);
			}
		}
	}

	internal unsafe static HRESULT GetGUID(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out Guid pguidValue)
	{
		fixed (Guid* pguidValue2 = &pguidValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetGUID(guidKey2, pguidValue2);
			}
		}
	}

	internal unsafe static HRESULT GetStringLength(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out uint pcchLength)
	{
		fixed (uint* pcchLength2 = &pcchLength)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetStringLength(guidKey2, pcchLength2);
			}
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT GetString(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, Span<char> pwszValue, uint* pcchLength)
	{
		fixed (char* ptr = pwszValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetString(guidKey2, ptr, (uint)pwszValue.Length, pcchLength);
			}
		}
	}

	internal unsafe static HRESULT GetAllocatedString(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out PWSTR ppwszValue, out uint pcchLength)
	{
		fixed (uint* pcchLength2 = &pcchLength)
		{
			fixed (PWSTR* ppwszValue2 = &ppwszValue)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return @this.GetAllocatedString(guidKey2, ppwszValue2, pcchLength2);
				}
			}
		}
	}

	internal unsafe static HRESULT GetBlobSize(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, out uint pcbBlobSize)
	{
		fixed (uint* pcbBlobSize2 = &pcbBlobSize)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetBlobSize(guidKey2, pcbBlobSize2);
			}
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT GetBlob(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, Span<byte> pBuf, uint* pcbBlobSize)
	{
		fixed (byte* pBuf2 = pBuf)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetBlob(guidKey2, pBuf2, (uint)pBuf.Length, pcbBlobSize);
			}
		}
	}

	internal unsafe static HRESULT GetAllocatedBlob(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, byte** ppBuf, out uint pcbSize)
	{
		fixed (uint* pcbSize2 = &pcbSize)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.GetAllocatedBlob(guidKey2, ppBuf, pcbSize2);
			}
		}
	}

	internal unsafe static HRESULT GetUnknown(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return @this.GetUnknown(guidKey2, riid2, ppv2);
				}
			}
		}
	}

	internal unsafe static HRESULT SetItem(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, in PROPVARIANT Value)
	{
		fixed (PROPVARIANT* value = &Value)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.SetItem(guidKey2, value);
			}
		}
	}

	internal unsafe static HRESULT DeleteItem(this IMFPresentationDescriptor.Interface @this, in Guid guidKey)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.DeleteItem(guidKey2);
		}
	}

	internal unsafe static HRESULT SetUINT32(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, uint unValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.SetUINT32(guidKey2, unValue);
		}
	}

	internal unsafe static HRESULT SetUINT64(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, ulong unValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.SetUINT64(guidKey2, unValue);
		}
	}

	internal unsafe static HRESULT SetDouble(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, double fValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.SetDouble(guidKey2, fValue);
		}
	}

	internal unsafe static HRESULT SetGUID(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, in Guid guidValue)
	{
		fixed (Guid* guidValue2 = &guidValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.SetGUID(guidKey2, guidValue2);
			}
		}
	}

	internal unsafe static HRESULT SetString(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, string wszValue)
	{
		fixed (char* ptr = wszValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.SetString(guidKey2, ptr);
			}
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT SetBlob(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, ReadOnlySpan<byte> pBuf)
	{
		fixed (byte* pBuf2 = pBuf)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return @this.SetBlob(guidKey2, pBuf2, (uint)pBuf.Length);
			}
		}
	}

	internal unsafe static HRESULT SetUnknown(this IMFPresentationDescriptor.Interface @this, in Guid guidKey, IUnknown* pUnknown)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return @this.SetUnknown(guidKey2, pUnknown);
		}
	}

	internal unsafe static HRESULT GetCount(this IMFPresentationDescriptor.Interface @this, out uint pcItems)
	{
		fixed (uint* pcItems2 = &pcItems)
		{
			return @this.GetCount(pcItems2);
		}
	}

	internal unsafe static HRESULT GetItemByIndex(this IMFPresentationDescriptor.Interface @this, uint unIndex, out Guid pguidKey, PROPVARIANT* pValue)
	{
		fixed (Guid* pguidKey2 = &pguidKey)
		{
			return @this.GetItemByIndex(unIndex, pguidKey2, pValue);
		}
	}

	internal unsafe static HRESULT GetStreamDescriptorCount(this IMFPresentationDescriptor.Interface @this, out uint pdwDescriptorCount)
	{
		fixed (uint* pdwDescriptorCount2 = &pdwDescriptorCount)
		{
			return @this.GetStreamDescriptorCount(pdwDescriptorCount2);
		}
	}

	internal unsafe static HRESULT GetStreamDescriptorByIndex(this IMFPresentationDescriptor.Interface @this, uint dwIndex, out BOOL pfSelected, IMFStreamDescriptor** ppDescriptor)
	{
		fixed (BOOL* pfSelected2 = &pfSelected)
		{
			return @this.GetStreamDescriptorByIndex(dwIndex, pfSelected2, ppDescriptor);
		}
	}
}
