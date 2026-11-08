using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SkrinshoterApp;

public static class ShareXCustomUploaderImporter
{
	public static CustomUploadProvider Import(string filePath)
	{
		JObject config = JObject.Parse(File.ReadAllText(filePath));
		string body = config.Value<string>("Body");
		if (!string.IsNullOrWhiteSpace(body) && !body.Equals("MultipartFormData", StringComparison.OrdinalIgnoreCase))
		{
			throw new NotSupportedException("Поддерживаются ShareX-конфиги с Body = MultipartFormData.");
		}

		CustomUploadProvider provider = new CustomUploadProvider
		{
			Name = config.Value<string>("Name") ?? Path.GetFileNameWithoutExtension(filePath),
			Url = config.Value<string>("RequestURL") ?? "",
			Method = (config.Value<string>("RequestMethod") ?? "POST").ToUpperInvariant(),
			FileField = config.Value<string>("FileFormName") ?? "file",
			ImageLink = config.Value<string>("URL") ?? "",
			DeletionLink = config.Value<string>("DeletionURL") ?? ""
		};
		provider.Headers = ReadDictionary(config["Headers"]);
		provider.FormFields = ReadDictionary(config["Arguments"]);

		if (!Uri.TryCreate(provider.Url, UriKind.Absolute, out Uri uri) || (uri.Scheme != "https" && uri.Scheme != "http"))
		{
			throw new FormatException("В ShareX-конфиге отсутствует корректный RequestURL.");
		}
		if (provider.Method != "POST" && provider.Method != "PUT" && provider.Method != "PATCH")
		{
			throw new NotSupportedException("Метод ShareX-конфига должен быть POST, PUT или PATCH.");
		}
		return provider;
	}

	private static Dictionary<string, string> ReadDictionary(JToken token)
	{
		Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		if (token is JObject values)
		{
			foreach (JProperty property in values.Properties())
			{
				result[property.Name] = property.Value.Type == JTokenType.Null ? "" : property.Value.ToString();
			}
		}
		return result;
	}
}
