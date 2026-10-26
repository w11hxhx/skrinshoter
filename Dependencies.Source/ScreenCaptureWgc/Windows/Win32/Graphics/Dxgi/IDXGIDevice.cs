using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("54EC77FA-1377-44E6-8C32-88FD5F44C84C")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIDevice
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, IDXGIAdapter**, HRESULT> GetAdapter_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, DXGI_SURFACE_DESC*, uint, DXGI_USAGE, DXGI_SHARED_RESOURCE*, IDXGISurface**, HRESULT> CreateSurface_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, IUnknown**, DXGI_RESIDENCY*, uint, HRESULT> QueryResourceResidency_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, int, HRESULT> SetGPUThreadPriority_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDevice*, int*, HRESULT> GetGPUThreadPriority_12;
	}

	[ComImport]
	[Guid("54EC77FA-1377-44E6-8C32-88FD5F44C84C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IDXGIObject.Interface
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
		unsafe HRESULT GetAdapter(IDXGIAdapter** pAdapter);

		[PreserveSig]
		unsafe HRESULT CreateSurface(DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, DXGI_USAGE Usage, [Optional] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface);

		[PreserveSig]
		unsafe HRESULT QueryResourceResidency(IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources);

		[PreserveSig]
		HRESULT SetGPUThreadPriority(int Priority);

		[PreserveSig]
		unsafe HRESULT GetGPUThreadPriority(int* pPriority);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1424783354, 4983, 17638, 140, 50, 136, 253, 95, 68, 200, 76);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIDevice*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, uint>)lpVtbl[1])((IDXGIDevice*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, uint>)lpVtbl[2])((IDXGIDevice*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIDevice*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIDevice*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	public unsafe HRESULT GetAdapter(IDXGIAdapter** pAdapter)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, IDXGIAdapter**, HRESULT>)lpVtbl[7])((IDXGIDevice*)Unsafe.AsPointer(ref this), pAdapter);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateSurface(in DXGI_SURFACE_DESC pDesc, uint NumSurfaces, DXGI_USAGE Usage, DXGI_SHARED_RESOURCE? pSharedResource, IDXGISurface** ppSurface)
	{
		fixed (DXGI_SURFACE_DESC* pDesc2 = &pDesc)
		{
			DXGI_SHARED_RESOURCE valueOrDefault = pSharedResource.GetValueOrDefault();
			return CreateSurface(pDesc2, NumSurfaces, Usage, pSharedResource.HasValue ? (&valueOrDefault) : null, ppSurface);
		}
	}

	public unsafe HRESULT CreateSurface(DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, DXGI_USAGE Usage, [Optional] DXGI_SHARED_RESOURCE* pSharedResource, IDXGISurface** ppSurface)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, DXGI_SURFACE_DESC*, uint, DXGI_USAGE, DXGI_SHARED_RESOURCE*, IDXGISurface**, HRESULT>)lpVtbl[8])((IDXGIDevice*)Unsafe.AsPointer(ref this), pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);
	}

	internal unsafe HRESULT QueryResourceResidency(IUnknown** ppResources, Span<DXGI_RESIDENCY> pResidencyStatus)
	{
		fixed (DXGI_RESIDENCY* pResidencyStatus2 = pResidencyStatus)
		{
			return QueryResourceResidency(ppResources, pResidencyStatus2, (uint)pResidencyStatus.Length);
		}
	}

	public unsafe HRESULT QueryResourceResidency(IUnknown** ppResources, DXGI_RESIDENCY* pResidencyStatus, uint NumResources)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, IUnknown**, DXGI_RESIDENCY*, uint, HRESULT>)lpVtbl[9])((IDXGIDevice*)Unsafe.AsPointer(ref this), ppResources, pResidencyStatus, NumResources);
	}

	public unsafe HRESULT SetGPUThreadPriority(int Priority)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, int, HRESULT>)lpVtbl[10])((IDXGIDevice*)Unsafe.AsPointer(ref this), Priority);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetGPUThreadPriority(out int pPriority)
	{
		fixed (int* pPriority2 = &pPriority)
		{
			return GetGPUThreadPriority(pPriority2);
		}
	}

	public unsafe HRESULT GetGPUThreadPriority(int* pPriority)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDevice*, int*, HRESULT>)lpVtbl[11])((IDXGIDevice*)Unsafe.AsPointer(ref this), pPriority);
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
