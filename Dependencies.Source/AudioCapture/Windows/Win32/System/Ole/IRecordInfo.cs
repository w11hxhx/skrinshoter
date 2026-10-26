using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Ole;

[Guid("0000002F-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IRecordInfo
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT> RecordInit_4;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT> RecordClear_5;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void*, HRESULT> RecordCopy_6;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, HRESULT> GetGuid_7;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, BSTR*, HRESULT> GetName_8;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, HRESULT> GetSize_9;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, ITypeInfo**, HRESULT> GetTypeInfo_10;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, PCWSTR, VARIANT*, HRESULT> GetField_11;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, PCWSTR, VARIANT*, void**, HRESULT> GetFieldNoCopy_12;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, PCWSTR, VARIANT*, HRESULT> PutField_13;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, PCWSTR, VARIANT*, HRESULT> PutFieldNoCopy_14;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, BSTR*, HRESULT> GetFieldNames_15;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, IRecordInfo*, BOOL> IsMatchingType_16;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*> RecordCreate_17;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void**, HRESULT> RecordCreateCopy_18;

		internal unsafe delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT> RecordDestroy_19;
	}

	[ComImport]
	[Guid("0000002F-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT RecordInit(void* pvNew);

		[PreserveSig]
		unsafe HRESULT RecordClear(void* pvExisting);

		[PreserveSig]
		unsafe HRESULT RecordCopy(void* pvExisting, void* pvNew);

		[PreserveSig]
		unsafe HRESULT GetGuid(Guid* pguid);

		[PreserveSig]
		unsafe HRESULT GetName(BSTR* pbstrName);

		[PreserveSig]
		unsafe HRESULT GetSize(uint* pcbSize);

		[PreserveSig]
		unsafe HRESULT GetTypeInfo(ITypeInfo** ppTypeInfo);

		[PreserveSig]
		unsafe HRESULT GetField(void* pvData, PCWSTR szFieldName, VARIANT* pvarField);

		[PreserveSig]
		unsafe HRESULT GetFieldNoCopy(void* pvData, PCWSTR szFieldName, VARIANT* pvarField, void** ppvDataCArray);

		[PreserveSig]
		unsafe HRESULT PutField(uint wFlags, void* pvData, PCWSTR szFieldName, VARIANT* pvarField);

		[PreserveSig]
		unsafe HRESULT PutFieldNoCopy(uint wFlags, void* pvData, PCWSTR szFieldName, VARIANT* pvarField);

		[PreserveSig]
		unsafe HRESULT GetFieldNames(uint* pcNames, BSTR* rgBstrNames);

		[PreserveSig]
		unsafe BOOL IsMatchingType(IRecordInfo* pRecordInfo);

		[PreserveSig]
		unsafe void* RecordCreate();

		[PreserveSig]
		unsafe HRESULT RecordCreateCopy(void* pvSource, void** ppvDest);

		[PreserveSig]
		unsafe HRESULT RecordDestroy(void* pvRecord);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(47, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, void**, HRESULT>)(*lpVtbl))((IRecordInfo*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint>)lpVtbl[1])((IRecordInfo*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint>)lpVtbl[2])((IRecordInfo*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT RecordInit(void* pvNew)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT>)lpVtbl[3])((IRecordInfo*)Unsafe.AsPointer(ref this), pvNew);
	}

	public unsafe HRESULT RecordClear(void* pvExisting)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT>)lpVtbl[4])((IRecordInfo*)Unsafe.AsPointer(ref this), pvExisting);
	}

	public unsafe HRESULT RecordCopy(void* pvExisting, void* pvNew)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void*, HRESULT>)lpVtbl[5])((IRecordInfo*)Unsafe.AsPointer(ref this), pvExisting, pvNew);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetGuid(out Guid pguid)
	{
		fixed (Guid* pguid2 = &pguid)
		{
			return GetGuid(pguid2);
		}
	}

	public unsafe HRESULT GetGuid(Guid* pguid)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, Guid*, HRESULT>)lpVtbl[6])((IRecordInfo*)Unsafe.AsPointer(ref this), pguid);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetName(out SysFreeStringSafeHandle pbstrName)
	{
		BSTR bSTR = default;
		HRESULT name = GetName(&bSTR);
		pbstrName = new SysFreeStringSafeHandle(bSTR);
		return name;
	}

	public unsafe HRESULT GetName(BSTR* pbstrName)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, BSTR*, HRESULT>)lpVtbl[7])((IRecordInfo*)Unsafe.AsPointer(ref this), pbstrName);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetSize(out uint pcbSize)
	{
		fixed (uint* pcbSize2 = &pcbSize)
		{
			return GetSize(pcbSize2);
		}
	}

	public unsafe HRESULT GetSize(uint* pcbSize)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, HRESULT>)lpVtbl[8])((IRecordInfo*)Unsafe.AsPointer(ref this), pcbSize);
	}

	public unsafe HRESULT GetTypeInfo(ITypeInfo** ppTypeInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, ITypeInfo**, HRESULT>)lpVtbl[9])((IRecordInfo*)Unsafe.AsPointer(ref this), ppTypeInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetField(void* pvData, string szFieldName, out VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return GetField(pvData, ptr, pvarField2);
			}
		}
	}

	public unsafe HRESULT GetField(void* pvData, PCWSTR szFieldName, VARIANT* pvarField)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, PCWSTR, VARIANT*, HRESULT>)lpVtbl[10])((IRecordInfo*)Unsafe.AsPointer(ref this), pvData, szFieldName, pvarField);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFieldNoCopy(void* pvData, string szFieldName, out VARIANT pvarField, out void* ppvDataCArray)
	{
		fixed (void** ppvDataCArray2 = &ppvDataCArray)
		{
			fixed (VARIANT* pvarField2 = &pvarField)
			{
				fixed (char* ptr = szFieldName)
				{
					return GetFieldNoCopy(pvData, ptr, pvarField2, ppvDataCArray2);
				}
			}
		}
	}

	public unsafe HRESULT GetFieldNoCopy(void* pvData, PCWSTR szFieldName, VARIANT* pvarField, void** ppvDataCArray)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, PCWSTR, VARIANT*, void**, HRESULT>)lpVtbl[11])((IRecordInfo*)Unsafe.AsPointer(ref this), pvData, szFieldName, pvarField, ppvDataCArray);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT PutField(uint wFlags, void* pvData, string szFieldName, in VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return PutField(wFlags, pvData, ptr, pvarField2);
			}
		}
	}

	public unsafe HRESULT PutField(uint wFlags, void* pvData, PCWSTR szFieldName, VARIANT* pvarField)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, PCWSTR, VARIANT*, HRESULT>)lpVtbl[12])((IRecordInfo*)Unsafe.AsPointer(ref this), wFlags, pvData, szFieldName, pvarField);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT PutFieldNoCopy(uint wFlags, void* pvData, string szFieldName, in VARIANT pvarField)
	{
		fixed (VARIANT* pvarField2 = &pvarField)
		{
			fixed (char* ptr = szFieldName)
			{
				return PutFieldNoCopy(wFlags, pvData, ptr, pvarField2);
			}
		}
	}

	public unsafe HRESULT PutFieldNoCopy(uint wFlags, void* pvData, PCWSTR szFieldName, VARIANT* pvarField)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint, void*, PCWSTR, VARIANT*, HRESULT>)lpVtbl[13])((IRecordInfo*)Unsafe.AsPointer(ref this), wFlags, pvData, szFieldName, pvarField);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFieldNames(ref uint pcNames, out SysFreeStringSafeHandle rgBstrNames)
	{
		fixed (uint* pcNames2 = &pcNames)
		{
			BSTR bSTR = default;
			HRESULT fieldNames = GetFieldNames(pcNames2, &bSTR);
			rgBstrNames = new SysFreeStringSafeHandle(bSTR);
			return fieldNames;
		}
	}

	public unsafe HRESULT GetFieldNames(uint* pcNames, BSTR* rgBstrNames)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, uint*, BSTR*, HRESULT>)lpVtbl[14])((IRecordInfo*)Unsafe.AsPointer(ref this), pcNames, rgBstrNames);
	}

	public unsafe BOOL IsMatchingType(IRecordInfo* pRecordInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, IRecordInfo*, BOOL>)lpVtbl[15])((IRecordInfo*)Unsafe.AsPointer(ref this), pRecordInfo);
	}

	public unsafe void* RecordCreate()
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*>)lpVtbl[16])((IRecordInfo*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT RecordCreateCopy(void* pvSource, out void* ppvDest)
	{
		fixed (void** ppvDest2 = &ppvDest)
		{
			return RecordCreateCopy(pvSource, ppvDest2);
		}
	}

	public unsafe HRESULT RecordCreateCopy(void* pvSource, void** ppvDest)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, void**, HRESULT>)lpVtbl[17])((IRecordInfo*)Unsafe.AsPointer(ref this), pvSource, ppvDest);
	}

	public unsafe HRESULT RecordDestroy(void* pvRecord)
	{
		return ((delegate* unmanaged[Stdcall]<IRecordInfo*, void*, HRESULT>)lpVtbl[18])((IRecordInfo*)Unsafe.AsPointer(ref this), pvRecord);
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
