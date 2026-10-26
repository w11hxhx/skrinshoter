using System;
using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;

namespace AudioVideoCore;

public static class RawImageExtensions
{
	public unsafe static void DrawImage(this RawImage baseImage, RawImage image, Point point)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (image.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Pixel format of image must be {PixelFormat.BGRA}.", "image");
		}
		using MemoryHandle memoryHandle = image.Data[0].Pin();
		using Bitmap bitmap = new Bitmap(image.Format.Width, image.Format.Height, image.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppArgb, (IntPtr)memoryHandle.Pointer);
		baseImage.DrawImage(bitmap, point);
	}

	public unsafe static void DrawImage(this RawImage baseImage, Bitmap bitmap, Point point)
	{
		if (baseImage == null)
		{
			throw new ArgumentNullException("baseImage");
		}
		if (bitmap == null)
		{
			throw new ArgumentNullException("bitmap");
		}
		if (baseImage.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Pixel format of base image must be {PixelFormat.BGRA}.", "baseImage");
		}
		if (bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
		{
			throw new ArgumentException($"Pixel format of bitmap must be {System.Drawing.Imaging.PixelFormat.Format32bppArgb}.", "bitmap");
		}
		using MemoryHandle memoryHandle = baseImage.Data[0].Pin();
		using Bitmap image = new Bitmap(baseImage.Format.Width, baseImage.Format.Height, baseImage.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppRgb, (IntPtr)memoryHandle.Pointer);
		using Graphics graphics = Graphics.FromImage(image);
		graphics.DrawImage(bitmap, point);
	}

	public unsafe static void XorImage(this RawImage baseImage, RawImage image, Point point)
	{
		if (baseImage == null)
		{
			throw new ArgumentNullException("baseImage");
		}
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (baseImage.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Pixel format of base image must be {PixelFormat.BGRA}.", "baseImage");
		}
		if (image.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException($"Pixel format of image must be {PixelFormat.BGRA}.", "image");
		}
		Rectangle a = new Rectangle(0, 0, baseImage.Format.Width, baseImage.Format.Height);
		Rectangle b = new Rectangle(point.X, point.Y, image.Format.Width, image.Format.Height);
		Rectangle rectangle = Rectangle.Intersect(a, b);
		if (rectangle.IsEmpty)
		{
			return;
		}
		using MemoryHandle memoryHandle = baseImage.Data[0].Pin();
		using MemoryHandle memoryHandle2 = image.Data[0].Pin();
		byte* ptr = (byte*)memoryHandle.Pointer + rectangle.Top * baseImage.Stride[0] + rectangle.Left * 4;
		byte* ptr2 = (byte*)memoryHandle2.Pointer + ((point.Y < 0) ? (-point.Y) : 0) * image.Stride[0] + ((point.X < 0) ? (-point.X) : 0) * 4;
		for (int i = 0; i < rectangle.Height; i++)
		{
			for (int j = 0; j < rectangle.Width; j++)
			{
				((int*)ptr)[j] ^= (int)((uint*)ptr2)[j];
			}
			ptr += baseImage.Stride[0];
			ptr2 += image.Stride[0];
		}
	}
}
