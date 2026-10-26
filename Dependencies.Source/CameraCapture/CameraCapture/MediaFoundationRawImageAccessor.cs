using System;
using AudioVideoCore;
using Common;
using Windows.Win32.Media.MediaFoundation;

namespace CameraCapture;

internal class MediaFoundationRawImageAccessor : BaseDisposable, IRawImageAccessor, IDisposable
{
	private readonly ImageFormat imageFormat;

	private readonly ComPtr<IMF2DBuffer2> buffer;

	public unsafe MediaFoundationRawImageAccessor(ImageFormat imageFormat, ComPtr<IMFSample> sample)
	{
		this.imageFormat = imageFormat ?? throw new ArgumentNullException("imageFormat");
		if (sample.Ptr == null)
		{
			throw new ArgumentNullException("sample");
		}
		IMFMediaBuffer* ptr = default;
		sample.Ptr->GetBufferByIndex(0u, &ptr).ThrowOnFailure("IMFSample.GetBufferByIndex(0)");
		using ComPtr<IMFMediaBuffer> comPtr = ptr;
		comPtr.Ptr->QueryInterface(typeof(IMF2DBuffer2).GUID, out var ppvObject).ThrowOnFailure("IMFMediaBuffer.QueryInterface(IMF2DBuffer2)");
		buffer = (IMF2DBuffer2*)ppvObject;
	}

	public unsafe RawImage GetImage()
	{
		return ThreadEx.RunInMTA(() =>
		{
			buffer.Ptr->Lock2DSize(MF2DBuffer_LockFlags.MF2DBuffer_LockFlags_Read, out var ppbScanline, out var plPitch, out var _, out var _).ThrowOnFailure("IMF2DBuffer2.Lock2DSize()");
			try
			{
				return new MediaFoundationRawImage(imageFormat, ppbScanline, plPitch, buffer);
			}
			catch
			{
				buffer.Ptr->Unlock2D();
				throw;
			}
		});
	}

	protected override void Dispose(bool disposing)
	{
		ThreadEx.RunInMTA(buffer.Dispose);
	}
}
