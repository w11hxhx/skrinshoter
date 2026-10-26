using System;
using System.Drawing;
using Common;

namespace SkrinshoterApp;

public class ResizingRectangleLimiter
{
	private readonly Rectangle initialResizingRectangle;

	private readonly double aspectRatio;

	private readonly uint resizingDirection;

	private readonly Size minSize;

	private readonly Size maxSize;

	public ResizingRectangleLimiter(Rectangle initialResizingRectangle, double aspectRatio, Rectangle boundingRectangle, uint resizingDirection, int minWidth)
	{
		this.initialResizingRectangle = initialResizingRectangle;
		this.aspectRatio = aspectRatio;
		this.resizingDirection = resizingDirection;
		minSize = new Size(minWidth, (int)Math.Round((double)minWidth / aspectRatio));
		maxSize = CalculateMaxSize(initialResizingRectangle, aspectRatio, boundingRectangle, resizingDirection);
	}

	public Rectangle Limit(Rectangle resizingRectangle)
	{
		Size size;
		switch (resizingDirection)
		{
		case 1u:
		{
			int num4 = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height6 = (int)Math.Round((double)num4 / aspectRatio);
			size = new Size(num4, height6);
			resizingRectangle.X = initialResizingRectangle.Right - size.Width;
			resizingRectangle.Y = initialResizingRectangle.Top - (size.Height - initialResizingRectangle.Height) / 2;
			break;
		}
		case 2u:
		{
			int num3 = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height5 = (int)Math.Round((double)num3 / aspectRatio);
			size = new Size(num3, height5);
			resizingRectangle.Y = initialResizingRectangle.Top - (size.Height - initialResizingRectangle.Height) / 2;
			break;
		}
		case 3u:
		{
			int num2 = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			int width6 = (int)Math.Round((double)num2 * aspectRatio);
			size = new Size(width6, num2);
			resizingRectangle.X = initialResizingRectangle.Left - (size.Width - initialResizingRectangle.Width) / 2;
			resizingRectangle.Y = initialResizingRectangle.Bottom - size.Height;
			break;
		}
		case 6u:
		{
			int num = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			int width5 = (int)Math.Round((double)num * aspectRatio);
			size = new Size(width5, num);
			resizingRectangle.X = initialResizingRectangle.Left - (size.Width - initialResizingRectangle.Width) / 2;
			break;
		}
		case 4u:
		{
			int width4 = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height4 = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			size = RestoreAspectRatioBasedOnMax(new Size(width4, height4), aspectRatio);
			resizingRectangle.X = initialResizingRectangle.Right - size.Width;
			resizingRectangle.Y = initialResizingRectangle.Bottom - size.Height;
			break;
		}
		case 5u:
		{
			int width3 = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height3 = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			size = RestoreAspectRatioBasedOnMax(new Size(width3, height3), aspectRatio);
			resizingRectangle.Y = initialResizingRectangle.Bottom - size.Height;
			break;
		}
		case 7u:
		{
			int width2 = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height2 = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			size = RestoreAspectRatioBasedOnMax(new Size(width2, height2), aspectRatio);
			resizingRectangle.X = initialResizingRectangle.Right - size.Width;
			break;
		}
		case 8u:
		{
			int width = MathEx.Clamp(resizingRectangle.Width, minSize.Width, maxSize.Width);
			int height = MathEx.Clamp(resizingRectangle.Height, minSize.Height, maxSize.Height);
			size = RestoreAspectRatioBasedOnMax(new Size(width, height), aspectRatio);
			break;
		}
		default:
			throw new InvalidOperationException($"Unknown resizing direction: {resizingDirection}.");
		}
		resizingRectangle.Size = size;
		return resizingRectangle;
	}

	private static Size CalculateMaxSize(Rectangle initialResizingRectangle, double aspectRatio, Rectangle boundingRectangle, uint resizingDirection)
	{
		int width;
		int height;
		switch (resizingDirection)
		{
		case 1u:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			height = Math.Min(boundingRectangle.Bottom - initialResizingRectangle.Bottom, initialResizingRectangle.Top - boundingRectangle.Top) * 2 + initialResizingRectangle.Height;
			break;
		case 2u:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			height = Math.Min(boundingRectangle.Bottom - initialResizingRectangle.Bottom, initialResizingRectangle.Top - boundingRectangle.Top) * 2 + initialResizingRectangle.Height;
			break;
		case 3u:
			width = Math.Min(boundingRectangle.Right - initialResizingRectangle.Right, initialResizingRectangle.Left - boundingRectangle.Left) * 2 + initialResizingRectangle.Width;
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case 6u:
			width = Math.Min(boundingRectangle.Right - initialResizingRectangle.Right, initialResizingRectangle.Left - boundingRectangle.Left) * 2 + initialResizingRectangle.Width;
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		case 4u:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case 5u:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			height = initialResizingRectangle.Bottom - boundingRectangle.Top;
			break;
		case 7u:
			width = initialResizingRectangle.Right - boundingRectangle.Left;
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		case 8u:
			width = boundingRectangle.Right - initialResizingRectangle.Left;
			height = boundingRectangle.Bottom - initialResizingRectangle.Top;
			break;
		default:
			throw new InvalidOperationException($"Unknown resizing direction: {resizingDirection}.");
		}
		return RestoreAspectRatioBasedOnMin(new Size(width, height), aspectRatio);
	}

	private static Size RestoreAspectRatioBasedOnMin(Size size, double aspectRatio)
	{
		int num = (int)Math.Round((double)size.Height * aspectRatio);
		if (size.Width < num)
		{
			size.Height = (int)Math.Round((double)size.Width / aspectRatio);
		}
		else
		{
			size.Width = num;
		}
		return size;
	}

	private static Size RestoreAspectRatioBasedOnMax(Size size, double aspectRatio)
	{
		int num = (int)Math.Round((double)size.Height * aspectRatio);
		if (size.Width > num)
		{
			size.Height = (int)Math.Round((double)size.Width / aspectRatio);
		}
		else
		{
			size.Width = num;
		}
		return size;
	}
}
