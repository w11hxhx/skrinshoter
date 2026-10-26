using System;
using AudioVideoCore;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class VideoFrameExtensions
{
	public unsafe static IDisposable FillAVFrame(this VideoFrame videoFrame, AVFrame* avFrame)
	{
		if (videoFrame == null)
		{
			throw new ArgumentNullException("videoFrame");
		}
		IDisposable result = videoFrame.Image.FillAVFrame(avFrame);
		avFrame->pts = videoFrame.Timestamp.Value;
		avFrame->time_base = new AVRational
		{
			num = 1,
			den = (int)videoFrame.Timestamp.TimeBase
		};
		return result;
	}
}
