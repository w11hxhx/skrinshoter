using System;

namespace AudioVideoCore;

public sealed class WebRtcVad
{
	private static readonly int[] validFrameLengths = new int[3] { 80, 160, 240 };

	private readonly VadCore core = new VadCore();

	public void Reset()
	{
		core.Init();
	}

	public void SetMode(int mode)
	{
		if (core.SetMode(mode) != 0)
		{
			throw new ArgumentOutOfRangeException("mode", mode, "Valid VAD modes are 0 to 3.");
		}
	}

	public bool ProcessFrame(ReadOnlySpan<short> frame)
	{
		if (Array.IndexOf(validFrameLengths, frame.Length) < 0)
		{
			throw new ArgumentException($"Invalid frame length: {frame.Length}. Valid lengths are 80, 160 and 240 samples.", "frame");
		}
		return core.CalcVad8khz(frame) > 0;
	}
}
