using AudioVideoCore;
using FFmpeg.AutoGen;

namespace FFmpeg;

public class FFmpegVideoFrame : VideoFrame
{
	public unsafe FFmpegVideoFrame(AVFrame* frame)
		: base(new FFmpegRawImage(frame), GetTimestamp(frame))
	{
	}

	private unsafe static FrameTimestamp GetTimestamp(AVFrame* frame)
	{
		return new FrameTimestamp(frame->pts, frame->time_base.den);
	}
}
