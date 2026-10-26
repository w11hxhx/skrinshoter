using System;
using AudioVideoCore;
using Common;
using Windows.Win32;
using Windows.Win32.Media.MediaFoundation;

namespace CameraCapture;

internal class VideoMediaType
{
	public int Width { get; }

	public int Height { get; }

	public Guid Subtype { get; }

	public PixelFormat PixelFormat { get; }

	public string PixelFormatAsString
	{
		get
		{
			if (PixelFormat != PixelFormat.None)
			{
				return PixelFormat.ToString();
			}
			if (Subtype == PInvoke.MFVideoFormat_MJPG)
			{
				return "MJPG";
			}
			return Subtype.ToString();
		}
	}

	public int FrameRateNumerator { get; }

	public int FrameRateDenominator { get; }

	public int FrameRate
	{
		get
		{
			if (FrameRateDenominator != 0)
			{
				return (int)Math.Round((double)FrameRateNumerator / (double)FrameRateDenominator);
			}
			return 0;
		}
	}

	public unsafe VideoMediaType(ComPtr<IMFMediaType> mediaType)
	{
		mediaType.Ptr->GetUINT64(in PInvoke.MF_MT_FRAME_SIZE, out var punValue).ThrowOnFailure("IMFMediaType.GetUINT64(MF_MT_FRAME_SIZE)");
		Width = (int)(punValue >> 32);
		Height = (int)punValue;
		mediaType.Ptr->GetGUID(in PInvoke.MF_MT_SUBTYPE, out var pguidValue).ThrowOnFailure("IMFMediaType.GetGUID(MF_MT_SUBTYPE)");
		Subtype = pguidValue;
		PixelFormat = pguidValue.ToPixelFormat();
		mediaType.Ptr->GetUINT64(in PInvoke.MF_MT_FRAME_RATE, out var punValue2).ThrowOnFailure("IMFMediaType.GetUINT64(MF_MT_FRAME_RATE)");
		FrameRateNumerator = (int)(punValue2 >> 32);
		FrameRateDenominator = (int)punValue2;
	}

	public override string ToString()
	{
		return $"{Width}x{Height}/{PixelFormatAsString}@{FrameRate}";
	}
}
