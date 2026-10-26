using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.SkrApi;

internal class SkrApiHttpClient
{
	private const int ConnectionTimeout = 1800;

	private static HttpClient _httpClient;

	private static HttpClient _statsHttpClient;

	public static string AppID { get; set; }

	public static string AppName { get; set; }

	public static string AppVersion { get; set; }

	public static HttpClient HttpClient
	{
		get
		{
			if (_httpClient == null)
			{
				_httpClient = CreateHttpClient();
				_httpClient.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_MainApi));
				string value = Convert.ToBase64String(new SkrAes128(AppID.ToUpper()).Encode());
				_httpClient.DefaultRequestHeaders.Add("X-Skr-Auth", value);
			}
			return _httpClient;
		}
	}

	public static HttpClient StatsHttpClient
	{
		get
		{
			if (_statsHttpClient == null)
			{
				_statsHttpClient = CreateHttpClient();
				_statsHttpClient.BaseAddress = new Uri(ProductUrl.Resolve(ProductResources.BaseUrl_StatsApi));
				string value = Convert.ToBase64String(new SkrAes128(AppID.ToUpper()).Encode());
				_statsHttpClient.DefaultRequestHeaders.Add("X-Skr-Auth", value);
			}
			return _statsHttpClient;
		}
	}

	private static HttpClient CreateHttpClient()
	{
		if (string.IsNullOrEmpty(AppID))
		{
			throw new InvalidOperationException("AppID is not set");
		}
		if (string.IsNullOrEmpty(AppName))
		{
			throw new InvalidOperationException("AppName is not set");
		}
		if (string.IsNullOrEmpty(AppVersion))
		{
			throw new InvalidOperationException("AppVersion is not set");
		}
		string text = ConfigurationManager.AppSettings["ProxyAddress"];
		WebProxy proxy = null;
		if (text != null)
		{
			proxy = new WebProxy(text);
		}
		HttpClient httpClient = new HttpClient(new HttpClientHandler
		{
			Proxy = proxy
		})
		{
			Timeout = TimeSpan.FromSeconds(1800.0)
		};
		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AppName + "/" + AppVersion + " (" + AppID.ToUpper() + ")");
		return httpClient;
	}
}
