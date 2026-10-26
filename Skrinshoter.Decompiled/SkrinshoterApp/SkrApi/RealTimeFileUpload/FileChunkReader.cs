using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nito.AsyncEx;

namespace SkrinshoterApp.SkrApi.RealTimeFileUpload;

internal class FileChunkReader : BaseDisposable
{
	public const int DefaultChunkSize = 524288;

	private readonly ILogger logger;

	private readonly string filePath;

	private readonly int chunkSize;

	private FileStream stream;

	private int nextChunkIndex;

	private readonly AsyncManualResetEvent completed = new AsyncManualResetEvent();

	public FileChunkReader(ILogger<FileChunkReader> logger, string filePath, int chunkSize = 524288)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		if (chunkSize <= 0)
		{
			throw new ArgumentOutOfRangeException("chunkSize");
		}
		this.filePath = filePath ?? throw new ArgumentNullException("filePath");
		this.chunkSize = chunkSize;
	}

	public async Task<FileChunk> ReadNextChunkAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		if (stream == null)
		{
			await OpenFileAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
		byte[] buffer = new byte[chunkSize];
		long offset = stream.Position;
		int num = await ReadExactlyAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		if (num == 0)
		{
			logger.LogInformation("Reached end of file");
			return null;
		}
		FileChunk fileChunk = new FileChunk(nextChunkIndex++, offset, new ReadOnlyMemory<byte>(buffer, 0, num));
		logger.LogInformation("Chunk read: Index={Index}, Size={Size}, Offset={Offset}", fileChunk.Index, fileChunk.Data.Length, fileChunk.Offset);
		return fileChunk;
	}

	public void Complete()
	{
		completed.Set();
	}

	public void Reset()
	{
		stream.Seek(0L, SeekOrigin.Begin);
		nextChunkIndex = 0;
	}

	private async Task OpenFileAsync(CancellationToken cancellationToken)
	{
		while (!File.Exists(filePath) && !completed.IsSet)
		{
			await Task.WhenAny(Task.Delay(1000, cancellationToken), completed.WaitAsync(cancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
			cancellationToken.ThrowIfCancellationRequested();
		}
		stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
		logger.LogInformation("Opened file for reading: \"{FilePath}\"", filePath);
	}

	private async Task<int> ReadExactlyAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
	{
		int totalBytes = 0;
		while (totalBytes < count)
		{
			bool isCompleted = completed.IsSet;
			int num = await stream.ReadAsync(buffer, offset, count - totalBytes, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (num == 0)
			{
				if (isCompleted)
				{
					break;
				}
				await Task.WhenAny(Task.Delay(1000, cancellationToken), completed.WaitAsync(cancellationToken)).ConfigureAwait(continueOnCapturedContext: false);
				cancellationToken.ThrowIfCancellationRequested();
			}
			else
			{
				offset += num;
				totalBytes += num;
			}
		}
		return totalBytes;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			stream?.Dispose();
		}
	}
}
