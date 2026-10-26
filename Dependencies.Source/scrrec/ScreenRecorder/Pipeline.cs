using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AudioCapture;
using AudioDevices;
using AudioVideoCore;
using Common;
using Common.ChannelPipeline;
using FFmpeg;
using Microsoft.Extensions.Logging;
using MousePointer;
using ScreenCaptureDda;
using ScreenCaptureWgc;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages.Events;
using ScreenRecorderCommon;

namespace ScreenRecorder;

internal class Pipeline
{
	private readonly ILogger logger;

	private readonly PipelineConfiguration configuration;

	private readonly IEventProducer eventProducer;

	private TimeSpan nextRecordingProgressEventTimestamp = TimeSpan.Zero;

	private readonly ILoggerFactory loggerFactory;

	private readonly ChannelReader<bool> pauseChannelReader;

	private readonly ChannelReader<SetAudioDeviceCommand> audioDeviceChannelReader;

	public ChannelWriter<bool> PauseChannelWriter { get; }

	public ChannelWriter<SetAudioDeviceCommand> AudioDeviceChannelWriter { get; }

	public Pipeline(PipelineConfiguration configuration, IEventProducer eventProducer, ILoggerFactory loggerFactory)
	{
		this.configuration = configuration ?? throw new ArgumentNullException("configuration");
		this.eventProducer = eventProducer ?? throw new ArgumentNullException("eventProducer");
		this.loggerFactory = loggerFactory ?? throw new ArgumentNullException("loggerFactory");
		logger = loggerFactory.CreateLogger<Pipeline>();
		Channel<bool> channel = Channel.CreateBounded<bool>(new BoundedChannelOptions(1)
		{
			SingleReader = true,
			FullMode = BoundedChannelFullMode.DropOldest
		});
		pauseChannelReader = channel.Reader;
		PauseChannelWriter = channel.Writer;
		Channel<SetAudioDeviceCommand> channel2 = Channel.CreateUnbounded<SetAudioDeviceCommand>(new UnboundedChannelOptions
		{
			SingleReader = true,
			SingleWriter = true
		});
		audioDeviceChannelReader = channel2.Reader;
		AudioDeviceChannelWriter = channel2.Writer;
	}

	public async Task RunAsync(CancellationToken stoppingToken)
	{
		bool isPaused = false;
		logger.LogInformation("Monitor: {deviceName}", configuration.MonitorName);
		logger.LogInformation("Monitor resolution: {width}x{height}", configuration.MonitorResolution.Width, configuration.MonitorResolution.Height);
		logger.LogInformation("Input video resolution: {width}x{height}", configuration.RecordingArea.Width, configuration.RecordingArea.Height);
		logger.LogInformation("Output video resolution: {width}x{height}", configuration.OutputVideoResolution.Width, configuration.OutputVideoResolution.Height);
		ScreenCaptureDda.MousePointer mousePointer = null;
		MousePipeline mousePipeline = null;
		IVideoCapturer videoCapturer = null;
		VideoFilter videoFilter = null;
		VideoEncoder videoEncoder = null;
		AudioCapturer outputAudioCapturer = null;
		AudioCapturer inputAudioCapturer = null;
		SpeechDetector inputSpeechDetector = null;
		AudioFilter audioFilter = null;
		AudioEncoder audioEncoder = null;
		VideoFileWriter videoFileWriter = null;
		try
		{
			StoppableStopwatch stopwatch = new StoppableStopwatch();
			if (configuration.ScreenCaptureApi == ScreenCaptureApi.DDA)
			{
				if (configuration.ShowMousePointer || configuration.HighlightMousePointer || configuration.HighlightMouseClicks)
				{
					mousePointer = new ScreenCaptureDda.MousePointer(configuration.MonitorName, configuration.ShowMousePointer, configuration.HighlightMousePointer, configuration.HighlightMouseClicks, configuration.RecordingArea.Location, stopwatch, loggerFactory);
				}
				videoCapturer = new VideoCapturerDda(configuration.MonitorName, configuration.RecordingArea, configuration.VideoFrameRate, mousePointer, stopwatch, loggerFactory.CreateLogger<VideoCapturerDda>());
			}
			else
			{
				if (configuration.ScreenCaptureApi != ScreenCaptureApi.WGC)
				{
					throw new InvalidOperationException($"Unsupported capture API for video: {configuration.ScreenCaptureApi}.");
				}
				bool showMousePointer = configuration.ShowMousePointer;
				if (configuration.HighlightMousePointer || configuration.HighlightMouseClicks)
				{
					showMousePointer = false;
					mousePipeline = new MousePipeline();
					if (configuration.HighlightMousePointer)
					{
						mousePipeline.AddStage(new MousePointerHighlighter());
					}
					if (configuration.HighlightMouseClicks)
					{
						mousePipeline.AddStage(new MouseClickHighlighter(stopwatch, loggerFactory));
					}
					if (configuration.ShowMousePointer && WgcSupportChecker.IsMouseHidingSupported())
					{
						mousePipeline.AddStage(new MousePointerRenderer(loggerFactory.CreateLogger<MousePointerRenderer>()));
					}
				}
				videoCapturer = new VideoCapturerWgc(configuration.MonitorName, configuration.RecordingArea, configuration.VideoFrameRate, showMousePointer, mousePipeline, stopwatch, loggerFactory);
			}
			videoFilter = new VideoFilter(videoCapturer.ImageFormat, new ImageFormat(configuration.OutputVideoResolution, PixelFormat.YUV420P), configuration.VideoScaleAlgorithm, loggerFactory.CreateLogger<VideoFilter>());
			videoEncoder = new VideoEncoder(new ImageFormat(configuration.OutputVideoResolution, PixelFormat.YUV420P), configuration.VideoFrameRate, loggerFactory.CreateLogger<VideoEncoder>());
			AudioFormat audioFormat = null;
			if (!configuration.CaptureTest)
			{
				inputSpeechDetector = new SpeechDetector(AudioDataFlow.Capture, eventProducer, loggerFactory.CreateLogger<SpeechDetector>());
				outputAudioCapturer = new AudioCapturer(AudioDataFlow.Render, stopwatch, new AudioLevelReporter(AudioDataFlow.Render, eventProducer), loggerFactory);
				inputAudioCapturer = new AudioCapturer(AudioDataFlow.Capture, stopwatch, new CompositeAudioFrameObserver(new IAudioFrameObserver[2]
				{
					new AudioLevelReporter(AudioDataFlow.Capture, eventProducer),
					inputSpeechDetector
				}, loggerFactory.CreateLogger<CompositeAudioFrameObserver>()), loggerFactory);
				outputAudioCapturer.DeviceFailed += AudioCapturer_DeviceFailed;
				inputAudioCapturer.DeviceFailed += AudioCapturer_DeviceFailed;
				await outputAudioCapturer.SetDevice(configuration.OutputAudioDeviceId, configuration.IsOutputAudioDeviceMuted);
				await inputAudioCapturer.SetDevice(configuration.InputAudioDeviceId, configuration.IsInputAudioDeviceMuted);
				audioFormat = new AudioFormat(48000, 2, SampleFormat.FloatP);
				logger.LogInformation("File audio format: {audioFormat}", audioFormat);
				audioEncoder = new AudioEncoder(audioFormat, loggerFactory.CreateLogger<AudioEncoder>());
				audioFilter = new AudioFilter(inputAudioCapturer.AudioFormat, outputAudioCapturer.AudioFormat, audioFormat, audioEncoder.InputFrameSize, loggerFactory.CreateLogger<AudioFilter>());
			}
			videoFileWriter = new VideoFileWriter(configuration.OutputFile, videoEncoder, audioEncoder, loggerFactory.CreateLogger<VideoFileWriter>());
			List<Task> tasks = new List<Task>();
			CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
			try
			{
				using (cts.Token.Register(() =>
				{
					videoCapturer?.Dispose();
					outputAudioCapturer?.Dispose();
					inputAudioCapturer?.Dispose();
				}))
				{
					try
					{
						TransformManyBlock<VideoFrame, VideoFrame> transformManyBlock = TransformManyBlock.Create(videoCapturer.Frames, (VideoFrame inputFrame) =>
						{
							TimeSpan timeSpan = inputFrame.Timestamp.ToTimeSpan();
							if (timeSpan >= nextRecordingProgressEventTimestamp)
							{
								eventProducer.Events.TryWrite(new RecordingProgressEvent(timeSpan, configuration.MaxRecordDuration));
								nextRecordingProgressEventTimestamp += new TimeSpan(10000000L);
							}
							if (timeSpan >= configuration.MaxRecordDuration)
							{
								cts.Cancel();
							}
							List<VideoFrame> result = videoFilter.ProcessFrame(inputFrame);
							inputFrame.Dispose();
							return result;
						}, (Exception ex) =>
						{
							outputAudioCapturer?.Dispose();
							inputAudioCapturer?.Dispose();
							return (IEnumerable<VideoFrame>)null;
						}, (ChannelPipelineBlockOptions options) =>
						{
							options.Capacity = 1;
						});
						tasks.Add(transformManyBlock.RunAsync(CancellationToken.None));
						TransformManyBlock<VideoFrame, CodedFrame> transformManyBlock2 = TransformManyBlock.Create(transformManyBlock.Reader, (VideoFrame videoFrame) =>
						{
							List<CodedFrame> result = videoEncoder.EncodeFrame(videoFrame);
							videoFrame.Dispose();
							return result;
						}, (Exception ex) => videoEncoder.EncodeFrame(null));
						tasks.Add(transformManyBlock2.RunAsync(CancellationToken.None));
						TransformBlock<CodedFrame, TaggedItem<CodedFrame, int>> transformBlock = TransformBlock.Create(transformManyBlock2.Reader, (CodedFrame frame) => TaggedItem.Create(frame, 0));
						tasks.Add(transformBlock.RunAsync(CancellationToken.None));
						TransformBlock<CodedFrame, TaggedItem<CodedFrame, int>> transformBlock2 = null;
						if (audioFormat != null)
						{
							List<ChannelReader<TaggedItem<AudioFrame, AudioSourceType>>> list = new List<ChannelReader<TaggedItem<AudioFrame, AudioSourceType>>>();
							if (outputAudioCapturer != null)
							{
								TransformBlock<AudioFrame, TaggedItem<AudioFrame, AudioSourceType>> transformBlock3 = TransformBlock.Create(outputAudioCapturer.Frames, (AudioFrame frame) => TaggedItem.Create(frame, AudioSourceType.System), (Exception ex) => new TaggedItem<AudioFrame, AudioSourceType>[1] { TaggedItem.Create<AudioFrame, AudioSourceType>(null, AudioSourceType.System) });
								tasks.Add(transformBlock3.RunAsync(CancellationToken.None));
								list.Add(transformBlock3.Reader);
							}
							if (inputAudioCapturer != null)
							{
								TransformBlock<AudioFrame, TaggedItem<AudioFrame, AudioSourceType>> transformBlock4 = TransformBlock.Create(inputAudioCapturer.Frames, (AudioFrame frame) => TaggedItem.Create(frame, AudioSourceType.Microphone), (Exception ex) => new TaggedItem<AudioFrame, AudioSourceType>[1] { TaggedItem.Create<AudioFrame, AudioSourceType>(null, AudioSourceType.Microphone) });
								tasks.Add(transformBlock4.RunAsync(CancellationToken.None));
								list.Add(transformBlock4.Reader);
							}
							OrderedJoinBlock<TaggedItem<AudioFrame, AudioSourceType>, FrameTimestamp> orderedJoinBlock = OrderedJoinBlock.Create(list, (TaggedItem<AudioFrame, AudioSourceType> x) => x.Item?.Timestamp ?? default(FrameTimestamp));
							tasks.Add(orderedJoinBlock.RunAsync(CancellationToken.None));
							TransformManyBlock<TaggedItem<AudioFrame, AudioSourceType>, AudioFrame> transformManyBlock3 = TransformManyBlock.Create(orderedJoinBlock.Reader, (TaggedItem<AudioFrame, AudioSourceType> taggedFrame) =>
							{
								List<AudioFrame> result = audioFilter.ProcessFrame(taggedFrame.Item, taggedFrame.Tag);
								AudioFrame item = taggedFrame.Item;
								if (item != null)
								{
									item.Dispose();
									return result;
								}
								return result;
							});
							tasks.Add(transformManyBlock3.RunAsync(CancellationToken.None));
							TransformManyBlock<AudioFrame, CodedFrame> transformManyBlock4 = TransformManyBlock.Create(transformManyBlock3.Reader, (AudioFrame audioFrame) =>
							{
								List<CodedFrame> result = audioEncoder.EncodeFrame(audioFrame);
								audioFrame.Dispose();
								return result;
							}, (Exception ex) => audioEncoder.EncodeFrame(null));
							tasks.Add(transformManyBlock4.RunAsync(CancellationToken.None));
							transformBlock2 = TransformBlock.Create(transformManyBlock4.Reader, (CodedFrame frame) => TaggedItem.Create(frame, 1));
							tasks.Add(transformBlock2.RunAsync(CancellationToken.None));
						}
						OrderedJoinBlock<TaggedItem<CodedFrame, int>, FrameTimestamp> orderedJoinBlock2 = ((audioFormat != null) ? OrderedJoinBlock.Create(new ChannelReader<TaggedItem<CodedFrame, int>>[2] { transformBlock.Reader, transformBlock2.Reader }, (TaggedItem<CodedFrame, int> x) => x.Item.Timestamp) : OrderedJoinBlock.Create(new ChannelReader<TaggedItem<CodedFrame, int>>[1] { transformBlock.Reader }, (TaggedItem<CodedFrame, int> x) => x.Item.Timestamp));
						tasks.Add(orderedJoinBlock2.RunAsync(CancellationToken.None));
						ConsumeBlock<TaggedItem<CodedFrame, int>> consumeBlock = ConsumeBlock.Create(orderedJoinBlock2.Reader, (TaggedItem<CodedFrame, int> taggedFrame) =>
						{
							videoFileWriter.WriteFrame(taggedFrame.Item, taggedFrame.Tag);
							taggedFrame.Item.Dispose();
						});
						Task videoFileWriterBlockTask = consumeBlock.RunAsync(CancellationToken.None);
						tasks.Add(videoFileWriterBlockTask);
						stopwatch.Start();
						ConsumeBlock<bool> consumeBlock2 = ConsumeBlock.Create(pauseChannelReader, (bool pauseRequested) =>
						{
							if (isPaused != pauseRequested)
							{
								isPaused = pauseRequested;
								if (isPaused)
								{
									stopwatch.Stop();
								}
								else
								{
									stopwatch.Start();
								}
							}
						});
						Task pauseCommandHandlerBlockTask = consumeBlock2.RunAsync(cts.Token);
						tasks.Add(pauseCommandHandlerBlockTask);
						ConsumeBlock<SetAudioDeviceCommand> consumeBlock3 = ConsumeBlock.Create(audioDeviceChannelReader, (SetAudioDeviceCommand command) =>
						{
							((command.DataFlow == AudioDataFlow.Render) ? outputAudioCapturer : inputAudioCapturer)?.SetDevice(command.DeviceId, command.IsMuted);
						});
						tasks.Add(consumeBlock3.RunAsync(cts.Token));
						await Task.WhenAny(videoFileWriterBlockTask, pauseCommandHandlerBlockTask);
						cts.Cancel();
						await Task.WhenAll(videoFileWriterBlockTask, pauseCommandHandlerBlockTask);
					}
					finally
					{
						cts.Cancel();
						await Task.WhenAny(Task.WhenAll(tasks));
					}
				}
			}
			finally
			{
				if (cts != null)
				{
					((IDisposable)cts).Dispose();
				}
			}
		}
		finally
		{
			videoCapturer?.Dispose();
			mousePipeline?.Dispose();
			mousePointer?.Dispose();
			videoFilter?.Dispose();
			videoEncoder?.Dispose();
			outputAudioCapturer?.Dispose();
			inputAudioCapturer?.Dispose();
			inputSpeechDetector?.Dispose();
			audioFilter?.Dispose();
			audioEncoder?.Dispose();
			videoFileWriter?.Dispose();
		}
	}

	private void AudioCapturer_DeviceFailed(object sender, AudioDeviceFailedEventArgs e)
	{
		eventProducer.Events.TryWrite(new AudioDeviceFailedEvent(e.DataFlow, e.DeviceId, e.Exception));
	}
}
