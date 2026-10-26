using System;
using Newtonsoft.Json;

namespace SkrinshoterApp;

public sealed class ZeroOneBooleanConverter : JsonConverter<bool>
{
	public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.String)
		{
			throw new JsonSerializationException($"Expected string token, got {reader.TokenType}.");
		}
		string text = (string)reader.Value;
		if (text == "0")
		{
			return false;
		}
		if (text == "1")
		{
			return true;
		}
		throw new JsonSerializationException("Expected \"0\" or \"1\", got \"" + text + "\".");
	}

	public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
	{
		writer.WriteValue(value ? "1" : "0");
	}
}
