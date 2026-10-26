using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("310D36A0-D2E7-4C0A-AA04-6A9D23B8886A")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGISwapChain
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT> GetDevice_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, DXGI_PRESENT, HRESULT> Present_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, Guid*, void**, HRESULT> GetBuffer_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, BOOL, IDXGIOutput*, HRESULT> SetFullscreenState_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, BOOL*, IDXGIOutput**, HRESULT> GetFullscreenState_12;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_SWAP_CHAIN_DESC*, HRESULT> GetDesc_13;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, uint, uint, DXGI_FORMAT, uint, HRESULT> ResizeBuffers_14;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_MODE_DESC*, HRESULT> ResizeTarget_15;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, IDXGIOutput**, HRESULT> GetContainingOutput_16;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_FRAME_STATISTICS*, HRESULT> GetFrameStatistics_17;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint*, HRESULT> GetLastPresentCount_18;
	}

	[ComImport]
	[Guid("310D36A0-D2E7-4C0A-AA04-6A9D23B8886A")]
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
		HRESULT Present(uint SyncInterval, DXGI_PRESENT Flags);

		[PreserveSig]
		unsafe HRESULT GetBuffer(uint Buffer, Guid* riid, void** ppSurface);

		[PreserveSig]
		unsafe HRESULT SetFullscreenState(BOOL Fullscreen, [Optional] IDXGIOutput* pTarget);

		[PreserveSig]
		unsafe HRESULT GetFullscreenState([Optional] BOOL* pFullscreen, [Optional] IDXGIOutput** ppTarget);

		[PreserveSig]
		unsafe HRESULT GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc);

		[PreserveSig]
		HRESULT ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags);

		[PreserveSig]
		unsafe HRESULT ResizeTarget(DXGI_MODE_DESC* pNewTargetParameters);

		[PreserveSig]
		unsafe HRESULT GetContainingOutput(IDXGIOutput** ppOutput);

		[PreserveSig]
		unsafe HRESULT GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats);

		[PreserveSig]
		unsafe HRESULT GetLastPresentCount(uint* pLastPresentCount);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(822949536u, 53991, 19466, 170, 4, 106, 157, 35, 184, 136, 106);

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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint>)lpVtbl[1])((IDXGISwapChain*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint>)lpVtbl[2])((IDXGISwapChain*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGISwapChain*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppParent);
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
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, Guid*, void**, HRESULT>)lpVtbl[7])((IDXGISwapChain*)Unsafe.AsPointer(ref this), riid, ppDevice);
	}

	public unsafe HRESULT Present(uint SyncInterval, DXGI_PRESENT Flags)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, DXGI_PRESENT, HRESULT>)lpVtbl[8])((IDXGISwapChain*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetBuffer(uint Buffer, in Guid riid, out void* ppSurface)
	{
		fixed (void** ppSurface2 = &ppSurface)
		{
			fixed (Guid* riid2 = &riid)
			{
				return GetBuffer(Buffer, riid2, ppSurface2);
			}
		}
	}

	public unsafe HRESULT GetBuffer(uint Buffer, Guid* riid, void** ppSurface)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, Guid*, void**, HRESULT>)lpVtbl[9])((IDXGISwapChain*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
	}

	public unsafe HRESULT SetFullscreenState(BOOL Fullscreen, [Optional] IDXGIOutput* pTarget)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, BOOL, IDXGIOutput*, HRESULT>)lpVtbl[10])((IDXGISwapChain*)Unsafe.AsPointer(ref this), Fullscreen, pTarget);
	}

	public unsafe HRESULT GetFullscreenState([Optional] BOOL* pFullscreen, [Optional] IDXGIOutput** ppTarget)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, BOOL*, IDXGIOutput**, HRESULT>)lpVtbl[11])((IDXGISwapChain*)Unsafe.AsPointer(ref this), pFullscreen, ppTarget);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDesc(out DXGI_SWAP_CHAIN_DESC pDesc)
	{
		fixed (DXGI_SWAP_CHAIN_DESC* pDesc2 = &pDesc)
		{
			return GetDesc(pDesc2);
		}
	}

	public unsafe HRESULT GetDesc(DXGI_SWAP_CHAIN_DESC* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_SWAP_CHAIN_DESC*, HRESULT>)lpVtbl[12])((IDXGISwapChain*)Unsafe.AsPointer(ref this), pDesc);
	}

	public unsafe HRESULT ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint, uint, uint, DXGI_FORMAT, uint, HRESULT>)lpVtbl[13])((IDXGISwapChain*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT ResizeTarget(in DXGI_MODE_DESC pNewTargetParameters)
	{
		fixed (DXGI_MODE_DESC* pNewTargetParameters2 = &pNewTargetParameters)
		{
			return ResizeTarget(pNewTargetParameters2);
		}
	}

	public unsafe HRESULT ResizeTarget(DXGI_MODE_DESC* pNewTargetParameters)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_MODE_DESC*, HRESULT>)lpVtbl[14])((IDXGISwapChain*)Unsafe.AsPointer(ref this), pNewTargetParameters);
	}

	public unsafe HRESULT GetContainingOutput(IDXGIOutput** ppOutput)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, IDXGIOutput**, HRESULT>)lpVtbl[15])((IDXGISwapChain*)Unsafe.AsPointer(ref this), ppOutput);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFrameStatistics(out DXGI_FRAME_STATISTICS pStats)
	{
		fixed (DXGI_FRAME_STATISTICS* pStats2 = &pStats)
		{
			return GetFrameStatistics(pStats2);
		}
	}

	public unsafe HRESULT GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, DXGI_FRAME_STATISTICS*, HRESULT>)lpVtbl[16])((IDXGISwapChain*)Unsafe.AsPointer(ref this), pStats);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetLastPresentCount(out uint pLastPresentCount)
	{
		fixed (uint* pLastPresentCount2 = &pLastPresentCount)
		{
			return GetLastPresentCount(pLastPresentCount2);
		}
	}

	public unsafe HRESULT GetLastPresentCount(uint* pLastPresentCount)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGISwapChain*, uint*, HRESULT>)lpVtbl[17])((IDXGISwapChain*)Unsafe.AsPointer(ref this), pLastPresentCount);
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
