using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMMDeviceEnumerator
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, EDataFlow, DEVICE_STATE, IMMDeviceCollection**, HRESULT> EnumAudioEndpoints_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, EDataFlow, ERole, IMMDevice**, HRESULT> GetDefaultAudioEndpoint_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, PCWSTR, IMMDevice**, HRESULT> GetDevice_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, IMMNotificationClient*, HRESULT> RegisterEndpointNotificationCallback_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, IMMNotificationClient*, HRESULT> UnregisterEndpointNotificationCallback_8;
	}

	[ComImport]
	[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT EnumAudioEndpoints(EDataFlow dataFlow, DEVICE_STATE dwStateMask, IMMDeviceCollection** ppDevices);

		[PreserveSig]
		unsafe HRESULT GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, IMMDevice** ppEndpoint);

		[PreserveSig]
		unsafe HRESULT GetDevice(PCWSTR pwstrId, IMMDevice** ppDevice);

		[PreserveSig]
		unsafe HRESULT RegisterEndpointNotificationCallback(IMMNotificationClient* pClient);

		[PreserveSig]
		unsafe HRESULT UnregisterEndpointNotificationCallback(IMMNotificationClient* pClient);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2841011410u, 38420, 20277, 167, 70, 222, 141, 182, 54, 23, 230);

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
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, Guid*, void**, HRESULT>)(*lpVtbl))((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, uint>)lpVtbl[1])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, uint>)lpVtbl[2])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT EnumAudioEndpoints(EDataFlow dataFlow, DEVICE_STATE dwStateMask, IMMDeviceCollection** ppDevices)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, EDataFlow, DEVICE_STATE, IMMDeviceCollection**, HRESULT>)lpVtbl[3])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), dataFlow, dwStateMask, ppDevices);
	}

	public unsafe HRESULT GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, IMMDevice** ppEndpoint)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, EDataFlow, ERole, IMMDevice**, HRESULT>)lpVtbl[4])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), dataFlow, role, ppEndpoint);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDevice(string pwstrId, IMMDevice** ppDevice)
	{
		fixed (char* ptr = pwstrId)
		{
			return GetDevice(ptr, ppDevice);
		}
	}

	public unsafe HRESULT GetDevice(PCWSTR pwstrId, IMMDevice** ppDevice)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, PCWSTR, IMMDevice**, HRESULT>)lpVtbl[5])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), pwstrId, ppDevice);
	}

	public unsafe HRESULT RegisterEndpointNotificationCallback(IMMNotificationClient* pClient)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, IMMNotificationClient*, HRESULT>)lpVtbl[6])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), pClient);
	}

	public unsafe HRESULT UnregisterEndpointNotificationCallback(IMMNotificationClient* pClient)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceEnumerator*, IMMNotificationClient*, HRESULT>)lpVtbl[7])((IMMDeviceEnumerator*)Unsafe.AsPointer(ref this), pClient);
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
