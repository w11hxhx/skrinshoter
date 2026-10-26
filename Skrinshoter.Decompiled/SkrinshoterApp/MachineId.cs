using Microsoft.Win32;

namespace SkrinshoterApp;

internal class MachineId
{
	public static string Id
	{
		get
		{
			using RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
			using RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography");
			return registryKey2?.GetValue("MachineGuid")?.ToString();
		}
	}
}
