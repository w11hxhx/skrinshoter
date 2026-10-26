using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("035F3AB4-482E-4E50-B41F-8A7F8BD8960B")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIResource
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT> GetDevice_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, HANDLE*, HRESULT> GetSharedHandle_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_USAGE*, HRESULT> GetUsage_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_RESOURCE_PRIORITY, HRESULT> SetEvictionPriority_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_RESOURCE_PRIORITY*, HRESULT> GetEvictionPriority_12;
	}

	[ComImport]
	[Guid("035F3AB4-482E-4E50-B41F-8A7F8BD8960B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IDXGIDeviceSubObject.Interface, IDXGIObject.Interface
	{
		[PreserveSig]
		new unsafe HRESULT SetPrivateData(Guid* Name, uint DataSize, void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateDataInterface(Guid* Name, [Optional] IUnknown* pUnknown);

		[PreserveSig]
		new unsafe HRESULT GetPrivateData(Guid* Name, uint* pDataSize, void* pData);

		[PreserveSig]
		new unsafe HRESULT GetParent(Guid* riid, void** ppParent);

		[PreserveSig]
		new unsafe HRESULT GetDevice(Guid* riid, void** ppDevice);

		[PreserveSig]
		unsafe HRESULT GetSharedHandle(HANDLE* pSharedHandle);

		[PreserveSig]
		unsafe HRESULT GetUsage(DXGI_USAGE* pUsage);

		[PreserveSig]
		HRESULT SetEvictionPriority(DXGI_RESOURCE_PRIORITY EvictionPriority);

		[PreserveSig]
		unsafe HRESULT GetEvictionPriority(DXGI_RESOURCE_PRIORITY* pEvictionPriority);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(56572596, 18478, 20048, 180, 31, 138, 127, 139, 216, 150, 11);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint>)lpVtbl[1])((IDXGIResource*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, uint>)lpVtbl[2])((IDXGIResource*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateData(in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return SetPrivateData(name, DataSize, pData);
		}
	}

	public unsafe HRESULT SetPrivateData(Guid* Name, uint DataSize, void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIResource*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateDataInterface(in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return SetPrivateDataInterface(name, pUnknown);
		}
	}

	public unsafe HRESULT SetPrivateDataInterface(Guid* Name, [Optional] IUnknown* pUnknown)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIResource*)Unsafe.AsPointer(ref this), Name, pUnknown);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetPrivateData(in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	public unsafe HRESULT GetPrivateData(Guid* Name, uint* pDataSize, void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIResource*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetParent(in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return GetParent(riid2, ppParent2);
			}
		}
	}

	public unsafe HRESULT GetParent(Guid* riid, void** ppParent)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDevice(in Guid riid, out void* ppDevice)
	{
		fixed (void** ppDevice2 = &ppDevice)
		{
			fixed (Guid* riid2 = &riid)
			{
				return GetDevice(riid2, ppDevice2);
			}
		}
	}

	public unsafe HRESULT GetDevice(Guid* riid, void** ppDevice)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, Guid*, void**, HRESULT>)lpVtbl[7])((IDXGIResource*)Unsafe.AsPointer(ref this), riid, ppDevice);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetSharedHandle(out SafeFileHandle pSharedHandle)
	{
		HANDLE hANDLE = default;
		HRESULT sharedHandle = GetSharedHandle(&hANDLE);
		pSharedHandle = new SafeFileHandle(hANDLE, ownsHandle: true);
		return sharedHandle;
	}

	public unsafe HRESULT GetSharedHandle(HANDLE* pSharedHandle)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, HANDLE*, HRESULT>)lpVtbl[8])((IDXGIResource*)Unsafe.AsPointer(ref this), pSharedHandle);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetUsage(out DXGI_USAGE pUsage)
	{
		fixed (DXGI_USAGE* pUsage2 = &pUsage)
		{
			return GetUsage(pUsage2);
		}
	}

	public unsafe HRESULT GetUsage(DXGI_USAGE* pUsage)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_USAGE*, HRESULT>)lpVtbl[9])((IDXGIResource*)Unsafe.AsPointer(ref this), pUsage);
	}

	public unsafe HRESULT SetEvictionPriority(DXGI_RESOURCE_PRIORITY EvictionPriority)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_RESOURCE_PRIORITY, HRESULT>)lpVtbl[10])((IDXGIResource*)Unsafe.AsPointer(ref this), EvictionPriority);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetEvictionPriority(out DXGI_RESOURCE_PRIORITY pEvictionPriority)
	{
		fixed (DXGI_RESOURCE_PRIORITY* pEvictionPriority2 = &pEvictionPriority)
		{
			return GetEvictionPriority(pEvictionPriority2);
		}
	}

	public unsafe HRESULT GetEvictionPriority(DXGI_RESOURCE_PRIORITY* pEvictionPriority)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIResource*, DXGI_RESOURCE_PRIORITY*, HRESULT>)lpVtbl[11])((IDXGIResource*)Unsafe.AsPointer(ref this), pEvictionPriority);
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
