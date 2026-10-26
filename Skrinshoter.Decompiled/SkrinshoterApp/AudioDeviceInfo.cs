namespace SkrinshoterApp;

public class AudioDeviceInfo
{
	public string Id { get; }

	public string Name { get; }

	public string RealDeviceName { get; }

	public AudioDeviceInfo(string id, string name, string realDeviceName = null)
	{
		Id = id;
		Name = name;
		RealDeviceName = realDeviceName;
	}
}
