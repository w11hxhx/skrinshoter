using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com;

[Guid("00020401-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ITypeInfo
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR**, HRESULT> GetTypeAttr_4;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeComp**, HRESULT> GetTypeComp_5;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint, FUNCDESC**, HRESULT> GetFuncDesc_6;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint, VARDESC**, HRESULT> GetVarDesc_7;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, uint, uint*, HRESULT> GetNames_8;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint, uint*, HRESULT> GetRefTypeOfImplType_9;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint, IMPLTYPEFLAGS*, HRESULT> GetImplTypeFlags_10;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, PWSTR*, uint, int*, HRESULT> GetIDsOfNames_11;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, void*, int, DISPATCH_FLAGS, DISPPARAMS*, VARIANT*, EXCEPINFO*, uint*, HRESULT> Invoke_12;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, BSTR*, uint*, BSTR*, HRESULT> GetDocumentation_13;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, BSTR*, BSTR*, ushort*, HRESULT> GetDllEntry_14;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, uint, ITypeInfo**, HRESULT> GetRefTypeInfo_15;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, void**, HRESULT> AddressOfMember_16;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, IUnknown*, Guid*, void**, HRESULT> CreateInstance_17;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, HRESULT> GetMops_18;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeLib**, uint*, HRESULT> GetContainingTypeLib_19;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR*, void> ReleaseTypeAttr_20;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, FUNCDESC*, void> ReleaseFuncDesc_21;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeInfo*, VARDESC*, void> ReleaseVarDesc_22;
	}

	[ComImport]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetTypeAttr(TYPEATTR** ppTypeAttr);

		[PreserveSig]
		unsafe HRESULT GetTypeComp(ITypeComp** ppTComp);

		[PreserveSig]
		unsafe HRESULT GetFuncDesc(uint index, FUNCDESC** ppFuncDesc);

		[PreserveSig]
		unsafe HRESULT GetVarDesc(uint index, VARDESC** ppVarDesc);

		[PreserveSig]
		unsafe HRESULT GetNames(int memid, BSTR* rgBstrNames, uint cMaxNames, uint* pcNames);

		[PreserveSig]
		unsafe HRESULT GetRefTypeOfImplType(uint index, uint* pRefType);

		[PreserveSig]
		unsafe HRESULT GetImplTypeFlags(uint index, IMPLTYPEFLAGS* pImplTypeFlags);

		[PreserveSig]
		unsafe HRESULT GetIDsOfNames(PWSTR* rgszNames, uint cNames, int* pMemId);

		[PreserveSig]
		unsafe HRESULT Invoke(void* pvInstance, int memid, DISPATCH_FLAGS wFlags, DISPPARAMS* pDispParams, VARIANT* pVarResult, EXCEPINFO* pExcepInfo, uint* puArgErr);

		[PreserveSig]
		unsafe HRESULT GetDocumentation(int memid, [Optional] BSTR* pBstrName, [Optional] BSTR* pBstrDocString, uint* pdwHelpContext, [Optional] BSTR* pBstrHelpFile);

		[PreserveSig]
		unsafe HRESULT GetDllEntry(int memid, INVOKEKIND invKind, [Optional] BSTR* pBstrDllName, [Optional] BSTR* pBstrName, ushort* pwOrdinal);

		[PreserveSig]
		unsafe HRESULT GetRefTypeInfo(uint hRefType, ITypeInfo** ppTInfo);

		[PreserveSig]
		unsafe HRESULT AddressOfMember(int memid, INVOKEKIND invKind, void** ppv);

		[PreserveSig]
		unsafe HRESULT CreateInstance(IUnknown* pUnkOuter, Guid* riid, void** ppvObj);

		[PreserveSig]
		unsafe HRESULT GetMops(int memid, BSTR* pBstrMops);

		[PreserveSig]
		unsafe HRESULT GetContainingTypeLib(ITypeLib** ppTLib, uint* pIndex);

		[PreserveSig]
		unsafe void ReleaseTypeAttr(TYPEATTR* pTypeAttr);

		[PreserveSig]
		unsafe void ReleaseFuncDesc(FUNCDESC* pFuncDesc);

		[PreserveSig]
		unsafe void ReleaseVarDesc(VARDESC* pVarDesc);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(132097, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, Guid*, void**, HRESULT>)(*lpVtbl))((ITypeInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint>)lpVtbl[1])((ITypeInfo*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint>)lpVtbl[2])((ITypeInfo*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetTypeAttr(out TYPEATTR* ppTypeAttr)
	{
		fixed (TYPEATTR** ppTypeAttr2 = &ppTypeAttr)
		{
			return GetTypeAttr(ppTypeAttr2);
		}
	}

	public unsafe HRESULT GetTypeAttr(TYPEATTR** ppTypeAttr)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR**, HRESULT>)lpVtbl[3])((ITypeInfo*)Unsafe.AsPointer(ref this), ppTypeAttr);
	}

	public unsafe HRESULT GetTypeComp(ITypeComp** ppTComp)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeComp**, HRESULT>)lpVtbl[4])((ITypeInfo*)Unsafe.AsPointer(ref this), ppTComp);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFuncDesc(uint index, out FUNCDESC* ppFuncDesc)
	{
		fixed (FUNCDESC** ppFuncDesc2 = &ppFuncDesc)
		{
			return GetFuncDesc(index, ppFuncDesc2);
		}
	}

	public unsafe HRESULT GetFuncDesc(uint index, FUNCDESC** ppFuncDesc)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, FUNCDESC**, HRESULT>)lpVtbl[5])((ITypeInfo*)Unsafe.AsPointer(ref this), index, ppFuncDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetVarDesc(uint index, out VARDESC* ppVarDesc)
	{
		fixed (VARDESC** ppVarDesc2 = &ppVarDesc)
		{
			return GetVarDesc(index, ppVarDesc2);
		}
	}

	public unsafe HRESULT GetVarDesc(uint index, VARDESC** ppVarDesc)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, VARDESC**, HRESULT>)lpVtbl[6])((ITypeInfo*)Unsafe.AsPointer(ref this), index, ppVarDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetNames(int memid, out SysFreeStringSafeHandle rgBstrNames, uint cMaxNames, out uint pcNames)
	{
		fixed (uint* pcNames2 = &pcNames)
		{
			BSTR bSTR = default;
			HRESULT names = GetNames(memid, &bSTR, cMaxNames, pcNames2);
			rgBstrNames = new SysFreeStringSafeHandle(bSTR);
			return names;
		}
	}

	public unsafe HRESULT GetNames(int memid, BSTR* rgBstrNames, uint cMaxNames, uint* pcNames)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, uint, uint*, HRESULT>)lpVtbl[7])((ITypeInfo*)Unsafe.AsPointer(ref this), memid, rgBstrNames, cMaxNames, pcNames);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetRefTypeOfImplType(uint index, out uint pRefType)
	{
		fixed (uint* pRefType2 = &pRefType)
		{
			return GetRefTypeOfImplType(index, pRefType2);
		}
	}

	public unsafe HRESULT GetRefTypeOfImplType(uint index, uint* pRefType)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, uint*, HRESULT>)lpVtbl[8])((ITypeInfo*)Unsafe.AsPointer(ref this), index, pRefType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetImplTypeFlags(uint index, out IMPLTYPEFLAGS pImplTypeFlags)
	{
		fixed (IMPLTYPEFLAGS* pImplTypeFlags2 = &pImplTypeFlags)
		{
			return GetImplTypeFlags(index, pImplTypeFlags2);
		}
	}

	public unsafe HRESULT GetImplTypeFlags(uint index, IMPLTYPEFLAGS* pImplTypeFlags)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, IMPLTYPEFLAGS*, HRESULT>)lpVtbl[9])((ITypeInfo*)Unsafe.AsPointer(ref this), index, pImplTypeFlags);
	}

	internal unsafe HRESULT GetIDsOfNames(ReadOnlySpan<PWSTR> rgszNames, Span<int> pMemId)
	{
		fixed (int* pMemId2 = pMemId)
		{
			fixed (PWSTR* rgszNames2 = rgszNames)
			{
				if (rgszNames.Length != pMemId.Length)
				{
					throw new ArgumentException();
				}
				return GetIDsOfNames(rgszNames2, (uint)pMemId.Length, pMemId2);
			}
		}
	}

	public unsafe HRESULT GetIDsOfNames(PWSTR* rgszNames, uint cNames, int* pMemId)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, PWSTR*, uint, int*, HRESULT>)lpVtbl[10])((ITypeInfo*)Unsafe.AsPointer(ref this), rgszNames, cNames, pMemId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Invoke(void* pvInstance, int memid, DISPATCH_FLAGS wFlags, ref DISPPARAMS pDispParams, out VARIANT pVarResult, out EXCEPINFO pExcepInfo, out uint puArgErr)
	{
		fixed (uint* puArgErr2 = &puArgErr)
		{
			fixed (EXCEPINFO* pExcepInfo2 = &pExcepInfo)
			{
				fixed (VARIANT* pVarResult2 = &pVarResult)
				{
					fixed (DISPPARAMS* pDispParams2 = &pDispParams)
					{
						return Invoke(pvInstance, memid, wFlags, pDispParams2, pVarResult2, pExcepInfo2, puArgErr2);
					}
				}
			}
		}
	}

	public unsafe HRESULT Invoke(void* pvInstance, int memid, DISPATCH_FLAGS wFlags, DISPPARAMS* pDispParams, VARIANT* pVarResult, EXCEPINFO* pExcepInfo, uint* puArgErr)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, void*, int, DISPATCH_FLAGS, DISPPARAMS*, VARIANT*, EXCEPINFO*, uint*, HRESULT>)lpVtbl[11])((ITypeInfo*)Unsafe.AsPointer(ref this), pvInstance, memid, wFlags, pDispParams, pVarResult, pExcepInfo, puArgErr);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDocumentation(int memid, out SysFreeStringSafeHandle pBstrName, out SysFreeStringSafeHandle pBstrDocString, out uint pdwHelpContext, out SysFreeStringSafeHandle pBstrHelpFile)
	{
		fixed (uint* pdwHelpContext2 = &pdwHelpContext)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			BSTR bSTR3 = default;
			HRESULT documentation = GetDocumentation(memid, &bSTR, &bSTR2, pdwHelpContext2, &bSTR3);
			pBstrName = new SysFreeStringSafeHandle(bSTR);
			pBstrDocString = new SysFreeStringSafeHandle(bSTR2);
			pBstrHelpFile = new SysFreeStringSafeHandle(bSTR3);
			return documentation;
		}
	}

	public unsafe HRESULT GetDocumentation(int memid, [Optional] BSTR* pBstrName, [Optional] BSTR* pBstrDocString, uint* pdwHelpContext, [Optional] BSTR* pBstrHelpFile)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, BSTR*, uint*, BSTR*, HRESULT>)lpVtbl[12])((ITypeInfo*)Unsafe.AsPointer(ref this), memid, pBstrName, pBstrDocString, pdwHelpContext, pBstrHelpFile);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDllEntry(int memid, INVOKEKIND invKind, out SysFreeStringSafeHandle pBstrDllName, out SysFreeStringSafeHandle pBstrName, out ushort pwOrdinal)
	{
		fixed (ushort* pwOrdinal2 = &pwOrdinal)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			HRESULT dllEntry = GetDllEntry(memid, invKind, &bSTR, &bSTR2, pwOrdinal2);
			pBstrDllName = new SysFreeStringSafeHandle(bSTR);
			pBstrName = new SysFreeStringSafeHandle(bSTR2);
			return dllEntry;
		}
	}

	public unsafe HRESULT GetDllEntry(int memid, INVOKEKIND invKind, [Optional] BSTR* pBstrDllName, [Optional] BSTR* pBstrName, ushort* pwOrdinal)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, BSTR*, BSTR*, ushort*, HRESULT>)lpVtbl[13])((ITypeInfo*)Unsafe.AsPointer(ref this), memid, invKind, pBstrDllName, pBstrName, pwOrdinal);
	}

	public unsafe HRESULT GetRefTypeInfo(uint hRefType, ITypeInfo** ppTInfo)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, uint, ITypeInfo**, HRESULT>)lpVtbl[14])((ITypeInfo*)Unsafe.AsPointer(ref this), hRefType, ppTInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT AddressOfMember(int memid, INVOKEKIND invKind, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			return AddressOfMember(memid, invKind, ppv2);
		}
	}

	public unsafe HRESULT AddressOfMember(int memid, INVOKEKIND invKind, void** ppv)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, INVOKEKIND, void**, HRESULT>)lpVtbl[15])((ITypeInfo*)Unsafe.AsPointer(ref this), memid, invKind, ppv);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateInstance(IUnknown* pUnkOuter, in Guid riid, out void* ppvObj)
	{
		fixed (void** ppvObj2 = &ppvObj)
		{
			fixed (Guid* riid2 = &riid)
			{
				return CreateInstance(pUnkOuter, riid2, ppvObj2);
			}
		}
	}

	public unsafe HRESULT CreateInstance(IUnknown* pUnkOuter, Guid* riid, void** ppvObj)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, IUnknown*, Guid*, void**, HRESULT>)lpVtbl[16])((ITypeInfo*)Unsafe.AsPointer(ref this), pUnkOuter, riid, ppvObj);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetMops(int memid, out SysFreeStringSafeHandle pBstrMops)
	{
		BSTR bSTR = default;
		HRESULT mops = GetMops(memid, &bSTR);
		pBstrMops = new SysFreeStringSafeHandle(bSTR);
		return mops;
	}

	public unsafe HRESULT GetMops(int memid, BSTR* pBstrMops)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, int, BSTR*, HRESULT>)lpVtbl[17])((ITypeInfo*)Unsafe.AsPointer(ref this), memid, pBstrMops);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetContainingTypeLib(ITypeLib** ppTLib, out uint pIndex)
	{
		fixed (uint* pIndex2 = &pIndex)
		{
			return GetContainingTypeLib(ppTLib, pIndex2);
		}
	}

	public unsafe HRESULT GetContainingTypeLib(ITypeLib** ppTLib, uint* pIndex)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeInfo*, ITypeLib**, uint*, HRESULT>)lpVtbl[18])((ITypeInfo*)Unsafe.AsPointer(ref this), ppTLib, pIndex);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ReleaseTypeAttr(in TYPEATTR pTypeAttr)
	{
		fixed (TYPEATTR* pTypeAttr2 = &pTypeAttr)
		{
			ReleaseTypeAttr(pTypeAttr2);
		}
	}

	public unsafe void ReleaseTypeAttr(TYPEATTR* pTypeAttr)
	{
		((delegate* unmanaged[Stdcall]<ITypeInfo*, TYPEATTR*, void>)lpVtbl[19])((ITypeInfo*)Unsafe.AsPointer(ref this), pTypeAttr);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ReleaseFuncDesc(in FUNCDESC pFuncDesc)
	{
		fixed (FUNCDESC* pFuncDesc2 = &pFuncDesc)
		{
			ReleaseFuncDesc(pFuncDesc2);
		}
	}

	public unsafe void ReleaseFuncDesc(FUNCDESC* pFuncDesc)
	{
		((delegate* unmanaged[Stdcall]<ITypeInfo*, FUNCDESC*, void>)lpVtbl[20])((ITypeInfo*)Unsafe.AsPointer(ref this), pFuncDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ReleaseVarDesc(in VARDESC pVarDesc)
	{
		fixed (VARDESC* pVarDesc2 = &pVarDesc)
		{
			ReleaseVarDesc(pVarDesc2);
		}
	}

	public unsafe void ReleaseVarDesc(VARDESC* pVarDesc)
	{
		((delegate* unmanaged[Stdcall]<ITypeInfo*, VARDESC*, void>)lpVtbl[21])((ITypeInfo*)Unsafe.AsPointer(ref this), pVarDesc);
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
