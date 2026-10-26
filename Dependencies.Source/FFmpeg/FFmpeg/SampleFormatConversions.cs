using System;
using AudioVideoCore;
using FFmpeg.AutoGen;

namespace FFmpeg;

public static class SampleFormatConversions
{
	public static AVSampleFormat ToAVSampleFormat(this SampleFormat sampleFormat)
	{
		return sampleFormat switch
		{
			SampleFormat.None => AVSampleFormat.AV_SAMPLE_FMT_NONE, 
			SampleFormat.U8 => AVSampleFormat.AV_SAMPLE_FMT_U8, 
			SampleFormat.S16 => AVSampleFormat.AV_SAMPLE_FMT_S16, 
			SampleFormat.S32 => AVSampleFormat.AV_SAMPLE_FMT_S32, 
			SampleFormat.S64 => AVSampleFormat.AV_SAMPLE_FMT_S64, 
			SampleFormat.Float => AVSampleFormat.AV_SAMPLE_FMT_FLT, 
			SampleFormat.Double => AVSampleFormat.AV_SAMPLE_FMT_DBL, 
			SampleFormat.U8P => AVSampleFormat.AV_SAMPLE_FMT_U8P, 
			SampleFormat.S16P => AVSampleFormat.AV_SAMPLE_FMT_S16P, 
			SampleFormat.S32P => AVSampleFormat.AV_SAMPLE_FMT_S32P, 
			SampleFormat.S64P => AVSampleFormat.AV_SAMPLE_FMT_S64P, 
			SampleFormat.FloatP => AVSampleFormat.AV_SAMPLE_FMT_FLTP, 
			SampleFormat.DoubleP => AVSampleFormat.AV_SAMPLE_FMT_DBLP, 
			_ => throw new InvalidOperationException($"Cannot convert {typeof(SampleFormat)}.{sampleFormat} to {typeof(AVSampleFormat)}."), 
		};
	}

	public static SampleFormat ToSampleFormat(this AVSampleFormat sampleFormat)
	{
		return sampleFormat switch
		{
			AVSampleFormat.AV_SAMPLE_FMT_NONE => SampleFormat.None, 
			AVSampleFormat.AV_SAMPLE_FMT_U8 => SampleFormat.U8, 
			AVSampleFormat.AV_SAMPLE_FMT_S16 => SampleFormat.S16, 
			AVSampleFormat.AV_SAMPLE_FMT_S32 => SampleFormat.S32, 
			AVSampleFormat.AV_SAMPLE_FMT_S64 => SampleFormat.S64, 
			AVSampleFormat.AV_SAMPLE_FMT_FLT => SampleFormat.Float, 
			AVSampleFormat.AV_SAMPLE_FMT_DBL => SampleFormat.Double, 
			AVSampleFormat.AV_SAMPLE_FMT_U8P => SampleFormat.U8P, 
			AVSampleFormat.AV_SAMPLE_FMT_S16P => SampleFormat.S16P, 
			AVSampleFormat.AV_SAMPLE_FMT_S32P => SampleFormat.S32P, 
			AVSampleFormat.AV_SAMPLE_FMT_S64P => SampleFormat.S64P, 
			AVSampleFormat.AV_SAMPLE_FMT_FLTP => SampleFormat.FloatP, 
			AVSampleFormat.AV_SAMPLE_FMT_DBLP => SampleFormat.DoubleP, 
			_ => throw new InvalidOperationException($"Cannot convert {typeof(AVSampleFormat)}.{sampleFormat} to {typeof(SampleFormat)}."), 
		};
	}
}
