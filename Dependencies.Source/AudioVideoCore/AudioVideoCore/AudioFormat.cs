using System;

namespace AudioVideoCore;

public class AudioFormat : IEquatable<AudioFormat>
{
	public int SampleRate { get; }

	public int Channels { get; }

	public SampleFormat SampleFormat { get; }

	public AudioFormat(int sampleRate, int channels, SampleFormat sampleFormat)
	{
		if (sampleRate <= 0)
		{
			throw new ArgumentOutOfRangeException("sampleRate");
		}
		if (channels <= 0)
		{
			throw new ArgumentOutOfRangeException("channels");
		}
		if (sampleFormat == SampleFormat.None)
		{
			throw new ArgumentException("Sample format must be specified.", "sampleFormat");
		}
		SampleRate = sampleRate;
		Channels = channels;
		SampleFormat = sampleFormat;
	}

	public bool Equals(AudioFormat other)
	{
		if ((object)other != null && SampleRate == other.SampleRate && Channels == other.Channels)
		{
			return SampleFormat == other.SampleFormat;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is AudioFormat other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return SampleRate.GetHashCode() ^ Channels.GetHashCode() ^ SampleFormat.GetHashCode();
	}

	public static bool operator ==(AudioFormat left, AudioFormat right)
	{
		return left?.Equals(right) ?? ((object)right == null);
	}

	public static bool operator !=(AudioFormat left, AudioFormat right)
	{
		return !(left == right);
	}

	public override string ToString()
	{
		return $"{SampleRate}Hz/{Channels}ch/{SampleFormat}";
	}
}
