namespace SkrinshoterApp;

internal interface ITrialStateStore
{
	byte[] CryptoKey { get; }

	bool TryRead(out string rawValue);

	void Write(string rawValue);
}
