using System;
using System.Collections.Generic;
using System.Configuration;

namespace SkrinshoterApp;

public static class ProductUrl
{
	private sealed class ServerUrlReplacement
	{
		private readonly string sourceServerUrl;

		private readonly string targetServerUrl;

		public ServerUrlReplacement(string sourceServerUrl, string targetServerUrl)
		{
			this.sourceServerUrl = sourceServerUrl;
			this.targetServerUrl = targetServerUrl;
		}

		public string TryResolve(string url)
		{
			if (!url.StartsWith(sourceServerUrl, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			if (!HasServerUrlBoundary(url, sourceServerUrl.Length))
			{
				return null;
			}
			return targetServerUrl + url.Substring(sourceServerUrl.Length);
		}

		private static bool HasServerUrlBoundary(string url, int index)
		{
			if (index == url.Length)
			{
				return true;
			}
			char c = url[index];
			if (c != '/' && c != '?')
			{
				return c == '#';
			}
			return true;
		}
	}

	private const string ServerUrlReplacementsSettingName = "ServerUrlReplacements";

	private static readonly IReadOnlyList<ServerUrlReplacement> serverUrlReplacements = LoadServerUrlReplacements();

	public static string Resolve(string url)
	{
		if (string.IsNullOrWhiteSpace(url))
		{
			throw new ArgumentException("URL is empty.", "url");
		}
		foreach (ServerUrlReplacement serverUrlReplacement in serverUrlReplacements)
		{
			string text = serverUrlReplacement.TryResolve(url);
			if (text != null)
			{
				return text;
			}
		}
		return url;
	}

	private static IReadOnlyList<ServerUrlReplacement> LoadServerUrlReplacements()
	{
		string text = ConfigurationManager.AppSettings["ServerUrlReplacements"];
		if (string.IsNullOrWhiteSpace(text))
		{
			return Array.Empty<ServerUrlReplacement>();
		}
		List<ServerUrlReplacement> list = new List<ServerUrlReplacement>();
		string[] array = text.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			ServerUrlReplacement serverUrlReplacement = ParseServerUrlReplacement(array[i]);
			if (serverUrlReplacement != null)
			{
				list.Add(serverUrlReplacement);
			}
		}
		return list;
	}

	private static ServerUrlReplacement ParseServerUrlReplacement(string value)
	{
		string[] array = value.Split(new string[1] { "=>" }, StringSplitOptions.None);
		if (array.Length != 2)
		{
			return null;
		}
		string text = ParseServerUrl(array[0]);
		string text2 = ParseServerUrl(array[1]);
		if (text == null || text2 == null)
		{
			return null;
		}
		return new ServerUrlReplacement(text, text2);
	}

	private static string ParseServerUrl(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		if (!Uri.TryCreate(value.Trim(), UriKind.Absolute, out var result))
		{
			return null;
		}
		if (result.Scheme != Uri.UriSchemeHttp && result.Scheme != Uri.UriSchemeHttps)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(result.UserInfo))
		{
			return null;
		}
		if (result.AbsolutePath != "/")
		{
			return null;
		}
		if (!string.IsNullOrEmpty(result.Query))
		{
			return null;
		}
		if (!string.IsNullOrEmpty(result.Fragment))
		{
			return null;
		}
		return result.GetLeftPart(UriPartial.Authority);
	}
}
