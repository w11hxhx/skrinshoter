using System;
using FFmpeg.AutoGen;

namespace FFmpeg;

public class FFmpegException : Exception
{
	public int FFmpegErrorCode { get; }

	public FFmpegException(string message, int ffmpegErrorCode)
		: base(message + ": " + GetErrorMessage(ffmpegErrorCode))
	{
		FFmpegErrorCode = ffmpegErrorCode;
	}

	private unsafe static string GetErrorMessage(int errorCode)
	{
		fixed (byte* ptr = &(new byte[64])[0])
		{
			ffmpeg.av_strerror(errorCode, ptr, 64uL);
			return new string((sbyte*)ptr);
		}
	}
}
