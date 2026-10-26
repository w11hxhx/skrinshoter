using System;
using AudioVideoCore;

namespace AudioCapture;

public interface IAudioFrameObserver
{
	void OnFrameCaptured(ReadOnlySpan<byte> data, AudioFormat format);
}
