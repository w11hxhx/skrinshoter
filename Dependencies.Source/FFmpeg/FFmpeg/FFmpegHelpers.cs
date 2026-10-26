using System;
using System.Runtime.InteropServices;
using FFmpeg.AutoGen;
using Microsoft.Extensions.Logging;

namespace FFmpeg;

public static class FFmpegHelpers
{
	private static ILogger logger;

	public unsafe static void SetupLogging(ILogger logger)
	{
		FFmpegHelpers.logger = logger ?? throw new ArgumentNullException("logger");
		ffmpeg.av_log_set_callback((av_log_set_callback_callback)((void* p0, int level, string format, byte* vl) =>
		{
			if (level > ffmpeg.av_log_get_level())
			{
				return;
			}
			int num = 1024;
			byte* ptr = stackalloc byte[(int)(uint)num];
			int num2 = 1;
			ffmpeg.av_log_format_line(p0, level, format, vl, ptr, num, &num2);
			string message = Marshal.PtrToStringAnsi((IntPtr)ptr).TrimEnd();
			using (FFmpegHelpers.logger.BeginScope("{logPrefix}", "[ffmpeg] "))
			{
				FFmpegHelpers.logger.LogInformation(message);
			}
		}));
	}
}
