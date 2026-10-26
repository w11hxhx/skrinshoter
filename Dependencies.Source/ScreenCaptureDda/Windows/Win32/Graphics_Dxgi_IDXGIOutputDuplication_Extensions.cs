using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Dxgi_IDXGIOutputDuplication_Extensions
{
	internal unsafe static HRESULT SetPrivateData(this IDXGIOutputDuplication.Interface @this, in Guid Name, uint DataSize, void* pData)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateData(name, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this IDXGIOutputDuplication.Interface @this, in Guid Name, IUnknown* pUnknown)
	{
		fixed (Guid* name = &Name)
		{
			return @this.SetPrivateDataInterface(name, pUnknown);
		}
	}

	internal unsafe static HRESULT GetPrivateData(this IDXGIOutputDuplication.Interface @this, in Guid Name, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* name = &Name)
			{
				return @this.GetPrivateData(name, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT GetParent(this IDXGIOutputDuplication.Interface @this, in Guid riid, out void* ppParent)
	{
		fixed (void** ppParent2 = &ppParent)
		{
			fixed (Guid* riid2 = &riid)
			{
				return @this.GetParent(riid2, ppParent2);
			}
		}
	}

	internal unsafe static void GetDesc(this IDXGIOutputDuplication.Interface @this, out DXGI_OUTDUPL_DESC pDesc)
	{
		fixed (DXGI_OUTDUPL_DESC* pDesc2 = &pDesc)
		{
			@this.GetDesc(pDesc2);
		}
	}

	internal unsafe static HRESULT AcquireNextFrame(this IDXGIOutputDuplication.Interface @this, uint TimeoutInMilliseconds, out DXGI_OUTDUPL_FRAME_INFO pFrameInfo, IDXGIResource** ppDesktopResource)
	{
		fixed (DXGI_OUTDUPL_FRAME_INFO* pFrameInfo2 = &pFrameInfo)
		{
			return @this.AcquireNextFrame(TimeoutInMilliseconds, pFrameInfo2, ppDesktopResource);
		}
	}

	internal unsafe static HRESULT GetFrameDirtyRects(this IDXGIOutputDuplication.Interface @this, uint DirtyRectsBufferSize, out RECT pDirtyRectsBuffer, out uint pDirtyRectsBufferSizeRequired)
	{
		fixed (uint* pDirtyRectsBufferSizeRequired2 = &pDirtyRectsBufferSizeRequired)
		{
			fixed (RECT* pDirtyRectsBuffer2 = &pDirtyRectsBuffer)
			{
				return @this.GetFrameDirtyRects(DirtyRectsBufferSize, pDirtyRectsBuffer2, pDirtyRectsBufferSizeRequired2);
			}
		}
	}

	internal unsafe static HRESULT GetFrameMoveRects(this IDXGIOutputDuplication.Interface @this, uint MoveRectsBufferSize, out DXGI_OUTDUPL_MOVE_RECT pMoveRectBuffer, out uint pMoveRectsBufferSizeRequired)
	{
		fixed (uint* pMoveRectsBufferSizeRequired2 = &pMoveRectsBufferSizeRequired)
		{
			fixed (DXGI_OUTDUPL_MOVE_RECT* pMoveRectBuffer2 = &pMoveRectBuffer)
			{
				return @this.GetFrameMoveRects(MoveRectsBufferSize, pMoveRectBuffer2, pMoveRectsBufferSizeRequired2);
			}
		}
	}

	internal unsafe static HRESULT GetFramePointerShape(this IDXGIOutputDuplication.Interface @this, uint PointerShapeBufferSize, void* pPointerShapeBuffer, out uint pPointerShapeBufferSizeRequired, out DXGI_OUTDUPL_POINTER_SHAPE_INFO pPointerShapeInfo)
	{
		fixed (DXGI_OUTDUPL_POINTER_SHAPE_INFO* pPointerShapeInfo2 = &pPointerShapeInfo)
		{
			fixed (uint* pPointerShapeBufferSizeRequired2 = &pPointerShapeBufferSizeRequired)
			{
				return @this.GetFramePointerShape(PointerShapeBufferSize, pPointerShapeBuffer, pPointerShapeBufferSizeRequired2, pPointerShapeInfo2);
			}
		}
	}

	internal unsafe static HRESULT MapDesktopSurface(this IDXGIOutputDuplication.Interface @this, out DXGI_MAPPED_RECT pLockedRect)
	{
		fixed (DXGI_MAPPED_RECT* pLockedRect2 = &pLockedRect)
		{
			return @this.MapDesktopSurface(pLockedRect2);
		}
	}
}
