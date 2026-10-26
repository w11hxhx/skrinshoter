using System;
using Common;

namespace AudioVideoCore;

public class VideoFrame : BaseDisposable
{
	public RawImage Image { get; }

	public FrameTimestamp Timestamp { get; set; }

	public VideoFrame(RawImage image, FrameTimestamp timestamp)
	{
		Image = image ?? throw new ArgumentNullException("image");
		Timestamp = timestamp;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Image.Dispose();
		}
	}
}
