using System;

namespace AudioVideoCore;

public interface IScreenCapturer : IDisposable
{
	bool GetFrame(RawImage image, TimeSpan timeout);
}
