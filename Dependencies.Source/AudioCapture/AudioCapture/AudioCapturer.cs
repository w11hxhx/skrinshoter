using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AudioDevices;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ScreenRecorderCommon;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Media.Audio;
using Windows.Win32.System.Com;

namespace AudioCapture;

public class AudioCapturer : SafeDisposable
{
	private sealed class DeviceRequest
	{
		public string DeviceId { get; }

		public bool IsMuted { get; }

		public TaskCompletionSource<bool> Completion { get; }

		public DeviceRequest(string deviceId, bool isMuted, TaskCompletionSource<bool> completion)
		{
			DeviceId = deviceId;
			IsMuted = isMuted;
			Completion = completion;
		}
	}

	private const long CaptureBufferDuration = 30000000L;

	private const int PollIntervalMs = 5;

	private const int MaxAllowedLagMs = 50;

	private readonly ILogger logger;

	private readonly AudioDataFlow dataFlow;

	private readonly string logPrefix;

	private readonly StoppableStopwatch stopwatch;

	private readonly IAudioFrameObserver frameObserver;

	private readonly AudioDeviceEnumerator deviceEnumerator;

	private readonly ChannelWriter<AudioFrame> frameWriter;

	private readonly Channel<DeviceRequest> deviceRequestChannel = Channel.CreateUnbounded<DeviceRequest>(new UnboundedChannelOptions
	{
		SingleReader = true
	});

	private readonly Thread captureThread;

	private ComPtr<IMMDevice> device;

	private ComPtr<IAudioClient> audioClient;

	private ComPtr<IAudioCaptureClient> audioCaptureClient;

	private string currentDeviceId;

	private bool isMuted;

	private bool isSilent = true;

	private bool wasStopwatchRunning;

	private long nextFrameTimestamp;

	private readonly AsyncEventInvoker<AudioDeviceFailedEventArgs> deviceFailedEventInvoker;

	public AudioFormat AudioFormat { get; private set; } = new AudioFormat(48000, 2, SampleFormat.Float);

	public ChannelReader<AudioFrame> Frames { get; }

	public event EventHandler<AudioDeviceFailedEventArgs> DeviceFailed
	{
		add
		{
			deviceFailedEventInvoker.Subscribe(value);
		}
		remove
		{
			deviceFailedEventInvoker.Unsubscribe(value);
		}
	}

	public AudioCapturer(AudioDataFlow dataFlow, StoppableStopwatch stopwatch, IAudioFrameObserver frameObserver, ILoggerFactory loggerFactory)
	{
		if (dataFlow == AudioDataFlow.All)
		{
			throw new ArgumentException("Data flow must be either Render or Capture.", "dataFlow");
		}
		if (loggerFactory == null)
		{
			loggerFactory = NullLoggerFactory.Instance;
		}
		logger = loggerFactory.CreateLogger(typeof(AudioCapturer).Namespace + "." + ((dataFlow == AudioDataFlow.Render) ? "Output" : "Input") + "AudioCapturer");
		this.dataFlow = dataFlow;
		logPrefix = ((dataFlow == AudioDataFlow.Render) ? "[out] " : "[in]  ");
		this.stopwatch = stopwatch ?? throw new ArgumentNullException("stopwatch");
		this.frameObserver = frameObserver;
		deviceFailedEventInvoker = new AsyncEventInvoker<AudioDeviceFailedEventArgs>(logger, "DeviceFailed");
		Channel<AudioFrame> channel = Channel.CreateUnbounded<AudioFrame>(new UnboundedChannelOptions
		{
			SingleWriter = true
		});
		frameWriter = channel.Writer;
		Frames = channel.Reader;
		try
		{
			deviceEnumerator = new AudioDeviceEnumerator(subscribeToChanges: false, loggerFactory);
			captureThread = new Thread(CaptureThreadProc)
			{
				Name = logPrefix + "audio capture",
				Priority = ThreadPriority.Highest,
				IsBackground = true
			};
			captureThread.SetApartmentState(ApartmentState.MTA);
			captureThread.Start();
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public Task SetDevice(string deviceId, bool isMuted)
	{
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
		DeviceRequest item = new DeviceRequest(deviceId, isMuted, taskCompletionSource);
		if (!deviceRequestChannel.Writer.TryWrite(item))
		{
			taskCompletionSource.TrySetResult(result: false);
		}
		return taskCompletionSource.Task;
	}

	private void CaptureThreadProc()
	{
		using (logger.BeginScope("{logPrefix}", logPrefix))
		{
			try
			{
				using HighResolutionSleeper highResolutionSleeper = new HighResolutionSleeper();
				while (!IsDisposed)
				{
					lock (InternalDisposeLockObject)
					{
						if (!IsDisposed)
						{
							DrainDeviceRequests();
							PollAudioDevice();
						}
					}
					highResolutionSleeper.Sleep(5);
				}
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Audio capture thread exited unexpectedly");
			}
			finally
			{
				frameWriter.TryComplete();
				UnbindDevice();
			}
		}
	}

	private void DrainDeviceRequests()
	{
		DeviceRequest deviceRequest = null;
		List<DeviceRequest> list = null;
		DeviceRequest item;
		while (deviceRequestChannel.Reader.TryRead(out item))
		{
			if (deviceRequest != null)
			{
				if (list == null)
				{
					list = new List<DeviceRequest>();
				}
				list.Add(deviceRequest);
			}
			deviceRequest = item;
		}
		if (deviceRequest == null)
		{
			return;
		}
		ApplyDeviceRequest(deviceRequest);
		deviceRequest.Completion?.TrySetResult(result: true);
		if (list == null)
		{
			return;
		}
		foreach (DeviceRequest item2 in list)
		{
			item2.Completion?.TrySetResult(result: true);
		}
	}

	private void ApplyDeviceRequest(DeviceRequest request)
	{
		if (request.DeviceId != currentDeviceId)
		{
			UnbindDevice();
			BindDevice(request.DeviceId);
		}
		isMuted = request.IsMuted;
	}

	private unsafe void BindDevice(string deviceId)
	{
		if (deviceId == null)
		{
			logger.LogInformation("No audio device bound");
			return;
		}
		try
		{
			IntPtr devicePointer = deviceEnumerator.GetDevicePointer(deviceId);
			device = (IMMDevice*)(void*)devicePointer;
			AudioDevice deviceInfo = AudioDeviceEnumerator.GetDeviceInfo(devicePointer);
			logger.LogInformation("Audio device: {audioDevice}", deviceInfo?.FriendlyName ?? "<unknown>");
			device.Ptr->Activate(typeof(IAudioClient).GUID, CLSCTX.CLSCTX_INPROC_SERVER, null, out var ppInterface).ThrowOnFailure("IMMDevice.Activate(IAudioClient)");
			audioClient = (IAudioClient*)ppInterface;
			audioClient.Ptr->GetMixFormat(out var ppDeviceFormat).ThrowOnFailure("IAudioClient.GetMixFormat()");
			AudioFormat audioFormat;
			try
			{
				audioFormat = WaveFormatConverter.ToAudioFormat(ppDeviceFormat);
				logger.LogInformation("Audio format: {audioFormat}", audioFormat);
				uint streamFlags = ((dataFlow == AudioDataFlow.Render) ? 131072u : 0u);
				try
				{
					audioClient.Ptr->Initialize(AUDCLNT_SHAREMODE.AUDCLNT_SHAREMODE_SHARED, streamFlags, 30000000L, 0L, ppDeviceFormat, null).ThrowOnFailure("IAudioClient.Initialize()");
				}
				catch (WinApiException ex) when (dataFlow == AudioDataFlow.Capture && ex.InnerException.HResult.In(HRESULT.E_ACCESSDENIED, HRESULT.E_UNEXPECTED))
				{
					throw new ErrorCodeException(ErrorCode.MicrophoneAccessDenied, ex);
				}
			}
			finally
			{
				PInvoke.CoTaskMemFree(ppDeviceFormat);
			}
			audioClient.Ptr->GetService(typeof(IAudioCaptureClient).GUID, out var ppv).ThrowOnFailure("IAudioClient.GetService(IAudioCaptureClient)");
			audioCaptureClient = (IAudioCaptureClient*)ppv;
			audioClient.Ptr->Start().ThrowOnFailure("IAudioClient.Start()");
			if (audioFormat.SampleRate != AudioFormat.SampleRate)
			{
				nextFrameTimestamp = (long)Math.Round((double)nextFrameTimestamp * (double)audioFormat.SampleRate / (double)AudioFormat.SampleRate);
			}
			AudioFormat = audioFormat;
			currentDeviceId = deviceId;
			isSilent = true;
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to bind audio device");
			UnbindDevice();
			deviceFailedEventInvoker.InvokeAsync(this, new AudioDeviceFailedEventArgs(dataFlow, deviceId, exception));
		}
	}

	private void PollAudioDevice()
	{
		long timestamp = stopwatch.GetTimestamp(out var isStarted);
		long num = ToSampleTimestamp(timestamp);
		bool flag = isStarted && !wasStopwatchRunning;
		wasStopwatchRunning = isStarted;
		long num2 = 0L;
		if (currentDeviceId != null)
		{
			long maxTimestamp;
			if (isStarted)
			{
				maxTimestamp = ((!(isMuted | flag)) ? long.MaxValue : nextFrameTimestamp);
			}
			else
			{
				maxTimestamp = num;
			}
			num2 = ReadDevicePackets(num, maxTimestamp);
		}
		if ((!isStarted | flag) || currentDeviceId == null || isMuted || isSilent)
		{
			GenerateSilence(num);
		}
		else if (dataFlow == AudioDataFlow.Capture && num2 > 0)
		{
			CompensateLag(num);
		}
	}

	private unsafe long ReadDevicePackets(long currentTimestamp, long maxTimestamp)
	{
		long num = 0L;
		try
		{
			long timestamp;
			while (true)
			{
				timestamp = Stopwatch.GetTimestamp();
				audioCaptureClient.Ptr->GetBuffer(out var ppData, out var pNumFramesToRead, out var pdwFlags, null, null).ThrowOnFailure("IAudioCaptureClient.GetBuffer()");
				if (pNumFramesToRead == 0)
				{
					break;
				}
				try
				{
					if (logger.IsEnabled(LogLevel.Debug))
					{
						logger.LogDebug("Captured {samples} audio samples, lag={lag}ms, flags={flags} [time={time}ms]", pNumFramesToRead, GetLagMs(currentTimestamp), (BufferFlags)pdwFlags, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
					}
					isSilent = (pdwFlags & 2) != 0;
					if (!isSilent)
					{
						NotifyFrameObserver(ppData, pNumFramesToRead);
						int num2 = (int)MathEx.Clamp(maxTimestamp - nextFrameTimestamp, 0L, pNumFramesToRead);
						if (num2 > 0)
						{
							int bytesPerSample = AudioFormat.SampleFormat.GetBytesPerSample();
							byte[] array = new byte[num2 * bytesPerSample * AudioFormat.Channels];
							Marshal.Copy((IntPtr)ppData, array, 0, array.Length);
							AudioFrame audioFrame = new AudioFrame(AudioFormat, array, new FrameTimestamp(nextFrameTimestamp, AudioFormat.SampleRate));
							nextFrameTimestamp += audioFrame.Samples;
							num += audioFrame.Samples;
							frameWriter.TryWrite(audioFrame);
						}
					}
				}
				finally
				{
					audioCaptureClient.Ptr->ReleaseBuffer(pNumFramesToRead).ThrowOnFailure("IAudioCaptureClient.ReleaseBuffer()");
				}
			}
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Captured 0 audio samples, lag={lag}ms [time={time}ms]", GetLagMs(currentTimestamp), StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			}
			return num;
		}
		catch (Exception exception)
		{
			HandleDeviceFailure(exception);
			return num;
		}
	}

	private unsafe void NotifyFrameObserver(byte* dataPtr, uint sampleCount)
	{
		if (frameObserver == null)
		{
			return;
		}
		int length = (int)sampleCount * AudioFormat.Channels * AudioFormat.SampleFormat.GetBytesPerSample();
		try
		{
			frameObserver.OnFrameCaptured(new ReadOnlySpan<byte>(dataPtr, length), AudioFormat);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Audio frame observer threw an exception");
		}
	}

	private void GenerateSilence(long currentSampleTimestamp)
	{
		int num = (int)(currentSampleTimestamp - nextFrameTimestamp);
		if (num > 0)
		{
			SilentAudioFrame silentAudioFrame = new SilentAudioFrame(AudioFormat, num, new FrameTimestamp(nextFrameTimestamp, AudioFormat.SampleRate));
			nextFrameTimestamp += silentAudioFrame.Samples;
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("Generated {samples} audio samples of silence", num);
			}
			frameWriter.TryWrite(silentAudioFrame);
		}
	}

	private void CompensateLag(long currentTimestamp)
	{
		long lagMs = GetLagMs(currentTimestamp);
		if (lagMs > 50)
		{
			logger.LogWarning("Audio lag {lag}ms exceeds {limit}ms limit, inserting {samples} samples of silence", lagMs, 50, currentTimestamp - nextFrameTimestamp);
			GenerateSilence(currentTimestamp);
		}
	}

	private long ToSampleTimestamp(long stopwatchTimestamp)
	{
		return (long)Math.Round((double)stopwatchTimestamp / (double)stopwatch.Frequency * (double)AudioFormat.SampleRate);
	}

	private long GetLagMs(long currentTimestamp)
	{
		return (currentTimestamp - nextFrameTimestamp) * 1000 / AudioFormat.SampleRate;
	}

	private void HandleDeviceFailure(Exception exception)
	{
		string deviceId = currentDeviceId;
		logger.LogError(exception, "Audio device failure");
		UnbindDevice();
		deviceFailedEventInvoker.InvokeAsync(this, new AudioDeviceFailedEventArgs(dataFlow, deviceId, exception));
	}

	private unsafe void UnbindDevice()
	{
		if (audioClient.Ptr != null)
		{
			audioClient.Ptr->Stop();
		}
		audioCaptureClient.Dispose();
		audioClient.Dispose();
		device.Dispose();
		currentDeviceId = null;
		isSilent = true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			deviceRequestChannel.Writer.TryComplete();
			captureThread?.Join();
			DeviceRequest item;
			while (deviceRequestChannel.Reader.TryRead(out item))
			{
				item.Completion?.TrySetResult(result: false);
			}
			deviceEnumerator?.Dispose();
		}
	}
}
