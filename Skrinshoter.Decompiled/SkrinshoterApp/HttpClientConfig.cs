using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using SkrinshoterApp.Common;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public static class HttpClientConfig
{
	public static class Names
	{
		public const string MainNoRetry = "MainNoRetry";

		public const string MainWithRetry = "MainWithRetry";

		public const string FileChunkUploader = "FileChunkUploader";

		public const string AppUpdateChecker = "AppUpdateChecker";

		public const string ExternalScreenshotUpload = "ExternalScreenshotUpload";
	}

	public static IServiceCollection AddHttpClients(this IServiceCollection services)
	{
		ServicePointManager.DefaultConnectionLimit = 20;
		services.AddHttpClient("MainNoRetry", (IServiceProvider sp, HttpClient client) =>
		{
			ConfigureHttpClient(sp, client);
			client.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_MainApi));
			string value = Convert.ToBase64String(new SkrAes128(sp.GetRequiredService<AppInfoProvider>().AppId.ToUpperInvariant()).Encode());
			client.DefaultRequestHeaders.Add("X-Skr-Auth", value);
		}).ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler).AddPolicyHandler(CreateNoRetryPolicy(TimeSpan.FromSeconds(30.0)));
		services.AddHttpClient("MainWithRetry", (IServiceProvider sp, HttpClient client) =>
		{
			ConfigureHttpClient(sp, client);
			client.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_MainApi));
			string value = Convert.ToBase64String(new SkrAes128(sp.GetRequiredService<AppInfoProvider>().AppId.ToUpperInvariant()).Encode());
			client.DefaultRequestHeaders.Add("X-Skr-Auth", value);
		}).ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler).AddPolicyHandler((IServiceProvider sp, HttpRequestMessage request) => CreateRetryPolicy(sp, request, TimeSpan.FromSeconds(30.0)));
		services.AddHttpClient("FileChunkUploader", (IServiceProvider sp, HttpClient client) =>
		{
			ConfigureHttpClient(sp, client);
			client.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_MainApi));
		}).ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler).AddPolicyHandler((IServiceProvider sp, HttpRequestMessage request) => CreateRetryPolicy(sp, request, Timeout.InfiniteTimeSpan));
		services.AddHttpClient("AppUpdateChecker", (IServiceProvider sp, HttpClient client) =>
		{
			ConfigureHttpClient(sp, client);
			client.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_Cdn));
		}).ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler).AddPolicyHandler((IServiceProvider sp, HttpRequestMessage request) => CreateRetryPolicy(sp, request, TimeSpan.FromSeconds(30.0)));
		services.AddHttpClient("ExternalScreenshotUpload", (IServiceProvider sp, HttpClient client) =>
		{
			ConfigureHttpClient(sp, client);
		}).ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler).AddPolicyHandler(CreateNoRetryPolicy(TimeSpan.FromMinutes(2.0)));
		return services;
	}

	private static void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient httpClient)
	{
		httpClient.Timeout = Timeout.InfiniteTimeSpan;
		string appInvariantName = ProductResources.AppInvariantName;
		string text = serviceProvider.GetRequiredService<AppInfoProvider>().Version.ToString();
		string appId = serviceProvider.GetRequiredService<AppInfoProvider>().AppId;
		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(appInvariantName + "/" + text + " (" + appId + ")");
	}

	private static HttpClientHandler CreateHttpClientHandler()
	{
		return new HttpClientHandler
		{
			Proxy = GetProxy(),
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
		};
	}

	private static WebProxy GetProxy()
	{
		string text = ConfigurationManager.AppSettings["ProxyAddress"];
		if (!string.IsNullOrWhiteSpace(text))
		{
			return new WebProxy(text);
		}
		return null;
	}

	private static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(IServiceProvider serviceProvider, HttpRequestMessage request, TimeSpan timeout)
	{
		ILogger logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("HttpRetry");
		AsyncTimeoutPolicy<HttpResponseMessage> innerPolicy = Policy.TimeoutAsync<HttpResponseMessage>(timeout, TimeoutStrategy.Optimistic);
		return HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>().OrResult((HttpResponseMessage r) => r.StatusCode == (HttpStatusCode)429)
			.WaitAndRetryAsync(5, (int retryAttempt) => TimeSpan.FromSeconds(Math.Pow(2.0, retryAttempt)), (DelegateResult<HttpResponseMessage> outcome, TimeSpan delay, int retryAttempt, Context context) =>
			{
				string text = request.RequestUri?.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped) ?? "<null>";
				string text2 = ((outcome.Exception != null) ? outcome.Exception.AggregateMessages() : ((outcome.Result != null) ? ((int)outcome.Result.StatusCode + " " + outcome.Result.ReasonPhrase) : "unknown"));
				logger.LogWarning("HTTP error (delaying for {Delay}s, then making retry {RetryAttempt}): {Method} \"{Uri}\" --> {Reason}", delay.TotalSeconds, retryAttempt, request.Method, text, text2);
			})
			.WrapAsync(innerPolicy);
	}

	private static IAsyncPolicy<HttpResponseMessage> CreateNoRetryPolicy(TimeSpan timeout)
	{
		return Policy.TimeoutAsync<HttpResponseMessage>(timeout, TimeoutStrategy.Optimistic);
	}
}
