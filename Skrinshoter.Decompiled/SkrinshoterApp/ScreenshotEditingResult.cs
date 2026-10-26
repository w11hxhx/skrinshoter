using AudioVideoCore;
using Common;

namespace SkrinshoterApp;

public class ScreenshotEditingResult : BaseDisposable
{
	public ScreenshotEditingAction Action { get; }

	public RawImage Image { get; }

	public string Path { get; }

	public ScreenshotEditingResult(ScreenshotEditingAction action, RawImage image, string path = null)
	{
		Action = action;
		Image = image;
		Path = path;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Image?.Dispose();
		}
	}
}
