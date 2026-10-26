using System;

namespace SkrinshoterApp.Common;

public static class UrlHelper
{
	public static string AddQueryString(string url, string queryString)
	{
		if (string.IsNullOrEmpty(queryString))
		{
			return url;
		}
		UriBuilder uriBuilder = new UriBuilder(url);
		string text = uriBuilder.Query.TrimStart('?');
		string text2 = queryString.TrimStart('?');
		uriBuilder.Query = (string.IsNullOrEmpty(text) ? text2 : (text + "&" + text2));
		return uriBuilder.Uri.ToString();
	}
}
