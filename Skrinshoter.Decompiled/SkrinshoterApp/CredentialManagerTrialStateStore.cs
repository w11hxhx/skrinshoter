using System;
using System.Runtime.InteropServices;
using System.Text;
using SkrinshoterApp.Properties;
using Windows.Win32;
using Windows.Win32.Security.Credentials;

namespace SkrinshoterApp;

internal sealed class CredentialManagerTrialStateStore : ITrialStateStore
{
	private static readonly string TargetName = "Sync.9J4D2" + ProductResources.TrialState_CredentialManager_Suffix;

	public byte[] CryptoKey => new byte[16]
	{
		75, 217, 32, 143, 113, 60, 165, 232, 22, 98,
		207, 57, 148, 125, 42, 182
	};

	public unsafe bool TryRead(out string rawValue)
	{
		rawValue = null;
		CREDENTIALW* ptr = null;
		try
		{
			fixed (char* targetName = TargetName)
			{
				if (!PInvoke.CredRead(targetName, CRED_TYPE.CRED_TYPE_GENERIC, 0u, &ptr))
				{
					return false;
				}
			}
			if (ptr == null || ptr->CredentialBlob == null)
			{
				return false;
			}
			int num = checked((int)ptr->CredentialBlobSize);
			if (num <= 0)
			{
				return false;
			}
			byte[] array = new byte[num];
			Marshal.Copy((IntPtr)ptr->CredentialBlob, array, 0, num);
			string text = Encoding.UTF8.GetString(array);
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
		finally
		{
			if (ptr != null)
			{
				PInvoke.CredFree(ptr);
			}
		}
	}

	public unsafe void Write(string rawValue)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(rawValue);
		try
		{
			fixed (char* targetName = TargetName)
			{
				fixed (byte* credentialBlob = bytes)
				{
					CREDENTIALW cREDENTIALW = new CREDENTIALW
					{
						Type = CRED_TYPE.CRED_TYPE_GENERIC,
						TargetName = targetName,
						CredentialBlobSize = checked((uint)bytes.Length),
						CredentialBlob = credentialBlob,
						Persist = CRED_PERSIST.CRED_PERSIST_LOCAL_MACHINE
					};
					PInvoke.CredWrite(&cREDENTIALW, 0u);
				}
			}
		}
		catch
		{
		}
	}
}
