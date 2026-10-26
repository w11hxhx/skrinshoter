using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("7FEE9E9A-4A89-47A6-899C-B6A53A70FB67")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFActivate
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, HRESULT> GetItem_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, MF_ATTRIBUTE_TYPE*, HRESULT> GetItemType_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, BOOL*, HRESULT> CompareItem_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, IMFAttributes*, MF_ATTRIBUTES_MATCH_TYPE, BOOL*, HRESULT> Compare_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT> GetUINT32_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, ulong*, HRESULT> GetUINT64_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, double*, HRESULT> GetDouble_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, HRESULT> GetGUID_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT> GetStringLength_12;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PWSTR, uint, uint*, HRESULT> GetString_13;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PWSTR*, uint*, HRESULT> GetAllocatedString_14;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT> GetBlobSize_15;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte*, uint, uint*, HRESULT> GetBlob_16;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte**, uint*, HRESULT> GetAllocatedBlob_17;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, void**, HRESULT> GetUnknown_18;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, HRESULT> SetItem_19;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, HRESULT> DeleteItem_20;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT> DeleteAllItems_21;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint, HRESULT> SetUINT32_22;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, ulong, HRESULT> SetUINT64_23;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, double, HRESULT> SetDouble_24;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, HRESULT> SetGUID_25;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PCWSTR, HRESULT> SetString_26;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte*, uint, HRESULT> SetBlob_27;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, IUnknown*, HRESULT> SetUnknown_28;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT> LockStore_29;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT> UnlockStore_30;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, uint*, HRESULT> GetCount_31;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, uint, Guid*, PROPVARIANT*, HRESULT> GetItemByIndex_32;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, IMFAttributes*, HRESULT> CopyAllItems_33;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, void**, HRESULT> ActivateObject_34;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT> ShutdownObject_35;

		internal unsafe delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT> DetachObject_36;
	}

	[ComImport]
	[Guid("7FEE9E9A-4A89-47A6-899C-B6A53A70FB67")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IMFAttributes.Interface
	{
		[PreserveSig]
		new unsafe HRESULT GetItem(Guid* guidKey, [Optional] PROPVARIANT* pValue);

		[PreserveSig]
		new unsafe HRESULT GetItemType(Guid* guidKey, MF_ATTRIBUTE_TYPE* pType);

		[PreserveSig]
		new unsafe HRESULT CompareItem(Guid* guidKey, PROPVARIANT* Value, BOOL* pbResult);

		[PreserveSig]
		new unsafe HRESULT Compare([Optional] IMFAttributes* pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, BOOL* pbResult);

		[PreserveSig]
		new unsafe HRESULT GetUINT32(Guid* guidKey, uint* punValue);

		[PreserveSig]
		new unsafe HRESULT GetUINT64(Guid* guidKey, ulong* punValue);

		[PreserveSig]
		new unsafe HRESULT GetDouble(Guid* guidKey, double* pfValue);

		[PreserveSig]
		new unsafe HRESULT GetGUID(Guid* guidKey, Guid* pguidValue);

		[PreserveSig]
		new unsafe HRESULT GetStringLength(Guid* guidKey, uint* pcchLength);

		[PreserveSig]
		new unsafe HRESULT GetString(Guid* guidKey, PWSTR pwszValue, uint cchBufSize, [Optional] uint* pcchLength);

		[PreserveSig]
		new unsafe HRESULT GetAllocatedString(Guid* guidKey, PWSTR* ppwszValue, uint* pcchLength);

		[PreserveSig]
		new unsafe HRESULT GetBlobSize(Guid* guidKey, uint* pcbBlobSize);

		[PreserveSig]
		new unsafe HRESULT GetBlob(Guid* guidKey, byte* pBuf, uint cbBufSize, [Optional] uint* pcbBlobSize);

		[PreserveSig]
		new unsafe HRESULT GetAllocatedBlob(Guid* guidKey, byte** ppBuf, uint* pcbSize);

		[PreserveSig]
		new unsafe HRESULT GetUnknown(Guid* guidKey, Guid* riid, void** ppv);

		[PreserveSig]
		new unsafe HRESULT SetItem(Guid* guidKey, PROPVARIANT* Value);

		[PreserveSig]
		new unsafe HRESULT DeleteItem(Guid* guidKey);

		[PreserveSig]
		new HRESULT DeleteAllItems();

		[PreserveSig]
		new unsafe HRESULT SetUINT32(Guid* guidKey, uint unValue);

		[PreserveSig]
		new unsafe HRESULT SetUINT64(Guid* guidKey, ulong unValue);

		[PreserveSig]
		new unsafe HRESULT SetDouble(Guid* guidKey, double fValue);

		[PreserveSig]
		new unsafe HRESULT SetGUID(Guid* guidKey, Guid* guidValue);

		[PreserveSig]
		new unsafe HRESULT SetString(Guid* guidKey, PCWSTR wszValue);

		[PreserveSig]
		new unsafe HRESULT SetBlob(Guid* guidKey, byte* pBuf, uint cbBufSize);

		[PreserveSig]
		new unsafe HRESULT SetUnknown(Guid* guidKey, IUnknown* pUnknown);

		[PreserveSig]
		new HRESULT LockStore();

		[PreserveSig]
		new HRESULT UnlockStore();

		[PreserveSig]
		new unsafe HRESULT GetCount(uint* pcItems);

		[PreserveSig]
		new unsafe HRESULT GetItemByIndex(uint unIndex, Guid* pguidKey, [Optional] PROPVARIANT* pValue);

		[PreserveSig]
		new unsafe HRESULT CopyAllItems(IMFAttributes* pDest);

		[PreserveSig]
		unsafe HRESULT ActivateObject(Guid* riid, void** ppv);

		[PreserveSig]
		HRESULT ShutdownObject();

		[PreserveSig]
		HRESULT DetachObject();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2146344602, 19081, 18342, 137, 156, 182, 165, 58, 112, 251, 103);

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT QueryInterface(in Guid riid, out void* ppvObject)
	{
		fixed (void** ppvObject2 = &ppvObject)
		{
			fixed (Guid* riid2 = &riid)
			{
				return QueryInterface(riid2, ppvObject2);
			}
		}
	}

	public unsafe HRESULT QueryInterface(Guid* riid, void** ppvObject)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFActivate*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, uint>)lpVtbl[1])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, uint>)lpVtbl[2])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetItem(in Guid guidKey, PROPVARIANT* pValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return GetItem(guidKey2, pValue);
		}
	}

	public unsafe HRESULT GetItem(Guid* guidKey, [Optional] PROPVARIANT* pValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[3])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetItemType(in Guid guidKey, out MF_ATTRIBUTE_TYPE pType)
	{
		fixed (MF_ATTRIBUTE_TYPE* pType2 = &pType)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetItemType(guidKey2, pType2);
			}
		}
	}

	public unsafe HRESULT GetItemType(Guid* guidKey, MF_ATTRIBUTE_TYPE* pType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, MF_ATTRIBUTE_TYPE*, HRESULT>)lpVtbl[4])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CompareItem(in Guid guidKey, in PROPVARIANT Value, out BOOL pbResult)
	{
		fixed (BOOL* pbResult2 = &pbResult)
		{
			fixed (PROPVARIANT* value = &Value)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return CompareItem(guidKey2, value, pbResult2);
				}
			}
		}
	}

	public unsafe HRESULT CompareItem(Guid* guidKey, PROPVARIANT* Value, BOOL* pbResult)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, BOOL*, HRESULT>)lpVtbl[5])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, Value, pbResult);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Compare(IMFAttributes* pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, out BOOL pbResult)
	{
		fixed (BOOL* pbResult2 = &pbResult)
		{
			return Compare(pTheirs, MatchType, pbResult2);
		}
	}

	public unsafe HRESULT Compare([Optional] IMFAttributes* pTheirs, MF_ATTRIBUTES_MATCH_TYPE MatchType, BOOL* pbResult)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, IMFAttributes*, MF_ATTRIBUTES_MATCH_TYPE, BOOL*, HRESULT>)lpVtbl[6])((IMFActivate*)Unsafe.AsPointer(ref this), pTheirs, MatchType, pbResult);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetUINT32(in Guid guidKey, out uint punValue)
	{
		fixed (uint* punValue2 = &punValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetUINT32(guidKey2, punValue2);
			}
		}
	}

	public unsafe HRESULT GetUINT32(Guid* guidKey, uint* punValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT>)lpVtbl[7])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, punValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetUINT64(in Guid guidKey, out ulong punValue)
	{
		fixed (ulong* punValue2 = &punValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetUINT64(guidKey2, punValue2);
			}
		}
	}

	public unsafe HRESULT GetUINT64(Guid* guidKey, ulong* punValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, ulong*, HRESULT>)lpVtbl[8])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, punValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDouble(in Guid guidKey, out double pfValue)
	{
		fixed (double* pfValue2 = &pfValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetDouble(guidKey2, pfValue2);
			}
		}
	}

	public unsafe HRESULT GetDouble(Guid* guidKey, double* pfValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, double*, HRESULT>)lpVtbl[9])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pfValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetGUID(in Guid guidKey, out Guid pguidValue)
	{
		fixed (Guid* pguidValue2 = &pguidValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetGUID(guidKey2, pguidValue2);
			}
		}
	}

	public unsafe HRESULT GetGUID(Guid* guidKey, Guid* pguidValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, HRESULT>)lpVtbl[10])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pguidValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetStringLength(in Guid guidKey, out uint pcchLength)
	{
		fixed (uint* pcchLength2 = &pcchLength)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetStringLength(guidKey2, pcchLength2);
			}
		}
	}

	public unsafe HRESULT GetStringLength(Guid* guidKey, uint* pcchLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT>)lpVtbl[11])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pcchLength);
	}

	internal unsafe HRESULT GetString(in Guid guidKey, Span<char> pwszValue, uint* pcchLength)
	{
		fixed (char* ptr = pwszValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetString(guidKey2, ptr, (uint)pwszValue.Length, pcchLength);
			}
		}
	}

	public unsafe HRESULT GetString(Guid* guidKey, PWSTR pwszValue, uint cchBufSize, [Optional] uint* pcchLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PWSTR, uint, uint*, HRESULT>)lpVtbl[12])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pwszValue, cchBufSize, pcchLength);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetAllocatedString(in Guid guidKey, out PWSTR ppwszValue, out uint pcchLength)
	{
		fixed (uint* pcchLength2 = &pcchLength)
		{
			fixed (PWSTR* ppwszValue2 = &ppwszValue)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return GetAllocatedString(guidKey2, ppwszValue2, pcchLength2);
				}
			}
		}
	}

	public unsafe HRESULT GetAllocatedString(Guid* guidKey, PWSTR* ppwszValue, uint* pcchLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PWSTR*, uint*, HRESULT>)lpVtbl[13])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, ppwszValue, pcchLength);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetBlobSize(in Guid guidKey, out uint pcbBlobSize)
	{
		fixed (uint* pcbBlobSize2 = &pcbBlobSize)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetBlobSize(guidKey2, pcbBlobSize2);
			}
		}
	}

	public unsafe HRESULT GetBlobSize(Guid* guidKey, uint* pcbBlobSize)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint*, HRESULT>)lpVtbl[14])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pcbBlobSize);
	}

	internal unsafe HRESULT GetBlob(in Guid guidKey, Span<byte> pBuf, uint* pcbBlobSize)
	{
		fixed (byte* pBuf2 = pBuf)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetBlob(guidKey2, pBuf2, (uint)pBuf.Length, pcbBlobSize);
			}
		}
	}

	public unsafe HRESULT GetBlob(Guid* guidKey, byte* pBuf, uint cbBufSize, [Optional] uint* pcbBlobSize)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte*, uint, uint*, HRESULT>)lpVtbl[15])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pBuf, cbBufSize, pcbBlobSize);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetAllocatedBlob(in Guid guidKey, byte** ppBuf, out uint pcbSize)
	{
		fixed (uint* pcbSize2 = &pcbSize)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return GetAllocatedBlob(guidKey2, ppBuf, pcbSize2);
			}
		}
	}

	public unsafe HRESULT GetAllocatedBlob(Guid* guidKey, byte** ppBuf, uint* pcbSize)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte**, uint*, HRESULT>)lpVtbl[16])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, ppBuf, pcbSize);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetUnknown(in Guid guidKey, in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				fixed (Guid* guidKey2 = &guidKey)
				{
					return GetUnknown(guidKey2, riid2, ppv2);
				}
			}
		}
	}

	public unsafe HRESULT GetUnknown(Guid* guidKey, Guid* riid, void** ppv)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, void**, HRESULT>)lpVtbl[17])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, riid, ppv);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetItem(in Guid guidKey, in PROPVARIANT Value)
	{
		fixed (PROPVARIANT* value = &Value)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return SetItem(guidKey2, value);
			}
		}
	}

	public unsafe HRESULT SetItem(Guid* guidKey, PROPVARIANT* Value)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[18])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, Value);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT DeleteItem(in Guid guidKey)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return DeleteItem(guidKey2);
		}
	}

	public unsafe HRESULT DeleteItem(Guid* guidKey)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, HRESULT>)lpVtbl[19])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey);
	}

	public unsafe HRESULT DeleteAllItems()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT>)lpVtbl[20])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetUINT32(in Guid guidKey, uint unValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return SetUINT32(guidKey2, unValue);
		}
	}

	public unsafe HRESULT SetUINT32(Guid* guidKey, uint unValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, uint, HRESULT>)lpVtbl[21])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, unValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetUINT64(in Guid guidKey, ulong unValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return SetUINT64(guidKey2, unValue);
		}
	}

	public unsafe HRESULT SetUINT64(Guid* guidKey, ulong unValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, ulong, HRESULT>)lpVtbl[22])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, unValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetDouble(in Guid guidKey, double fValue)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return SetDouble(guidKey2, fValue);
		}
	}

	public unsafe HRESULT SetDouble(Guid* guidKey, double fValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, double, HRESULT>)lpVtbl[23])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, fValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetGUID(in Guid guidKey, in Guid guidValue)
	{
		fixed (Guid* guidValue2 = &guidValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return SetGUID(guidKey2, guidValue2);
			}
		}
	}

	public unsafe HRESULT SetGUID(Guid* guidKey, Guid* guidValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, Guid*, HRESULT>)lpVtbl[24])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, guidValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetString(in Guid guidKey, string wszValue)
	{
		fixed (char* ptr = wszValue)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return SetString(guidKey2, ptr);
			}
		}
	}

	public unsafe HRESULT SetString(Guid* guidKey, PCWSTR wszValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, PCWSTR, HRESULT>)lpVtbl[25])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, wszValue);
	}

	internal unsafe HRESULT SetBlob(in Guid guidKey, ReadOnlySpan<byte> pBuf)
	{
		fixed (byte* pBuf2 = pBuf)
		{
			fixed (Guid* guidKey2 = &guidKey)
			{
				return SetBlob(guidKey2, pBuf2, (uint)pBuf.Length);
			}
		}
	}

	public unsafe HRESULT SetBlob(Guid* guidKey, byte* pBuf, uint cbBufSize)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, byte*, uint, HRESULT>)lpVtbl[26])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pBuf, cbBufSize);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetUnknown(in Guid guidKey, IUnknown* pUnknown)
	{
		fixed (Guid* guidKey2 = &guidKey)
		{
			return SetUnknown(guidKey2, pUnknown);
		}
	}

	public unsafe HRESULT SetUnknown(Guid* guidKey, IUnknown* pUnknown)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, IUnknown*, HRESULT>)lpVtbl[27])((IMFActivate*)Unsafe.AsPointer(ref this), guidKey, pUnknown);
	}

	public unsafe HRESULT LockStore()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT>)lpVtbl[28])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT UnlockStore()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT>)lpVtbl[29])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCount(out uint pcItems)
	{
		fixed (uint* pcItems2 = &pcItems)
		{
			return GetCount(pcItems2);
		}
	}

	public unsafe HRESULT GetCount(uint* pcItems)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, uint*, HRESULT>)lpVtbl[30])((IMFActivate*)Unsafe.AsPointer(ref this), pcItems);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetItemByIndex(uint unIndex, out Guid pguidKey, PROPVARIANT* pValue)
	{
		fixed (Guid* pguidKey2 = &pguidKey)
		{
			return GetItemByIndex(unIndex, pguidKey2, pValue);
		}
	}

	public unsafe HRESULT GetItemByIndex(uint unIndex, Guid* pguidKey, [Optional] PROPVARIANT* pValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, uint, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[31])((IMFActivate*)Unsafe.AsPointer(ref this), unIndex, pguidKey, pValue);
	}

	public unsafe HRESULT CopyAllItems(IMFAttributes* pDest)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, IMFAttributes*, HRESULT>)lpVtbl[32])((IMFActivate*)Unsafe.AsPointer(ref this), pDest);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT ActivateObject(in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				return ActivateObject(riid2, ppv2);
			}
		}
	}

	public unsafe HRESULT ActivateObject(Guid* riid, void** ppv)
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, Guid*, void**, HRESULT>)lpVtbl[33])((IMFActivate*)Unsafe.AsPointer(ref this), riid, ppv);
	}

	public unsafe HRESULT ShutdownObject()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT>)lpVtbl[34])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT DetachObject()
	{
		return ((delegate* unmanaged[Stdcall]<IMFActivate*, HRESULT>)lpVtbl[35])((IMFActivate*)Unsafe.AsPointer(ref this));
	}

	internal unsafe HRESULT QueryInterface<T>(out T* ppv) where T : unmanaged
	{
		Guid gUID = typeof(T).GUID;
		void* ptr = default;
		HRESULT result = QueryInterface(&gUID, &ptr);
		if (result.Succeeded)
		{
			ppv = (T*)ptr;
		}
		else
		{
			ppv = null;
		}
		return result;
	}
}
