using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMFTransform_Extensions
{
	internal unsafe static HRESULT GetStreamLimits(this IMFTransform.Interface @this, out uint pdwInputMinimum, out uint pdwInputMaximum, out uint pdwOutputMinimum, out uint pdwOutputMaximum)
	{
		fixed (uint* pdwOutputMaximum2 = &pdwOutputMaximum)
		{
			fixed (uint* pdwOutputMinimum2 = &pdwOutputMinimum)
			{
				fixed (uint* pdwInputMaximum2 = &pdwInputMaximum)
				{
					fixed (uint* pdwInputMinimum2 = &pdwInputMinimum)
					{
						return @this.GetStreamLimits(pdwInputMinimum2, pdwInputMaximum2, pdwOutputMinimum2, pdwOutputMaximum2);
					}
				}
			}
		}
	}

	internal unsafe static HRESULT GetStreamCount(this IMFTransform.Interface @this, out uint pcInputStreams, out uint pcOutputStreams)
	{
		fixed (uint* pcOutputStreams2 = &pcOutputStreams)
		{
			fixed (uint* pcInputStreams2 = &pcInputStreams)
			{
				return @this.GetStreamCount(pcInputStreams2, pcOutputStreams2);
			}
		}
	}

	internal unsafe static HRESULT GetStreamIDs(this IMFTransform.Interface @this, Span<uint> pdwInputIDs, Span<uint> pdwOutputIDs)
	{
		fixed (uint* pdwOutputIDs2 = pdwOutputIDs)
		{
			fixed (uint* pdwInputIDs2 = pdwInputIDs)
			{
				return @this.GetStreamIDs((uint)pdwInputIDs.Length, pdwInputIDs2, (uint)pdwOutputIDs.Length, pdwOutputIDs2);
			}
		}
	}

	internal unsafe static HRESULT GetInputStreamInfo(this IMFTransform.Interface @this, uint dwInputStreamID, out MFT_INPUT_STREAM_INFO pStreamInfo)
	{
		fixed (MFT_INPUT_STREAM_INFO* pStreamInfo2 = &pStreamInfo)
		{
			return @this.GetInputStreamInfo(dwInputStreamID, pStreamInfo2);
		}
	}

	internal unsafe static HRESULT GetOutputStreamInfo(this IMFTransform.Interface @this, uint dwOutputStreamID, out MFT_OUTPUT_STREAM_INFO pStreamInfo)
	{
		fixed (MFT_OUTPUT_STREAM_INFO* pStreamInfo2 = &pStreamInfo)
		{
			return @this.GetOutputStreamInfo(dwOutputStreamID, pStreamInfo2);
		}
	}

	internal unsafe static HRESULT AddInputStreams(this IMFTransform.Interface @this, uint cStreams, in uint adwStreamIDs)
	{
		fixed (uint* adwStreamIDs2 = &adwStreamIDs)
		{
			return @this.AddInputStreams(cStreams, adwStreamIDs2);
		}
	}

	internal unsafe static HRESULT GetInputStatus(this IMFTransform.Interface @this, uint dwInputStreamID, out uint pdwFlags)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			return @this.GetInputStatus(dwInputStreamID, pdwFlags2);
		}
	}

	internal unsafe static HRESULT GetOutputStatus(this IMFTransform.Interface @this, out uint pdwFlags)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			return @this.GetOutputStatus(pdwFlags2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT ProcessOutput(this IMFTransform.Interface @this, uint dwFlags, ReadOnlySpan<MFT_OUTPUT_DATA_BUFFER> pOutputSamples, out uint pdwStatus)
	{
		fixed (uint* pdwStatus2 = &pdwStatus)
		{
			fixed (MFT_OUTPUT_DATA_BUFFER* pOutputSamples2 = pOutputSamples)
			{
				return @this.ProcessOutput(dwFlags, (uint)pOutputSamples.Length, pOutputSamples2, pdwStatus2);
			}
		}
	}
}
