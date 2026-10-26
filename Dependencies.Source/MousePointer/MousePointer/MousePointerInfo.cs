using System;
using System.ComponentModel;
using System.Drawing;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace MousePointer;

public class MousePointerInfo : BaseDisposable
{
	private readonly ILogger logger;

	public bool IsVisible { get; private set; }

	public Point Position { get; private set; }

	internal HCURSOR OriginalCursorHandle { get; private set; }

	internal HCURSOR CopiedCursorHandle { get; private set; }

	public ushort ResId { get; private set; }

	public Size OriginalSize { get; private set; }

	public int ScaledSize { get; }

	public double ScaleFactor { get; }

	public MousePointerInfo(ILogger<MousePointerInfo> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (!TryFillCursorInfo())
		{
			return;
		}
		try
		{
			double scaleFactor = new MonitorInfo(PInvoke.MonitorFromPoint(Position, MONITOR_FROM_FLAGS.MONITOR_DEFAULTTONEAREST)).GetScaleFactor();
			int num = (int)Math.Round(32.0 * scaleFactor);
			ScaledSize = ((num < 64) ? (num & -16) : (num & -32));
			ScaleFactor = (double)ScaledSize / 32.0;
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	private unsafe bool TryFillCursorInfo()
	{
		for (int i = 1; i <= 3; i++)
		{
			CURSORINFO pci = new CURSORINFO
			{
				cbSize = (uint)sizeof(CURSORINFO)
			};
			if (!PInvoke.GetCursorInfo(ref pci))
			{
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Mouse Pointer: Try {try} of 3: GetCursorInfo: {error}", i, new Win32Exception().Message);
				}
				IsVisible = false;
				return false;
			}
			IsVisible = pci.flags == CURSORINFO_FLAGS.CURSOR_SHOWING;
			Position = pci.ptScreenPos;
			OriginalCursorHandle = pci.hCursor;
			ICONINFOEXW iCONINFOEXW = new ICONINFOEXW
			{
				cbSize = (uint)sizeof(ICONINFOEXW)
			};
			if (!PInvoke.GetIconInfoEx(pci.hCursor, &iCONINFOEXW))
			{
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Mouse Pointer: Try {try} of 3: GetIconInfoEx: {error}", i, new Win32Exception().Message);
				}
				continue;
			}
			ResId = iCONINFOEXW.wResID;
			bool isNull = iCONINFOEXW.hbmColor.IsNull;
			try
			{
				HBITMAP hBITMAP = (isNull ? iCONINFOEXW.hbmMask : iCONINFOEXW.hbmColor);
				if (!hBITMAP.IsNull)
				{
					BITMAP bITMAP = default;
					if (PInvoke.GetObject(new HGDIOBJ(hBITMAP.Value), sizeof(BITMAP), &bITMAP) != 0)
					{
						int bmWidth = bITMAP.bmWidth;
						int num = Math.Abs(bITMAP.bmHeight);
						if (isNull)
						{
							num /= 2;
						}
						OriginalSize = new Size(bmWidth, num);
					}
				}
			}
			finally
			{
				if (!iCONINFOEXW.hbmMask.IsNull)
				{
					PInvoke.DeleteObject(iCONINFOEXW.hbmMask);
				}
				if (!iCONINFOEXW.hbmColor.IsNull)
				{
					PInvoke.DeleteObject(iCONINFOEXW.hbmColor);
				}
			}
			CopiedCursorHandle = new HCURSOR(PInvoke.CopyIcon(pci.hCursor).Value);
			if (CopiedCursorHandle.IsNull)
			{
				if (logger.IsEnabled(LogLevel.Debug))
				{
					logger.LogDebug("Mouse Pointer: Try {try} of 3: CopyIcon: {error}", i, new Win32Exception().Message);
				}
				continue;
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Mouse Pointer: IsVisible={isVisible}, Position={position}, Handle={handle}, Size={width}x{height}, Hotspot={hotspot}, IsMonochrome={isMonochrome}, ResId={resId}, ModName=\"{modName}\", ResName=\"{resName}\"", IsVisible, Position, $"0x{(ulong)OriginalCursorHandle.Value:X}", OriginalSize.Width, OriginalSize.Height, new Point((int)iCONINFOEXW.xHotspot, (int)iCONINFOEXW.yHotspot), isNull, ResId, iCONINFOEXW.szModName, iCONINFOEXW.szResName);
			}
			return true;
		}
		IsVisible = false;
		return false;
	}

	protected override void Dispose(bool disposing)
	{
		if (!CopiedCursorHandle.IsNull)
		{
			PInvoke.DestroyIcon(CopiedCursorHandle);
		}
	}
}
