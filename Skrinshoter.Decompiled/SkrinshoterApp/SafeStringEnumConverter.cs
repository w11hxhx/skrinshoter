using System;
using System.Linq;
using Newtonsoft.Json;

namespace SkrinshoterApp;

public class SafeStringEnumConverter : JsonConverter
{
	private readonly int defaultValue;

	public SafeStringEnumConverter()
	{
	}

	public SafeStringEnumConverter(int defaultValue)
	{
		this.defaultValue = defaultValue;
	}

	public override bool CanConvert(Type objectType)
	{
		return (IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum;
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		bool flag = IsNullableType(objectType);
		Type enumType = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
		string[] names = Enum.GetNames(enumType);
		if (reader.TokenType == JsonToken.String)
		{
			string enumText = reader.Value.ToString();
			if (!string.IsNullOrEmpty(enumText))
			{
				string text = names.Where((string n) => string.Equals(n, enumText, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
				if (text != null)
				{
					return Enum.Parse(enumType, text);
				}
			}
		}
		else if (reader.TokenType == JsonToken.Integer)
		{
			int value = Convert.ToInt32(reader.Value);
			if (((int[])Enum.GetValues(enumType)).Contains(value))
			{
				return Enum.Parse(enumType, value.ToString());
			}
		}
		if (!flag)
		{
			return Enum.Parse(enumType, defaultValue.ToString());
		}
		return null;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		writer.WriteValue(value.ToString());
	}

	private bool IsNullableType(Type t)
	{
		if (t.IsGenericType)
		{
			return t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
		return false;
	}
}
