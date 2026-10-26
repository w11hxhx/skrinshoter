using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using SkrinshoterApp.Common;

namespace SkrinshoterApp;

internal class FilePostUploader
{
	private readonly HttpClient httpClient;

	private readonly string requestFileName;

	private readonly string filePath;

	private readonly Dictionary<string, string> additionalFields;

	private readonly string urlPath;

	private readonly CancellationToken cancellationToken;

	private Action<int> onProgress;

	private readonly Func<HttpResponseMessage, Task> responseChecker;

	public FilePostUploader(HttpClient httpClient, string requestFileName, string filePath, Dictionary<string, string> additionalFields, string urlPath, CancellationToken cancellationToken, Action<int> onProgress, Func<HttpResponseMessage, Task> responseChecker)
	{
		this.httpClient = httpClient;
		this.requestFileName = requestFileName;
		this.filePath = filePath;
		this.additionalFields = additionalFields;
		this.urlPath = urlPath;
		this.cancellationToken = cancellationToken;
		this.onProgress = onProgress;
		this.responseChecker = responseChecker;
	}

	public async Task Upload()
	{
		MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent("----MyBoundary" + DateTime.Now.Ticks.ToString("x"));
		foreach (KeyValuePair<string, string> additionalField in additionalFields)
		{
			multipartFormDataContent.Add(new StringContent(additionalField.Value), additionalField.Key);
		}
		using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		long fileLength = fileStream.Length;
		ProgressableStreamContent progressableStreamContent = new ProgressableStreamContent(fileStream, new SynchronousProgress<long>((long bytes) =>
		{
			int obj = (int)(bytes * 100 / fileLength);
			onProgress?.Invoke(obj);
		}));
		progressableStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
		{
			Name = "\"" + requestFileName + "\"",
			FileName = "\"" + Path.GetFileName(filePath) + "\""
		};
		progressableStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
		multipartFormDataContent.Add(progressableStreamContent);
		HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(urlPath, multipartFormDataContent, cancellationToken);
		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			if (httpResponseMessage.StatusCode == (HttpStatusCode)507)
			{
				throw new CloudStorageQuotaExceededException();
			}
			throw new Exception(httpResponseMessage.ReasonPhrase);
		}
		await responseChecker(httpResponseMessage);
	}
}
