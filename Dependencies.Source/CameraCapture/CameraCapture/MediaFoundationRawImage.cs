using System;
using AudioVideoCore;
using Common;
using Windows.Win32.Media.MediaFoundation;

namespace CameraCapture;

internal class MediaFoundationRawImage : RawImage
{
	private readonly ComPtr<IMF2DBuffer2> buffer;

	public unsafe MediaFoundationRawImage(ImageFormat format, void* data, int stride, ComPtr<IMF2DBuffer2> buffer)
		: base(format, data, stride)
	{
		if (buffer.Ptr == null)
		{
			throw new ArgumentNullException("buffer");
		}
		this.buffer = buffer;
	}

	protected unsafe override void Dispose(bool disposing)
	{
		ThreadEx.RunInMTA(() => buffer.Ptr->Unlock2D());
	}
}
