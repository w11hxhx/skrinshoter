using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Direct3D11_ID3D11DeviceContext_Extensions
{
	internal unsafe static HRESULT GetPrivateData(this ID3D11DeviceContext.Interface @this, in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return @this.GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT SetPrivateData(this ID3D11DeviceContext.Interface @this, in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateData(guid2, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this ID3D11DeviceContext.Interface @this, in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateDataInterface(guid2, pData);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void IASetVertexBuffers(this ID3D11DeviceContext.Interface @this, uint StartSlot, ID3D11Buffer** ppVertexBuffers, ReadOnlySpan<uint> pStrides, ReadOnlySpan<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			fixed (uint* pStrides2 = pStrides)
			{
				if (pStrides.Length != pOffsets.Length)
				{
					throw new ArgumentException();
				}
				@this.IASetVertexBuffers(StartSlot, (uint)pOffsets.Length, ppVertexBuffers, pStrides2, pOffsets2);
			}
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void OMSetRenderTargetsAndUnorderedAccessViews(this ID3D11DeviceContext.Interface @this, uint NumRTVs, ID3D11RenderTargetView** ppRenderTargetViews, ID3D11DepthStencilView* pDepthStencilView, uint UAVStartSlot, ID3D11UnorderedAccessView** ppUnorderedAccessViews, ReadOnlySpan<uint> pUAVInitialCounts)
	{
		fixed (uint* pUAVInitialCounts2 = pUAVInitialCounts)
		{
			@this.OMSetRenderTargetsAndUnorderedAccessViews(NumRTVs, ppRenderTargetViews, pDepthStencilView, UAVStartSlot, (uint)pUAVInitialCounts.Length, ppUnorderedAccessViews, pUAVInitialCounts2);
		}
	}

	internal unsafe static void OMSetBlendState(this ID3D11DeviceContext.Interface @this, ID3D11BlendState* pBlendState, ReadOnlySpan<float> BlendFactor, uint SampleMask)
	{
		fixed (float* blendFactor = BlendFactor)
		{
			if (BlendFactor.Length < 4)
			{
				throw new ArgumentException();
			}
			@this.OMSetBlendState(pBlendState, blendFactor, SampleMask);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void SOSetTargets(this ID3D11DeviceContext.Interface @this, ID3D11Buffer** ppSOTargets, ReadOnlySpan<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			@this.SOSetTargets((uint)pOffsets.Length, ppSOTargets, pOffsets2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void RSSetViewports(this ID3D11DeviceContext.Interface @this, ReadOnlySpan<D3D11_VIEWPORT> pViewports)
	{
		fixed (D3D11_VIEWPORT* pViewports2 = pViewports)
		{
			@this.RSSetViewports((uint)pViewports.Length, pViewports2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void RSSetScissorRects(this ID3D11DeviceContext.Interface @this, ReadOnlySpan<RECT> pRects)
	{
		fixed (RECT* pRects2 = pRects)
		{
			@this.RSSetScissorRects((uint)pRects.Length, pRects2);
		}
	}

	internal unsafe static void CopySubresourceRegion(this ID3D11DeviceContext.Interface @this, ID3D11Resource* pDstResource, uint DstSubresource, uint DstX, uint DstY, uint DstZ, ID3D11Resource* pSrcResource, uint SrcSubresource, D3D11_BOX? pSrcBox)
	{
		D3D11_BOX valueOrDefault = pSrcBox.GetValueOrDefault();
		@this.CopySubresourceRegion(pDstResource, DstSubresource, DstX, DstY, DstZ, pSrcResource, SrcSubresource, pSrcBox.HasValue ? (&valueOrDefault) : null);
	}

	internal unsafe static void UpdateSubresource(this ID3D11DeviceContext.Interface @this, ID3D11Resource* pDstResource, uint DstSubresource, D3D11_BOX? pDstBox, void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch)
	{
		D3D11_BOX valueOrDefault = pDstBox.GetValueOrDefault();
		@this.UpdateSubresource(pDstResource, DstSubresource, pDstBox.HasValue ? (&valueOrDefault) : null, pSrcData, SrcRowPitch, SrcDepthPitch);
	}

	internal unsafe static void ClearRenderTargetView(this ID3D11DeviceContext.Interface @this, ID3D11RenderTargetView* pRenderTargetView, ReadOnlySpan<float> ColorRGBA)
	{
		fixed (float* colorRGBA = ColorRGBA)
		{
			if (ColorRGBA.Length < 4)
			{
				throw new ArgumentException();
			}
			@this.ClearRenderTargetView(pRenderTargetView, colorRGBA);
		}
	}

	internal unsafe static void ClearUnorderedAccessViewUint(this ID3D11DeviceContext.Interface @this, ID3D11UnorderedAccessView* pUnorderedAccessView, ReadOnlySpan<uint> Values)
	{
		fixed (uint* values = Values)
		{
			if (Values.Length < 4)
			{
				throw new ArgumentException();
			}
			@this.ClearUnorderedAccessViewUint(pUnorderedAccessView, values);
		}
	}

	internal unsafe static void ClearUnorderedAccessViewFloat(this ID3D11DeviceContext.Interface @this, ID3D11UnorderedAccessView* pUnorderedAccessView, ReadOnlySpan<float> Values)
	{
		fixed (float* values = Values)
		{
			if (Values.Length < 4)
			{
				throw new ArgumentException();
			}
			@this.ClearUnorderedAccessViewFloat(pUnorderedAccessView, values);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void CSSetUnorderedAccessViews(this ID3D11DeviceContext.Interface @this, uint StartSlot, ID3D11UnorderedAccessView** ppUnorderedAccessViews, ReadOnlySpan<uint> pUAVInitialCounts)
	{
		fixed (uint* pUAVInitialCounts2 = pUAVInitialCounts)
		{
			@this.CSSetUnorderedAccessViews(StartSlot, (uint)pUAVInitialCounts.Length, ppUnorderedAccessViews, pUAVInitialCounts2);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static void IAGetVertexBuffers(this ID3D11DeviceContext.Interface @this, uint StartSlot, ID3D11Buffer** ppVertexBuffers, Span<uint> pStrides, Span<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			fixed (uint* pStrides2 = pStrides)
			{
				if (pStrides.Length != pOffsets.Length)
				{
					throw new ArgumentException();
				}
				@this.IAGetVertexBuffers(StartSlot, (uint)pOffsets.Length, ppVertexBuffers, pStrides2, pOffsets2);
			}
		}
	}

	internal unsafe static void IAGetPrimitiveTopology(this ID3D11DeviceContext.Interface @this, out D3D_PRIMITIVE_TOPOLOGY pTopology)
	{
		fixed (D3D_PRIMITIVE_TOPOLOGY* pTopology2 = &pTopology)
		{
			@this.IAGetPrimitiveTopology(pTopology2);
		}
	}

	internal unsafe static void OMGetBlendState(this ID3D11DeviceContext.Interface @this, ID3D11BlendState** ppBlendState, Span<float> BlendFactor, uint* pSampleMask)
	{
		fixed (float* blendFactor = BlendFactor)
		{
			if (BlendFactor.Length < 4)
			{
				throw new ArgumentException();
			}
			@this.OMGetBlendState(ppBlendState, blendFactor, pSampleMask);
		}
	}

	internal unsafe static void RSGetViewports(this ID3D11DeviceContext.Interface @this, ref uint pNumViewports, Span<D3D11_VIEWPORT> pViewports)
	{
		fixed (D3D11_VIEWPORT* pViewports2 = pViewports)
		{
			fixed (uint* pNumViewports2 = &pNumViewports)
			{
				@this.RSGetViewports(pNumViewports2, pViewports2);
			}
		}
	}

	internal unsafe static void RSGetScissorRects(this ID3D11DeviceContext.Interface @this, ref uint pNumRects, Span<RECT> pRects)
	{
		fixed (RECT* pRects2 = pRects)
		{
			fixed (uint* pNumRects2 = &pNumRects)
			{
				@this.RSGetScissorRects(pNumRects2, pRects2);
			}
		}
	}
}
