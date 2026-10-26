using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_ITypeLib_Extensions
{
	internal unsafe static HRESULT GetTypeInfoType(this ITypeLib.Interface @this, uint index, out TYPEKIND pTKind)
	{
		fixed (TYPEKIND* pTKind2 = &pTKind)
		{
			return @this.GetTypeInfoType(index, pTKind2);
		}
	}

	internal unsafe static HRESULT GetTypeInfoOfGuid(this ITypeLib.Interface @this, in Guid guid, ITypeInfo** ppTinfo)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.GetTypeInfoOfGuid(guid2, ppTinfo);
		}
	}

	internal unsafe static HRESULT GetLibAttr(this ITypeLib.Interface @this, out TLIBATTR* ppTLibAttr)
	{
		fixed (TLIBATTR** ppTLibAttr2 = &ppTLibAttr)
		{
			return @this.GetLibAttr(ppTLibAttr2);
		}
	}

	internal unsafe static HRESULT GetDocumentation(this ITypeLib.Interface @this, int index, out SysFreeStringSafeHandle pBstrName, out SysFreeStringSafeHandle pBstrDocString, out uint pdwHelpContext, out SysFreeStringSafeHandle pBstrHelpFile)
	{
		fixed (uint* pdwHelpContext2 = &pdwHelpContext)
		{
			BSTR bSTR = default;
			BSTR bSTR2 = default;
			BSTR bSTR3 = default;
			HRESULT documentation = @this.GetDocumentation(index, &bSTR, &bSTR2, pdwHelpContext2, &bSTR3);
			pBstrName = new SysFreeStringSafeHandle(bSTR);
			pBstrDocString = new SysFreeStringSafeHandle(bSTR2);
			pBstrHelpFile = new SysFreeStringSafeHandle(bSTR3);
			return documentation;
		}
	}

	internal unsafe static HRESULT IsName(this ITypeLib.Interface @this, ref Span<char> szNameBuf, uint lHashVal, out BOOL pfName)
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
				HRESULT result = @this.IsName(szNameBuf2, lHashVal, pfName2);
				szNameBuf = szNameBuf.Slice(0, szNameBuf2.Length);
				return result;
			}
		}
	}

	internal unsafe static HRESULT FindName(this ITypeLib.Interface @this, ref Span<char> szNameBuf, uint lHashVal, ITypeInfo** ppTInfo, Span<int> rgMemId, ref ushort pcFound)
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
					HRESULT result = @this.FindName(szNameBuf2, lHashVal, ppTInfo, rgMemId2, pcFound2);
					szNameBuf = szNameBuf.Slice(0, szNameBuf2.Length);
					return result;
				}
			}
		}
	}

	internal unsafe static void ReleaseTLibAttr(this ITypeLib.Interface @this, in TLIBATTR pTLibAttr)
	{
		fixed (TLIBATTR* pTLibAttr2 = &pTLibAttr)
		{
			@this.ReleaseTLibAttr(pTLibAttr2);
		}
	}
}
