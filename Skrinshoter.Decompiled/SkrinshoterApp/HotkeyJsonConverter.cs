using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;

namespace SkrinshoterApp;

public class HotkeyJsonConverter : JsonConverter<Hotkey>
{
	private static readonly int[] ShiftCodes = new int[3] { 16, 160, 161 };

	private static readonly int[] CtrlCodes = new int[3] { 17, 162, 163 };

	private static readonly int[] AltCodes = new int[3] { 18, 164, 165 };

	private static readonly int[] WinCodes = new int[2] { 91, 92 };

	public override void WriteJson(JsonWriter writer, Hotkey hotkey, JsonSerializer serializer)
	{
		List<int> list = new List<int>();
		if (hotkey.Modifiers.HasFlag(ModifierKeys.Control))
		{
			list.Add(CtrlCodes[0]);
		}
		if (hotkey.Modifiers.HasFlag(ModifierKeys.Alt))
		{
			list.Add(AltCodes[0]);
		}
		if (hotkey.Modifiers.HasFlag(ModifierKeys.Shift))
		{
			list.Add(ShiftCodes[0]);
		}
		if (hotkey.Modifiers.HasFlag(ModifierKeys.Windows))
		{
			list.Add(WinCodes[0]);
		}
		if (hotkey.Key != Key.None)
		{
			list.Add(KeyInterop.VirtualKeyFromKey(hotkey.Key));
		}
		string value = string.Join(",", list.Select((int i) => i.ToString(CultureInfo.InvariantCulture)));
		writer.WriteValue(value);
	}

	public override Hotkey ReadJson(JsonReader reader, Type objectType, Hotkey existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.String)
		{
			return Hotkey.Empty;
		}
		ModifierKeys modifierKeys = ModifierKeys.None;
		Key key = Key.None;
		string[] array = ((string)reader.Value).Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			if (int.TryParse(array[i].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
			{
				if (ShiftCodes.Contains(result))
				{
					modifierKeys |= ModifierKeys.Shift;
				}
				else if (CtrlCodes.Contains(result))
				{
					modifierKeys |= ModifierKeys.Control;
				}
				else if (AltCodes.Contains(result))
				{
					modifierKeys |= ModifierKeys.Alt;
				}
				else if (WinCodes.Contains(result))
				{
					modifierKeys |= ModifierKeys.Windows;
				}
				else
				{
					key = KeyInterop.KeyFromVirtualKey(result);
				}
			}
		}
		Hotkey hotkey = new Hotkey(modifierKeys, key);
		if (!Hotkey.IsValid(hotkey))
		{
			return Hotkey.Empty;
		}
		return hotkey;
	}
}
