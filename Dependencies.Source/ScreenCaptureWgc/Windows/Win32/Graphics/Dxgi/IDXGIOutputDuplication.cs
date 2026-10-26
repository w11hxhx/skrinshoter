using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Dxgi;

[Guid("191CFAC3-A341-470D-B26E-A864F428319C")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDXGIOutputDuplication
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint, void*, HRESULT> SetPrivateData_4;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_5;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint*, void*, HRESULT> GetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, HRESULT> GetParent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_OUTDUPL_DESC*, void> GetDesc_8;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_FRAME_INFO*, IDXGIResource**, HRESULT> AcquireNextFrame_9;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, RECT*, uint*, HRESULT> GetFrameDirtyRects_10;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_MOVE_RECT*, uint*, HRESULT> GetFrameMoveRects_11;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, void*, uint*, DXGI_OUTDUPL_POINTER_SHAPE_INFO*, HRESULT> GetFramePointerShape_12;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_MAPPED_RECT*, HRESULT> MapDesktopSurface_13;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, HRESULT> UnMapDesktopSurface_14;

		internal unsafe delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, HRESULT> ReleaseFrame_15;
	}

	[ComImport]
	[Guid("191CFAC3-A341-470D-B26E-A864F428319C")]
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
		unsafe void GetDesc(DXGI_OUTDUPL_DESC* pDesc);

		[PreserveSig]
		unsafe HRESULT AcquireNextFrame(uint TimeoutInMilliseconds, DXGI_OUTDUPL_FRAME_INFO* pFrameInfo, IDXGIResource** ppDesktopResource);

		[PreserveSig]
		unsafe HRESULT GetFrameDirtyRects(uint DirtyRectsBufferSize, RECT* pDirtyRectsBuffer, uint* pDirtyRectsBufferSizeRequired);

		[PreserveSig]
		unsafe HRESULT GetFrameMoveRects(uint MoveRectsBufferSize, DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer, uint* pMoveRectsBufferSizeRequired);

		[PreserveSig]
		unsafe HRESULT GetFramePointerShape(uint PointerShapeBufferSize, void* pPointerShapeBuffer, uint* pPointerShapeBufferSizeRequired, DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo);

		[PreserveSig]
		unsafe HRESULT MapDesktopSurface(DXGI_MAPPED_RECT* pLockedRect);

		[PreserveSig]
		HRESULT UnMapDesktopSurface();

		[PreserveSig]
		HRESULT ReleaseFrame();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(421329603u, 41793, 18189, 178, 110, 168, 100, 244, 40, 49, 156);

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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, HRESULT>)(*lpVtbl))((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint>)lpVtbl[1])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint>)lpVtbl[2])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint, void*, HRESULT>)lpVtbl[3])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, IUnknown*, HRESULT>)lpVtbl[4])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, pUnknown);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, uint*, void*, HRESULT>)lpVtbl[5])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), Name, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, Guid*, void**, HRESULT>)lpVtbl[6])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), riid, ppParent);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void GetDesc(out DXGI_OUTDUPL_DESC pDesc)
	{
		fixed (DXGI_OUTDUPL_DESC* pDesc2 = &pDesc)
		{
			GetDesc(pDesc2);
		}
	}

	public unsafe void GetDesc(DXGI_OUTDUPL_DESC* pDesc)
	{
		((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_OUTDUPL_DESC*, void>)lpVtbl[7])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), pDesc);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT AcquireNextFrame(uint TimeoutInMilliseconds, out DXGI_OUTDUPL_FRAME_INFO pFrameInfo, IDXGIResource** ppDesktopResource)
	{
		fixed (DXGI_OUTDUPL_FRAME_INFO* pFrameInfo2 = &pFrameInfo)
		{
			return AcquireNextFrame(TimeoutInMilliseconds, pFrameInfo2, ppDesktopResource);
		}
	}

	public unsafe HRESULT AcquireNextFrame(uint TimeoutInMilliseconds, DXGI_OUTDUPL_FRAME_INFO* pFrameInfo, IDXGIResource** ppDesktopResource)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_FRAME_INFO*, IDXGIResource**, HRESULT>)lpVtbl[8])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), TimeoutInMilliseconds, pFrameInfo, ppDesktopResource);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFrameDirtyRects(uint DirtyRectsBufferSize, out RECT pDirtyRectsBuffer, out uint pDirtyRectsBufferSizeRequired)
	{
		fixed (uint* pDirtyRectsBufferSizeRequired2 = &pDirtyRectsBufferSizeRequired)
		{
			fixed (RECT* pDirtyRectsBuffer2 = &pDirtyRectsBuffer)
			{
				return GetFrameDirtyRects(DirtyRectsBufferSize, pDirtyRectsBuffer2, pDirtyRectsBufferSizeRequired2);
			}
		}
	}

	public unsafe HRESULT GetFrameDirtyRects(uint DirtyRectsBufferSize, RECT* pDirtyRectsBuffer, uint* pDirtyRectsBufferSizeRequired)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, RECT*, uint*, HRESULT>)lpVtbl[9])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), DirtyRectsBufferSize, pDirtyRectsBuffer, pDirtyRectsBufferSizeRequired);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFrameMoveRects(uint MoveRectsBufferSize, out DXGI_OUTDUPL_MOVE_RECT pMoveRectBuffer, out uint pMoveRectsBufferSizeRequired)
	{
		fixed (uint* pMoveRectsBufferSizeRequired2 = &pMoveRectsBufferSizeRequired)
		{
			fixed (DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer2 = &pMoveRectBuffer)
			{
				return GetFrameMoveRects(MoveRectsBufferSize, pMoveRectBuffer2, pMoveRectsBufferSizeRequired2);
			}
		}
	}

	public unsafe HRESULT GetFrameMoveRects(uint MoveRectsBufferSize, DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer, uint* pMoveRectsBufferSizeRequired)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, DXGI_OUTDUPL_MOVE_RECT*, uint*, HRESULT>)lpVtbl[10])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), MoveRectsBufferSize, pMoveRectBuffer, pMoveRectsBufferSizeRequired);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetFramePointerShape(uint PointerShapeBufferSize, void* pPointerShapeBuffer, out uint pPointerShapeBufferSizeRequired, out DXGI_OUTDUPL_POINTER_SHAPE_INFO pPointerShapeInfo)
	{
		fixed (DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo2 = &pPointerShapeInfo)
		{
			fixed (uint* pPointerShapeBufferSizeRequired2 = &pPointerShapeBufferSizeRequired)
			{
				return GetFramePointerShape(PointerShapeBufferSize, pPointerShapeBuffer, pPointerShapeBufferSizeRequired2, pPointerShapeInfo2);
			}
		}
	}

	public unsafe HRESULT GetFramePointerShape(uint PointerShapeBufferSize, void* pPointerShapeBuffer, uint* pPointerShapeBufferSizeRequired, DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, uint, void*, uint*, DXGI_OUTDUPL_POINTER_SHAPE_INFO*, HRESULT>)lpVtbl[11])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), PointerShapeBufferSize, pPointerShapeBuffer, pPointerShapeBufferSizeRequired, pPointerShapeInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT MapDesktopSurface(out DXGI_MAPPED_RECT pLockedRect)
	{
		fixed (DXGI_MAPPED_RECT* pLockedRect2 = &pLockedRect)
		{
			return MapDesktopSurface(pLockedRect2);
		}
	}

	public unsafe HRESULT MapDesktopSurface(DXGI_MAPPED_RECT* pLockedRect)
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, DXGI_MAPPED_RECT*, HRESULT>)lpVtbl[12])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this), pLockedRect);
	}

	public unsafe HRESULT UnMapDesktopSurface()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, HRESULT>)lpVtbl[13])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT ReleaseFrame()
	{
		return ((delegate* unmanaged[Stdcall]<IDXGIOutputDuplication*, HRESULT>)lpVtbl[14])((IDXGIOutputDuplication*)Unsafe.AsPointer(ref this));
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
