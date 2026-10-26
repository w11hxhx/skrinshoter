using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Media.MediaFoundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Media_MediaFoundation_IMF2DBuffer_Extensions
{
	internal unsafe static HRESULT Lock2D(this IMF2DBuffer.Interface @this, out byte* ppbScanline0, out int plPitch)
	{
		fixed (int* plPitch2 = &plPitch)
		{
			fixed (byte** ppbScanline1 = &ppbScanline0)
			{
				return @this.Lock2D(ppbScanline1, plPitch2);
			}
		}
	}

	internal unsafe static HRESULT GetScanline0AndPitch(this IMF2DBuffer.Interface @this, out byte* pbScanline0, out int plPitch)
	{
		fixed (int* plPitch2 = &plPitch)
		{
			fixed (byte** pbScanline1 = &pbScanline0)
			{
				return @this.GetScanline0AndPitch(pbScanline1, plPitch2);
			}
		}
	}

	internal unsafe static HRESULT IsContiguousFormat(this IMF2DBuffer.Interface @this, out BOOL pfIsContiguous)
	{
		fixed (BOOL* pfIsContiguous2 = &pfIsContiguous)
		{
			return @this.IsContiguousFormat(pfIsContiguous2);
		}
	}

	internal unsafe static HRESULT GetContiguousLength(this IMF2DBuffer.Interface @this, out uint pcbLength)
	{
		fixed (uint* pcbLength2 = &pcbLength)
		{
			return @this.GetContiguousLength(pcbLength2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT ContiguousCopyTo(this IMF2DBuffer.Interface @this, Span<byte> pbDestBuffer)
	{
		fixed (byte* pbDestBuffer2 = pbDestBuffer)
		{
			return @this.ContiguousCopyTo(pbDestBuffer2, (uint)pbDestBuffer.Length);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT ContiguousCopyFrom(this IMF2DBuffer.Interface @this, ReadOnlySpan<byte> pbSrcBuffer)
	{
		fixed (byte* pbSrcBuffer2 = pbSrcBuffer)
		{
			return @this.ContiguousCopyFrom(pbSrcBuffer2, (uint)pbSrcBuffer.Length);
		}
	}
}
