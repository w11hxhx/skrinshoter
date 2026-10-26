using System;
using System.IO;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

internal sealed class AppDataTrialStateStore : ITrialStateStore
{
	private static readonly string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataLink", "Cache", "4F7");

	private static readonly string stateFilePath = Path.Combine(directoryPath, "rou" + ProductResources.TrialState_AppData_Suffix);

	public byte[] CryptoKey => new byte[16]
	{
		167, 28, 243, 72, 157, 34, 110, 177, 95, 200,
		52, 122, 226, 25, 99, 212
	};

	public bool TryRead(out string rawValue)
	{
		rawValue = null;
		try
		{
			if (!File.Exists(stateFilePath))
			{
				return false;
			}
			string text = File.ReadAllText(stateFilePath);
			if (string.IsNullOrWhiteSpace(text))
			{
				return false;
			}
			rawValue = text;
			return true;
		}
		catch
		{
			return false;
		}
	}

	public void Write(string rawValue)
	{
		string text = stateFilePath + ".tmp";
		try
		{
			Directory.CreateDirectory(directoryPath);
			File.WriteAllText(text, rawValue);
			if (File.Exists(stateFilePath))
			{
				File.Replace(text, stateFilePath, null, ignoreMetadataErrors: true);
			}
			else
			{
				File.Move(text, stateFilePath);
			}
		}
		catch
		{
		}
		finally
		{
			try
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			catch
			{
			}
		}
	}
}
