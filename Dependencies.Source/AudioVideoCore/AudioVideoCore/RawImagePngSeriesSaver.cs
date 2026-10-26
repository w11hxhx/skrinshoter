using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Common;

namespace AudioVideoCore;

public class RawImagePngSeriesSaver : SafeDisposable
{
	private sealed class FrameBuffer : IDisposable
	{
		public UnmanagedRawImage Image { get; }

		public int FrameNumber { get; set; }

		public FrameBuffer(ImageFormat format)
		{
			Image = new UnmanagedRawImage(format);
		}

		public void Dispose()
		{
			Image.Dispose();
		}
	}

	private readonly ImageFormat format;

	private readonly int rowSize;

	private readonly int frameSize;

	private readonly string directoryPath;

	private readonly bool filterDuplicateFrames;

	private readonly bool useSequentialFileNumbering;

	private readonly Channel<FrameBuffer> freeBuffers;

	private readonly Channel<FrameBuffer> readyBuffers;

	private readonly List<FrameBuffer> allBuffers;

	private readonly CancellationTokenSource cts = new CancellationTokenSource();

	private readonly Task workerTask;

	private ExceptionDispatchInfo backgroundException;

	private bool isCompleting;

	private int activePublicMethodCount;

	private FrameBuffer lastSavedFrameBuffer;

	private int nextSequentialFileNumber;

	public string DirectoryPath => directoryPath;

	public RawImagePngSeriesSaver(ImageFormat format, int bufferCount = 8, string directoryPath = null, bool filterDuplicateFrames = false, bool useSequentialFileNumbering = false)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException("Only BGRA pixel format is supported.", "format");
		}
		if (bufferCount <= 0)
		{
			throw new ArgumentOutOfRangeException("bufferCount");
		}
		if (filterDuplicateFrames && bufferCount < 2)
		{
			throw new ArgumentOutOfRangeException("bufferCount", "At least two buffers are required when duplicate filtering is enabled.");
		}
		try
		{
			this.format = format;
			rowSize = format.Width * 4;
			frameSize = rowSize * format.Height;
			this.directoryPath = directoryPath ?? CreateDefaultDirectoryPath();
			this.filterDuplicateFrames = filterDuplicateFrames;
			this.useSequentialFileNumbering = useSequentialFileNumbering;
			if (this.directoryPath.Length == 0)
			{
				throw new ArgumentException("Directory path cannot be empty.", "directoryPath");
			}
			Directory.CreateDirectory(this.directoryPath);
			freeBuffers = Channel.CreateBounded<FrameBuffer>(new BoundedChannelOptions(bufferCount)
			{
				SingleReader = false,
				SingleWriter = false,
				FullMode = BoundedChannelFullMode.Wait
			});
			readyBuffers = Channel.CreateBounded<FrameBuffer>(new BoundedChannelOptions(bufferCount)
			{
				SingleReader = true,
				SingleWriter = false,
				FullMode = BoundedChannelFullMode.Wait
			});
			allBuffers = new List<FrameBuffer>(bufferCount);
			for (int i = 0; i < bufferCount; i++)
			{
				FrameBuffer item = new FrameBuffer(format);
				allBuffers.Add(item);
				if (!freeBuffers.Writer.TryWrite(item))
				{
					throw new InvalidOperationException("Failed to initialize free frame buffer queue.");
				}
			}
			workerTask = Task.Run((Func<Task>)ProcessReadyBuffersAsync);
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public void SaveFrame(RawImage image, int frameNumber)
	{
		if (!TryEnterPublicMethod())
		{
			return;
		}
		try
		{
			ThrowIfBackgroundFailed();
			ThrowIfCompleting();
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (!useSequentialFileNumbering && frameNumber < 0)
			{
				throw new ArgumentOutOfRangeException("frameNumber");
			}
			ValidateImage(image);
			FrameBuffer frameBuffer = RentBuffer();
			if (frameBuffer == null)
			{
				return;
			}
			bool flag = true;
			try
			{
				if (!CanAcceptFrameAfterWait())
				{
					return;
				}
				CopyImage(image, frameBuffer.Image);
				frameBuffer.FrameNumber = frameNumber;
				if (!PublishBuffer(frameBuffer))
				{
					return;
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					ReturnBuffer(frameBuffer);
				}
			}
			ThrowIfBackgroundFailed();
		}
		finally
		{
			ExitPublicMethod();
		}
	}

	public async Task CompleteAsync()
	{
		if (!TryEnterPublicMethod())
		{
			return;
		}
		try
		{
			lock (InternalDisposeLockObject)
			{
				if (IsDisposed)
				{
					return;
				}
				if (!isCompleting)
				{
					isCompleting = true;
					readyBuffers.Writer.TryComplete();
				}
			}
			await workerTask.ConfigureAwait(continueOnCapturedContext: false);
			ThrowIfBackgroundFailed();
		}
		finally
		{
			ExitPublicMethod();
		}
	}

	private bool TryEnterPublicMethod()
	{
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return false;
			}
			activePublicMethodCount++;
			return true;
		}
	}

	private void ExitPublicMethod()
	{
		lock (InternalDisposeLockObject)
		{
			activePublicMethodCount--;
			if (activePublicMethodCount == 0)
			{
				Monitor.PulseAll(InternalDisposeLockObject);
			}
		}
	}

	private FrameBuffer RentBuffer()
	{
		if (freeBuffers.Reader.TryRead(out var item))
		{
			return item;
		}
		try
		{
			return freeBuffers.Reader.ReadAsync(cts.Token).AsTask().GetAwaiter()
				.GetResult();
		}
		catch (OperationCanceledException)
		{
			GetBackgroundException()?.Throw();
			if (IsDisposed)
			{
				return null;
			}
			throw;
		}
	}

	private bool PublishBuffer(FrameBuffer frameBuffer)
	{
		if (readyBuffers.Writer.TryWrite(frameBuffer))
		{
			return true;
		}
		try
		{
			readyBuffers.Writer.WriteAsync(frameBuffer, cts.Token).AsTask().GetAwaiter()
				.GetResult();
			return true;
		}
		catch (OperationCanceledException)
		{
			GetBackgroundException()?.Throw();
			if (IsDisposed)
			{
				return false;
			}
			throw;
		}
		catch (ChannelClosedException)
		{
			GetBackgroundException()?.Throw();
			if (IsDisposed)
			{
				return false;
			}
			throw new InvalidOperationException("Cannot accept new frames after completion has started.");
		}
	}

	private void ReturnBuffer(FrameBuffer frameBuffer)
	{
		if (!freeBuffers.Writer.TryWrite(frameBuffer))
		{
			throw new InvalidOperationException("Failed to return frame buffer to free queue.");
		}
	}

	private void ValidateImage(RawImage image)
	{
		if (image.Format.PixelFormat != PixelFormat.BGRA)
		{
			throw new ArgumentException("Image pixel format must be BGRA.", "image");
		}
		if (image.Format.Width != format.Width)
		{
			throw new ArgumentException("Image width does not match saver format.", "image");
		}
		if (image.Format.Height != format.Height)
		{
			throw new ArgumentException("Image height does not match saver format.", "image");
		}
		if (image.Data.Count != 1)
		{
			throw new ArgumentException("Image must contain exactly one data plane.", "image");
		}
		if (image.Stride.Count != 1)
		{
			throw new ArgumentException("Image must contain exactly one stride value.", "image");
		}
		if (image.Stride[0] < rowSize)
		{
			throw new ArgumentException("Image stride is less than the required row size.", "image");
		}
	}

	private bool CanAcceptFrameAfterWait()
	{
		ExceptionDispatchInfo exceptionDispatchInfo;
		bool flag;
		bool flag2;
		lock (InternalDisposeLockObject)
		{
			exceptionDispatchInfo = backgroundException;
			flag = IsDisposed;
			flag2 = isCompleting;
		}
		exceptionDispatchInfo?.Throw();
		if (flag)
		{
			return false;
		}
		if (flag2)
		{
			throw new InvalidOperationException("Cannot accept new frames after completion has started.");
		}
		return true;
	}

	private unsafe void CopyImage(RawImage source, UnmanagedRawImage destination)
	{
		using MemoryHandle memoryHandle = source.Data[0].Pin();
		using MemoryHandle memoryHandle2 = destination.Data[0].Pin();
		RawImage.CopyRegion((byte*)memoryHandle.Pointer, source.Stride[0], (byte*)memoryHandle2.Pointer, destination.Stride[0], rowSize, format.Height);
	}

	private async Task ProcessReadyBuffersAsync()
	{
		try
		{
			while (await readyBuffers.Reader.WaitToReadAsync().ConfigureAwait(continueOnCapturedContext: false))
			{
				FrameBuffer item;
				while (readyBuffers.Reader.TryRead(out item))
				{
					FrameBuffer frameBuffer = ProcessBuffer(item);
					if (frameBuffer != null)
					{
						ReturnBuffer(frameBuffer);
					}
				}
			}
		}
		catch (Exception ex)
		{
			SetBackgroundException(ex);
			throw;
		}
	}

	private FrameBuffer ProcessBuffer(FrameBuffer frameBuffer)
	{
		if (ShouldSkipDuplicateFrame(frameBuffer))
		{
			return frameBuffer;
		}
		int outputFrameNumber = GetOutputFrameNumber(frameBuffer);
		SaveBuffer(frameBuffer, outputFrameNumber);
		if (useSequentialFileNumbering)
		{
			nextSequentialFileNumber++;
		}
		if (!filterDuplicateFrames)
		{
			return frameBuffer;
		}
		FrameBuffer result = lastSavedFrameBuffer;
		lastSavedFrameBuffer = frameBuffer;
		return result;
	}

	private bool ShouldSkipDuplicateFrame(FrameBuffer frameBuffer)
	{
		if (!filterDuplicateFrames)
		{
			return false;
		}
		if (lastSavedFrameBuffer == null)
		{
			return false;
		}
		return AreImagesIdentical(frameBuffer.Image, lastSavedFrameBuffer.Image);
	}

	private int GetOutputFrameNumber(FrameBuffer frameBuffer)
	{
		if (useSequentialFileNumbering)
		{
			return nextSequentialFileNumber;
		}
		return frameBuffer.FrameNumber;
	}

	private unsafe bool AreImagesIdentical(RawImage first, RawImage second)
	{
		using MemoryHandle memoryHandle = first.Data[0].Pin();
		using MemoryHandle memoryHandle2 = second.Data[0].Pin();
		ulong* pointer = (ulong*)memoryHandle.Pointer;
		ulong* pointer2 = (ulong*)memoryHandle2.Pointer;
		int num = frameSize / 8;
		for (int i = 0; i < num; i++)
		{
			if (pointer[i] != pointer2[i])
			{
				return false;
			}
		}
		if (frameSize - num * 8 == 4)
		{
			uint* ptr = (uint*)(pointer + num);
			uint* ptr2 = (uint*)(pointer2 + num);
			if (*ptr != *ptr2)
			{
				return false;
			}
		}
		return true;
	}

	private unsafe void SaveBuffer(FrameBuffer frameBuffer, int frameNumber)
	{
		string filename = Path.Combine(directoryPath, $"frame_{frameNumber:D8}.png");
		using MemoryHandle memoryHandle = frameBuffer.Image.Data[0].Pin();
		using Bitmap bitmap = new Bitmap(format.Width, format.Height, frameBuffer.Image.Stride[0], System.Drawing.Imaging.PixelFormat.Format32bppArgb, (IntPtr)memoryHandle.Pointer);
		bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
	}

	private void SetBackgroundException(Exception ex)
	{
		lock (InternalDisposeLockObject)
		{
			if (backgroundException != null)
			{
				return;
			}
			backgroundException = ExceptionDispatchInfo.Capture(ex);
		}
		readyBuffers.Writer.TryComplete(ex);
		cts.Cancel();
	}

	private void ThrowIfBackgroundFailed()
	{
		GetBackgroundException()?.Throw();
	}

	private void ThrowIfCompleting()
	{
		lock (InternalDisposeLockObject)
		{
			if (isCompleting)
			{
				throw new InvalidOperationException("Cannot accept new frames after completion has started.");
			}
		}
	}

	private ExceptionDispatchInfo GetBackgroundException()
	{
		lock (InternalDisposeLockObject)
		{
			return backgroundException;
		}
	}

	private static string CreateDefaultDirectoryPath()
	{
		string path = Path.Combine(Path.GetTempPath(), "RawImagePngSeriesSaver");
		string path2 = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + Guid.NewGuid().ToString("N");
		return Path.Combine(path, path2);
	}

	private void WaitForPublicMethodsToFinish()
	{
		lock (InternalDisposeLockObject)
		{
			while (activePublicMethodCount != 0)
			{
				Monitor.Wait(InternalDisposeLockObject);
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}
		lock (InternalDisposeLockObject)
		{
			if (!isCompleting)
			{
				isCompleting = true;
				readyBuffers?.Writer.TryComplete();
			}
		}
		cts.Cancel();
		try
		{
			workerTask?.GetAwaiter().GetResult();
		}
		catch
		{
		}
		WaitForPublicMethodsToFinish();
		cts.Dispose();
		if (allBuffers == null)
		{
			return;
		}
		foreach (FrameBuffer allBuffer in allBuffers)
		{
			allBuffer.Dispose();
		}
	}
}
