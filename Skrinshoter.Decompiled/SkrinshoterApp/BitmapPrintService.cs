using System;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class BitmapPrintService
{
	public void Print(BitmapSource bitmapSource, string printerName, PrintTicket printTicket)
	{
		if (bitmapSource == null)
		{
			throw new ArgumentNullException("bitmapSource");
		}
		if (string.IsNullOrEmpty(printerName))
		{
			throw new ArgumentException("Printer name is required.", "printerName");
		}
		if (printTicket == null)
		{
			throw new ArgumentNullException("printTicket");
		}
		string path = null;
		string printQueueName = printerName;
		if (Uri.TryCreate(printerName, UriKind.Absolute, out var result) && result.IsUnc)
		{
			path = "\\\\" + result.Host;
			printQueueName = Uri.UnescapeDataString(result.AbsolutePath.TrimStart('/'));
		}
		using PrintServer printServer = new PrintServer(path);
		using PrintQueue printQueue = printServer.GetPrintQueue(printQueueName);
		PrintTicket printTicket2 = printQueue.MergeAndValidatePrintTicket(printQueue.UserPrintTicket, printTicket).ValidatedPrintTicket ?? printTicket;
		PrintCapabilities printCapabilities = printQueue.GetPrintCapabilities(printTicket2);
		Size pageSize = GetPageSizeInDips(printTicket2, printCapabilities);
		if (pageSize.Width <= 0.0 || pageSize.Height <= 0.0)
		{
			pageSize = new Size(816.0, 1056.0);
		}
		NormalizePageSizeForOrientation(printTicket2, ref pageSize);
		Rect rect = GetImageableAreaInDips(printCapabilities, pageSize);
		if (IsLandscape(printTicket2) && rect.Height > rect.Width && pageSize.Width > pageSize.Height)
		{
			rect = new Rect(rect.X, rect.Y, rect.Height, rect.Width);
		}
		Rect dest = FitToPage(((printTicket2.PageBorderless.HasValue && printTicket2.PageBorderless.Value == PageBorderless.Borderless) || (NearlyEqual(rect.X, 0.0) && NearlyEqual(rect.Y, 0.0) && NearlyEqual(rect.Width, pageSize.Width) && NearlyEqual(rect.Height, pageSize.Height))) ? new Rect(0.0, 0.0, pageSize.Width, pageSize.Height) : rect, bitmapSource);
		int num = ChooseTargetDpi(printTicket2, printCapabilities, 300);
		double num2 = dest.Width / 96.0;
		double num3 = dest.Height / 96.0;
		int targetPxW = Math.Max(1, (int)Math.Round(num2 * (double)num));
		int targetPxH = Math.Max(1, (int)Math.Round(num3 * (double)num));
		BitmapSource preparedBitmap = PrepareBitmapForPrint(bitmapSource, targetPxW, targetPxH, num);
		FixedPage fixedPage = BuildFixedPage(pageSize, preparedBitmap, dest);
		printQueue.CurrentJobSettings.Description = Resources.PrintJobName;
		PrintQueue.CreateXpsDocumentWriter(printQueue).Write(fixedPage, printTicket2);
	}

	private static Size GetPageSizeInDips(PrintTicket ticket, PrintCapabilities caps)
	{
		if (ticket != null && ticket.PageMediaSize?.Width.HasValue == true && ticket.PageMediaSize.Height.HasValue)
		{
			return new Size(ticket.PageMediaSize.Width.Value, ticket.PageMediaSize.Height.Value);
		}
		if (caps?.PageImageableArea != null)
		{
			PageImageableArea pageImageableArea = caps.PageImageableArea;
			return new Size(pageImageableArea.OriginWidth + pageImageableArea.ExtentWidth, pageImageableArea.OriginHeight + pageImageableArea.ExtentHeight);
		}
		return new Size(0.0, 0.0);
	}

	private static void NormalizePageSizeForOrientation(PrintTicket ticket, ref Size pageSize)
	{
		if (ticket != null && ticket.PageOrientation.HasValue)
		{
			bool num = ticket.PageOrientation == PageOrientation.Landscape || ticket.PageOrientation == PageOrientation.ReverseLandscape;
			bool flag = pageSize.Width >= pageSize.Height;
			if (num != flag)
			{
				pageSize = new Size(pageSize.Height, pageSize.Width);
			}
		}
	}

	private static Rect GetImageableAreaInDips(PrintCapabilities caps, Size pageSize)
	{
		if (caps?.PageImageableArea != null)
		{
			PageImageableArea pageImageableArea = caps.PageImageableArea;
			return new Rect(pageImageableArea.OriginWidth, pageImageableArea.OriginHeight, pageImageableArea.ExtentWidth, pageImageableArea.ExtentHeight);
		}
		return new Rect(0.0, 0.0, pageSize.Width, pageSize.Height);
	}

	private static bool IsLandscape(PrintTicket ticket)
	{
		if (ticket == null || !ticket.PageOrientation.HasValue)
		{
			return false;
		}
		if (ticket.PageOrientation != PageOrientation.Landscape)
		{
			return ticket.PageOrientation == PageOrientation.ReverseLandscape;
		}
		return true;
	}

	private static bool NearlyEqual(double a, double b, double eps = 0.5)
	{
		return Math.Abs(a - b) <= eps;
	}

	private static Rect FitToPage(Rect target, BitmapSource src)
	{
		double num = (double)src.PixelWidth / src.DpiX;
		double num2 = (double)src.PixelHeight / src.DpiY;
		double num3 = num / num2;
		double num4 = target.Width / 96.0;
		double num5 = target.Height / 96.0;
		double num6;
		double num7;
		if (num3 >= num4 / num5)
		{
			num6 = num4;
			num7 = num4 / num3;
		}
		else
		{
			num7 = num5;
			num6 = num5 * num3;
		}
		double width = num6 * 96.0;
		double height = num7 * 96.0;
		return new Rect(target.X, target.Y, width, height);
	}

	private static int ChooseTargetDpi(PrintTicket ticket, PrintCapabilities caps, int fallbackDpi)
	{
		if (ticket?.PageResolution != null && ticket.PageResolution.QualitativeResolution.HasValue)
		{
			return ticket.PageResolution.QualitativeResolution.Value switch
			{
				PageQualitativeResolution.High => 600, 
				PageQualitativeResolution.Normal => 300, 
				PageQualitativeResolution.Draft => 150, 
				_ => fallbackDpi, 
			};
		}
		if (caps?.PageResolutionCapability != null && caps.PageResolutionCapability.Count > 0)
		{
			int num = caps.PageResolutionCapability.Select((PageResolution r) => r.QualitativeResolution.HasValue ? (r.QualitativeResolution.Value switch
			{
				PageQualitativeResolution.High => 600, 
				PageQualitativeResolution.Normal => 300, 
				PageQualitativeResolution.Draft => 150, 
				_ => 0, 
			}) : 0).DefaultIfEmpty(0).Max();
			if (num >= 150)
			{
				return num;
			}
		}
		return fallbackDpi;
	}

	private static BitmapSource PrepareBitmapForPrint(BitmapSource src, int targetPxW, int targetPxH, int targetDpi)
	{
		BitmapSource bitmapSource = src;
		if (bitmapSource.Format != PixelFormats.Bgr32)
		{
			bitmapSource = new FormatConvertedBitmap(bitmapSource, PixelFormats.Bgr32, null, 0.0);
		}
		double scaleX = (double)targetPxW / (double)bitmapSource.PixelWidth;
		double scaleY = (double)targetPxH / (double)bitmapSource.PixelHeight;
		TransformedBitmap transformedBitmap = new TransformedBitmap(bitmapSource, new ScaleTransform(scaleX, scaleY));
		int num = targetPxW * 4;
		byte[] pixels = new byte[targetPxH * num];
		transformedBitmap.CopyPixels(pixels, num, 0);
		BitmapSource bitmapSource2 = BitmapSource.Create(targetPxW, targetPxH, targetDpi, targetDpi, PixelFormats.Bgr32, null, pixels, num);
		bitmapSource2.Freeze();
		return bitmapSource2;
	}

	private static FixedPage BuildFixedPage(Size pageSize, BitmapSource preparedBitmap, Rect dest)
	{
		FixedPage fixedPage = new FixedPage
		{
			Width = pageSize.Width,
			Height = pageSize.Height,
			Background = Brushes.White,
			SnapsToDevicePixels = true,
			UseLayoutRounding = true
		};
		Image image = new Image
		{
			Width = dest.Width,
			Height = dest.Height,
			Stretch = Stretch.Fill,
			Source = preparedBitmap,
			SnapsToDevicePixels = true,
			UseLayoutRounding = true
		};
		RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
		FixedPage.SetLeft(image, dest.X);
		FixedPage.SetTop(image, dest.Y);
		fixedPage.Children.Add(image);
		fixedPage.Measure(new Size(fixedPage.Width, fixedPage.Height));
		fixedPage.Arrange(new Rect(new Point(0.0, 0.0), new Size(fixedPage.Width, fixedPage.Height)));
		fixedPage.UpdateLayout();
		return fixedPage;
	}
}
