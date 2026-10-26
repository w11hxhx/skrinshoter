using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.WinRT;

[Guid("AF86E2E0-B12D-4C6A-9C5A-D7AA65101E90")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IInspectable
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, uint*, Guid**, HRESULT> GetIids_4;

		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, HSTRING*, HRESULT> GetRuntimeClassName_5;

		internal unsafe delegate* unmanaged[Stdcall]<IInspectable*, TrustLevel*, HRESULT> GetTrustLevel_6;
	}

	[ComImport]
	[Guid("AF86E2E0-B12D-4C6A-9C5A-D7AA65101E90")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetIids(uint* iidCount, Guid** iids);

		[PreserveSig]
		unsafe HRESULT GetRuntimeClassName(HSTRING* className);

		[PreserveSig]
		unsafe HRESULT GetTrustLevel(TrustLevel* trustLevel);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2944852704u, 45357, 19562, 156, 90, 215, 170, 101, 16, 30, 144);

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
		return ((delegate* unmanaged[Stdcall]<IInspectable*, Guid*, void**, HRESULT>)(*lpVtbl))((IInspectable*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IInspectable*, uint>)lpVtbl[1])((IInspectable*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IInspectable*, uint>)lpVtbl[2])((IInspectable*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetIids(out uint iidCount, out Guid* iids)
	{
		fixed (Guid** iids2 = &iids)
		{
			fixed (uint* iidCount2 = &iidCount)
			{
				return GetIids(iidCount2, iids2);
			}
		}
	}

	public unsafe HRESULT GetIids(uint* iidCount, Guid** iids)
	{
		return ((delegate* unmanaged[Stdcall]<IInspectable*, uint*, Guid**, HRESULT>)lpVtbl[3])((IInspectable*)Unsafe.AsPointer(ref this), iidCount, iids);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetRuntimeClassName(out WindowsDeleteStringSafeHandle className)
	{
		HSTRING hSTRING = default;
		HRESULT runtimeClassName = GetRuntimeClassName(&hSTRING);
		className = new WindowsDeleteStringSafeHandle(hSTRING);
		return runtimeClassName;
	}

	public unsafe HRESULT GetRuntimeClassName(HSTRING* className)
	{
		return ((delegate* unmanaged[Stdcall]<IInspectable*, HSTRING*, HRESULT>)lpVtbl[4])((IInspectable*)Unsafe.AsPointer(ref this), className);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetTrustLevel(out TrustLevel trustLevel)
	{
		fixed (TrustLevel* trustLevel2 = &trustLevel)
		{
			return GetTrustLevel(trustLevel2);
		}
	}

	public unsafe HRESULT GetTrustLevel(TrustLevel* trustLevel)
	{
		return ((delegate* unmanaged[Stdcall]<IInspectable*, TrustLevel*, HRESULT>)lpVtbl[5])((IInspectable*)Unsafe.AsPointer(ref this), trustLevel);
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
