using System;
using AudioVideoCore;
using Windows.Win32.Media.Audio;
using Windows.Win32.Media.KernelStreaming;
using Windows.Win32.Media.Multimedia;

namespace AudioCapture;

internal static class WaveFormatConverter
{
	public unsafe static AudioFormat ToAudioFormat(WAVEFORMATEX* waveFormat)
	{
		if (waveFormat == null)
		{
			throw new ArgumentNullException("waveFormat");
		}
		int nSamplesPerSec = (int)waveFormat->nSamplesPerSec;
		int nChannels = waveFormat->nChannels;
		int wBitsPerSample = waveFormat->wBitsPerSample;
		ushort wFormatTag = waveFormat->wFormatTag;
		bool flag;
		bool flag2;
		if (wFormatTag == 65534)
		{
			Guid subFormat = ((WAVEFORMATEXTENSIBLE*)waveFormat)->SubFormat;
			flag = subFormat == typeof(KSDATAFORMAT_SUBTYPE_PCM).GUID;
			flag2 = subFormat == typeof(KSDATAFORMAT_SUBTYPE_IEEE_FLOAT).GUID;
		}
		else
		{
			flag = wFormatTag == 1;
			flag2 = wFormatTag == 3;
		}
		SampleFormat sampleFormat = SampleFormat.None;
		if (flag)
		{
			switch (wBitsPerSample)
			{
			case 8:
				sampleFormat = SampleFormat.U8;
				break;
			case 16:
				sampleFormat = SampleFormat.S16;
				break;
			case 32:
				sampleFormat = SampleFormat.S32;
				break;
			case 64:
				sampleFormat = SampleFormat.S64;
				break;
			}
		}
		else if (flag2)
		{
			switch (wBitsPerSample)
			{
			case 32:
				sampleFormat = SampleFormat.Float;
				break;
			case 64:
				sampleFormat = SampleFormat.Double;
				break;
			}
		}
		if (sampleFormat == SampleFormat.None)
		{
			throw new InvalidOperationException($"Cannot convert wave format (tag={wFormatTag}, bits={wBitsPerSample}) to {typeof(AudioFormat)}.");
		}
		return new AudioFormat(nSamplesPerSec, nChannels, sampleFormat);
	}
}
