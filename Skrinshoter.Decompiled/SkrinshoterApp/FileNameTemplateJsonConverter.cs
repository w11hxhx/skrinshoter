using System;
using Newtonsoft.Json;

namespace SkrinshoterApp;

public class FileNameTemplateJsonConverter : JsonConverter<FileNameTemplate>
{
	public override void WriteJson(JsonWriter writer, FileNameTemplate value, JsonSerializer serializer)
	{
		if (value == null)
		{
			throw new JsonSerializationException("Value is null.");
		}
		writer.WriteValue(value.ToString());
	}

	public override FileNameTemplate ReadJson(JsonReader reader, Type objectType, FileNameTemplate existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.String)
		{
			return new FileNameTemplate((string)reader.Value);
		}
		throw new JsonSerializationException("Expected string value.");
	}
}
