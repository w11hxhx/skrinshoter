using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SkrinshoterApp;

public class ScrollingCapturePreviewWindowVM : LocalizableObservableObject
{
	private const double PanelShadowWidth = 3.0;

	private readonly ILogger logger;

	private WriteableBitmap backingBitmap;

	private int backingWidth;

	private int backingHeight;

	private int lastSurfaceVersion;

	private double lastDpiScale;

	private int lastCroppedHeight;

	[ObservableProperty]
	private BitmapSource previewBitmap;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public BitmapSource PreviewBitmap
	{
		get
		{
			return previewBitmap;
		}
		set
		{
			if (!EqualityComparer<BitmapSource>.Default.Equals(previewBitmap, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PreviewBitmap);
				previewBitmap = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PreviewBitmap);
			}
		}
	}

	public ScrollingCapturePreviewWindowVM(ILogger<ScrollingCapturePreviewWindowVM> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
	}

	public unsafe bool RenderPreview(ScrollingCapturePreviewBuilder builder, Size panelSize, double dpiScale)
	{
		if (builder == null)
		{
			return false;
		}
		panelSize.Width -= 6.0;
		panelSize.Height -= 6.0;
		if (panelSize.Width <= 0.0 || panelSize.Height <= 0.0 || dpiScale <= 0.0)
		{
			return false;
		}
		int num = (int)Math.Round(panelSize.Width * dpiScale);
		int num2 = (int)Math.Round(panelSize.Height * dpiScale);
		if (num == 0 || num2 == 0)
		{
			return false;
		}
		long timestamp = Stopwatch.GetTimestamp();
		ScrollingCapturePreviewBuilder.PreviewSurfaceInfo surfaceInfo = builder.EnsureSurfaceForTarget(num, num2);
		if (surfaceInfo.Width <= 0 || surfaceInfo.CapacityHeight <= 0)
		{
			return false;
		}
		bool flag = TryPrepareBackingBitmapAndBitmapSource(surfaceInfo, dpiScale);
		if (backingBitmap == null)
		{
			return false;
		}
		backingBitmap.Lock();
		try
		{
			byte* dstBase = (byte*)(void*)backingBitmap.BackBuffer;
			int backBufferStride = backingBitmap.BackBufferStride;
			int contentHeight;
			int contentWidth;
			int dirtyTopResult;
			int dirtyHeightResult;
			if (flag)
			{
				builder.TryCopyAllRows(dstBase, backBufferStride, backingHeight, out contentHeight, out contentWidth);
				dirtyTopResult = 0;
				dirtyHeightResult = contentHeight;
			}
			else
			{
				builder.TryCopyDirtyRows(dstBase, backBufferStride, backingHeight, out dirtyTopResult, out dirtyHeightResult, out contentHeight, out contentWidth);
			}
			UpdateCroppedBitmapSource(contentWidth, contentHeight);
			if (dirtyHeightResult > 0)
			{
				backingBitmap.AddDirtyRect(new Int32Rect(0, dirtyTopResult, contentWidth, dirtyHeightResult));
			}
		}
		finally
		{
			backingBitmap.Unlock();
		}
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Rendered scrolling capture preview: target={targetWidth}x{targetHeight}, surfaceLevel={level}, surfaceSize={surfaceWidth}x{surfaceHeight}, surfaceVersion={version}, dpiScale={dpiScale} [time={time}ms]", num, num2, surfaceInfo.Level, surfaceInfo.Width, surfaceInfo.ContentHeight, surfaceInfo.Version, dpiScale, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
		}
		return true;
	}

	private bool TryPrepareBackingBitmapAndBitmapSource(ScrollingCapturePreviewBuilder.PreviewSurfaceInfo surfaceInfo, double dpiScale)
	{
		if (surfaceInfo.Width <= 0 || surfaceInfo.CapacityHeight <= 0 || dpiScale <= 0.0)
		{
			return false;
		}
		if (backingBitmap != null && backingWidth == surfaceInfo.Width && backingHeight == surfaceInfo.CapacityHeight && lastSurfaceVersion == surfaceInfo.Version && Math.Abs(lastDpiScale - dpiScale) < 0.0001)
		{
			return false;
		}
		backingWidth = surfaceInfo.Width;
		backingHeight = surfaceInfo.CapacityHeight;
		lastSurfaceVersion = surfaceInfo.Version;
		lastDpiScale = dpiScale;
		lastCroppedHeight = 0;
		backingBitmap = new WriteableBitmap(backingWidth, backingHeight, 96.0 * dpiScale, 96.0 * dpiScale, PixelFormats.Bgra32, null);
		PreviewBitmap = new CroppedBitmap(backingBitmap, new Int32Rect(0, 0, backingWidth, 1));
		return true;
	}

	private void UpdateCroppedBitmapSource(int contentWidth, int contentHeight)
	{
		if (contentWidth > 0)
		{
			if (contentHeight <= 0)
			{
				contentHeight = 1;
			}
			if (contentHeight > backingHeight)
			{
				contentHeight = backingHeight;
			}
			if (PreviewBitmap == null || lastCroppedHeight != contentHeight)
			{
				lastCroppedHeight = contentHeight;
				PreviewBitmap = new CroppedBitmap(backingBitmap, new Int32Rect(0, 0, contentWidth, contentHeight));
			}
		}
	}
}
