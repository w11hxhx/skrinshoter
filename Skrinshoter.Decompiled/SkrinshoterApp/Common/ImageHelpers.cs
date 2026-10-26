using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace SkrinshoterApp.Common;

public static class ImageHelpers
{
	public static Size GetImageDimensions(string filePath)
	{
		try
		{
			using FileStream bitmapStream = File.OpenRead(filePath);
			BitmapDecoder bitmapDecoder = BitmapDecoder.Create(bitmapStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
			return new Size(bitmapDecoder.Frames[0].PixelWidth, bitmapDecoder.Frames[0].PixelHeight);
		}
		catch
		{
			return Size.Empty;
		}
	}

	public static Bitmap ResizeImage(Image image, int width, int height)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (width <= 0)
		{
			throw new ArgumentOutOfRangeException("width");
		}
		if (height <= 0)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		Rectangle destRect = new Rectangle(0, 0, width, height);
		Bitmap bitmap = new Bitmap(width, height);
		bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.CompositingMode = CompositingMode.SourceCopy;
		graphics.CompositingQuality = CompositingQuality.HighQuality;
		graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		graphics.SmoothingMode = SmoothingMode.HighQuality;
		graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
		using ImageAttributes imageAttributes = new ImageAttributes();
		imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
		graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
		return bitmap;
	}

	public static Bitmap DownscaleImage(Bitmap image, int maxDimension)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (maxDimension <= 0)
		{
			throw new ArgumentOutOfRangeException("maxDimension");
		}
		int num = Math.Max(image.Width, image.Height);
		if (num <= maxDimension)
		{
			return image;
		}
		double num2 = (double)num / (double)maxDimension;
		int width = Math.Max((int)Math.Round((double)image.Width / num2), 1);
		int height = Math.Max((int)Math.Round((double)image.Height / num2), 1);
		return ResizeImage(image, width, height);
	}

	public static string DownscaleImage(string filePath, int maxDimension, ImageFormat imageFormat, int jpegQuality = 100)
	{
		using Bitmap image = new Bitmap(filePath);
		using Bitmap bitmap = DownscaleImage(image, maxDimension);
		ImageCodecInfo imageEncoder = GetImageEncoder(imageFormat);
		string text = imageEncoder.FilenameExtension.Split(';')[0].Replace("*", "").ToLowerInvariant();
		string text2 = Path.GetTempPath() + Guid.NewGuid().ToString() + text;
		if (imageFormat == ImageFormat.Jpeg)
		{
			EncoderParameters encoderParameters = new EncoderParameters();
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);
			using FileStream stream = new FileStream(text2, FileMode.Create, FileAccess.Write);
			bitmap.Save(stream, imageEncoder, encoderParameters);
		}
		else
		{
			using FileStream stream2 = new FileStream(text2, FileMode.Create, FileAccess.Write);
			bitmap.Save(stream2, imageFormat);
		}
		return text2;
	}

	private static ImageCodecInfo GetImageEncoder(ImageFormat format)
	{
		ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
		foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
		{
			if (imageCodecInfo.FormatID == format.Guid)
			{
				return imageCodecInfo;
			}
		}
		throw new InvalidOperationException($"Image encoder for format {format} not found.");
	}

	public static void SaveAsJpeg(this Image image, string path, int quality)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (quality < 0 || quality > 100)
		{
			throw new ArgumentOutOfRangeException("quality", quality, "JPEG quality must be between 0 and 100.");
		}
		ImageCodecInfo imageEncoder = GetImageEncoder(ImageFormat.Jpeg);
		using EncoderParameters encoderParameters = new EncoderParameters();
		encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
		using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
		image.Save(stream, imageEncoder, encoderParameters);
	}

	public static void SaveAsPng(this Image image, string path)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
		image.Save(stream, ImageFormat.Png);
	}
}
