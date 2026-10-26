using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("CAFCB56C-6AC3-4889-BF47-9E23BBD260EC")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGISurface
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT> GetDevice_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_SURFACE_DESC*, HRESULT> GetDesc_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_MAPPED_RECT*, DXGI_MAP_FLAGS, HRESULT> Map_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISurface*, HRESULT> Unmap_11;
	}

	[ComImport]
	[Guid("CAFCB56C-6AC3-4889-BF47-9E23BBD260EC")]
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
		unsafe HRESULT GetDesc(DXGI_SURFACE_DESC* pDesc);

		[PreserveSig]
		unsafe HRESULT Map(DXGI_MAPPED_RECT* pLockedRect, DXGI_MAP_FLAGS MapFlags);

		[PreserveSig]
		HRESULT Unmap();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3405559148u, 27331, 18569, 191, 71, 158, 35, 187, 210, 96, 236);

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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, uint>)lpVtbl[1])((IDXGISurface*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, uint>)lpVtbl[2])((IDXGISurface*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGISurface*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGISurface*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGISurface*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppParent);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, Guid*, void**, HRESULT>)lpVtbl[7])((IDXGISurface*)Unsafe.AsPointer(ref this), riid, ppDevice);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDesc(out DXGI_SURFACE_DESC pDesc)
	{
		fixed (DXGI_SURFACE_DESC* pDesc2 = &pDesc)
		{
			return GetDesc(pDesc2);
		}
	}

	public unsafe HRESULT GetDesc(DXGI_SURFACE_DESC* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_SURFACE_DESC*, HRESULT>)lpVtbl[8])((IDXGISurface*)Unsafe.AsPointer(ref this), pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Map(out DXGI_MAPPED_RECT pLockedRect, DXGI_MAP_FLAGS MapFlags)
	{
		fixed (DXGI_MAPPED_RECT* pLockedRect2 = &pLockedRect)
		{
			return Map(pLockedRect2, MapFlags);
		}
	}

	public unsafe HRESULT Map(DXGI_MAPPED_RECT* pLockedRect, DXGI_MAP_FLAGS MapFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, DXGI_MAPPED_RECT*, DXGI_MAP_FLAGS, HRESULT>)lpVtbl[9])((IDXGISurface*)Unsafe.AsPointer(ref this), pLockedRect, MapFlags);
	}

	public unsafe HRESULT Unmap()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISurface*, HRESULT>)lpVtbl[10])((IDXGISurface*)Unsafe.AsPointer(ref this));
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
