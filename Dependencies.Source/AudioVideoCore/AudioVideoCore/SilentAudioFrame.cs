using System;

namespace AudioVideoCore;

public class SilentAudioFrame : AudioFrame
{
	public SilentAudioFrame(AudioFormat format, int samples, FrameTimestamp timestamp)
		: base(format, samples, timestamp)
	{
		if (Format.SampleFormat != SampleFormat.U8 && Format.SampleFormat != SampleFormat.U8P)
		{
			return;
		}
		foreach (Memory<byte> datum in Data)
		{
			datum.Span.Fill(128);
		}
	}
}
