using System;
using System.Globalization;
using System.Windows.Media;
using Newtonsoft.Json;

namespace SkrinshoterApp;

public class ColorJsonConverter : JsonConverter<Color>
{
	public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
	{
		int value2 = (value.B << 16) | (value.G << 8) | value.R;
		writer.WriteValue(value2);
	}

	public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Integer)
		{
			int num = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);
			return Color.FromRgb((byte)(num & 0xFF), (byte)((num >> 8) & 0xFF), (byte)((num >> 16) & 0xFF));
		}
		return Colors.Black;
	}
}
