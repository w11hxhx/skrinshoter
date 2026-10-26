using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Variant;

namespace Windows.Win32.System.Com;

[Guid("00020400-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDispatch
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, uint*, HRESULT> GetTypeInfoCount_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, uint, uint, ITypeInfo**, HRESULT> GetTypeInfo_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, Guid*, PWSTR*, uint, uint, int*, HRESULT> GetIDsOfNames_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDispatch*, int, Guid*, uint, DISPATCH_FLAGS, DISPPARAMS*, VARIANT*, EXCEPINFO*, uint*, HRESULT> Invoke_7;
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(132096, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IDispatch*, Guid*, void**, HRESULT>)(*lpVtbl))((IDispatch*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, uint>)lpVtbl[1])((IDispatch*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, uint>)lpVtbl[2])((IDispatch*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetTypeInfoCount(out uint pctinfo)
	{
		fixed (uint* pctinfo2 = &pctinfo)
		{
			return GetTypeInfoCount(pctinfo2);
		}
	}

	public unsafe HRESULT GetTypeInfoCount(uint* pctinfo)
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, uint*, HRESULT>)lpVtbl[3])((IDispatch*)Unsafe.AsPointer(ref this), pctinfo);
	}

	public unsafe HRESULT GetTypeInfo(uint iTInfo, uint lcid, ITypeInfo** ppTInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, uint, uint, ITypeInfo**, HRESULT>)lpVtbl[4])((IDispatch*)Unsafe.AsPointer(ref this), iTInfo, lcid, ppTInfo);
	}

	internal unsafe HRESULT GetIDsOfNames(in Guid riid, ReadOnlySpan<PWSTR> rgszNames, uint lcid, Span<int> rgDispId)
	{
		fixed (int* rgDispId2 = rgDispId)
		{
			fixed (PWSTR* rgszNames2 = rgszNames)
			{
				fixed (Guid* riid2 = &riid)
				{
					if (rgszNames.Length != rgDispId.Length)
					{
						throw new ArgumentException();
					}
					return GetIDsOfNames(riid2, rgszNames2, (uint)rgDispId.Length, lcid, rgDispId2);
				}
			}
		}
	}

	public unsafe HRESULT GetIDsOfNames(Guid* riid, PWSTR* rgszNames, uint cNames, uint lcid, int* rgDispId)
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, Guid*, PWSTR*, uint, uint, int*, HRESULT>)lpVtbl[5])((IDispatch*)Unsafe.AsPointer(ref this), riid, rgszNames, cNames, lcid, rgDispId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Invoke(int dispIdMember, in Guid riid, uint lcid, DISPATCH_FLAGS wFlags, in DISPPARAMS pDispParams, VARIANT* pVarResult, EXCEPINFO* pExcepInfo, uint* puArgErr)
	{
		fixed (DISPPARAMS* pDispParams2 = &pDispParams)
		{
			fixed (Guid* riid2 = &riid)
			{
				return Invoke(dispIdMember, riid2, lcid, wFlags, pDispParams2, pVarResult, pExcepInfo, puArgErr);
			}
		}
	}

	public unsafe HRESULT Invoke(int dispIdMember, Guid* riid, uint lcid, DISPATCH_FLAGS wFlags, DISPPARAMS* pDispParams, [Optional] VARIANT* pVarResult, [Optional] EXCEPINFO* pExcepInfo, [Optional] uint* puArgErr)
	{
		return ((delegate* unmanaged[Stdcall]<IDispatch*, int, Guid*, uint, DISPATCH_FLAGS, DISPPARAMS*, VARIANT*, EXCEPINFO*, uint*, HRESULT>)lpVtbl[6])((IDispatch*)Unsafe.AsPointer(ref this), dispIdMember, riid, lcid, wFlags, pDispParams, pVarResult, pExcepInfo, puArgErr);
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
