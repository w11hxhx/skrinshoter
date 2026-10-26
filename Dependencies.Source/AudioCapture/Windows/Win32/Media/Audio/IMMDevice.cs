using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace Windows.Win32.Media.Audio;

[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMMDevice
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, Guid*, CLSCTX, PROPVARIANT*, void**, HRESULT> Activate_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, STGM, IPropertyStore**, HRESULT> OpenPropertyStore_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, PWSTR*, HRESULT> GetId_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDevice*, DEVICE_STATE*, HRESULT> GetState_7;
	}

	[ComImport]
	[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Activate(Guid* iid, CLSCTX dwClsCtx, [Optional] PROPVARIANT* pActivationParams, void** ppInterface);

		[PreserveSig]
		unsafe HRESULT OpenPropertyStore(STGM stgmAccess, IPropertyStore** ppProperties);

		[PreserveSig]
		unsafe HRESULT GetId(PWSTR* ppstrId);

		[PreserveSig]
		unsafe HRESULT GetState(DEVICE_STATE* pdwState);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3597010495u, 5511, 20035, 129, 241, 185, 72, 232, 7, 54, 63);

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
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, Guid*, void**, HRESULT>)(*lpVtbl))((IMMDevice*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, uint>)lpVtbl[1])((IMMDevice*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, uint>)lpVtbl[2])((IMMDevice*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Activate(in Guid iid, CLSCTX dwClsCtx, PROPVARIANT? pActivationParams, out void* ppInterface)
	{
		fixed (void** ppInterface2 = &ppInterface)
		{
			fixed (Guid* iid2 = &iid)
			{
				PROPVARIANT valueOrDefault = pActivationParams.GetValueOrDefault();
				return Activate(iid2, dwClsCtx, pActivationParams.HasValue ? (&valueOrDefault) : null, ppInterface2);
			}
		}
	}

	public unsafe HRESULT Activate(Guid* iid, CLSCTX dwClsCtx, [Optional] PROPVARIANT* pActivationParams, void** ppInterface)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, Guid*, CLSCTX, PROPVARIANT*, void**, HRESULT>)lpVtbl[3])((IMMDevice*)Unsafe.AsPointer(ref this), iid, dwClsCtx, pActivationParams, ppInterface);
	}

	public unsafe HRESULT OpenPropertyStore(STGM stgmAccess, IPropertyStore** ppProperties)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, STGM, IPropertyStore**, HRESULT>)lpVtbl[4])((IMMDevice*)Unsafe.AsPointer(ref this), stgmAccess, ppProperties);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetId(out PWSTR ppstrId)
	{
		fixed (PWSTR* ppstrId2 = &ppstrId)
		{
			return GetId(ppstrId2);
		}
	}

	public unsafe HRESULT GetId(PWSTR* ppstrId)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, PWSTR*, HRESULT>)lpVtbl[5])((IMMDevice*)Unsafe.AsPointer(ref this), ppstrId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetState(out DEVICE_STATE pdwState)
	{
		fixed (DEVICE_STATE* pdwState2 = &pdwState)
		{
			return GetState(pdwState2);
		}
	}

	public unsafe HRESULT GetState(DEVICE_STATE* pdwState)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDevice*, DEVICE_STATE*, HRESULT>)lpVtbl[6])((IMMDevice*)Unsafe.AsPointer(ref this), pdwState);
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
