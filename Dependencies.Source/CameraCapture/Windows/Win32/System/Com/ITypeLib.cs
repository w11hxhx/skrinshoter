using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[Guid("00020402-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ITypeLib
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, uint> GetTypeInfoCount_4;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, uint, ITypeInfo**, HRESULT> GetTypeInfo_5;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, uint, TYPEKIND*, HRESULT> GetTypeInfoType_6;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, ITypeInfo**, HRESULT> GetTypeInfoOfGuid_7;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR**, HRESULT> GetLibAttr_8;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, ITypeComp**, HRESULT> GetTypeComp_9;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, int, BSTR*, BSTR*, uint*, BSTR*, HRESULT> GetDocumentation_10;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, PWSTR, uint, BOOL*, HRESULT> IsName_11;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, PWSTR, uint, ITypeInfo**, int*, ushort*, HRESULT> FindName_12;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR*, void> ReleaseTLibAttr_13;
	}

	[ComImport]
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		uint GetTypeInfoCount();

		[PreserveSig]
		unsafe HRESULT GetTypeInfo(uint index, ITypeInfo** ppTInfo);

		[PreserveSig]
		unsafe HRESULT GetTypeInfoType(uint index, TYPEKIND* pTKind);

		[PreserveSig]
		unsafe HRESULT GetTypeInfoOfGuid(Guid* guid, ITypeInfo** ppTinfo);

		[PreserveSig]
		unsafe HRESULT GetLibAttr(TLIBATTR** ppTLibAttr);

		[PreserveSig]
		unsafe HRESULT GetTypeComp(ITypeComp** ppTComp);

		[PreserveSig]
		unsafe HRESULT GetDocumentation(int index, [Optional] BSTR* pBstrName, [Optional] BSTR* pBstrDocString, uint* pdwHelpContext, [Optional] BSTR* pBstrHelpFile);

		[PreserveSig]
		unsafe HRESULT IsName(PWSTR szNameBuf, uint lHashVal, BOOL* pfName);

		[PreserveSig]
		unsafe HRESULT FindName(PWSTR szNameBuf, uint lHashVal, ITypeInfo** ppTInfo, int* rgMemId, ushort* pcFound);

		[PreserveSig]
		unsafe void ReleaseTLibAttr(TLIBATTR* pTLibAttr);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(132098, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, void**, HRESULT>)(*lpVtbl))((ITypeLib*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)lpVtbl[1])((ITypeLib*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)lpVtbl[2])((ITypeLib*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint GetTypeInfoCount()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint>)lpVtbl[3])((ITypeLib*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT GetTypeInfo(uint index, ITypeInfo** ppTInfo)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint, ITypeInfo**, HRESULT>)lpVtbl[4])((ITypeLib*)Unsafe.AsPointer(ref this), index, ppTInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetTypeInfoType(uint index, out TYPEKIND pTKind)
	{
		fixed (TYPEKIND* pTKind2 = &pTKind)
		{
			return GetTypeInfoType(index, pTKind2);
		}
	}

	public unsafe HRESULT GetTypeInfoType(uint index, TYPEKIND* pTKind)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, uint, TYPEKIND*, HRESULT>)lpVtbl[5])((ITypeLib*)Unsafe.AsPointer(ref this), index, pTKind);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetTypeInfoOfGuid(in Guid guid, ITypeInfo** ppTinfo)
	{
		fixed (Guid* guid2 = &guid)
		{
			return GetTypeInfoOfGuid(guid2, ppTinfo);
		}
	}

	public unsafe HRESULT GetTypeInfoOfGuid(Guid* guid, ITypeInfo** ppTinfo)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, Guid*, ITypeInfo**, HRESULT>)lpVtbl[6])((ITypeLib*)Unsafe.AsPointer(ref this), guid, ppTinfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetLibAttr(out TLIBATTR* ppTLibAttr)
	{
		fixed (TLIBATTR** ppTLibAttr2 = &ppTLibAttr)
		{
			return GetLibAttr(ppTLibAttr2);
		}
	}

	public unsafe HRESULT GetLibAttr(TLIBATTR** ppTLibAttr)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR**, HRESULT>)lpVtbl[7])((ITypeLib*)Unsafe.AsPointer(ref this), ppTLibAttr);
	}

	public unsafe HRESULT GetTypeComp(ITypeComp** ppTComp)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, ITypeComp**, HRESULT>)lpVtbl[8])((ITypeLib*)Unsafe.AsPointer(ref this), ppTComp);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDocumentation(int index, out SysFreeStringSafeHandle pBstrName, out SysFreeStringSafeHandle pBstrDocString, out uint pdwHelpContext, out SysFreeStringSafeHandle pBstrHelpFile)
	{
		fixed (uint* pdwHelpContext2 = &pdwHelpContext)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			BSTR bSTR3 = default;
			HRESULT documentation = GetDocumentation(index, &bSTR, &bSTR2, pdwHelpContext2, &bSTR3);
			pBstrName = new SysFreeStringSafeHandle(bSTR);
			pBstrDocString = new SysFreeStringSafeHandle(bSTR2);
			pBstrHelpFile = new SysFreeStringSafeHandle(bSTR3);
			return documentation;
		}
	}

	public unsafe HRESULT GetDocumentation(int index, [Optional] BSTR* pBstrName, [Optional] BSTR* pBstrDocString, uint* pdwHelpContext, [Optional] BSTR* pBstrHelpFile)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, int, BSTR*, BSTR*, uint*, BSTR*, HRESULT>)lpVtbl[9])((ITypeLib*)Unsafe.AsPointer(ref this), index, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT IsName(ref Span<char> szNameBuf, uint lHashVal, out BOOL pfName)
	{
		if (szNameBuf != null && szNameBuf.LastIndexOf('\0') == -1)
		{
			throw new ArgumentException("Required null terminator missing.", "szNameBuf");
		}
		fixed (BOOL* pfName2 = &pfName)
		{
			fixed (char* ptr = szNameBuf)
			{
				PWSTR szNameBuf2 = ptr;
				HRESULT result = IsName(szNameBuf2, lHashVal, pfName2);
				szNameBuf = szNameBuf.Slice(0, szNameBuf2.Length);
				return result;
			}
		}
	}

	public unsafe HRESULT IsName(PWSTR szNameBuf, uint lHashVal, BOOL* pfName)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, PWSTR, uint, BOOL*, HRESULT>)lpVtbl[10])((ITypeLib*)Unsafe.AsPointer(ref this), szNameBuf, lHashVal, pfName);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT FindName(ref Span<char> szNameBuf, uint lHashVal, ITypeInfo** ppTInfo, Span<int> rgMemId, ref ushort pcFound)
	{
		if (szNameBuf != null && szNameBuf.LastIndexOf('\0') == -1)
		{
			throw new ArgumentException("Required null terminator missing.", "szNameBuf");
		}
		fixed (ushort* pcFound2 = &pcFound)
		{
			fixed (int* rgMemId2 = rgMemId)
			{
				fixed (char* ptr = szNameBuf)
				{
					PWSTR szNameBuf2 = ptr;
					HRESULT result = FindName(szNameBuf2, lHashVal, ppTInfo, rgMemId2, pcFound2);
					szNameBuf = szNameBuf.Slice(0, szNameBuf2.Length);
					return result;
				}
			}
		}
	}

	public unsafe HRESULT FindName(PWSTR szNameBuf, uint lHashVal, ITypeInfo** ppTInfo, int* rgMemId, ushort* pcFound)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeLib*, PWSTR, uint, ITypeInfo**, int*, ushort*, HRESULT>)lpVtbl[11])((ITypeLib*)Unsafe.AsPointer(ref this), szNameBuf, lHashVal, ppTInfo, rgMemId, pcFound);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ReleaseTLibAttr(in TLIBATTR pTLibAttr)
	{
		fixed (TLIBATTR* pTLibAttr2 = &pTLibAttr)
		{
			ReleaseTLibAttr(pTLibAttr2);
		}
	}

	public unsafe void ReleaseTLibAttr(TLIBATTR* pTLibAttr)
	{
		((delegate* unmanaged[Stdcall]<ITypeLib*, TLIBATTR*, void>)lpVtbl[12])((ITypeLib*)Unsafe.AsPointer(ref this), pTLibAttr);
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
