using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("00CDDEA8-939B-4B83-A340-A685226666CC")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIOutput1
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_OUTPUT_DESC*, HRESULT> GetDesc_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, DXGI_ENUM_MODES, uint*, DXGI_MODE_DESC*, HRESULT> GetDisplayModeList_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, HRESULT> FindClosestMatchingMode_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, HRESULT> WaitForVBlank_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, BOOL, HRESULT> TakeOwnership_12;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, void> ReleaseOwnership_13;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL_CAPABILITIES*, HRESULT> GetGammaControlCapabilities_14;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, HRESULT> SetGammaControl_15;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, HRESULT> GetGammaControl_16;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, HRESULT> SetDisplaySurface_17;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, HRESULT> GetDisplaySurfaceData_18;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FRAME_STATISTICS*, HRESULT> GetFrameStatistics_19;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, DXGI_ENUM_MODES, uint*, DXGI_MODE_DESC1*, HRESULT> GetDisplayModeList1_20;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, HRESULT> FindClosestMatchingMode1_21;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGIResource*, HRESULT> GetDisplaySurfaceData1_22;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, IDXGIOutputDuplication**, HRESULT> DuplicateOutput_23;
	}

	[ComImport]
	[Guid("00CDDEA8-939B-4B83-A340-A685226666CC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IDXGIOutput.Interface, IDXGIObject.Interface
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
		new unsafe HRESULT GetDesc(DXGI_OUTPUT_DESC* pDesc);

		[PreserveSig]
		new unsafe HRESULT GetDisplayModeList(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, uint* pNumModes, [Optional] DXGI_MODE_DESC* pDesc);

		[PreserveSig]
		new unsafe HRESULT FindClosestMatchingMode(DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, [Optional] IUnknown* pConcernedDevice);

		[PreserveSig]
		new HRESULT WaitForVBlank();

		[PreserveSig]
		new unsafe HRESULT TakeOwnership(IUnknown* pDevice, BOOL Exclusive);

		[PreserveSig]
		new void ReleaseOwnership();

		[PreserveSig]
		new unsafe HRESULT GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps);

		[PreserveSig]
		new unsafe HRESULT SetGammaControl(DXGI_GAMMA_CONTROL* pArray);

		[PreserveSig]
		new unsafe HRESULT GetGammaControl(DXGI_GAMMA_CONTROL* pArray);

		[PreserveSig]
		new unsafe HRESULT SetDisplaySurface(IDXGISurface* pScanoutSurface);

		[PreserveSig]
		new unsafe HRESULT GetDisplaySurfaceData(IDXGISurface* pDestination);

		[PreserveSig]
		new unsafe HRESULT GetFrameStatistics(DXGI_FRAME_STATISTICS* pStats);

		[PreserveSig]
		unsafe HRESULT GetDisplayModeList1(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, uint* pNumModes, [Optional] DXGI_MODE_DESC1* pDesc);

		[PreserveSig]
		unsafe HRESULT FindClosestMatchingMode1(DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, [Optional] IUnknown* pConcernedDevice);

		[PreserveSig]
		unsafe HRESULT GetDisplaySurfaceData1(IDXGIResource* pDestination);

		[PreserveSig]
		unsafe HRESULT DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(13491880u, 37787, 19331, 163, 64, 166, 133, 34, 102, 102, 204);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIOutput1*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint>)lpVtbl[1])((IDXGIOutput1*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, uint>)lpVtbl[2])((IDXGIOutput1*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIOutput1*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIOutput1*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDesc(out DXGI_OUTPUT_DESC pDesc)
	{
		fixed (DXGI_OUTPUT_DESC* pDesc2 = &pDesc)
		{
			return GetDesc(pDesc2);
		}
	}

	public unsafe HRESULT GetDesc(DXGI_OUTPUT_DESC* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_OUTPUT_DESC*, HRESULT>)lpVtbl[7])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDisplayModeList(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, ref uint pNumModes, Span<DXGI_MODE_DESC> pDesc)
	{
		fixed (DXGI_MODE_DESC* pDesc2 = pDesc)
		{
			fixed (uint* pNumModes2 = &pNumModes)
			{
				return GetDisplayModeList(EnumFormat, Flags, pNumModes2, pDesc2);
			}
		}
	}

	public unsafe HRESULT GetDisplayModeList(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, uint* pNumModes, [Optional] DXGI_MODE_DESC* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, DXGI_ENUM_MODES, uint*, DXGI_MODE_DESC*, HRESULT>)lpVtbl[8])((IDXGIOutput1*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT FindClosestMatchingMode(in DXGI_MODE_DESC pModeToMatch, out DXGI_MODE_DESC pClosestMatch, IUnknown* pConcernedDevice)
	{
		fixed (DXGI_MODE_DESC* pClosestMatch2 = &pClosestMatch)
		{
			fixed (DXGI_MODE_DESC* pModeToMatch2 = &pModeToMatch)
			{
				return FindClosestMatchingMode(pModeToMatch2, pClosestMatch2, pConcernedDevice);
			}
		}
	}

	public unsafe HRESULT FindClosestMatchingMode(DXGI_MODE_DESC* pModeToMatch, DXGI_MODE_DESC* pClosestMatch, [Optional] IUnknown* pConcernedDevice)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown*, HRESULT>)lpVtbl[9])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
	}

	public unsafe HRESULT WaitForVBlank()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, HRESULT>)lpVtbl[10])((IDXGIOutput1*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT TakeOwnership(IUnknown* pDevice, BOOL Exclusive)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, BOOL, HRESULT>)lpVtbl[11])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDevice, Exclusive);
	}

	public unsafe void ReleaseOwnership()
	{
		((delegate* unmanaged[Stdcall]<IDXGIOutput1*, void>)lpVtbl[12])((IDXGIOutput1*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetGammaControlCapabilities(out DXGI_GAMMA_CONTROL_CAPABILITIES pGammaCaps)
	{
		fixed (DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps2 = &pGammaCaps)
		{
			return GetGammaControlCapabilities(pGammaCaps2);
		}
	}

	public unsafe HRESULT GetGammaControlCapabilities(DXGI_GAMMA_CONTROL_CAPABILITIES* pGammaCaps)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL_CAPABILITIES*, HRESULT>)lpVtbl[13])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pGammaCaps);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetGammaControl(in DXGI_GAMMA_CONTROL pArray)
	{
		fixed (DXGI_GAMMA_CONTROL* gammaControl = &pArray)
		{
			return SetGammaControl(gammaControl);
		}
	}

	public unsafe HRESULT SetGammaControl(DXGI_GAMMA_CONTROL* pArray)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, HRESULT>)lpVtbl[14])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pArray);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetGammaControl(out DXGI_GAMMA_CONTROL pArray)
	{
		fixed (DXGI_GAMMA_CONTROL* pArray2 = &pArray)
		{
			return GetGammaControl(pArray2);
		}
	}

	public unsafe HRESULT GetGammaControl(DXGI_GAMMA_CONTROL* pArray)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_GAMMA_CONTROL*, HRESULT>)lpVtbl[15])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pArray);
	}

	public unsafe HRESULT SetDisplaySurface(IDXGISurface* pScanoutSurface)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, HRESULT>)lpVtbl[16])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pScanoutSurface);
	}

	public unsafe HRESULT GetDisplaySurfaceData(IDXGISurface* pDestination)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGISurface*, HRESULT>)lpVtbl[17])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDestination);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FRAME_STATISTICS*, HRESULT>)lpVtbl[18])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pStats);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDisplayModeList1(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, ref uint pNumModes, Span<DXGI_MODE_DESC1> pDesc)
	{
		fixed (DXGI_MODE_DESC1* pDesc2 = pDesc)
		{
			fixed (uint* pNumModes2 = &pNumModes)
			{
				return GetDisplayModeList1(EnumFormat, Flags, pNumModes2, pDesc2);
			}
		}
	}

	public unsafe HRESULT GetDisplayModeList1(DXGI_FORMAT EnumFormat, DXGI_ENUM_MODES Flags, uint* pNumModes, [Optional] DXGI_MODE_DESC1* pDesc)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_FORMAT, DXGI_ENUM_MODES, uint*, DXGI_MODE_DESC1*, HRESULT>)lpVtbl[19])((IDXGIOutput1*)Unsafe.AsPointer(ref this), EnumFormat, Flags, pNumModes, pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT FindClosestMatchingMode1(in DXGI_MODE_DESC1 pModeToMatch, out DXGI_MODE_DESC1 pClosestMatch, IUnknown* pConcernedDevice)
	{
		fixed (DXGI_MODE_DESC1* pClosestMatch2 = &pClosestMatch)
		{
			fixed (DXGI_MODE_DESC1* pModeToMatch2 = &pModeToMatch)
			{
				return FindClosestMatchingMode1(pModeToMatch2, pClosestMatch2, pConcernedDevice);
			}
		}
	}

	public unsafe HRESULT FindClosestMatchingMode1(DXGI_MODE_DESC1* pModeToMatch, DXGI_MODE_DESC1* pClosestMatch, [Optional] IUnknown* pConcernedDevice)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, DXGI_MODE_DESC1*, DXGI_MODE_DESC1*, IUnknown*, HRESULT>)lpVtbl[20])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pModeToMatch, pClosestMatch, pConcernedDevice);
	}

	public unsafe HRESULT GetDisplaySurfaceData1(IDXGIResource* pDestination)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IDXGIResource*, HRESULT>)lpVtbl[21])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDestination);
	}

	public unsafe HRESULT DuplicateOutput(IUnknown* pDevice, IDXGIOutputDuplication** ppOutputDuplication)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutput1*, IUnknown*, IDXGIOutputDuplication**, HRESULT>)lpVtbl[22])((IDXGIOutput1*)Unsafe.AsPointer(ref this), pDevice, ppOutputDuplication);
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
