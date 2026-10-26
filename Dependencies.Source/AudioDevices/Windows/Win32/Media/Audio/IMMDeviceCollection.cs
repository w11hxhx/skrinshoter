using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMMDeviceCollection
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceCollection*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint*, HRESULT> GetCount_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint, IMMDevice**, HRESULT> Item_5;
	}

	[ComImport]
	[Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetCount(uint* pcDevices);

		[PreserveSig]
		unsafe HRESULT Item(uint nDevice, IMMDevice** ppDevice);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(198681022, 31258, 17627, 131, 151, 204, 83, 146, 56, 123, 94);

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
		return ((delegate* unmanaged[Stdcall]<IMMDeviceCollection*, Guid*, void**, HRESULT>)(*lpVtbl))((IMMDeviceCollection*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint>)lpVtbl[1])((IMMDeviceCollection*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint>)lpVtbl[2])((IMMDeviceCollection*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCount(out uint pcDevices)
	{
		fixed (uint* pcDevices2 = &pcDevices)
		{
			return GetCount(pcDevices2);
		}
	}

	public unsafe HRESULT GetCount(uint* pcDevices)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint*, HRESULT>)lpVtbl[3])((IMMDeviceCollection*)Unsafe.AsPointer(ref this), pcDevices);
	}

	public unsafe HRESULT Item(uint nDevice, IMMDevice** ppDevice)
	{
		return ((delegate* unmanaged[Stdcall]<IMMDeviceCollection*, uint, IMMDevice**, HRESULT>)lpVtbl[4])((IMMDeviceCollection*)Unsafe.AsPointer(ref this), nDevice, ppDevice);
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
