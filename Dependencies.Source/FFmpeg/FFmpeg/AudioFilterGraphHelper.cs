using System.Collections.Generic;
using AudioVideoCore;
using Common;
using FFmpeg.AutoGen;

namespace FFmpeg;

internal static class AudioFilterGraphHelper
{
	public unsafe static AVFilterContext* CreateInputFilter(AVFilterGraph* graph, AudioFormat audioFormat)
	{
		AVFilter* ptr = ffmpeg.avfilter_get_by_name("abuffer");
		if (ptr == null)
		{
			throw new FFmpegException("avfilter_get_by_name(abuffer)", ffmpeg.AVERROR_FILTER_NOT_FOUND);
		}
		AVFilterContext* ptr2 = ffmpeg.avfilter_graph_alloc_filter(graph, ptr, "abuffer");
		if (ptr2 == null)
		{
			throw new FFmpegException("avfilter_graph_alloc_filter(abuffer)", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		string args = $"sample_rate={audioFormat.SampleRate}:sample_fmt={(int)audioFormat.SampleFormat.ToAVSampleFormat()}:channels={audioFormat.Channels}:time_base=1/{audioFormat.SampleRate}";
		int num = ffmpeg.avfilter_init_str(ptr2, args);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_init_str(abuffer)", num);
		}
		return ptr2;
	}

	public unsafe static AVFilterContext* CreateOutputFilter(AVFilterGraph* graph)
	{
		AVFilter* ptr = ffmpeg.avfilter_get_by_name("abuffersink");
		if (ptr == null)
		{
			throw new FFmpegException("avfilter_get_by_name(abuffersink)", ffmpeg.AVERROR_FILTER_NOT_FOUND);
		}
		AVFilterContext* ptr2 = ffmpeg.avfilter_graph_alloc_filter(graph, ptr, "abuffersink");
		if (ptr2 == null)
		{
			throw new FFmpegException("avfilter_graph_alloc_filter(abuffersink)", ffmpeg.AVERROR(ffmpeg.ENOMEM));
		}
		int num = ffmpeg.avfilter_init_str(ptr2, null);
		if (num < 0)
		{
			throw new FFmpegException("avfilter_init_str(abuffersink)", num);
		}
		return ptr2;
	}

	public unsafe static void BuildFilterGraph(AVFilterGraph* graph, string filtersDescription, IReadOnlyList<KeyValuePair<string, UnmanagedPtr<AVFilterContext>>> graphInputs, AVFilterContext* graphOutput)
	{
		AVFilterInOut* ptr = ffmpeg.avfilter_inout_alloc();
		AVFilterInOut* ptr2 = null;
		try
		{
			if (ptr == null)
			{
				throw new FFmpegException("avfilter_inout_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
			}
			AVFilterInOut* ptr3 = null;
			foreach (KeyValuePair<string, UnmanagedPtr<AVFilterContext>> graphInput in graphInputs)
			{
				AVFilterInOut* ptr4 = ffmpeg.avfilter_inout_alloc();
				if (ptr4 == null)
				{
					throw new FFmpegException("avfilter_inout_alloc()", ffmpeg.AVERROR(ffmpeg.ENOMEM));
				}
				ptr4->name = ffmpeg.av_strdup(graphInput.Key);
				ptr4->filter_ctx = graphInput.Value.Ptr;
				if (ptr3 == null)
				{
					ptr2 = ptr4;
				}
				else
				{
					ptr3->next = ptr4;
				}
				ptr3 = ptr4;
			}
			ptr->name = ffmpeg.av_strdup("out");
			ptr->filter_ctx = graphOutput;
			int num = ffmpeg.avfilter_graph_parse_ptr(graph, filtersDescription, &ptr, &ptr2, null);
			if (num < 0)
			{
				throw new FFmpegException("avfilter_graph_parse_ptr()", num);
			}
		}
		finally
		{
			ffmpeg.avfilter_inout_free(&ptr);
			ffmpeg.avfilter_inout_free(&ptr2);
		}
	}
}
