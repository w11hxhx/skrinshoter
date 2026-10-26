using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.SkrChildApp;

namespace SkrinshoterApp;

public class ScrollingCaptureEngine : SafeDisposable
{
	private const int MaxCaptureFramesPerSecond = 30;

	private const int BufferCount = 3;

	private readonly ILoggerFactory loggerFactory;

	private readonly ILogger logger;

	private readonly VideoModuleAppProxy videoModule;

	private readonly Rectangle captureArea;

	private readonly string monitorName;

	private readonly SharedMemoryRawImage[] images = new SharedMemoryRawImage[3];

	private readonly int[] frameIndices = new int[3];

	private readonly BlockingCollection<int> freeBufferIndices = new BlockingCollection<int>();

	private readonly BlockingCollection<int> readyBufferIndices = new BlockingCollection<int>();

	private readonly CancellationTokenSource cts = new CancellationTokenSource();

	private readonly ScrollingCaptureImage image;

	private readonly TaskCompletionSource<bool> firstFragmentAddedTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

	private readonly long minCaptureIntervalTicks;

	private readonly HighResolutionSleeper sleeper = new HighResolutionSleeper();

	private Task startTask;

	private Task stopTask;

	private bool isVideoModuleCaptureStarted;

	private Task workersTask;

	private int nextFrameIndex;

	private readonly AsyncEventInvoker<EventArgs> reachedMaxHeightEventInvoker;

	public ScrollingCaptureImage Image => image;

	public event EventHandler<EventArgs> ReachedMaxHeight
	{
		add
		{
			reachedMaxHeightEventInvoker.Subscribe(value);
		}
		remove
		{
			reachedMaxHeightEventInvoker.Unsubscribe(value);
		}
	}

	public ScrollingCaptureEngine(VideoModuleAppProxy videoModule, Rectangle captureArea, string monitorName, ILoggerFactory loggerFactory)
	{
		this.videoModule = videoModule;
		this.captureArea = captureArea;
		this.monitorName = monitorName;
		this.loggerFactory = loggerFactory;
		logger = loggerFactory.CreateLogger<ScrollingCaptureEngine>();
		try
		{
			image = new ScrollingCaptureImage(captureArea.Width);
			for (int i = 0; i < 3; i++)
			{
				images[i] = new SharedMemoryRawImage(new ImageFormat(captureArea.Size, PixelFormat.BGRA));
				freeBufferIndices.Add(i);
			}
			reachedMaxHeightEventInvoker = new AsyncEventInvoker<EventArgs>(logger, "ReachedMaxHeight");
			minCaptureIntervalTicks = (long)((double)Stopwatch.Frequency / 30.0);
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public Task Start()
	{
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return Task.CompletedTask;
			}
			if (stopTask != null)
			{
				throw new InvalidOperationException("Cannot call Start after Stop.");
			}
			if (startTask == null)
			{
				startTask = StartCoreAsync(cts.Token);
			}
			return startTask;
		}
	}

	public void Stop()
	{
		Task task;
		lock (InternalDisposeLockObject)
		{
			if (stopTask == null)
			{
				stopTask = StopCoreAsync();
			}
			task = stopTask;
		}
		Task.WhenAny(task).GetAwaiter().GetResult();
	}

	private async Task StartCoreAsync(CancellationToken cancellationToken)
	{
		await new CaptureStartSlaveAppCommand(videoModule, monitorName, captureArea, GetScreenCaptureApi()).Execute().ConfigureAwait(continueOnCapturedContext: false);
		lock (InternalDisposeLockObject)
		{
			isVideoModuleCaptureStarted = true;
			cancellationToken.ThrowIfCancellationRequested();
			Task task = Task.Run(() => CaptureFramesLoop(cancellationToken));
			Task task2 = Task.Run(() =>
			{
				ProcessFramesLoop(cancellationToken);
			});
			workersTask = Task.WhenAll(task, task2);
		}
		await firstFragmentAddedTcs.Task.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task StopCoreAsync()
	{
		cts.Cancel();
		Task task;
		lock (InternalDisposeLockObject)
		{
			task = startTask;
		}
		if (task != null)
		{
			await Task.WhenAny(task).ConfigureAwait(continueOnCapturedContext: false);
		}
		Task task2;
		bool localIsVideoModuleCaptureStarted;
		lock (InternalDisposeLockObject)
		{
			task2 = workersTask;
			localIsVideoModuleCaptureStarted = isVideoModuleCaptureStarted;
		}
		if (task2 != null)
		{
			await Task.WhenAny(task2).ConfigureAwait(continueOnCapturedContext: false);
		}
		if (localIsVideoModuleCaptureStarted)
		{
			await videoModule.SendCommand(VideoModuleCommand.captureStop).ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	private async Task CaptureFramesLoop(CancellationToken cancellationToken)
	{
		try
		{
			long nextCaptureTimestamp = 0L;
			while (!cancellationToken.IsCancellationRequested)
			{
				int bufferIndex = freeBufferIndices.Take(cancellationToken);
				WaitForNextCaptureSlot(ref nextCaptureTimestamp, cancellationToken);
				await GetNextFrame(bufferIndex, cancellationToken);
				frameIndices[bufferIndex] = nextFrameIndex++;
				readyBufferIndices.Add(bufferIndex, cancellationToken);
			}
		}
		catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
		{
			firstFragmentAddedTcs.TrySetCanceled();
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Error while capturing scrolling screenshot frames.");
			firstFragmentAddedTcs.TrySetException(exception);
			cts.Cancel();
			throw;
		}
	}

	private void ProcessFramesLoop(CancellationToken cancellationToken)
	{
		try
		{
			int num = readyBufferIndices.Take(cancellationToken);
			PreprocessedFrame first = PreprocessBuffer(num);
			AddFragment(num, 0, images[num].Format.Height);
			firstFragmentAddedTcs.TrySetResult(result: true);
			ScrollingCaptureShiftDetector scrollingCaptureShiftDetector = new ScrollingCaptureShiftDetector(images[num].Format.Width, images[num].Format.Height, loggerFactory.CreateLogger<ScrollingCaptureShiftDetector>());
			while (!cancellationToken.IsCancellationRequested)
			{
				int num2 = readyBufferIndices.Take(cancellationToken);
				PreprocessedFrame preprocessedFrame = PreprocessBuffer(num2);
				int? num3 = scrollingCaptureShiftDetector.FindVerticalShift(first, preprocessedFrame, out var movingCenter, frameIndices[num2]);
				if (num3 > 0)
				{
					int height = images[num2].Format.Height;
					int num4 = movingCenter - num3.Value / 2;
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num4 + num3 > height)
					{
						num4 = height - num3.Value;
					}
					int offsetY = num4;
					int num5 = height - num4;
					int num6 = num5 - num3.Value;
					if (num6 < 0)
					{
						num6 = 0;
					}
					if (AddFragment(num2, offsetY, num5, num6) < num5)
					{
						reachedMaxHeightEventInvoker.InvokeAsync(this, EventArgs.Empty);
						cts.Cancel();
						break;
					}
				}
				freeBufferIndices.Add(num);
				num = num2;
				first = preprocessedFrame;
			}
		}
		catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
		{
			firstFragmentAddedTcs.TrySetCanceled();
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Error while processing scrolling screenshot frames.");
			firstFragmentAddedTcs.TrySetException(exception);
			cts.Cancel();
			throw;
		}
	}

	private void WaitForNextCaptureSlot(ref long nextCaptureTimestamp, CancellationToken cancellationToken)
	{
		if (nextCaptureTimestamp == 0L)
		{
			nextCaptureTimestamp = Stopwatch.GetTimestamp() + minCaptureIntervalTicks;
			return;
		}
		long timestamp;
		long num;
		while (true)
		{
			cancellationToken.ThrowIfCancellationRequested();
			timestamp = Stopwatch.GetTimestamp();
			num = timestamp - nextCaptureTimestamp;
			if (num >= 0)
			{
				break;
			}
			sleeper.Sleep(1);
		}
		if (num >= minCaptureIntervalTicks)
		{
			nextCaptureTimestamp = timestamp;
		}
		nextCaptureTimestamp += minCaptureIntervalTicks;
	}

	private unsafe PreprocessedFrame PreprocessBuffer(int bufferIndex)
	{
		using MemoryHandle memoryHandle = images[bufferIndex].Data[0].Pin();
		return ScrollingCaptureShiftDetector.Preprocess((IntPtr)memoryHandle.Pointer, images[bufferIndex].Format.Width, images[bufferIndex].Format.Height);
	}

	private async Task GetNextFrame(int bufferIndex, CancellationToken cancellationToken)
	{
		bool flag = false;
		while (!flag)
		{
			flag = await new CaptureGetFrameSlaveAppCommand(videoModule, images[bufferIndex].SharedMemoryName, TimeSpan.FromMilliseconds(50.0)).Execute();
			cancellationToken.ThrowIfCancellationRequested();
		}
	}

	private int AddFragment(int bufferIndex, int offsetY, int lineCount, int overwriteLineCount = 0)
	{
		return image.AddFragment(images[bufferIndex], offsetY, lineCount, overwriteLineCount);
	}

	private static ScreenCaptureApi GetScreenCaptureApi()
	{
		if (!WgcSupportChecker.IsMouseHidingSupported())
		{
			return ScreenCaptureApi.DDA;
		}
		return ScreenCaptureApi.WGC;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Stop();
			cts.Dispose();
			sleeper.Dispose();
			freeBufferIndices.Dispose();
			readyBufferIndices.Dispose();
			image?.Dispose();
			for (int i = 0; i < images.Length; i++)
			{
				images[i]?.Dispose();
			}
		}
	}
}
