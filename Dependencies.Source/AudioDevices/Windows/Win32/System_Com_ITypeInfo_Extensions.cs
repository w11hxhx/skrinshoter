using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Variant;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_ITypeInfo_Extensions
{
	internal unsafe static HRESULT GetTypeAttr(this ITypeInfo.Interface @this, out TYPEATTR* ppTypeAttr)
	{
		fixed (TYPEATTR** ppTypeAttr2 = &ppTypeAttr)
		{
			return @this.GetTypeAttr(ppTypeAttr2);
		}
	}

	internal unsafe static HRESULT GetFuncDesc(this ITypeInfo.Interface @this, uint index, out FUNCDESC* ppFuncDesc)
	{
		fixed (FUNCDESC** ppFuncDesc2 = &ppFuncDesc)
		{
			return @this.GetFuncDesc(index, ppFuncDesc2);
		}
	}

	internal unsafe static HRESULT GetVarDesc(this ITypeInfo.Interface @this, uint index, out VARDESC* ppVarDesc)
	{
		fixed (VARDESC** ppVarDesc2 = &ppVarDesc)
		{
			return @this.GetVarDesc(index, ppVarDesc2);
		}
	}

	internal unsafe static HRESULT GetNames(this ITypeInfo.Interface @this, int memid, out SysFreeStringSafeHandle rgBstrNames, uint cMaxNames, out uint pcNames)
	{
		fixed (uint* pcNames2 = &pcNames)
		{
			BSTR bSTR = default;
			HRESULT names = @this.GetNames(memid, &bSTR, cMaxNames, pcNames2);
			rgBstrNames = new SysFreeStringSafeHandle(bSTR);
			return names;
		}
	}

	internal unsafe static HRESULT GetRefTypeOfImplType(this ITypeInfo.Interface @this, uint index, out uint pRefType)
	{
		fixed (uint* pRefType2 = &pRefType)
		{
			return @this.GetRefTypeOfImplType(index, pRefType2);
		}
	}

	internal unsafe static HRESULT GetImplTypeFlags(this ITypeInfo.Interface @this, uint index, out IMPLTYPEFLAGS pImplTypeFlags)
	{
		fixed (IMPLTYPEFLAGS* pImplTypeFlags2 = &pImplTypeFlags)
		{
			return @this.GetImplTypeFlags(index, pImplTypeFlags2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT GetIDsOfNames(this ITypeInfo.Interface @this, ReadOnlySpan<PWSTR> rgszNames, Span<int> pMemId)
	{
		fixed (int* pMemId2 = pMemId)
		{
			fixed (PWSTR* rgszNames2 = rgszNames)
			{
				if (rgszNames.Length != pMemId.Length)
				{
					throw new ArgumentException();
				}
				return @this.GetIDsOfNames(rgszNames2, (uint)pMemId.Length, pMemId2);
			}
		}
	}

	internal unsafe static HRESULT Invoke(this ITypeInfo.Interface @this, void* pvInstance, int memid, DISPATCH_FLAGS wFlags, ref DISPPARAMS pDispParams, out VARIANT pVarResult, out EXCEPINFO pExcepInfo, out uint puArgErr)
	{
		fixed (uint* puArgErr2 = &puArgErr)
		{
			fixed (EXCEPINFO* pExcepInfo2 = &pExcepInfo)
			{
				fixed (VARIANT* pVarResult2 = &pVarResult)
				{
					fixed (DISPPARAMS* pDispParams2 = &pDispParams)
					{
						return @this.Invoke(pvInstance, memid, wFlags, pDispParams2, pVarResult2, pExcepInfo2, puArgErr2);
					}
				}
			}
		}
	}

	internal unsafe static HRESULT GetDocumentation(this ITypeInfo.Interface @this, int memid, out SysFreeStringSafeHandle pBstrName, out SysFreeStringSafeHandle pBstrDocString, out uint pdwHelpContext, out SysFreeStringSafeHandle pBstrHelpFile)
	{
		fixed (uint* pdwHelpContext2 = &pdwHelpContext)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			BSTR bSTR3 = default;
			HRESULT documentation = @this.GetDocumentation(memid, &bSTR, &bSTR2, pdwHelpContext2, &bSTR3);
			pBstrName = new SysFreeStringSafeHandle(bSTR);
			pBstrDocString = new SysFreeStringSafeHandle(bSTR2);
			pBstrHelpFile = new SysFreeStringSafeHandle(bSTR3);
			return documentation;
		}
	}

	internal unsafe static HRESULT GetDllEntry(this ITypeInfo.Interface @this, int memid, INVOKEKIND invKind, out SysFreeStringSafeHandle pBstrDllName, out SysFreeStringSafeHandle pBstrName, out ushort pwOrdinal)
	{
		fixed (ushort* pwOrdinal2 = &pwOrdinal)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			HRESULT dllEntry = @this.GetDllEntry(memid, invKind, &bSTR, &bSTR2, pwOrdinal2);
			pBstrDllName = new SysFreeStringSafeHandle(bSTR);
			pBstrName = new SysFreeStringSafeHandle(bSTR2);
			return dllEntry;
		}
	}

	internal unsafe static HRESULT AddressOfMember(this ITypeInfo.Interface @this, int memid, INVOKEKIND invKind, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			return @this.AddressOfMember(memid, invKind, ppv2);
		}
	}

	internal unsafe static HRESULT CreateInstance(this ITypeInfo.Interface @this, IUnknown* pUnkOuter, in Guid riid, out void* ppvObj)
	{
		fixed (void** ppvObj2 = &ppvObj)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.CreateInstance(pUnkOuter, riid2, ppvObj2);
			}
		}
	}

	internal unsafe static HRESULT GetMops(this ITypeInfo.Interface @this, int memid, out SysFreeStringSafeHandle pBstrMops)
	{
		BSTR bSTR = default;
		HRESULT mops = @this.GetMops(memid, &bSTR);
		pBstrMops = new SysFreeStringSafeHandle(bSTR);
		return mops;
	}

	internal unsafe static HRESULT GetContainingTypeLib(this ITypeInfo.Interface @this, ITypeLib** ppTLib, out uint pIndex)
	{
		fixed (uint* pIndex2 = &pIndex)
		{
			return @this.GetContainingTypeLib(ppTLib, pIndex2);
		}
	}

	internal unsafe static void ReleaseTypeAttr(this ITypeInfo.Interface @this, in TYPEATTR pTypeAttr)
	{
		fixed (TYPEATTR* pTypeAttr2 = &pTypeAttr)
		{
			@this.ReleaseTypeAttr(pTypeAttr2);
		}
	}

	internal unsafe static void ReleaseFuncDesc(this ITypeInfo.Interface @this, in FUNCDESC pFuncDesc)
	{
		fixed (FUNCDESC* pFuncDesc2 = &pFuncDesc)
		{
			@this.ReleaseFuncDesc(pFuncDesc2);
		}
	}

	internal unsafe static void ReleaseVarDesc(this ITypeInfo.Interface @this, in VARDESC pVarDesc)
	{
		fixed (VARDESC* pVarDesc2 = &pVarDesc)
		{
			@this.ReleaseVarDesc(pVarDesc2);
		}
	}
}
