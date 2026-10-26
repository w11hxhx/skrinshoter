using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AudioCapture;
using AudioDevices;
using AudioVideoCore;
using Common;
using FFmpeg;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ScreenRecorder.OutgoingMessages.Events;

namespace ScreenRecorder;

internal sealed class SpeechDetector : BaseDisposable, IAudioFrameObserver
{
	private const int VadMode = 3;

	private static readonly AudioFormat vadAudioFormat = new AudioFormat(8000, 1, SampleFormat.S16);

	private const int FrameDurationMs = 30;

	private static readonly int FrameSamples = vadAudioFormat.SampleRate / 1000 * 30;

	private const int WindowFrameCount = 10;

	private const int TriggerFrameCount = 9;

	private const int SustainedSpeechMs = 700;

	private readonly AudioDataFlow dataFlow;

	private readonly IEventProducer eventProducer;

	private readonly ILogger logger;

	private readonly WebRtcVad vad = new WebRtcVad();

	private AudioFrameConverter converter;

	private byte[] inputBuffer = Array.Empty<byte>();

	private long inputSamples;

	private readonly Queue<bool> window = new Queue<bool>(11);

	private int voicedFrameCount;

	private bool episodeActive;

	private int episodeFrameCount;

	public SpeechDetector(AudioDataFlow dataFlow, IEventProducer eventProducer, ILogger<SpeechDetector> logger = null)
	{
		if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
		{
			throw new ArgumentException($"Invalid value: {dataFlow}", "dataFlow");
		}
		this.dataFlow = dataFlow;
		this.eventProducer = eventProducer ?? throw new ArgumentNullException("eventProducer");
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		vad.SetMode(3);
	}

	public void OnFrameCaptured(ReadOnlySpan<byte> data, AudioFormat format)
	{
		if (converter == null || converter.InputFormat != format)
		{
			RecreateConverter(format);
		}
		if (data.Length > inputBuffer.Length)
		{
			inputBuffer = new byte[data.Length];
		}
		data.CopyTo(inputBuffer);
		AudioFrame audioFrame = new AudioFrame(format, new Memory<byte>(inputBuffer, 0, data.Length), new FrameTimestamp(inputSamples, format.SampleRate));
		inputSamples += audioFrame.Samples;
		foreach (AudioFrame item in converter.ProcessFrame(audioFrame))
		{
			using (item)
			{
				ProcessVadDecision(vad.ProcessFrame(MemoryMarshal.Cast<byte, short>(item.Data[0].Span)));
			}
		}
	}

	private void RecreateConverter(AudioFormat format)
	{
		converter?.Dispose();
		converter = null;
		converter = new AudioFrameConverter(format, vadAudioFormat, logger, FrameSamples);
		inputSamples = 0L;
		vad.Reset();
		vad.SetMode(3);
		window.Clear();
		voicedFrameCount = 0;
		episodeActive = false;
		episodeFrameCount = 0;
	}

	private void ProcessVadDecision(bool voiced)
	{
		window.Enqueue(voiced);
		if (voiced)
		{
			voicedFrameCount++;
		}
		if (window.Count > 10 && window.Dequeue())
		{
			voicedFrameCount--;
		}
		if (!episodeActive)
		{
			if (window.Count == 10 && voicedFrameCount >= 9)
			{
				episodeActive = true;
				episodeFrameCount = 10;
			}
		}
		else
		{
			episodeFrameCount++;
			if (10 - voicedFrameCount >= 9)
			{
				episodeActive = false;
			}
		}
		eventProducer.Events.TryWrite(new SpeechActivityEvent(dataFlow, episodeActive && episodeFrameCount * 30 >= 700));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			converter?.Dispose();
			converter = null;
		}
	}
}
