namespace CameraCapture;

public class CameraDeviceDescription
{
	public string Id { get; }

	public string Name { get; }

	public CameraDeviceDescription(string id, string name)
	{
		Id = id;
		Name = name;
	}
}
