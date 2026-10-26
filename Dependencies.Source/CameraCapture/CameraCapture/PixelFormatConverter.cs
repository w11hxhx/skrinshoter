using System;
using System.Diagnostics;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com;

namespace CameraCapture;

internal class PixelFormatConverter : BaseDisposable
{
	private readonly ILogger logger;

	private ComPtr<IMFTransform> transform;

	public ImageFormat InputImageFormat { get; }

	public ImageFormat OutputImageFormat { get; }

	public PixelFormatConverter(ImageFormat inputImageFormat, ILogger<PixelFormatConverter> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		InputImageFormat = inputImageFormat ?? throw new ArgumentNullException("inputImageFormat");
		OutputImageFormat = new ImageFormat(inputImageFormat.Width, inputImageFormat.Height, PixelFormat.BGRX);
		try
		{
			ThreadEx.RunInMTA(CreateTransformObject);
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe ComPtr<IMFSample> Convert(ComPtr<IMFSample> inputSample)
	{
		return ThreadEx.RunInMTA(() =>
		{
			long timestamp = Stopwatch.GetTimestamp();
			transform.Ptr->ProcessInput(0u, inputSample, 0u).ThrowOnFailure("IMFTransform.ProcessInput()");
			ComPtr<IMFSample> comPtr = CreateOutputSample();
			try
			{
				MFT_OUTPUT_DATA_BUFFER mFT_OUTPUT_DATA_BUFFER = new MFT_OUTPUT_DATA_BUFFER
				{
					pSample = comPtr
				};
				uint num = default;
				transform.Ptr->ProcessOutput(0u, 1u, &mFT_OUTPUT_DATA_BUFFER, &num).ThrowOnFailure("IMFTransform.ProcessOutput()");
				if (mFT_OUTPUT_DATA_BUFFER.pEvents != null)
				{
					mFT_OUTPUT_DATA_BUFFER.pEvents->Release();
				}
			}
			catch
			{
				comPtr.Dispose();
				throw;
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Converted camera video frame from {inputImageFormat} to {outputImageFormat} [time={time}ms]", InputImageFormat, OutputImageFormat, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return comPtr;
		});
	}

	private unsafe void CreateTransformObject()
	{
		PInvoke.CoCreateInstance(in PInvoke.CLSID_VideoProcessorMFT, null, CLSCTX.CLSCTX_INPROC_SERVER, typeof(IMFTransform).GUID, out var ppv).ThrowOnFailure("CoCreateInstance(CLSID_VideoProcessorMFT)");
		transform = (IMFTransform*)ppv;
		SetInputType();
		SetOutputType();
	}

	private unsafe void SetInputType()
	{
		IMFMediaType* ptr = default;
		PInvoke.MFCreateMediaType(&ptr).ThrowOnFailure("MFCreateMediaType()");
		using ComPtr<IMFMediaType> comPtr = ptr;
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_MAJOR_TYPE, in PInvoke.MFMediaType_Video).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_MAJOR_TYPE)");
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_SUBTYPE, InputImageFormat.PixelFormat.ToGuid()).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_SUBTYPE)");
		comPtr.Ptr->SetUINT64(in PInvoke.MF_MT_FRAME_SIZE, (ulong)(((long)InputImageFormat.Width << 32) | (uint)InputImageFormat.Height)).ThrowOnFailure("IMFMediaType.SetUINT64(MF_MT_FRAME_SIZE)");
		transform.Ptr->SetInputType(0u, comPtr, 0u).ThrowOnFailure("IMFTransform.SetInputType()");
	}

	private unsafe void SetOutputType()
	{
		IMFMediaType* ptr = default;
		PInvoke.MFCreateMediaType(&ptr).ThrowOnFailure("MFCreateMediaType()");
		using ComPtr<IMFMediaType> comPtr = ptr;
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_MAJOR_TYPE, in PInvoke.MFMediaType_Video).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_MAJOR_TYPE)");
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_SUBTYPE, OutputImageFormat.PixelFormat.ToGuid()).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_SUBTYPE)");
		comPtr.Ptr->SetUINT64(in PInvoke.MF_MT_FRAME_SIZE, (ulong)(((long)OutputImageFormat.Width << 32) | (uint)OutputImageFormat.Height)).ThrowOnFailure("IMFMediaType.SetUINT64(MF_MT_FRAME_SIZE)");
		transform.Ptr->SetOutputType(0u, comPtr, 0u).ThrowOnFailure("IMFTransform.SetOutputType()");
	}

	private unsafe ComPtr<IMFSample> CreateOutputSample()
	{
		uint dwFourCC = BitConverter.ToUInt32(OutputImageFormat.PixelFormat.ToGuid().ToByteArray(), 0);
		IMFMediaBuffer* ptr = default;
		PInvoke.MFCreate2DMediaBuffer((uint)OutputImageFormat.Width, (uint)OutputImageFormat.Height, dwFourCC, false, &ptr).ThrowOnFailure("MFCreate2DMediaBuffer()");
		using ComPtr<IMFMediaBuffer> comPtr = ptr;
		IMFSample* ptr2 = default;
		PInvoke.MFCreateSample(&ptr2).ThrowOnFailure("MFCreateSample()");
		using ComPtr<IMFSample> comPtr2 = ptr2;
		comPtr2.Ptr->AddBuffer(comPtr).ThrowOnFailure("IMFSample.AddBuffer()");
		comPtr2.Ptr->AddRef();
		return ptr2;
	}

	protected override void Dispose(bool disposing)
	{
		ThreadEx.RunInMTA(transform.Dispose);
	}
}
