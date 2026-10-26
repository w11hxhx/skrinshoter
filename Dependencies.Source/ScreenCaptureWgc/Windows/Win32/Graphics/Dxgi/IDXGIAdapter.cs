using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("2411E7E1-12AC-4CCF-BD14-9798E8534DC0")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIAdapter
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint, IDXGIOutput**, HRESULT> EnumOutputs_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, DXGI_ADAPTER_DESC*, HRESULT> GetDesc_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, long*, HRESULT> CheckInterfaceSupport_10;
	}

	[ComImport]
	[Guid("2411E7E1-12AC-4CCF-BD14-9798E8534DC0")]
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
		unsafe HRESULT EnumOutputs(uint Output, IDXGIOutput** ppOutput);

		[PreserveSig]
		unsafe HRESULT GetDesc(DXGI_ADAPTER_DESC* pDesc);

		[PreserveSig]
		unsafe HRESULT CheckInterfaceSupport(Guid* InterfaceName, long* pUMDVersion);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(605153249, 4780, 19663, 189, 20, 151, 152, 232, 83, 77, 192);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIAdapter*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint>)lpVtbl[1])((IDXGIAdapter*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint>)lpVtbl[2])((IDXGIAdapter*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIAdapter*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIAdapter*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	public unsafe HRESULT EnumOutputs(uint Output, IDXGIOutput** ppOutput)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, uint, IDXGIOutput**, HRESULT>)lpVtbl[7])((IDXGIAdapter*)Unsafe.AsPointer(ref this), Output, ppOutput);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, DXGI_ADAPTER_DESC*, HRESULT>)lpVtbl[8])((IDXGIAdapter*)Unsafe.AsPointer(ref this), pDesc);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIAdapter*, Guid*, long*, HRESULT>)lpVtbl[9])((IDXGIAdapter*)Unsafe.AsPointer(ref this), InterfaceName, pUMDVersion);
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
