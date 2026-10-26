using System;
using AudioCapture;
using AudioDevices;
using Common;
using Microsoft.Extensions.Logging;
using ScreenRecorder.OutgoingMessages.Events;

namespace ScreenRecorder;

internal sealed class AudioLevelSession : BaseDisposable
{
	private readonly StoppableStopwatch stopwatch = new StoppableStopwatch();

	private readonly AudioCapturer outputAudioCapturer;

	private readonly AudioCapturer inputAudioCapturer;

	private readonly IEventProducer eventProducer;

	public AudioLevelSession(string inputDeviceId, bool isInputMuted, string outputDeviceId, bool isOutputMuted, IEventProducer eventProducer, ILoggerFactory loggerFactory)
	{
		this.eventProducer = eventProducer ?? throw new ArgumentNullException("eventProducer");
		try
		{
			outputAudioCapturer = new AudioCapturer(AudioDataFlow.Render, stopwatch, new AudioLevelReporter(AudioDataFlow.Render, eventProducer), loggerFactory);
			inputAudioCapturer = new AudioCapturer(AudioDataFlow.Capture, stopwatch, new AudioLevelReporter(AudioDataFlow.Capture, eventProducer), loggerFactory);
			outputAudioCapturer.DeviceFailed += AudioCapturer_DeviceFailed;
			inputAudioCapturer.DeviceFailed += AudioCapturer_DeviceFailed;
			outputAudioCapturer.SetDevice(outputDeviceId, isOutputMuted);
			inputAudioCapturer.SetDevice(inputDeviceId, isInputMuted);
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public void SetDevice(AudioDataFlow dataFlow, string deviceId, bool isMuted)
	{
		((dataFlow == AudioDataFlow.Render) ? outputAudioCapturer : inputAudioCapturer).SetDevice(deviceId, isMuted);
	}

	private void AudioCapturer_DeviceFailed(object sender, AudioDeviceFailedEventArgs e)
	{
		eventProducer.Events.TryWrite(new AudioDeviceFailedEvent(e.DataFlow, e.DeviceId, e.Exception));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			outputAudioCapturer?.Dispose();
			inputAudioCapturer?.Dispose();
		}
	}
}
