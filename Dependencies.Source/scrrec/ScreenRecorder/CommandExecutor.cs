using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using ScreenCaptureDda;
using ScreenCaptureGdi;
using ScreenCaptureWgc;
using ScreenRecorder.Commands;
using ScreenRecorder.OutgoingMessages.Responses;
using ScreenRecorderCommon;

namespace ScreenRecorder;

internal class CommandExecutor
{
	private readonly ILogger logger;

	private readonly ICommandConsumer messageChannel;

	private readonly ILoggerFactory loggerFactory;

	private Pipeline pipeline;

	private PipelineState pipelineState;

	private PipelineConfiguration pipelineConfiguration;

	private Task pipelineTask;

	private bool isPipelineCanceled;

	private CancellationTokenSource pipelineCts;

	private IScreenCapturer screenCapturer;

	private CaptureStartCommand captureStartCommand;

	private AudioLevelSession audioLevelSession;

	private readonly object lockObject = new object();

	public CommandExecutor(ICommandConsumer messageChannel, ILoggerFactory loggerFactory)
	{
		this.messageChannel = messageChannel ?? throw new ArgumentNullException("messageChannel");
		this.loggerFactory = loggerFactory ?? throw new ArgumentNullException("loggerFactory");
		logger = loggerFactory.CreateLogger<CommandExecutor>();
	}

	public async Task RunAsync(CancellationToken cancellationToken)
	{
		while (true)
		{
			Command command = await messageChannel.Commands.ReadAsync(cancellationToken);
			if (CanLogCommand(command.Kind))
			{
				logger.LogInformation("Processing command \"{command}\"", command.Kind);
			}
			try
			{
				switch (command.Kind)
				{
				case VideoModuleCommand.start:
					await ProcessStartCommandAsync((StartCommand)command);
					break;
				case VideoModuleCommand.stop:
					await ProcessStopCommandAsync();
					break;
				case VideoModuleCommand.cancel:
					await ProcessCancelCommandAsync();
					break;
				case VideoModuleCommand.togglePause:
					await ProcessTogglePauseCommandAsync();
					break;
				case VideoModuleCommand.setAudioDevice:
					await ProcessSetAudioDeviceCommandAsync((SetAudioDeviceCommand)command);
					break;
				case VideoModuleCommand.audioLevelStart:
					await ProcessAudioLevelStartCommandAsync((AudioLevelStartCommand)command);
					break;
				case VideoModuleCommand.audioLevelStop:
					await ProcessAudioLevelStopCommandAsync();
					break;
				case VideoModuleCommand.screenshot:
					await ProcessScreenshotCommandAsync((ScreenshotCommand)command);
					break;
				case VideoModuleCommand.captureStart:
					await ProcessCaptureStartCommandAsync((CaptureStartCommand)command);
					break;
				case VideoModuleCommand.captureStop:
					await ProcessCaptureStopCommandAsync();
					break;
				case VideoModuleCommand.captureGetFrame:
					await ProcessCaptureGetFrameCommandAsync((CaptureGetFrameCommand)command);
					break;
				case VideoModuleCommand.terminate:
					await ProcessTerminateCommandAsync();
					break;
				default:
					throw new NotImplementedException("Not implemented command.");
				}
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Error processing command \"{command}\"", command.Kind);
				await messageChannel.Responses.WriteAsync(new Response(command.Kind, exception));
			}
		}
	}

	private ValueTask ProcessStartCommandAsync(StartCommand command)
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(default(PipelineState));
			audioLevelSession?.Dispose();
			audioLevelSession = null;
			try
			{
				pipelineConfiguration = new PipelineConfiguration(command);
				pipeline = new Pipeline(pipelineConfiguration, messageChannel, loggerFactory);
				isPipelineCanceled = false;
				pipelineCts = new CancellationTokenSource();
				pipelineTask = Task.Run(() => pipeline.RunAsync(pipelineCts.Token)).ContinueWith((Func<Task, ValueTask>)PipelineStoppedAsync);
			}
			catch
			{
				CleanupPipeline();
				throw;
			}
			pipelineState = PipelineState.Running;
			return default;
		}
	}

	private async ValueTask ProcessStopCommandAsync()
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(PipelineState.Running, PipelineState.Paused);
			pipelineCts.Cancel();
		}
		await Task.WhenAny(pipelineTask);
		await messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.stop));
	}

	private async ValueTask ProcessCancelCommandAsync()
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(PipelineState.Running, PipelineState.Paused);
			isPipelineCanceled = true;
			pipelineCts.Cancel();
		}
		await Task.WhenAny(pipelineTask);
		await messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.cancel));
	}

	private ValueTask ProcessTogglePauseCommandAsync()
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(PipelineState.Running, PipelineState.Paused);
			bool flag = pipelineState == PipelineState.Running;
			pipeline.PauseChannelWriter.TryWrite(flag);
			pipelineState = ((!flag) ? PipelineState.Running : PipelineState.Paused);
			return messageChannel.Responses.WriteAsync(new TogglePauseResponse(flag));
		}
	}

	private ValueTask ProcessSetAudioDeviceCommandAsync(SetAudioDeviceCommand command)
	{
		lock (lockObject)
		{
			if (audioLevelSession != null)
			{
				audioLevelSession.SetDevice(command.DataFlow, command.DeviceId, command.IsMuted);
			}
			else
			{
				EnsureValidPipelineState(PipelineState.Running, PipelineState.Paused);
				pipeline.AudioDeviceChannelWriter.TryWrite(command);
			}
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.setAudioDevice));
		}
	}

	private ValueTask ProcessAudioLevelStartCommandAsync(AudioLevelStartCommand command)
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(default(PipelineState));
			if (audioLevelSession != null)
			{
				throw new InvalidOperationException("Audio level session already started");
			}
			audioLevelSession = new AudioLevelSession(command.InputDeviceId, command.IsInputMuted, command.OutputDeviceId, command.IsOutputMuted, messageChannel, loggerFactory);
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.audioLevelStart));
		}
	}

	private ValueTask ProcessAudioLevelStopCommandAsync()
	{
		lock (lockObject)
		{
			audioLevelSession?.Dispose();
			audioLevelSession = null;
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.audioLevelStop));
		}
	}

	private ValueTask ProcessScreenshotCommandAsync(ScreenshotCommand command)
	{
		lock (lockObject)
		{
			long timestamp = Stopwatch.GetTimestamp();
			using (SharedMemoryRawImage image = new SharedMemoryRawImage(new ImageFormat(command.CaptureArea.Size, PixelFormat.BGRA), command.SharedMemoryName))
			{
				switch (command.ScreenCaptureApi)
				{
				case ScreenCaptureApi.GDI:
					new ScreenCapturerGdi(command.CaptureArea, command.ShowMousePointer, loggerFactory).TakeScreenshot(image);
					break;
				case ScreenCaptureApi.DDA:
				{
					using (ScreenCaptureDda.MousePointer mousePointer = (command.ShowMousePointer ? new ScreenCaptureDda.MousePointer(command.MonitorName, showMousePointer: true, highlightMousePointer: false, highlightMouseClicks: false, command.CaptureArea.Location, null, loggerFactory) : null))
					{
						using ScreenCapturerDda screenCapturerDda = new ScreenCapturerDda(command.MonitorName, command.CaptureArea, mousePointer, loggerFactory.CreateLogger<ScreenCapturerDda>());
						if (!screenCapturerDda.GetFrame(image, TimeSpan.FromMilliseconds(500.0)))
						{
							throw new TimeoutException();
						}
					}
					break;
				}
				case ScreenCaptureApi.WGC:
				{
					using (ScreenCapturerWgc screenCapturerWgc = new ScreenCapturerWgc(command.MonitorName, command.CaptureArea, command.ShowMousePointer, loggerFactory.CreateLogger<ScreenCapturerWgc>()))
					{
						if (!screenCapturerWgc.GetFrame(image, TimeSpan.FromMilliseconds(500.0)))
						{
							throw new TimeoutException();
						}
					}
					break;
				}
				default:
					throw new InvalidOperationException($"Unsupported capture API for screenshots: {command.ScreenCaptureApi}.");
				}
			}
			logger.LogInformation("Screenshot successfully taken through {api} [time={time}ms]", command.ScreenCaptureApi, StopwatchHelper.GetDurationInWholeMillisecondsSince(timestamp));
			return messageChannel.Responses.WriteAsync(new ScreenshotResponse());
		}
	}

	private ValueTask ProcessCaptureStartCommandAsync(CaptureStartCommand command)
	{
		lock (lockObject)
		{
			if (screenCapturer != null)
			{
				throw new InvalidOperationException("Screen capture already started");
			}
			screenCapturer = command.ScreenCaptureApi switch
			{
				ScreenCaptureApi.DDA => (IScreenCapturer)new ScreenCapturerDda(command.MonitorName, command.CaptureArea, null, loggerFactory.CreateLogger<ScreenCapturerDda>()), 
				ScreenCaptureApi.WGC => new ScreenCapturerWgc(command.MonitorName, command.CaptureArea, showMousePointer: false, loggerFactory.CreateLogger<ScreenCapturerWgc>()), 
				_ => throw new InvalidOperationException($"Unsupported screen capture API: {command.ScreenCaptureApi}."), 
			};
			captureStartCommand = command;
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.captureStart));
		}
	}

	private ValueTask ProcessCaptureStopCommandAsync()
	{
		lock (lockObject)
		{
			if (screenCapturer == null)
			{
				throw new InvalidOperationException("Screen capture not started");
			}
			screenCapturer.Dispose();
			screenCapturer = null;
			captureStartCommand = null;
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.captureStop));
		}
	}

	private ValueTask ProcessCaptureGetFrameCommandAsync(CaptureGetFrameCommand command)
	{
		lock (lockObject)
		{
			if (screenCapturer == null)
			{
				throw new InvalidOperationException("Screen capture not started");
			}
			bool frame;
			using (SharedMemoryRawImage image = new SharedMemoryRawImage(new ImageFormat(captureStartCommand.CaptureArea.Size, PixelFormat.BGRA), command.SharedMemoryName))
			{
				frame = screenCapturer.GetFrame(image, command.Timeout);
			}
			return messageChannel.Responses.WriteAsync(new CaptureGetFrameResponse(frame));
		}
	}

	private ValueTask ProcessTerminateCommandAsync()
	{
		lock (lockObject)
		{
			EnsureValidPipelineState(default(PipelineState));
			return messageChannel.Responses.WriteAsync(new Response(VideoModuleCommand.terminate));
		}
	}

	private ValueTask PipelineStoppedAsync(Task completedTask)
	{
		lock (lockObject)
		{
			pipelineState = PipelineState.Stopped;
			if (isPipelineCanceled)
			{
				try
				{
					File.Delete(pipelineConfiguration.OutputFile);
					logger.LogInformation("Deleted file \"{file}\"", pipelineConfiguration.OutputFile);
				}
				catch (Exception exception)
				{
					logger.LogError(exception, "Error deleting file \"{file}\"", pipelineConfiguration.OutputFile);
				}
			}
			CleanupPipeline();
			if (completedTask.IsFaulted)
			{
				if (!(completedTask.Exception.Flatten().InnerException is ErrorCodeException { ErrorCode: ErrorCode.DuplicateOutputUnsupported }))
				{
					logger.LogError(completedTask.Exception, "Error in pipeline");
				}
				return messageChannel.Responses.WriteAsync(new StartResponse(canceled: false, completedTask.Exception));
			}
			return messageChannel.Responses.WriteAsync(new StartResponse(isPipelineCanceled));
		}
	}

	private void EnsureValidPipelineState(params PipelineState[] states)
	{
		if (!states.Contains(pipelineState))
		{
			throw new InvalidOperationException($"Invalid state: {pipelineState}.");
		}
	}

	private void CleanupPipeline()
	{
		pipeline = null;
		pipelineConfiguration = null;
		pipelineCts?.Dispose();
		pipelineCts = null;
	}

	private static bool CanLogCommand(VideoModuleCommand commandKind)
	{
		return commandKind != VideoModuleCommand.captureGetFrame;
	}
}
