using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Channels;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MoreLinq.Extensions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace CameraCapture;

public class CameraCapturer : SafeDisposable
{
	private const uint StreamIndex = 4294967292u;

	private readonly ILoggerFactory loggerFactory;

	private readonly ILogger logger;

	private readonly bool reducedLogging;

	private readonly string cameraDeviceId;

	private readonly Size requestedFrameSize;

	private readonly int requestedFrameRate;

	private IReadOnlyCollection<VideoMediaType> nativeVideoMediaTypes;

	private VideoMediaType currentVideoMediaType;

	private ComPtr<IMFSourceReaderEx> sourceReader;

	private PixelFormatConverter pixelFormatConverter;

	private int capturedFrameCounter;

	private long? logStatisticsNextTimestamp;

	private readonly ChannelWriter<IRawImageAccessor> frameWriter;

	private Thread captureThread;

	private CancellationTokenSource cts = new CancellationTokenSource();

	public ChannelReader<IRawImageAccessor> Frames { get; }

	public CameraCapturer(string cameraDeviceId, Size frameSize, int frameRate, ILoggerFactory loggerFactory, bool reducedLogging)
	{
		this.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
		logger = loggerFactory.CreateLogger<CameraCapturer>();
		this.reducedLogging = reducedLogging;
		if (frameSize.Width <= 0 || frameSize.Height <= 0)
		{
			throw new ArgumentOutOfRangeException("frameSize");
		}
		if (frameRate <= 0)
		{
			throw new ArgumentOutOfRangeException("frameRate");
		}
		this.cameraDeviceId = cameraDeviceId ?? throw new ArgumentNullException("cameraDeviceId");
		requestedFrameSize = frameSize;
		requestedFrameRate = frameRate;
		try
		{
			Channel<IRawImageAccessor> channel = Channel.CreateBounded<IRawImageAccessor>(new BoundedChannelOptions(1)
			{
				SingleWriter = true
			});
			frameWriter = channel.Writer;
			Frames = channel.Reader;
			captureThread = new Thread(CaptureVideo)
			{
				Name = "camera video capture",
				Priority = ThreadPriority.Highest,
				IsBackground = true
			};
			captureThread.Start();
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public bool ShouldCurrentVideoMediaTypeBeChanged(Size requestedFrameSize, int requestedFrameRate)
	{
		return ThreadEx.RunInMTA(() =>
		{
			if (nativeVideoMediaTypes == null || currentVideoMediaType == null)
			{
				return false;
			}
			VideoMediaType videoMediaType = SelectOptimalVideoMediaType(requestedFrameSize, requestedFrameRate);
			return videoMediaType.Width != currentVideoMediaType.Width || videoMediaType.Height != currentVideoMediaType.Height || videoMediaType.FrameRate != currentVideoMediaType.FrameRate;
		});
	}

	private unsafe void CreateCaptureObjects()
	{
		using ComPtr<IMFActivate> device = GetCameraDeviceById(cameraDeviceId);
		if (!reducedLogging)
		{
			LogCameraDeviceProperties(device);
		}
		device.Ptr->ActivateObject(typeof(IMFMediaSource).GUID, out var ppv).ThrowOnFailure("IMFActivate.ActivateObject(IMFMediaSource)");
		using ComPtr<IMFMediaSource> mediaSource = (IMFMediaSource*)ppv;
		sourceReader = CreateSourceReader(mediaSource);
		nativeVideoMediaTypes = GetNativeVideoMediaTypes();
		if (!reducedLogging)
		{
			logger.LogInformation("Camera supports {formatCount} native video formats", nativeVideoMediaTypes.Count);
			logger.LogInformation("Camera frame rates: {frameRates}", from x in nativeVideoMediaTypes.Select((VideoMediaType x) => x.FrameRate).Distinct()
				orderby x descending
				select x);
			foreach (IGrouping<string, VideoMediaType> item in from x in nativeVideoMediaTypes
				group x by x.PixelFormatAsString into x
				orderby x.Key
				select x)
			{
				logger.LogInformation("Camera pixel format {pixelFormat}: {frameSizes}", item.Key, from x in item
					orderby x.Width descending, x.Height descending
					group x by $"{x.Width}x{x.Height}" into x
					select $"{x.Key}@{x.Max((VideoMediaType videoMediaType2) => videoMediaType2.FrameRate)}");
			}
		}
		if (!reducedLogging)
		{
			logger.LogInformation("Default camera format: {videoMediaType}", GetCurrentVideoMediaType());
		}
		logger.LogInformation("Requested frame size and frame rate: {frameWidth}x{frameHeight}@{frameRate}", requestedFrameSize.Width, requestedFrameSize.Height, requestedFrameRate);
		currentVideoMediaType = SelectOptimalVideoMediaType(requestedFrameSize, requestedFrameRate);
		logger.LogInformation("Selected camera format: {videoMediaType}", currentVideoMediaType);
		SetNativeVideoMediaType(currentVideoMediaType);
		VideoMediaType videoMediaType = GetCurrentVideoMediaType();
		if (videoMediaType.Width != currentVideoMediaType.Width || videoMediaType.Height != currentVideoMediaType.Height || videoMediaType.FrameRate != currentVideoMediaType.FrameRate || videoMediaType.Subtype != currentVideoMediaType.Subtype)
		{
			throw new InvalidOperationException("Output media type on the camera does not match its native media type.");
		}
		ImageFormat inputImageFormat = new ImageFormat(currentVideoMediaType.Width, currentVideoMediaType.Height, currentVideoMediaType.PixelFormat);
		pixelFormatConverter = new PixelFormatConverter(inputImageFormat, loggerFactory.CreateLogger<PixelFormatConverter>());
	}

	private unsafe static ComPtr<IMFActivate> GetCameraDeviceById(string cameraDeviceId)
	{
		IMFAttributes* ptr = default;
		PInvoke.MFCreateAttributes(&ptr, 1u).ThrowOnFailure("MFCreateAttributes()");
		using ComPtr<IMFAttributes> comPtr = ptr;
		comPtr.Ptr->SetGUID(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID).ThrowOnFailure("IMFAttributes.SetGUID(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE)");
		PInvoke.MFEnumDeviceSources(comPtr, out var pppSourceActivate, out var pcSourceActivate).ThrowOnFailure("MFEnumDeviceSources()");
		IMFActivate* ptr2 = null;
		for (int i = 0; i < pcSourceActivate; i++)
		{
			try
			{
				using ComPtr<IMFActivate> comPtr2 = pppSourceActivate[i];
				comPtr2.Ptr->GetAllocatedString(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, out var ppwszValue, out var _).ThrowOnFailure("IMFActivate.GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK)");
				string text = ppwszValue.ToString();
				Marshal.FreeCoTaskMem((IntPtr)(char*)ppwszValue);
				if (text == cameraDeviceId)
				{
					ptr2 = comPtr2.Ptr;
					ptr2->AddRef();
				}
			}
			catch
			{
			}
		}
		Marshal.FreeCoTaskMem((IntPtr)pppSourceActivate);
		if (ptr2 == null)
		{
			throw new InvalidOperationException("Camera " + cameraDeviceId + " not found.");
		}
		return ptr2;
	}

	private unsafe void LogCameraDeviceProperties(ComPtr<IMFActivate> device)
	{
		device.Ptr->GetAllocatedString(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, out var ppwszValue, out var _).ThrowOnFailure("IMFActivate.GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME)");
		logger.LogInformation("Camera name: {cameraName}", ppwszValue);
		Marshal.FreeCoTaskMem((IntPtr)(char*)ppwszValue);
	}

	private unsafe static ComPtr<IMFSourceReaderEx> CreateSourceReader(ComPtr<IMFMediaSource> mediaSource)
	{
		IMFAttributes* ptr = default;
		PInvoke.MFCreateAttributes(&ptr, 1u).ThrowOnFailure("MFCreateAttributes()");
		using ComPtr<IMFAttributes> comPtr = ptr;
		comPtr.Ptr->SetUINT32(in PInvoke.MF_LOW_LATENCY, 1u).ThrowOnFailure("IMFAttributes.SetUINT32(MF_LOW_LATENCY)");
		comPtr.Ptr->SetUINT32(in PInvoke.MF_READWRITE_DISABLE_CONVERTERS, 1u).ThrowOnFailure("IMFAttributes.SetUINT32(MF_READWRITE_DISABLE_CONVERTERS)");
		IMFSourceReader* ptr2 = default;
		PInvoke.MFCreateSourceReaderFromMediaSource(mediaSource, comPtr, &ptr2).ThrowOnFailure("MFCreateSourceReaderFromMediaSource()");
		using ComPtr<IMFSourceReader> comPtr2 = ptr2;
		comPtr2.Ptr->QueryInterface(typeof(IMFSourceReaderEx).GUID, out var ppvObject).ThrowOnFailure("IMFSourceReader.QueryInterface(IMFSourceReaderEx)");
		return (IMFSourceReaderEx*)ppvObject;
	}

	private unsafe VideoMediaType GetCurrentVideoMediaType()
	{
		IMFMediaType* ptr = default;
		sourceReader.Ptr->GetCurrentMediaType(4294967292u, &ptr).ThrowOnFailure("IMFSourceReader.GetCurrentMediaType()");
		using ComPtr<IMFMediaType> mediaType = ptr;
		return new VideoMediaType(mediaType);
	}

	private unsafe List<VideoMediaType> GetNativeVideoMediaTypes()
	{
		List<VideoMediaType> list = new List<VideoMediaType>();
		uint num = 0u;
		IMFMediaType* ptr = default;
		while (true)
		{
			HRESULT nativeMediaType = sourceReader.Ptr->GetNativeMediaType(4294967292u, num, &ptr);
			if (nativeMediaType == HRESULT.MF_E_NO_MORE_TYPES)
			{
				break;
			}
			nativeMediaType.ThrowOnFailure("IMFSourceReader.GetNativeMediaType()");
			using (ComPtr<IMFMediaType> mediaType = ptr)
			{
				list.Add(new VideoMediaType(mediaType));
			}
			num++;
		}
		return list;
	}

	private VideoMediaType SelectOptimalVideoMediaType(Size requestedFrameSize, int requestedFrameRate)
	{
		List<VideoMediaType> list = nativeVideoMediaTypes.Where((VideoMediaType x) => x.PixelFormat != PixelFormat.None).ToList();
		if (list.Count == 0)
		{
			throw new NotSupportedException("None of the camera media types are supported.");
		}
		list = list.Minima((VideoMediaType x) => Math.Abs(x.FrameRate - requestedFrameRate)).Maxima((VideoMediaType x) => x.FrameRate).ToList();
		List<VideoMediaType> list2 = list.Where((VideoMediaType x) => x.Width >= 640).ToList();
		if (list2.Count > 0)
		{
			list = list2;
		}
		list = list.Minima((VideoMediaType x) => GetXoredSquare(requestedFrameSize, new Size(x.Width, x.Height))).Maxima((VideoMediaType x) => x.Width * x.Height).ToList();
		return list.Maxima((VideoMediaType x) => GetPixelFormatPriority(x.PixelFormat)).First();
	}

	private static int GetXoredSquare(Size size1, Size size2)
	{
		Rectangle a = new Rectangle(Point.Empty, size1);
		Rectangle b = new Rectangle(Point.Empty, size2);
		Rectangle rectangle = Rectangle.Intersect(a, b);
		return a.Width * a.Height + b.Width * b.Height - rectangle.Width * rectangle.Height * 2;
	}

	private static int GetPixelFormatPriority(PixelFormat pixelFormat)
	{
		return pixelFormat switch
		{
			PixelFormat.BGRX => 4, 
			PixelFormat.BGRA => 3, 
			PixelFormat.NV12 => 2, 
			PixelFormat.YUYV422 => 1, 
			_ => 0, 
		};
	}

	private unsafe void SetNativeVideoMediaType(VideoMediaType newMediaType)
	{
		IMFMediaType* ptr = default;
		PInvoke.MFCreateMediaType(&ptr).ThrowOnFailure("MFCreateMediaType()");
		using ComPtr<IMFMediaType> comPtr = ptr;
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_MAJOR_TYPE, in PInvoke.MFMediaType_Video).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_MAJOR_TYPE)");
		comPtr.Ptr->SetGUID(in PInvoke.MF_MT_SUBTYPE, newMediaType.Subtype).ThrowOnFailure("IMFMediaType.SetGUID(MF_MT_SUBTYPE)");
		comPtr.Ptr->SetUINT64(in PInvoke.MF_MT_FRAME_SIZE, (ulong)(((long)newMediaType.Width << 32) | (uint)newMediaType.Height)).ThrowOnFailure("IMFMediaType.SetUINT64(MF_MT_FRAME_SIZE)");
		comPtr.Ptr->SetUINT64(in PInvoke.MF_MT_FRAME_RATE, (ulong)(((long)newMediaType.FrameRateNumerator << 32) | (uint)newMediaType.FrameRateDenominator)).ThrowOnFailure("IMFMediaType.SetUINT64(MF_MT_FRAME_RATE)");
		sourceReader.Ptr->SetNativeMediaType(4294967292u, comPtr, out var _).ThrowOnFailure("IMFSourceReaderEx.SetNativeMediaType()");
	}

	private void CaptureVideo()
	{
		try
		{
			CreateCaptureObjects();
			using HighResolutionSleeper highResolutionSleeper = new HighResolutionSleeper();
			while (!IsDisposed)
			{
				if (TryCaptureFrame(out var frame))
				{
					frameWriter.WriteAsync(frame, cts.Token).AsTask().GetAwaiter()
						.GetResult();
				}
				highResolutionSleeper.Sleep(1);
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception error)
		{
			if (!IsDisposed)
			{
				frameWriter.Complete(error);
			}
		}
		finally
		{
			frameWriter.TryComplete();
		}
	}

	private unsafe bool TryCaptureFrame(out IRawImageAccessor frame)
	{
		frame = null;
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return false;
			}
			long timestamp = Stopwatch.GetTimestamp();
			MF_SOURCE_READER_FLAG mF_SOURCE_READER_FLAG = default;
			long num = default;
			IMFSample* ptr = default;
			sourceReader.Ptr->ReadSample(4294967292u, 0u, null, (uint*)(&mF_SOURCE_READER_FLAG), &num, &ptr).ThrowOnFailure("IMFSourceReader.ReadSample()");
			if (mF_SOURCE_READER_FLAG != 0 && mF_SOURCE_READER_FLAG != MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_STREAMTICK)
			{
				logger.LogWarning("Camera capturer received non-zero stream flags: {streamFlags}", mF_SOURCE_READER_FLAG);
			}
			if (ptr == null)
			{
				return false;
			}
			using ComPtr<IMFSample> inputSample = ptr;
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Captured camera video frame: ts={ts}, {width}x{height} [time={time}ms]", num, currentVideoMediaType.Width, currentVideoMediaType.Height, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			using ComPtr<IMFSample> sample = pixelFormatConverter.Convert(inputSample);
			frame = new MediaFoundationRawImageAccessor(pixelFormatConverter.OutputImageFormat, sample);
			long valueOrDefault = logStatisticsNextTimestamp.GetValueOrDefault();
			if (!logStatisticsNextTimestamp.HasValue)
			{
				valueOrDefault = Stopwatch.GetTimestamp() + Stopwatch.Frequency;
				logStatisticsNextTimestamp = valueOrDefault;
			}
			capturedFrameCounter++;
			LogStatisticsIfTimeIsRight();
			return true;
		}
	}

	private void LogStatisticsIfTimeIsRight()
	{
		if (Stopwatch.GetTimestamp() >= logStatisticsNextTimestamp)
		{
			logger.LogInformation("Captured {actualFrames}/{expectedFrames} camera video frames per second", capturedFrameCounter, currentVideoMediaType.FrameRate);
			logStatisticsNextTimestamp += Stopwatch.Frequency;
			capturedFrameCounter = 0;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			cts.Cancel();
			captureThread?.Join();
			captureThread = null;
			cts.Dispose();
			cts = null;
			pixelFormatConverter?.Dispose();
			pixelFormatConverter = null;
		}
		ThreadEx.RunInMTA(sourceReader.Dispose);
	}
}
