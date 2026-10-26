using System;
using System.Buffers;
using System.ComponentModel;
using System.Drawing;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using MousePointer;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace ScreenCaptureGdi;

public class ScreenCapturerGdi
{
	private readonly Rectangle captureArea;

	private readonly bool showMousePointer;

	private readonly ILoggerFactory loggerFactory;

	public ScreenCapturerGdi(Rectangle captureArea, bool showMousePointer, ILoggerFactory loggerFactory)
	{
		if (captureArea.Width <= 0 || captureArea.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("captureArea");
		}
		this.captureArea = captureArea;
		this.showMousePointer = showMousePointer;
		this.loggerFactory = loggerFactory ?? throw new ArgumentNullException("loggerFactory");
	}

	public unsafe void TakeScreenshot(RawImage image)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (image.Format.Width != captureArea.Width || image.Format.Height != captureArea.Height)
		{
			throw new ArgumentException("Image size does not match capture area size.", "image");
		}
		if (image.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Unsupported pixel format: {image.Format.PixelFormat}.", "image");
		}
		if (!VirtualScreen.GetRectangle().Contains(captureArea))
		{
			throw new InvalidOperationException("Screenshot capture area extends beyond virtual screen.");
		}
		HDC hDC = HDC.Null;
		HDC hDC2 = HDC.Null;
		HBITMAP hBITMAP = HBITMAP.Null;
		try
		{
			hDC = PInvoke.GetDC(HWND.Null);
			if (hDC == HDC.Null)
			{
				throw new WinApiException("GetDC()");
			}
			hDC2 = PInvoke.CreateCompatibleDC(hDC);
			if (hDC2 == HDC.Null)
			{
				throw new WinApiException("CreateCompatibleDC()");
			}
			hBITMAP = PInvoke.CreateCompatibleBitmap(hDC, captureArea.Width, captureArea.Height);
			if (hBITMAP == HBITMAP.Null)
			{
				throw new WinApiException("CreateCompatibleBitmap()");
			}
			HGDIOBJ hGDIOBJ = PInvoke.SelectObject(hDC2, hBITMAP);
			if (hGDIOBJ == HGDIOBJ.Null)
			{
				throw new WinApiException("SelectObject()");
			}
			try
			{
				if (!PInvoke.BitBlt(hDC2, 0, 0, captureArea.Width, captureArea.Height, hDC, captureArea.Left, captureArea.Top, ROP_CODE.SRCCOPY))
				{
					throw new WinApiException("BitBlt()", new Win32Exception());
				}
			}
			finally
			{
				PInvoke.SelectObject(hDC2, hGDIOBJ);
			}
			using MemoryHandle memoryHandle = image.Data[0].Pin();
			BITMAPINFO bITMAPINFO = new BITMAPINFO
			{
				bmiHeader = 
				{
					biSize = (uint)sizeof(BITMAPINFOHEADER),
					biWidth = captureArea.Width,
					biHeight = -captureArea.Height,
					biPlanes = 1,
					biBitCount = 32,
					biCompression = 0u
				}
			};
			if (PInvoke.GetDIBits(hDC2, hBITMAP, 0u, (uint)captureArea.Height, memoryHandle.Pointer, &bITMAPINFO, DIB_USAGE.DIB_RGB_COLORS) == 0)
			{
				throw new WinApiException("GetDIBits()");
			}
			if (!showMousePointer)
			{
				return;
			}
			using MousePointerRenderer mousePointerRenderer = new MousePointerRenderer(loggerFactory.CreateLogger<MousePointerRenderer>());
			using MousePointerInfo mousePointerInfo = new MousePointerInfo(loggerFactory.CreateLogger<MousePointerInfo>());
			mousePointerRenderer.Draw(image, captureArea.Location, mousePointerInfo);
		}
		finally
		{
			if (hBITMAP != HBITMAP.Null)
			{
				PInvoke.DeleteObject(hBITMAP);
			}
			if (hDC2 != HDC.Null)
			{
				PInvoke.DeleteDC(hDC2);
			}
			if (hDC != HDC.Null)
			{
				PInvoke.ReleaseDC(HWND.Null, hDC);
			}
		}
	}
}
