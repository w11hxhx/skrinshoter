using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Win32;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MousePointer;

public class MousePointerRenderer : SafeDisposable, IMouseOverlay, IDisposable
{
	private struct CursorData
	{
		public int Width;

		public int Height;

		public int HotspotX;

		public int HotspotY;

		public bool IsMonochrome;

		public byte[] Color32;

		public int ColorStride;

		public byte[] Mask1;

		public int MaskStride1;

		public int XorMaskOffset;
	}

	private readonly ILogger logger;

	private CursorData? cursorData;

	private string cursorPath;

	private HCURSOR cursorHandle;

	public MousePointerRenderer(ILogger<MousePointerRenderer> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
	}

	public void Draw(RawImage image, Point imagePosition, MousePointerInfo mousePointerInfo)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (mousePointerInfo == null)
		{
			throw new ArgumentNullException("mousePointerInfo");
		}
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed && mousePointerInfo.IsVisible)
			{
				bool num = mousePointerInfo.ResId != 0 && mousePointerInfo.ScaledSize > 32;
				Size size = ((mousePointerInfo.ResId != 0) ? new Size(mousePointerInfo.ScaledSize, mousePointerInfo.ScaledSize) : mousePointerInfo.OriginalSize);
				if ((num && TryGetCursorDataFromFile(mousePointerInfo.ResId, mousePointerInfo.ScaledSize, out var data)) || TryGetCursorDataFromSystemHandle(mousePointerInfo.OriginalCursorHandle, mousePointerInfo.CopiedCursorHandle, size, out data))
				{
					int x = mousePointerInfo.Position.X - data.HotspotX - imagePosition.X;
					int y = mousePointerInfo.Position.Y - data.HotspotY - imagePosition.Y;
					BlitCursor(image, new Point(x, y), in data);
				}
			}
		}
	}

	private unsafe bool TryGetCursorDataFromFile(ushort resId, int size, out CursorData data)
	{
		data = default;
		string cursorFilePath = GetCursorFilePath(resId);
		if (string.IsNullOrEmpty(cursorFilePath))
		{
			return false;
		}
		ref CursorData? reference = ref cursorData;
		if (reference.HasValue && reference.GetValueOrDefault().Width == size && cursorPath != null && string.Equals(cursorPath, cursorFilePath, StringComparison.OrdinalIgnoreCase))
		{
			data = cursorData.Value;
			return true;
		}
		fixed (char* value = cursorFilePath)
		{
			HANDLE hANDLE = PInvoke.LoadImage(HINSTANCE.Null, new PCWSTR(value), GDI_IMAGE_TYPE.IMAGE_CURSOR, size, size, IMAGE_FLAGS.LR_LOADFROMFILE);
			if (hANDLE == HANDLE.Null)
			{
				return false;
			}
			HCURSOR hCursor = new HCURSOR(hANDLE.Value);
			try
			{
				if (!GetCursorData(new HICON(hCursor.Value), out data))
				{
					return false;
				}
			}
			finally
			{
				PInvoke.DestroyCursor(hCursor);
			}
		}
		cursorData = data;
		cursorPath = cursorFilePath;
		cursorHandle = HCURSOR.Null;
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("TryGetCursorDataFromFile: Cursor updated");
		}
		return true;
	}

	private static string GetCursorFilePath(ushort resId)
	{
		string registryNameByResId = GetRegistryNameByResId(resId);
		if (string.IsNullOrEmpty(registryNameByResId))
		{
			return null;
		}
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Cursors", writable: false);
			string text = registryKey?.GetValue(registryNameByResId) as string;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			text = Environment.ExpandEnvironmentVariables(text.Trim());
			if (File.Exists(text))
			{
				return text;
			}
		}
		catch
		{
		}
		return null;
	}

	private static string GetRegistryNameByResId(ushort resId)
	{
		return resId switch
		{
			32512 => "Arrow", 
			32513 => "IBeam", 
			32514 => "Wait", 
			32515 => "Crosshair", 
			32516 => "UpArrow", 
			32631 => "NWPen", 
			32642 => "SizeNWSE", 
			32643 => "SizeNESW", 
			32644 => "SizeWE", 
			32645 => "SizeNS", 
			32646 => "SizeAll", 
			32648 => "No", 
			32649 => "Hand", 
			32650 => "AppStarting", 
			32651 => "Help", 
			32671 => "Pin", 
			32672 => "Person", 
			_ => null, 
		};
	}

	private unsafe bool TryGetCursorDataFromSystemHandle(HCURSOR hOriginalCursor, HCURSOR hCopiedCursor, Size size, out CursorData data)
	{
		data = default;
		if (cursorData?.Width == size.Width && cursorData?.Height == size.Height && cursorPath == null && cursorHandle.Value == hOriginalCursor.Value)
		{
			data = cursorData.Value;
			return true;
		}
		HANDLE hANDLE = PInvoke.CopyImage(new HANDLE(hCopiedCursor.Value), GDI_IMAGE_TYPE.IMAGE_CURSOR, size.Width, size.Height, IMAGE_FLAGS.LR_COPYFROMRESOURCE);
		if (hANDLE == HANDLE.Null)
		{
			return false;
		}
		HCURSOR hCursor = new HCURSOR(hANDLE.Value);
		try
		{
			if (!GetCursorData(new HICON(hCursor.Value), out data))
			{
				return false;
			}
		}
		finally
		{
			if (!hCursor.IsNull)
			{
				PInvoke.DestroyCursor(hCursor);
			}
		}
		cursorData = data;
		cursorPath = null;
		cursorHandle = hOriginalCursor;
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("TryGetCursorDataFromSystemHandle: Cursor updated");
		}
		return true;
	}

	private unsafe static bool GetCursorData(HICON hIcon, out CursorData data)
	{
		data = default;
		ICONINFO iCONINFO = default;
		if (!PInvoke.GetIconInfo(hIcon, &iCONINFO))
		{
			return false;
		}
		try
		{
			data.HotspotX = (int)iCONINFO.xHotspot;
			data.HotspotY = (int)iCONINFO.yHotspot;
			if (iCONINFO.hbmColor != HBITMAP.Null)
			{
				BITMAP bITMAP = default;
				if (PInvoke.GetObject(iCONINFO.hbmColor, sizeof(BITMAP), &bITMAP) == 0)
				{
					return false;
				}
				int bmWidth = bITMAP.bmWidth;
				int height = Math.Abs(bITMAP.bmHeight);
				byte[] bitmap32TopDown = GetBitmap32TopDown(iCONINFO.hbmColor, bmWidth, height, out var stride);
				byte[] mask = null;
				int stride2 = 0;
				if (iCONINFO.hbmMask != HBITMAP.Null)
				{
					mask = GetBitmap1TopDown(iCONINFO.hbmMask, bmWidth, height, out stride2);
				}
				data.Width = bmWidth;
				data.Height = height;
				data.IsMonochrome = false;
				data.Color32 = bitmap32TopDown;
				data.ColorStride = stride;
				data.Mask1 = mask;
				data.MaskStride1 = stride2;
				data.XorMaskOffset = 0;
				return true;
			}
			BITMAP bITMAP2 = default;
			if (PInvoke.GetObject(iCONINFO.hbmMask, sizeof(BITMAP), &bITMAP2) == 0)
			{
				return false;
			}
			int bmWidth2 = bITMAP2.bmWidth;
			int num = Math.Abs(bITMAP2.bmHeight);
			int num2 = num / 2;
			byte[] bitmap1TopDown = GetBitmap1TopDown(iCONINFO.hbmMask, bmWidth2, num, out var stride3);
			data.Width = bmWidth2;
			data.Height = num2;
			data.IsMonochrome = true;
			data.Color32 = null;
			data.ColorStride = 0;
			data.Mask1 = bitmap1TopDown;
			data.MaskStride1 = stride3;
			data.XorMaskOffset = stride3 * num2;
			return true;
		}
		finally
		{
			if (iCONINFO.hbmMask != HBITMAP.Null)
			{
				PInvoke.DeleteObject(iCONINFO.hbmMask);
			}
			if (iCONINFO.hbmColor != HBITMAP.Null)
			{
				PInvoke.DeleteObject(iCONINFO.hbmColor);
			}
		}
	}

	private unsafe static byte[] GetBitmap32TopDown(HBITMAP hBitmap, int width, int height, out int stride)
	{
		stride = width * 4;
		byte[] array = new byte[stride * height];
		BITMAPINFO bITMAPINFO = new BITMAPINFO
		{
			bmiHeader = 
			{
				biSize = (uint)sizeof(BITMAPINFOHEADER),
				biWidth = width,
				biPlanes = 1,
				biBitCount = 32,
				biCompression = 0u
			}
		};
		HDC dC = PInvoke.GetDC(HWND.Null);
		try
		{
			bITMAPINFO.bmiHeader.biHeight = -height;
			fixed (byte* lpvBits = array)
			{
				if (PInvoke.GetDIBits(dC, hBitmap, 0u, (uint)height, lpvBits, &bITMAPINFO, DIB_USAGE.DIB_RGB_COLORS) != 0)
				{
					return array;
				}
			}
			byte[] array2 = new byte[stride * height];
			fixed (byte* lpvBits2 = array2)
			{
				bITMAPINFO.bmiHeader.biHeight = height;
				if (PInvoke.GetDIBits(dC, hBitmap, 0u, (uint)height, lpvBits2, &bITMAPINFO, DIB_USAGE.DIB_RGB_COLORS) == 0)
				{
					throw new WinApiException("GetDIBits(32bpp)");
				}
				for (int i = 0; i < height; i++)
				{
					Buffer.BlockCopy(array2, (height - 1 - i) * stride, array, i * stride, stride);
				}
			}
			return array;
		}
		finally
		{
			if (dC != HDC.Null)
			{
				PInvoke.ReleaseDC(HWND.Null, dC);
			}
		}
	}

	private unsafe static byte[] GetBitmap1TopDown(HBITMAP hBitmap, int width, int height, out int stride)
	{
		stride = (width + 31) / 32 * 4;
		byte[] array = new byte[stride * height];
		BITMAPINFO bITMAPINFO = new BITMAPINFO
		{
			bmiHeader = 
			{
				biSize = (uint)sizeof(BITMAPINFOHEADER),
				biWidth = width,
				biPlanes = 1,
				biBitCount = 1,
				biCompression = 0u
			}
		};
		HDC dC = PInvoke.GetDC(HWND.Null);
		try
		{
			bITMAPINFO.bmiHeader.biHeight = -height;
			fixed (byte* lpvBits = array)
			{
				if (PInvoke.GetDIBits(dC, hBitmap, 0u, (uint)height, lpvBits, &bITMAPINFO, DIB_USAGE.DIB_RGB_COLORS) != 0)
				{
					return array;
				}
			}
			byte[] array2 = new byte[stride * height];
			fixed (byte* lpvBits2 = array2)
			{
				bITMAPINFO.bmiHeader.biHeight = height;
				if (PInvoke.GetDIBits(dC, hBitmap, 0u, (uint)height, lpvBits2, &bITMAPINFO, DIB_USAGE.DIB_RGB_COLORS) == 0)
				{
					throw new WinApiException("GetDIBits(1bpp)");
				}
				for (int i = 0; i < height; i++)
				{
					Buffer.BlockCopy(array2, (height - 1 - i) * stride, array, i * stride, stride);
				}
			}
			return array;
		}
		finally
		{
			if (dC != HDC.Null)
			{
				PInvoke.ReleaseDC(HWND.Null, dC);
			}
		}
	}

	private unsafe static void BlitCursor(RawImage image, Point position, in CursorData cursor)
	{
		int num = Math.Max(0, position.X);
		int num2 = Math.Max(0, position.Y);
		int num3 = Math.Min(image.Format.Width, position.X + cursor.Width);
		int num4 = Math.Min(image.Format.Height, position.Y + cursor.Height);
		if (num3 <= num || num4 <= num2)
		{
			return;
		}
		int num5 = num3 - num;
		int num6 = num4 - num2;
		int num7 = num - position.X;
		int num8 = num2 - position.Y;
		int num9 = image.Stride[0];
		fixed (byte* ptr = image.Data[0].Span)
		{
			if (!cursor.IsMonochrome)
			{
				byte[] color = cursor.Color32;
				int colorStride = cursor.ColorStride;
				for (int i = 0; i < num6; i++)
				{
					int num10 = num8 + i;
					int num11 = num2 + i;
					int num12 = num10 * colorStride + num7 * 4;
					uint* ptr2 = (uint*)(ptr + num11 * num9 + num * 4);
					for (int j = 0; j < num5; j++)
					{
						byte b = color[num12];
						byte b2 = color[num12 + 1];
						byte b3 = color[num12 + 2];
						byte b4 = color[num12 + 3];
						switch (b4)
						{
						case byte.MaxValue:
							*ptr2 = PackBGRA(b, b2, b3, byte.MaxValue);
							break;
						default:
						{
							uint num13 = *ptr2;
							int num14 = (int)(num13 & 0xFF);
							int num15 = (int)((num13 >> 8) & 0xFF);
							int num16 = (int)((num13 >> 16) & 0xFF);
							int num17 = 255 - b4;
							int num18 = b * b4 + num14 * num17 + 128;
							byte b5 = (byte)(num18 + (num18 >> 8) >> 8);
							int num19 = b2 * b4 + num15 * num17 + 128;
							byte g = (byte)(num19 + (num19 >> 8) >> 8);
							int num20 = b3 * b4 + num16 * num17 + 128;
							byte r = (byte)(num20 + (num20 >> 8) >> 8);
							*ptr2 = PackBGRA(b5, g, r, byte.MaxValue);
							break;
						}
						case 0:
							break;
						}
						num12 += 4;
						ptr2++;
					}
				}
				return;
			}
			byte[] mask = cursor.Mask1;
			int maskStride = cursor.MaskStride1;
			int xorMaskOffset = cursor.XorMaskOffset;
			for (int k = 0; k < num6; k++)
			{
				int num21 = num8 + k;
				int num22 = num2 + k;
				uint* ptr3 = (uint*)(ptr + num22 * num9 + num * 4);
				int num23 = num21 * maskStride + (num7 >> 3);
				int num24 = num23 + xorMaskOffset;
				int num25 = 128 >> (num7 & 7);
				for (int l = 0; l < num5; l++)
				{
					bool flag = (mask[num23] & (byte)num25) != 0;
					bool flag2 = (mask[num24] & (byte)num25) != 0;
					uint num26 = *ptr3 & 0xFFFFFF;
					uint num27 = (flag ? 16777215u : 0u);
					uint num28 = (flag2 ? 16777215u : 0u);
					uint num29 = (num26 & num27) ^ num28;
					*ptr3 = num29 | 0xFF000000u;
					ptr3++;
					num25 >>= 1;
					if (num25 == 0)
					{
						num25 = 128;
						num23++;
						num24++;
					}
				}
			}
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint PackBGRA(byte b, byte g, byte r, byte a)
	{
		return (uint)(b | (g << 8) | (r << 16) | (a << 24));
	}
}
