using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("29038F61-3839-4626-91FD-086879011A05")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIAdapter1
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint, IDXGIOutput**, HRESULT> EnumOutputs_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC*, HRESULT> GetDesc_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, long*, HRESULT> CheckInterfaceSupport_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC1*, HRESULT> GetDesc1_11;
	}

	[ComImport]
	[Guid("29038F61-3839-4626-91FD-086879011A05")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IDXGIAdapter.Interface, IDXGIObject.Interface
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
		new unsafe HRESULT EnumOutputs(uint Output, IDXGIOutput** ppOutput);

		[PreserveSig]
		new unsafe HRESULT GetDesc(DXGI_ADAPTER_DESC* pDesc);

		[PreserveSig]
		new unsafe HRESULT CheckInterfaceSupport(Guid* InterfaceName, long* pUMDVersion);

		[PreserveSig]
		unsafe HRESULT GetDesc1(DXGI_ADAPTER_DESC1* pDesc);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(688099169, 14393, 17958, 145, 253, 8, 104, 121, 1, 26, 5);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIAdapter1*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint>)lpVtbl[1])((IDXGIAdapter1*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint>)lpVtbl[2])((IDXGIAdapter1*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	public unsafe HRESULT EnumOutputs(uint Output, IDXGIOutput** ppOutput)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, uint, IDXGIOutput**, HRESULT>)lpVtbl[7])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), Output, ppOutput);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDesc(out DXGI_ADAPTER_DESC pDesc)
	{
		fixed (DXGI_ADAPTER_DESC* pDesc2 = &pDesc)
		{
			return GetDesc(pDesc2);
		}
	}

	public unsafe HRESULT GetDesc(DXGI_ADAPTER_DESC* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC*, HRESULT>)lpVtbl[8])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CheckInterfaceSupport(in Guid InterfaceName, out long pUMDVersion)
	{
		fixed (long* pUMDVersion2 = &pUMDVersion)
		{
			fixed (Guid* interfaceName = &InterfaceName)
			{
				return CheckInterfaceSupport(interfaceName, pUMDVersion2);
			}
		}
	}

	public unsafe HRESULT CheckInterfaceSupport(Guid* InterfaceName, long* pUMDVersion)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, Guid*, long*, HRESULT>)lpVtbl[9])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDesc1(out DXGI_ADAPTER_DESC1 pDesc)
	{
		fixed (DXGI_ADAPTER_DESC1* pDesc2 = &pDesc)
		{
			return GetDesc1(pDesc2);
		}
	}

	public unsafe HRESULT GetDesc1(DXGI_ADAPTER_DESC1* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter1*, DXGI_ADAPTER_DESC1*, HRESULT>)lpVtbl[10])((IDXGIAdapter1*)Unsafe.AsPointer(ref this), pDesc);
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
