using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Ole;
using Windows.Win32.System.Variant;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Ole_IRecordInfo_Extensions
{
	internal unsafe static HRESULT GetGuid(this IRecordInfo.Interface @this, out Guid pguid)
	{
		fixed (Guid* pguid2 = &pguid)
		{
			return @this.GetGuid(pguid2);
		}
	}

	internal unsafe static HRESULT GetName(this IRecordInfo.Interface @this, out SysFreeStringSafeHandle pbstrName)
	{
		BSTR bSTR = default;
		HRESULT name = @this.GetName(&bSTR);
		pbstrName = new SysFreeStringSafeHandle(bSTR);
		return name;
	}

	internal unsafe static HRESULT GetSize(this IRecordInfo.Interface @this, out uint pcbSize)
	{
		fixed (uint* pcbSize2 = &pcbSize)
		{
			return @this.GetSize(pcbSize2);
		}
	}

	internal unsafe static HRESULT GetField(this IRecordInfo.Interface @this, void* pvData, string szFieldName, out VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return @this.GetField(pvData, ptr, pvarField2);
			}
		}
	}

	internal unsafe static HRESULT GetFieldNoCopy(this IRecordInfo.Interface @this, void* pvData, string szFieldName, out VARIANT pvarField, out void* ppvDataCArray)
	{
		fixed (void** ppvDataCArray2 = &ppvDataCArray)
		{
			fixed (VARIANT* pvarField2 = &pvarField)
			{
				fixed (char* ptr = szFieldName)
				{
					return @this.GetFieldNoCopy(pvData, ptr, pvarField2, ppvDataCArray2);
				}
			}
		}
	}

	internal unsafe static HRESULT PutField(this IRecordInfo.Interface @this, uint wFlags, void* pvData, string szFieldName, in VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return @this.PutField(wFlags, pvData, ptr, pvarField2);
			}
		}
	}

	internal unsafe static HRESULT PutFieldNoCopy(this IRecordInfo.Interface @this, uint wFlags, void* pvData, string szFieldName, in VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return @this.PutFieldNoCopy(wFlags, pvData, ptr, pvarField2);
			}
		}
	}

	internal unsafe static HRESULT GetFieldNames(this IRecordInfo.Interface @this, ref uint pcNames, out SysFreeStringSafeHandle rgBstrNames)
	{
		fixed (uint* pcNames2 = &pcNames)
		{
			BSTR bSTR = default;
			HRESULT fieldNames = @this.GetFieldNames(pcNames2, &bSTR);
			rgBstrNames = new SysFreeStringSafeHandle(bSTR);
			return fieldNames;
		}
	}

	internal unsafe static HRESULT RecordCreateCopy(this IRecordInfo.Interface @this, void* pvSource, out void* ppvDest)
	{
		fixed (void** ppvDest2 = &ppvDest)
		{
			return @this.RecordCreateCopy(pvSource, ppvDest2);
		}
	}
}
