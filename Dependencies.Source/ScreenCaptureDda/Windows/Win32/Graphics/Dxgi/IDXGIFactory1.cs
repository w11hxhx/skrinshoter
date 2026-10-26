using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("770AAE78-F26F-4DBA-A829-253C83D1B387")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIFactory1
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter**, HRESULT> EnumAdapters_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND, DXGI_MWA_FLAGS, HRESULT> MakeWindowAssociation_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND*, HRESULT> GetWindowAssociation_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, HRESULT> CreateSwapChain_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, HMODULE, IDXGIAdapter**, HRESULT> CreateSoftwareAdapter_12;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter1**, HRESULT> EnumAdapters1_13;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIFactory1*, BOOL> IsCurrent_14;
	}

	[ComImport]
	[Guid("770AAE78-F26F-4DBA-A829-253C83D1B387")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IDXGIFactory.Interface, IDXGIObject.Interface
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
		new unsafe HRESULT EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter);

		[PreserveSig]
		new HRESULT MakeWindowAssociation(HWND WindowHandle, DXGI_MWA_FLAGS Flags);

		[PreserveSig]
		new unsafe HRESULT GetWindowAssociation(HWND* pWindowHandle);

		[PreserveSig]
		new unsafe HRESULT CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain);

		[PreserveSig]
		new unsafe HRESULT CreateSoftwareAdapter(HMODULE Module, IDXGIAdapter** ppAdapter);

		[PreserveSig]
		unsafe HRESULT EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter);

		[PreserveSig]
		BOOL IsCurrent();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1997188728u, 62063, 19898, 168, 41, 37, 60, 131, 209, 179, 135);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIFactory1*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint>)lpVtbl[1])((IDXGIFactory1*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint>)lpVtbl[2])((IDXGIFactory1*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIFactory1*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	public unsafe HRESULT EnumAdapters(uint Adapter, IDXGIAdapter** ppAdapter)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter**, HRESULT>)lpVtbl[7])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
	}

	public unsafe HRESULT MakeWindowAssociation(HWND WindowHandle, DXGI_MWA_FLAGS Flags)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND, DXGI_MWA_FLAGS, HRESULT>)lpVtbl[8])((IDXGIFactory1*)Unsafe.AsPointer(ref this), WindowHandle, Flags);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetWindowAssociation(out HWND pWindowHandle)
	{
		fixed (HWND* pWindowHandle2 = &pWindowHandle)
		{
			return GetWindowAssociation(pWindowHandle2);
		}
	}

	public unsafe HRESULT GetWindowAssociation(HWND* pWindowHandle)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HWND*, HRESULT>)lpVtbl[9])((IDXGIFactory1*)Unsafe.AsPointer(ref this), pWindowHandle);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateSwapChain(IUnknown* pDevice, in DXGI_SWAP_CHAIN_DESC pDesc, IDXGISwapChain** ppSwapChain)
	{
		fixed (DXGI_SWAP_CHAIN_DESC* pDesc2 = &pDesc)
		{
			return CreateSwapChain(pDevice, pDesc2, ppSwapChain);
		}
	}

	public unsafe HRESULT CreateSwapChain(IUnknown* pDevice, DXGI_SWAP_CHAIN_DESC* pDesc, IDXGISwapChain** ppSwapChain)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, IUnknown*, DXGI_SWAP_CHAIN_DESC*, IDXGISwapChain**, HRESULT>)lpVtbl[10])((IDXGIFactory1*)Unsafe.AsPointer(ref this), pDevice, pDesc, ppSwapChain);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateSoftwareAdapter(SafeHandle Module, IDXGIAdapter** ppAdapter)
	{
		bool success = false;
		try
		{
			if (Module != null)
			{
				Module.DangerousAddRef(ref success);
				HMODULE module = (HMODULE)Module.DangerousGetHandle();
				return CreateSoftwareAdapter(module, ppAdapter);
			}
			throw new ArgumentNullException("Module");
		}
		finally
		{
			if (success)
			{
				Module.DangerousRelease();
			}
		}
	}

	public unsafe HRESULT CreateSoftwareAdapter(HMODULE Module, IDXGIAdapter** ppAdapter)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, HMODULE, IDXGIAdapter**, HRESULT>)lpVtbl[11])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Module, ppAdapter);
	}

	public unsafe HRESULT EnumAdapters1(uint Adapter, IDXGIAdapter1** ppAdapter)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, uint, IDXGIAdapter1**, HRESULT>)lpVtbl[12])((IDXGIFactory1*)Unsafe.AsPointer(ref this), Adapter, ppAdapter);
	}

	public unsafe BOOL IsCurrent()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIFactory1*, BOOL>)lpVtbl[13])((IDXGIFactory1*)Unsafe.AsPointer(ref this));
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
