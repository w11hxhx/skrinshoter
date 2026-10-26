using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace SkrinshoterApp.SkrApi.RealTimeFileUpload;

internal class FileChunkUploader : IAsyncDisposable
{
	private class StartResponseDto
	{
		[JsonRequired]
		public string SessionID { get; set; }

		[JsonRequired]
		public string Link { get; set; }
	}

	private class StatusResponseDto
	{
		[JsonRequired]
		public string Status { get; set; }
	}

	private class FinishResponseDto
	{
		[JsonRequired]
		public string Md5Sum { get; set; }
	}

	private enum State
	{
		Created,
		Started,
		Finished,
		Destroyed,
		Disposed
	}

	private readonly ILogger logger;

	private readonly HttpClient httpClient;

	private readonly string appId;

	private readonly string fileName;

	private readonly string windowTitle;

	private string sessionId;

	private State state;

	public FileChunkUploader(HttpClient httpClient, ILogger<FileChunkUploader> logger, string appId, string fileName, string windowTitle)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.httpClient = httpClient ?? throw new ArgumentNullException("httpClient");
		this.appId = appId ?? throw new ArgumentNullException("appId");
		this.fileName = fileName ?? throw new ArgumentNullException("fileName");
		this.windowTitle = windowTitle ?? throw new ArgumentNullException("windowTitle");
		if (!Guid.TryParseExact(appId, "B", out var _))
		{
			throw new FormatException("appId has wrong format.");
		}
	}

	public async Task<Uri> StartAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		EnsureValidState(default(State));
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/video-uploader/start");
		request.Headers.Add("appID", appId);
		request.Headers.Add("filename", Uri.EscapeDataString(fileName));
		request.Headers.Add("windowTitle", Uri.EscapeDataString(windowTitle));
		request.Headers.Add("client", "windows");
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		if (response.StatusCode == (HttpStatusCode)507)
		{
			throw new CloudStorageQuotaExceededException();
		}
		response.EnsureSuccessStatusCode();
		StartResponseDto startResponseDto = JsonConvert.DeserializeObject<StartResponseDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false));
		sessionId = startResponseDto.SessionID;
		state = State.Started;
		logger.LogInformation("File uploading started");
		return new Uri(startResponseDto.Link);
	}

	public async Task UploadChunkAsync(FileChunk chunk, IProgress<long> progressHandler = null, CancellationToken cancellationToken = default(CancellationToken))
	{
		if (chunk == null)
		{
			throw new ArgumentNullException("chunk");
		}
		EnsureValidState(State.Started);
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/video-uploader/chunks");
		request.Headers.Add("session-id", sessionId);
		request.Headers.Add("chunk-id", chunk.Index.ToString());
		request.Content = new ProgressableStreamContent(chunk.Data.AsStream(), progressHandler);
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		response.EnsureSuccessStatusCode();
		StatusResponseDto statusResponseDto = JsonConvert.DeserializeObject<StatusResponseDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false));
		if (statusResponseDto.Status != "received")
		{
			throw new HttpRequestException("Received wrong response from server: " + statusResponseDto.Status);
		}
		logger.LogInformation("Chunk uploaded: Index={Index}", chunk.Index);
	}

	public async Task<string> FinishAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		EnsureValidState(State.Started);
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/video-uploader/finish");
		request.Headers.Add("session-id", sessionId);
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		response.EnsureSuccessStatusCode();
		FinishResponseDto finishResponseDto = JsonConvert.DeserializeObject<FinishResponseDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false));
		state = State.Finished;
		logger.LogInformation("File uploading finished: MD5={Md5}", finishResponseDto.Md5Sum);
		return finishResponseDto.Md5Sum;
	}

	public async Task DestroyAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		EnsureValidState(State.Started, State.Finished);
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/video-uploader/destroy");
		request.Headers.Add("session-id", sessionId);
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		response.EnsureSuccessStatusCode();
		StatusResponseDto statusResponseDto = JsonConvert.DeserializeObject<StatusResponseDto>(await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false));
		if (statusResponseDto.Status != "session destroyed" && statusResponseDto.Status != "file destroyed")
		{
			throw new HttpRequestException("Received wrong response from server: " + statusResponseDto.Status);
		}
		state = State.Destroyed;
		logger.LogInformation("File uploading canceled");
	}

	private void EnsureValidState(params State[] states)
	{
		if (!states.Contains(state))
		{
			throw new InvalidOperationException($"Invalid state: {state}.");
		}
	}

	public async ValueTask DisposeAsync()
	{
		if (state == State.Started)
		{
			try
			{
				using CancellationTokenSource cts = new CancellationTokenSource(5000);
				await DestroyAsync(cts.Token).ConfigureAwait(continueOnCapturedContext: false);
			}
			catch
			{
			}
		}
		state = State.Disposed;
		GC.SuppressFinalize(this);
	}
}
