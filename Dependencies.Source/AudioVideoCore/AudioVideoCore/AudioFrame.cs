using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AudioVideoCore;

public class AudioFrame : BaseDisposable
{
	public AudioFormat Format { get; }

	public IReadOnlyList<Memory<byte>> Data { get; }

	public int Samples { get; }

	public FrameTimestamp Timestamp { get; set; }

	public AudioFrame(AudioFormat format, IReadOnlyList<Memory<byte>> data, FrameTimestamp timestamp)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		int num = ((!format.SampleFormat.IsPlanar()) ? 1 : format.Channels);
		if (data.Count != num)
		{
			throw new ArgumentException($"Invalid number of data planes for audio format {format}, expected: {num}, actual: {data.Count}.", "data");
		}
		if (data.Any((Memory<byte> x) => x.Length != data[0].Length))
		{
			throw new ArgumentException("All data planes must have the same size.", "data");
		}
		int bytesPerSample = format.SampleFormat.GetBytesPerSample();
		if (data[0].Length % bytesPerSample != 0)
		{
			throw new ArgumentException("Data size must be a multiple of the sample size.", "data");
		}
		int num2 = data[0].Length / bytesPerSample;
		if (format.SampleFormat.IsPacked())
		{
			if (num2 % format.Channels != 0)
			{
				throw new ArgumentException("Data size must be a multiple of the sample size * number of channels.", "data");
			}
			num2 /= format.Channels;
		}
		Format = format;
		Data = data;
		Samples = num2;
		Timestamp = timestamp;
	}

	public AudioFrame(AudioFormat format, Memory<byte> data, FrameTimestamp timestamp)
		: this(format, new Memory<byte>[1] { data }, timestamp)
	{
	}

	public AudioFrame(AudioFormat format, int samples, FrameTimestamp timestamp)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (samples < 0)
		{
			throw new ArgumentOutOfRangeException("samples");
		}
		List<Memory<byte>> list = new List<Memory<byte>>();
		int bytesPerSample = format.SampleFormat.GetBytesPerSample();
		if (format.SampleFormat.IsPacked())
		{
			list.Add(new byte[samples * bytesPerSample * format.Channels]);
		}
		else
		{
			for (int i = 0; i < format.Channels; i++)
			{
				list.Add(new byte[samples * bytesPerSample]);
			}
		}
		Format = format;
		Data = list;
		Samples = samples;
		Timestamp = timestamp;
	}
}
