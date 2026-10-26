using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using SkrinshoterApp.Common;

namespace SkrinshoterApp.SkrApi.RealTimeFileUpload;

public class RealTimeVideoFileUploader : BaseDisposable
{
	private readonly string filePath;

	private readonly IProgress<long> progressHandler;

	private readonly FileChunkReader fileReader;

	private readonly FileChunkUploader fileUploader;

	private readonly TaskCompletionSource<Uri> fileUrlTcs = new TaskCompletionSource<Uri>();

	private long successfullyUploadedBytes;

	public long FileSize { get; private set; }

	public RealTimeVideoFileUploader(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory, string filePath, string friendlyFileName, string windowTitle, string appId, IProgress<long> progressHandler = null)
	{
		this.filePath = filePath ?? throw new ArgumentNullException("filePath");
		if (friendlyFileName == null)
		{
			throw new ArgumentNullException("friendlyFileName");
		}
		if (appId == null)
		{
			throw new ArgumentNullException("appId");
		}
		this.progressHandler = progressHandler;
		fileReader = new FileChunkReader(loggerFactory?.CreateLogger<FileChunkReader>(), filePath);
		HttpClient httpClient = httpClientFactory.CreateClient("FileChunkUploader");
		fileUploader = new FileChunkUploader(httpClient, loggerFactory?.CreateLogger<FileChunkUploader>(), appId, friendlyFileName, windowTitle);
	}

	public async Task UploadAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		try
		{
			try
			{
				Uri result = await fileUploader.StartAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				fileUrlTcs.SetResult(result);
			}
			catch (Exception exception)
			{
				fileUrlTcs.SetException(exception);
				throw;
			}
			FileChunk fileChunk;
			while ((fileChunk = await fileReader.ReadNextChunkAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)) != null)
			{
				if (fileChunk.Index > 0)
				{
					await UploadChunk(fileChunk, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				}
			}
			if (FileSize == 0L)
			{
				throw new InvalidOperationException("Video file is empty.");
			}
			fileReader.Reset();
			fileChunk = await fileReader.ReadNextChunkAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (fileChunk == null)
			{
				throw new InvalidOperationException("Chunk 0 is empty.");
			}
			await UploadChunk(fileChunk, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			string md5hash = await fileUploader.FinishAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				CheckMd5Hash(md5hash);
			}
			catch
			{
				await fileUploader.DestroyAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				throw;
			}
		}
		catch
		{
			await fileUploader.DisposeAsync().ConfigureAwait(continueOnCapturedContext: false);
			throw;
		}
	}

	public async Task<Uri> GetFileUrlAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		return await fileUrlTcs.Task.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
	}

	public void Complete()
	{
		fileReader.Complete();
		FileSize = new FileInfo(filePath).Length;
	}

	private async Task UploadChunk(FileChunk chunk, CancellationToken cancellationToken)
	{
		await fileUploader.UploadChunkAsync(chunk, new SynchronousProgress<long>(ProgressHandler), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		successfullyUploadedBytes += chunk.Data.Length;
	}

	private void ProgressHandler(long currentChunkProgress)
	{
		progressHandler?.Report(successfullyUploadedBytes + currentChunkProgress);
	}

	private void CheckMd5Hash(string md5hash)
	{
		using MD5 mD = MD5.Create();
		using FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.SequentialScan);
		if (BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", "").ToLower() != md5hash.ToLower())
		{
			throw new InvalidOperationException("MD5 mismatch");
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			fileReader.Dispose();
		}
	}
}
