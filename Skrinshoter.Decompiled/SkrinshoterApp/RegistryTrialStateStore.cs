using Microsoft.Win32;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

internal sealed class RegistryTrialStateStore : ITrialStateStore
{
	private const string KeyPath = "Software\\ClrBridge\\Catalog\\6D2";

	private static readonly string ValueName = "p" + ProductResources.TrialState_Registry_Suffix;

	public byte[] CryptoKey => new byte[16]
	{
		225, 87, 172, 48, 107, 244, 152, 29, 115, 194,
		78, 133, 41, 218, 100, 15
	};

	public bool TryRead(out string rawValue)
	{
		rawValue = null;
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\ClrBridge\\Catalog\\6D2", writable: false);
			if (registryKey == null)
			{
				return false;
			}
			string text = registryKey.GetValue(ValueName) as string;
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
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\ClrBridge\\Catalog\\6D2", writable: true);
			registryKey?.SetValue(ValueName, rawValue, RegistryValueKind.String);
		}
		catch
		{
		}
	}
}
