using System;
using AudioCapture;
using AudioDevices;
using AudioVideoCore;
using Common;
using ScreenRecorder.OutgoingMessages.Events;

namespace ScreenRecorder;

internal sealed class AudioLevelReporter : IAudioFrameObserver
{
	private readonly AudioDataFlow dataFlow;

	private readonly IEventProducer eventProducer;

	public AudioLevelReporter(AudioDataFlow dataFlow, IEventProducer eventProducer)
	{
		if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
		{
			throw new ArgumentException($"Invalid value: {dataFlow}", "dataFlow");
		}
		this.dataFlow = dataFlow;
		this.eventProducer = eventProducer ?? throw new ArgumentNullException("eventProducer");
	}

	public void OnFrameCaptured(ReadOnlySpan<byte> data, AudioFormat format)
	{
		float level = AudioLevelMeter.ComputeRms(data, format);
		eventProducer.Events.TryWrite(new AudioLevelEvent(dataFlow, level));
	}
}
