using System;
using System.Drawing;

namespace AudioVideoCore;

public class ImageFormat : IEquatable<ImageFormat>
{
	public int Width { get; }

	public int Height { get; }

	public PixelFormat PixelFormat { get; }

	public ImageFormat(int width, int height, PixelFormat pixelFormat)
	{
		if (width <= 0)
		{
			throw new ArgumentOutOfRangeException("width");
		}
		if (height <= 0)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		if (pixelFormat == PixelFormat.None)
		{
			throw new ArgumentException("Pixel format must be specified.", "pixelFormat");
		}
		Width = width;
		Height = height;
		PixelFormat = pixelFormat;
	}

	public ImageFormat(Size size, PixelFormat pixelFormat)
		: this(size.Width, size.Height, pixelFormat)
	{
	}

	public bool Equals(ImageFormat other)
	{
		if ((object)other != null && Width == other.Width && Height == other.Height)
		{
			return PixelFormat == other.PixelFormat;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is ImageFormat other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return Width.GetHashCode() ^ Height.GetHashCode() ^ PixelFormat.GetHashCode();
	}

	public static bool operator ==(ImageFormat left, ImageFormat right)
	{
		return left?.Equals(right) ?? ((object)right == null);
	}

	public static bool operator !=(ImageFormat left, ImageFormat right)
	{
		return !(left == right);
	}

	public override string ToString()
	{
		return $"{Width}x{Height}/{PixelFormat}";
	}
}
