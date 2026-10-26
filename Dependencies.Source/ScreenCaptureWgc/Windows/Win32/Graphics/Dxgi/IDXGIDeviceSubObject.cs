using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("3D3E0379-F9DE-4D58-BB6C-18D62992F1A6")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIDeviceSubObject
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT> GetDevice_8;
	}

	[ComImport]
	[Guid("3D3E0379-F9DE-4D58-BB6C-18D62992F1A6")]
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
		unsafe HRESULT GetDevice(Guid* riid, void** ppDevice);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1027474297u, 63966, 19800, 187, 108, 24, 214, 41, 146, 241, 166);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint>)lpVtbl[1])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, uint>)lpVtbl[2])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppParent);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIDeviceSubObject*, Guid*, void**, HRESULT>)lpVtbl[7])((IDXGIDeviceSubObject*)Unsafe.AsPointer(ref this), riid, ppDevice);
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
