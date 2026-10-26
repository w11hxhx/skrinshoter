using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SkrinshoterApp;

public sealed class ExternalScreenshotUploadService
{
	private sealed class Provider
	{
		public ScreenshotUploadDestination Destination { get; }

		public string Name { get; }

		public string RequestUrl { get; }

		public string ResponseProperty { get; }

		public Provider(ScreenshotUploadDestination destination, string name, string requestUrl, string responseProperty)
		{
			Destination = destination;
			Name = name;
			RequestUrl = requestUrl;
			ResponseProperty = responseProperty;
		}
	}

	private static readonly IReadOnlyDictionary<ScreenshotUploadDestination, Provider> Providers =
		new Dictionary<ScreenshotUploadDestination, Provider>
		{
			{ ScreenshotUploadDestination.KappaLol, new Provider(ScreenshotUploadDestination.KappaLol, "kappa.lol", "https://kappa.lol/api/upload", "link") },
			{ ScreenshotUploadDestination.Xyli, new Provider(ScreenshotUploadDestination.Xyli, "Xyli", "https://img.xyli.eu/upload", "link") },
			{ ScreenshotUploadDestination.ZeroXl1r, new Provider(ScreenshotUploadDestination.ZeroXl1r, "0xl1r.cfd", "https://0xl1r.cfd/upload", "link") },
			{ ScreenshotUploadDestination.NoikCloud, new Provider(ScreenshotUploadDestination.NoikCloud, "NoikCloud", "https://noikcloud.xyz/upload", "url") }
		};

	private readonly IHttpClientFactory httpClientFactory;

	public ExternalScreenshotUploadService(IHttpClientFactory httpClientFactory)
	{
		this.httpClientFactory = httpClientFactory;
	}

	public async Task<ScreenshotUploadResult> UploadAsync(ScreenshotUploadDestination destination, string filePath, CancellationToken cancellationToken)
	{
		if (!Providers.TryGetValue(destination, out Provider provider))
		{
			throw new ArgumentOutOfRangeException(nameof(destination));
		}

		using HttpClient httpClient = httpClientFactory.CreateClient(HttpClientConfig.Names.ExternalScreenshotUpload);
		using MultipartFormDataContent form = new MultipartFormDataContent();
		using FileStream stream = File.OpenRead(filePath);
		using StreamContent file = new StreamContent(stream);
		form.Add(file, "file", Path.GetFileName(filePath));
		using HttpResponseMessage response = await httpClient.PostAsync(provider.RequestUrl, form, cancellationToken);
		string responseText = await response.Content.ReadAsStringAsync();
		response.EnsureSuccessStatusCode();

		JObject json = JObject.Parse(responseText);
		string url = json.Value<string>(provider.ResponseProperty);
		if (string.IsNullOrWhiteSpace(url))
		{
			string error = json.Value<string>("error") ?? json.Value<string>("message") ?? "Сервис не вернул ссылку на файл.";
			throw new InvalidOperationException(provider.Name + ": " + error);
		}

		if (!Uri.TryCreate(url, UriKind.Absolute, out Uri _))
		{
			throw new InvalidOperationException(provider.Name + ": получена некорректная ссылка.");
		}

		return new ScreenshotUploadResult(destination, provider.Name, url);
	}
}

public sealed class ScreenshotUploadResult
{
	public ScreenshotUploadDestination Destination { get; }

	public string ServiceName { get; }

	public string Url { get; }

	public ScreenshotUploadResult(ScreenshotUploadDestination destination, string serviceName, string url)
	{
		Destination = destination;
		ServiceName = serviceName;
		Url = url;
	}
}
