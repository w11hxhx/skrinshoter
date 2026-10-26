using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public class ProgressableStreamContent : HttpContent
{
	private const int defaultBufferSize = 4096;

	private Stream content;

	private int bufferSize;

	private bool contentConsumed;

	private IProgress<long> progressHandler;

	public ProgressableStreamContent(Stream content, IProgress<long> progressHandler, int bufferSize = 4096)
	{
		if (content == null)
		{
			throw new ArgumentNullException("content");
		}
		if (bufferSize <= 0)
		{
			throw new ArgumentOutOfRangeException("bufferSize");
		}
		this.content = content;
		this.bufferSize = bufferSize;
		this.progressHandler = progressHandler;
		Headers.ContentLength = content.Length;
	}

	protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
	{
		if (content == null)
		{
			throw new InvalidOperationException("Content is null.");
		}
		if (contentConsumed)
		{
			if (!content.CanSeek)
			{
				throw new InvalidOperationException("Content already consumed.");
			}
			content.Position = 0L;
		}
		contentConsumed = true;
		byte[] buffer = new byte[bufferSize];
		_ = content.Length;
		long uploaded = 0L;
		while (true)
		{
			int length = await content.ReadAsync(buffer, 0, buffer.Length);
			if (length <= 0)
			{
				break;
			}
			await stream.WriteAsync(buffer, 0, length);
			uploaded += length;
			progressHandler?.Report(uploaded);
		}
	}

	protected override bool TryComputeLength(out long length)
	{
		length = content.Length;
		return true;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			content.Dispose();
		}
		base.Dispose(disposing);
	}
}
