using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;
using SkrinshoterApp.Services.Progress;

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
			{ ScreenshotUploadDestination.Nuuls, new Provider(ScreenshotUploadDestination.Nuuls, "i.nuuls.com", "https://i.nuuls.com/upload", "") },
			{ ScreenshotUploadDestination.KappaLol, new Provider(ScreenshotUploadDestination.KappaLol, "kappa.lol", "https://kappa.lol/api/upload", "link") },
			{ ScreenshotUploadDestination.Xyli, new Provider(ScreenshotUploadDestination.Xyli, "Xyli", "https://img.xyli.eu/upload", "link") },
			{ ScreenshotUploadDestination.ZeroXl1r, new Provider(ScreenshotUploadDestination.ZeroXl1r, "0xl1r.cfd", "https://0xl1r.cfd/upload", "link") },
			{ ScreenshotUploadDestination.NoikCloud, new Provider(ScreenshotUploadDestination.NoikCloud, "NoikCloud", "https://noikcloud.xyz/upload", "url") }
		};

	private readonly IHttpClientFactory httpClientFactory;

	private readonly IProgressWindowService progressWindowService;

	public ExternalScreenshotUploadService(IHttpClientFactory httpClientFactory, IProgressWindowService progressWindowService)
	{
		this.httpClientFactory = httpClientFactory;
		this.progressWindowService = progressWindowService;
	}

	public async Task<ScreenshotUploadResult> UploadAsync(ScreenshotUploadDestination destination, string filePath, CancellationToken cancellationToken)
	{
		if (!Providers.TryGetValue(destination, out Provider provider))
		{
			throw new ArgumentOutOfRangeException(nameof(destination));
		}

		return await UploadAsync(new CustomUploadProvider { Name = provider.Name, Url = provider.RequestUrl, ImageLink = provider.ResponseProperty }, filePath, cancellationToken, destination);
	}

	public async Task<ScreenshotUploadResult> UploadAsync(CustomUploadProvider provider, string filePath, CancellationToken cancellationToken, ScreenshotUploadDestination destination = ScreenshotUploadDestination.Custom)
	{
		using IProgressSession session = progressWindowService.TryShow("Загрузка в " + provider.Name, true, externalToken: cancellationToken);
		session.SetIndeterminate(false);
		cancellationToken = session.Token;
		using HttpClient httpClient = httpClientFactory.CreateClient(HttpClientConfig.Names.ExternalScreenshotUpload);
		using MultipartFormDataContent form = new MultipartFormDataContent();
		foreach (KeyValuePair<string, string> field in provider.FormFields)
		{
			form.Add(new StringContent(field.Value ?? ""), field.Key);
		}
		using FileStream stream = File.OpenRead(filePath);
		using ProgressFileContent file = new ProgressFileContent(stream, session.Progress, cancellationToken);
		file.Headers.ContentType = new MediaTypeHeaderValue(Path.GetExtension(filePath).ToLowerInvariant() switch
		{
			".mp4" => "video/mp4",
			".png" => "image/png",
			".jpg" or ".jpeg" => "image/jpeg",
			_ => "application/octet-stream"
		});
		form.Add(file, provider.FileField, Path.GetFileName(filePath));
		using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(provider.Method), provider.Url) { Content = form };
		foreach (KeyValuePair<string, string> header in provider.Headers)
		{
			if (!request.Headers.TryAddWithoutValidation(header.Key, header.Value))
			{
				throw new InvalidOperationException("Некорректный заголовок: " + header.Key);
			}
		}
		using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);
		string responseText = await response.Content.ReadAsStringAsync();
		response.EnsureSuccessStatusCode();

		string url = responseText.Trim().Trim('"');
		if (!string.IsNullOrWhiteSpace(provider.ImageLink))
		{
			url = ResolveResponseExpression(provider.ImageLink, responseText);
		}
		else if (url.StartsWith("{"))
		{
			JToken json = JToken.Parse(responseText);
			url = (string)(json["link"] ?? json["url"]);
		}
		if (!Uri.TryCreate(url, UriKind.Absolute, out Uri parsed) || (parsed.Scheme != "https" && parsed.Scheme != "http"))
		{
			throw new InvalidOperationException(provider.Name + ": сервис не вернул HTTP-ссылку на файл.");
		}
		session.Progress.Report(100);

		string deletionUrl = null;
		if (!string.IsNullOrWhiteSpace(provider.DeletionLink))
		{
			string candidate = ResolveResponseExpression(provider.DeletionLink, responseText);
			if (Uri.TryCreate(candidate, UriKind.Absolute, out Uri deletionUri) && (deletionUri.Scheme == "https" || deletionUri.Scheme == "http"))
			{
				deletionUrl = candidate;
			}
		}
		return new ScreenshotUploadResult(destination, provider.Name, url, deletionUrl);
	}

	private static string ResolveResponseExpression(string expression, string responseText)
	{
		string trimmed = expression.Trim();
		JToken json = null;
		string JsonValue(string path)
		{
			json ??= JToken.Parse(responseText);
			return (string)json.SelectToken(path.Trim());
		}

		if (!trimmed.Contains("{json:") && !trimmed.Contains("$json:"))
		{
			return Uri.TryCreate(trimmed, UriKind.Absolute, out Uri _) ? trimmed : JsonValue(trimmed);
		}
		string result = Regex.Replace(trimmed, @"\{json:([^}]+)\}", match => JsonValue(match.Groups[1].Value) ?? "", RegexOptions.IgnoreCase);
		result = Regex.Replace(result, @"\$json:([^$]+)\$", match => JsonValue(match.Groups[1].Value) ?? "", RegexOptions.IgnoreCase);
		return result.Replace("{response}", responseText).Replace("$response$", responseText);
	}
}

public sealed class ScreenshotUploadResult
{
	public ScreenshotUploadDestination Destination { get; }

	public string ServiceName { get; }

	public string Url { get; }

	public string DeletionUrl { get; }

	public ScreenshotUploadResult(ScreenshotUploadDestination destination, string serviceName, string url) : this(destination, serviceName, url, null) { }

	public ScreenshotUploadResult(ScreenshotUploadDestination destination, string serviceName, string url, string deletionUrl)
	{
		Destination = destination;
		ServiceName = serviceName;
		Url = url;
		DeletionUrl = deletionUrl;
	}
}
