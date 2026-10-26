using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AudioVideoCore;

namespace SkrinshoterApp;

public static class RawImageExtensions
{
	public static BitmapSource ToBitmapSource(this SharedMemoryRawImage image)
	{
		return Imaging.CreateBitmapSourceFromMemorySection(image.GetHandle(), image.Format.Width, image.Format.Height, PixelFormats.Bgr32, image.Stride[0], 0);
	}
}
