using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SkrinshoterApp.Properties;
using Windows.Win32;
using Windows.Win32.Storage.FileSystem;

namespace SkrinshoterApp;

public sealed class SettingsStorage
{
	private readonly SettingsNormalizer settingsNormalizer;

	private readonly ILogger logger;

	private readonly string settingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProductResources.AppFolderName, "settings.json");

	private readonly object lockObject = new object();

	private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
	{
		Converters = new List<JsonConverter>(2)
		{
			new ColorJsonConverter(),
			new HotkeyJsonConverter()
		}
	};

	public SettingsStorage(SettingsNormalizer settingsNormalizer, ILogger<SettingsStorage> logger)
	{
		this.settingsNormalizer = settingsNormalizer;
		this.logger = logger;
		JsonSerializerSettings jsonSerializerSettings = serializerSettings;
		jsonSerializerSettings.Error = (EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>)Delegate.Combine(jsonSerializerSettings.Error, (EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>)((object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) =>
		{
			logger.LogWarning(args.ErrorContext.Error, "Invalid value for setting '{Path}'. Using default.", args.ErrorContext.Path);
			args.ErrorContext.Handled = true;
		}));
	}

	public void Save(Settings settings)
	{
		if (AppDesignMode.Enabled)
		{
			return;
		}
		string directoryName = Path.GetDirectoryName(settingsFilePath);
		string text = settingsFilePath + ".tmp";
		try
		{
			string s = Serialize(settings);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			lock (lockObject)
			{
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using (FileStream fileStream = File.Create(text, 4096, FileOptions.WriteThrough))
				{
					fileStream.Write(bytes, 0, bytes.Length);
					fileStream.Flush(flushToDisk: true);
				}
				if (!PInvoke.MoveFileEx(text, settingsFilePath, MOVE_FILE_FLAGS.MOVEFILE_REPLACE_EXISTING | MOVE_FILE_FLAGS.MOVEFILE_WRITE_THROUGH))
				{
					throw new WinApiException("MoveFileEx()", new Win32Exception());
				}
			}
			logger.LogInformation("Saved settings to file");
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to save settings to file");
			lock (lockObject)
			{
				if (File.Exists(text))
				{
					try
					{
						File.Delete(text);
						return;
					}
					catch
					{
						return;
					}
				}
			}
		}
	}

	public Settings Load()
	{
		Settings settings;
		try
		{
			string json = (File.Exists(settingsFilePath) ? File.ReadAllText(settingsFilePath) : "{}");
			settings = Deserialize(json);
			if (settings == null)
			{
				throw new JsonSerializationException("Settings is null.");
			}
			settingsNormalizer.Normalize(settings);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to load settings from file. Reset to defaults.");
			settings = new Settings();
		}
		Save(settings);
		return settings;
	}

	public string Serialize(Settings settings)
	{
		return JsonConvert.SerializeObject(settings, Formatting.Indented, serializerSettings);
	}

	public Settings Deserialize(string json)
	{
		return JsonConvert.DeserializeObject<Settings>(json, serializerSettings);
	}
}
