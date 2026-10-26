using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SkrinshoterApp;

public sealed class TrialStateStorage
{
	private readonly ITrialStateStore[] stores = new ITrialStateStore[3]
	{
		new AppDataTrialStateStore(),
		new RegistryTrialStateStore(),
		new CredentialManagerTrialStateStore()
	};

	private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

	public TrialStateStorage()
	{
		JsonSerializerSettings jsonSerializerSettings = serializerSettings;
		jsonSerializerSettings.Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(jsonSerializerSettings.Error, (EventHandler<ErrorEventArgs>)((object sender, ErrorEventArgs args) =>
		{
			args.ErrorContext.Handled = true;
		}));
	}

	public TrialState Load()
	{
		TrialState selectedState = null;
		ITrialStateStore[] array = stores;
		foreach (ITrialStateStore store in array)
		{
			ReadState(store, ref selectedState);
		}
		if (selectedState == null)
		{
			selectedState = new TrialState();
		}
		Save(selectedState);
		return selectedState;
	}

	public void Save(TrialState state)
	{
		ITrialStateStore[] array = stores;
		foreach (ITrialStateStore trialStateStore in array)
		{
			try
			{
				string rawValue = Serialize(state, trialStateStore.CryptoKey);
				trialStateStore.Write(rawValue);
			}
			catch
			{
			}
		}
	}

	private void ReadState(ITrialStateStore store, ref TrialState selectedState)
	{
		if (!store.TryRead(out var rawValue))
		{
			return;
		}
		TrialState trialState;
		try
		{
			trialState = Deserialize(rawValue, store.CryptoKey);
		}
		catch
		{
			return;
		}
		if (trialState != null && trialState.Revision >= 0)
		{
			if (selectedState == null)
			{
				selectedState = trialState;
			}
			else if (trialState.Revision > selectedState.Revision)
			{
				selectedState = trialState;
			}
		}
	}

	private string Serialize(TrialState state, byte[] cryptoKey)
	{
		string text = JsonConvert.SerializeObject(state, Formatting.None, serializerSettings);
		return Convert.ToBase64String(new SkrAes128(cryptoKey, text).Encode());
	}

	private TrialState Deserialize(string rawValue, byte[] cryptoKey)
	{
		byte[] aes128UserData = Convert.FromBase64String(rawValue);
		return JsonConvert.DeserializeObject<TrialState>(new SkrAes128(cryptoKey, aes128UserData).Decode(), serializerSettings);
	}
}
