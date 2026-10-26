using System;
using System.Buffers;
using System.Runtime.InteropServices;
using AudioVideoCore;
using Common;

namespace SkrinshoterApp;

public class ScrollingCaptureImage : SafeDisposable
{
	public const int MaxImageHeight = 32768;

	private const int BlockHeight = 2048;

	private readonly int width;

	private int height;

	private readonly int stride;

	private readonly IntPtr[] blocks = new IntPtr[(int)Math.Ceiling(16.0)];

	private int currentBlockIndex = -1;

	private int currentBlockFreeLineCount;

	private readonly ScrollingCaptureImageDebugOverlay debugOverlay;

	public int Width => width;

	public int Height
	{
		get
		{
			lock (InternalDisposeLockObject)
			{
				return height;
			}
		}
	}

	public double AspectRatio
	{
		get
		{
			lock (InternalDisposeLockObject)
			{
				return (height == 0) ? 1.0 : ((double)width / (double)height);
			}
		}
	}

	public object SyncRoot => InternalDisposeLockObject;

	public event EventHandler<ScrollingCaptureImageUpdatedEventArgs> Updated;

	public ScrollingCaptureImage(int width)
	{
		if (width <= 0)
		{
			throw new ArgumentOutOfRangeException("width", "Width must be positive.");
		}
		this.width = width;
		stride = width * 4;
		debugOverlay = new ScrollingCaptureImageDebugOverlay(this);
	}

	public int AddFragment(RawImage image, int offsetY, int lineCount, int overwriteLineCount = 0, bool drawDebugFragmentBoundaryLine = false, int? debugFragmentIndex = null)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (!image.Format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {image.Format.PixelFormat}.", "image");
		}
		if (image.Format.Width != width)
		{
			throw new ArgumentException("Image width must match scrolling image width.", "image");
		}
		if (offsetY < 0 || offsetY > image.Format.Height)
		{
			throw new ArgumentOutOfRangeException("offsetY");
		}
		if (lineCount < 0 || lineCount > image.Format.Height)
		{
			throw new ArgumentOutOfRangeException("lineCount");
		}
		if ((long)offsetY + (long)lineCount > image.Format.Height)
		{
			throw new ArgumentOutOfRangeException("lineCount");
		}
		if (overwriteLineCount < 0)
		{
			throw new ArgumentOutOfRangeException("overwriteLineCount");
		}
		int num = 0;
		if (debugFragmentIndex < num)
		{
			throw new ArgumentOutOfRangeException("debugFragmentIndex");
		}
		int num2 = 0;
		int num3;
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				num = num2;
				goto IL_02a8;
			}
			if (overwriteLineCount > 0)
			{
				if (overwriteLineCount > height)
				{
					overwriteLineCount = height;
				}
				height -= overwriteLineCount;
				if (height > 0)
				{
					currentBlockIndex = (height - 1) / 2048;
					currentBlockFreeLineCount = 2048 - (height - currentBlockIndex * 2048);
				}
				else
				{
					currentBlockIndex = -1;
					currentBlockFreeLineCount = 0;
				}
			}
			num3 = height;
			num2 = Math.Min(lineCount, 32768 - height);
			if (num2 == 0)
			{
				num = num2;
				goto IL_02a8;
			}
			lineCount = num2;
			while (lineCount > 0)
			{
				if (currentBlockFreeLineCount == 0)
				{
					if (blocks[currentBlockIndex + 1] == IntPtr.Zero)
					{
						blocks[currentBlockIndex + 1] = Marshal.AllocHGlobal(2048 * stride);
					}
					currentBlockIndex++;
					currentBlockFreeLineCount = 2048;
				}
				int num4 = Math.Min(lineCount, currentBlockFreeLineCount);
				CopyLines(image, offsetY, num4);
				offsetY += num4;
				lineCount -= num4;
				currentBlockFreeLineCount -= num4;
				height += num4;
			}
			debugOverlay.DrawAddedFragmentDebugDecorations(num3, height, drawDebugFragmentBoundaryLine, debugFragmentIndex);
		}
		Updated?.Invoke(this, new ScrollingCaptureImageUpdatedEventArgs(num3));
		return num2;
		IL_02a8:
		return num;
	}

	public unsafe void CopyTo(RawImage image)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (!image.Format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {image.Format.PixelFormat}.", "image");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			if (image.Format.Width != width || image.Format.Height != height)
			{
				throw new ArgumentException("Image dimensions are wrong.", "image");
			}
			using MemoryHandle memoryHandle = image.Data[0].Pin();
			byte* ptr = (byte*)memoryHandle.Pointer;
			int num = height;
			int num2 = 0;
			while (num > 0)
			{
				byte* srcData = (byte*)(void*)blocks[num2];
				int num3 = Math.Min(num, 2048);
				RawImage.CopyRegion(srcData, stride, ptr, image.Stride[0], stride, num3);
				ptr += num3 * image.Stride[0];
				num2++;
				num -= num3;
			}
		}
	}

	public unsafe byte* GetRowPointerUnchecked(int y)
	{
		int num = y / 2048;
		int num2 = y - num * 2048;
		return (byte*)(void*)blocks[num] + num2 * stride;
	}

	private unsafe void CopyLines(RawImage image, int offsetY, int lineCount)
	{
		if (lineCount > currentBlockFreeLineCount)
		{
			throw new ArgumentOutOfRangeException("lineCount", "lineCount cannot exceed currentBlockFreeLineCount.");
		}
		using MemoryHandle memoryHandle = image.Data[0].Pin();
		RawImage.CopyRegion((byte*)memoryHandle.Pointer + offsetY * image.Stride[0], dstData: (byte*)(void*)blocks[currentBlockIndex] + (2048 - currentBlockFreeLineCount) * stride, srcStride: image.Stride[0], dstStride: stride, rowSize: stride, rowCount: lineCount);
	}

	protected override void Dispose(bool disposing)
	{
		for (int i = 0; i < blocks.Length; i++)
		{
			if (blocks[i] != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(blocks[i]);
				blocks[i] = IntPtr.Zero;
			}
		}
		currentBlockIndex = -1;
		currentBlockFreeLineCount = 0;
	}
}
