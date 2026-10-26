using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMMNotificationClient
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, DEVICE_STATE, HRESULT> OnDeviceStateChanged_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, HRESULT> OnDeviceAdded_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, HRESULT> OnDeviceRemoved_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, EDataFlow, ERole, PCWSTR, HRESULT> OnDefaultDeviceChanged_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, PROPERTYKEY, HRESULT> OnPropertyValueChanged_8;
	}

	[ComImport]
	[Guid("7991EEC9-7E89-4D85-8390-6C703CEC60C0")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		HRESULT OnDeviceStateChanged(PCWSTR pwstrDeviceId, DEVICE_STATE dwNewState);

		[PreserveSig]
		HRESULT OnDeviceAdded(PCWSTR pwstrDeviceId);

		[PreserveSig]
		HRESULT OnDeviceRemoved(PCWSTR pwstrDeviceId);

		[PreserveSig]
		HRESULT OnDefaultDeviceChanged(EDataFlow flow, ERole role, PCWSTR pwstrDefaultDeviceId);

		[PreserveSig]
		HRESULT OnPropertyValueChanged(PCWSTR pwstrDeviceId, PROPERTYKEY key);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2039606985, 32393, 19845, 131, 144, 108, 112, 60, 236, 96, 192);

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
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, Guid*, void**, HRESULT>)(*lpVtbl))((IMMNotificationClient*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, uint>)lpVtbl[1])((IMMNotificationClient*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, uint>)lpVtbl[2])((IMMNotificationClient*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OnDeviceStateChanged(string pwstrDeviceId, DEVICE_STATE dwNewState)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return OnDeviceStateChanged(ptr, dwNewState);
		}
	}

	public unsafe HRESULT OnDeviceStateChanged(PCWSTR pwstrDeviceId, DEVICE_STATE dwNewState)
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, DEVICE_STATE, HRESULT>)lpVtbl[3])((IMMNotificationClient*)Unsafe.AsPointer(ref this), pwstrDeviceId, dwNewState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OnDeviceAdded(string pwstrDeviceId)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return OnDeviceAdded(ptr);
		}
	}

	public unsafe HRESULT OnDeviceAdded(PCWSTR pwstrDeviceId)
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, HRESULT>)lpVtbl[4])((IMMNotificationClient*)Unsafe.AsPointer(ref this), pwstrDeviceId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OnDeviceRemoved(string pwstrDeviceId)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return OnDeviceRemoved(ptr);
		}
	}

	public unsafe HRESULT OnDeviceRemoved(PCWSTR pwstrDeviceId)
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, HRESULT>)lpVtbl[5])((IMMNotificationClient*)Unsafe.AsPointer(ref this), pwstrDeviceId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OnDefaultDeviceChanged(EDataFlow flow, ERole role, string pwstrDefaultDeviceId)
	{
		fixed (char* ptr = pwstrDefaultDeviceId)
		{
			return OnDefaultDeviceChanged(flow, role, ptr);
		}
	}

	public unsafe HRESULT OnDefaultDeviceChanged(EDataFlow flow, ERole role, PCWSTR pwstrDefaultDeviceId)
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, EDataFlow, ERole, PCWSTR, HRESULT>)lpVtbl[6])((IMMNotificationClient*)Unsafe.AsPointer(ref this), flow, role, pwstrDefaultDeviceId);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OnPropertyValueChanged(string pwstrDeviceId, PROPERTYKEY key)
	{
		fixed (char* ptr = pwstrDeviceId)
		{
			return OnPropertyValueChanged(ptr, key);
		}
	}

	public unsafe HRESULT OnPropertyValueChanged(PCWSTR pwstrDeviceId, PROPERTYKEY key)
	{
		return ((delegate* unmanaged[Stdcall]<IMMNotificationClient*, PCWSTR, PROPERTYKEY, HRESULT>)lpVtbl[7])((IMMNotificationClient*)Unsafe.AsPointer(ref this), pwstrDeviceId, key);
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
