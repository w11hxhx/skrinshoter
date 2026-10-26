using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class Graphics_Direct3D11_ID3D11Device_Extensions
{
	internal unsafe static HRESULT CreateBuffer(this ID3D11Device.Interface @this, in D3D11_BUFFER_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Buffer** ppBuffer)
	{
		fixed (D3D11_BUFFER_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return @this.CreateBuffer(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppBuffer);
		}
	}

	internal unsafe static HRESULT CreateTexture1D(this ID3D11Device.Interface @this, in D3D11_TEXTURE1D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture1D** ppTexture1D)
	{
		fixed (D3D11_TEXTURE1D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return @this.CreateTexture1D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture1D);
		}
	}

	internal unsafe static HRESULT CreateTexture2D(this ID3D11Device.Interface @this, in D3D11_TEXTURE2D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture2D** ppTexture2D)
	{
		fixed (D3D11_TEXTURE2D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return @this.CreateTexture2D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture2D);
		}
	}

	internal unsafe static HRESULT CreateTexture3D(this ID3D11Device.Interface @this, in D3D11_TEXTURE3D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture3D** ppTexture3D)
	{
		fixed (D3D11_TEXTURE3D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return @this.CreateTexture3D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture3D);
		}
	}

	internal unsafe static HRESULT CreateShaderResourceView(this ID3D11Device.Interface @this, ID3D11Resource* pResource, D3D11_SHADER_RESOURCE_VIEW_DESC? pDesc, ID3D11ShaderResourceView** ppSRView)
	{
		D3D11_SHADER_RESOURCE_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return @this.CreateShaderResourceView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppSRView);
	}

	internal unsafe static HRESULT CreateUnorderedAccessView(this ID3D11Device.Interface @this, ID3D11Resource* pResource, D3D11_UNORDERED_ACCESS_VIEW_DESC? pDesc, ID3D11UnorderedAccessView** ppUAView)
	{
		D3D11_UNORDERED_ACCESS_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return @this.CreateUnorderedAccessView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppUAView);
	}

	internal unsafe static HRESULT CreateRenderTargetView(this ID3D11Device.Interface @this, ID3D11Resource* pResource, D3D11_RENDER_TARGET_VIEW_DESC? pDesc, ID3D11RenderTargetView** ppRTView)
	{
		D3D11_RENDER_TARGET_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return @this.CreateRenderTargetView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppRTView);
	}

	internal unsafe static HRESULT CreateDepthStencilView(this ID3D11Device.Interface @this, ID3D11Resource* pResource, D3D11_DEPTH_STENCIL_VIEW_DESC? pDesc, ID3D11DepthStencilView** ppDepthStencilView)
	{
		D3D11_DEPTH_STENCIL_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return @this.CreateDepthStencilView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppDepthStencilView);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CreateInputLayout(this ID3D11Device.Interface @this, ReadOnlySpan<D3D11_INPUT_ELEMENT_DESC> pInputElementDescs, void* pShaderBytecodeWithInputSignature, nuint BytecodeLength, ID3D11InputLayout** ppInputLayout)
	{
		fixed (D3D11_INPUT_ELEMENT_DESC* pInputElementDescs2 = pInputElementDescs)
		{
			return @this.CreateInputLayout(pInputElementDescs2, (uint)pInputElementDescs.Length, pShaderBytecodeWithInputSignature, BytecodeLength, ppInputLayout);
		}
	}

	internal unsafe static HRESULT CreateGeometryShaderWithStreamOutput(this ID3D11Device.Interface @this, void* pShaderBytecode, nuint BytecodeLength, ReadOnlySpan<D3D11_SO_DECLARATION_ENTRY> pSODeclaration, ReadOnlySpan<uint> pBufferStrides, uint RasterizedStream, ID3D11ClassLinkage* pClassLinkage, ID3D11GeometryShader** ppGeometryShader)
	{
		fixed (uint* pBufferStrides2 = pBufferStrides)
		{
			fixed (D3D11_SO_DECLARATION_ENTRY* pSODeclaration2 = pSODeclaration)
			{
				return @this.CreateGeometryShaderWithStreamOutput(pShaderBytecode, BytecodeLength, pSODeclaration2, (uint)pSODeclaration.Length, pBufferStrides2, (uint)pBufferStrides.Length, RasterizedStream, pClassLinkage, ppGeometryShader);
			}
		}
	}

	internal unsafe static HRESULT CreateBlendState(this ID3D11Device.Interface @this, in D3D11_BLEND_DESC pBlendStateDesc, ID3D11BlendState** ppBlendState)
	{
		fixed (D3D11_BLEND_DESC* pBlendStateDesc2 = &pBlendStateDesc)
		{
			return @this.CreateBlendState(pBlendStateDesc2, ppBlendState);
		}
	}

	internal unsafe static HRESULT CreateDepthStencilState(this ID3D11Device.Interface @this, in D3D11_DEPTH_STENCIL_DESC pDepthStencilDesc, ID3D11DepthStencilState** ppDepthStencilState)
	{
		fixed (D3D11_DEPTH_STENCIL_DESC* pDepthStencilDesc2 = &pDepthStencilDesc)
		{
			return @this.CreateDepthStencilState(pDepthStencilDesc2, ppDepthStencilState);
		}
	}

	internal unsafe static HRESULT CreateRasterizerState(this ID3D11Device.Interface @this, in D3D11_RASTERIZER_DESC pRasterizerDesc, ID3D11RasterizerState** ppRasterizerState)
	{
		fixed (D3D11_RASTERIZER_DESC* pRasterizerDesc2 = &pRasterizerDesc)
		{
			return @this.CreateRasterizerState(pRasterizerDesc2, ppRasterizerState);
		}
	}

	internal unsafe static HRESULT CreateSamplerState(this ID3D11Device.Interface @this, in D3D11_SAMPLER_DESC pSamplerDesc, ID3D11SamplerState** ppSamplerState)
	{
		fixed (D3D11_SAMPLER_DESC* pSamplerDesc2 = &pSamplerDesc)
		{
			return @this.CreateSamplerState(pSamplerDesc2, ppSamplerState);
		}
	}

	internal unsafe static HRESULT CreateQuery(this ID3D11Device.Interface @this, in D3D11_QUERY_DESC pQueryDesc, ID3D11Query** ppQuery)
	{
		fixed (D3D11_QUERY_DESC* pQueryDesc2 = &pQueryDesc)
		{
			return @this.CreateQuery(pQueryDesc2, ppQuery);
		}
	}

	internal unsafe static HRESULT CreatePredicate(this ID3D11Device.Interface @this, in D3D11_QUERY_DESC pPredicateDesc, ID3D11Predicate** ppPredicate)
	{
		fixed (D3D11_QUERY_DESC* pPredicateDesc2 = &pPredicateDesc)
		{
			return @this.CreatePredicate(pPredicateDesc2, ppPredicate);
		}
	}

	internal unsafe static HRESULT CreateCounter(this ID3D11Device.Interface @this, in D3D11_COUNTER_DESC pCounterDesc, ID3D11Counter** ppCounter)
	{
		fixed (D3D11_COUNTER_DESC* pCounterDesc2 = &pCounterDesc)
		{
			return @this.CreateCounter(pCounterDesc2, ppCounter);
		}
	}

	internal unsafe static HRESULT OpenSharedResource(this ID3D11Device.Interface @this, SafeHandle hResource, in Guid ReturnedInterface, void** ppResource)
	{
		bool success = false;
		try
		{
			fixed (Guid* returnedInterface = &ReturnedInterface)
			{
				if (hResource != null)
				{
					hResource.DangerousAddRef(ref success);
					HANDLE hResource2 = (HANDLE)hResource.DangerousGetHandle();
					return @this.OpenSharedResource(hResource2, returnedInterface, ppResource);
				}
				throw new ArgumentNullException("hResource");
			}
		}
		finally
		{
			if (success)
			{
				hResource.DangerousRelease();
			}
		}
	}

	internal unsafe static HRESULT CheckFormatSupport(this ID3D11Device.Interface @this, DXGI_FORMAT Format, out uint pFormatSupport)
	{
		fixed (uint* pFormatSupport2 = &pFormatSupport)
		{
			return @this.CheckFormatSupport(Format, pFormatSupport2);
		}
	}

	internal unsafe static HRESULT CheckMultisampleQualityLevels(this ID3D11Device.Interface @this, DXGI_FORMAT Format, uint SampleCount, out uint pNumQualityLevels)
	{
		fixed (uint* pNumQualityLevels2 = &pNumQualityLevels)
		{
			return @this.CheckMultisampleQualityLevels(Format, SampleCount, pNumQualityLevels2);
		}
	}

	internal unsafe static void CheckCounterInfo(this ID3D11Device.Interface @this, out D3D11_COUNTER_INFO pCounterInfo)
	{
		fixed (D3D11_COUNTER_INFO* pCounterInfo2 = &pCounterInfo)
		{
			@this.CheckCounterInfo(pCounterInfo2);
		}
	}

	internal unsafe static HRESULT CheckCounter(this ID3D11Device.Interface @this, in D3D11_COUNTER_DESC pDesc, out D3D11_COUNTER_TYPE pType, out uint pActiveCounters, PSTR szName, uint* pNameLength, PSTR szUnits, uint* pUnitsLength, PSTR szDescription, uint* pDescriptionLength)
	{
		fixed (uint* pActiveCounters2 = &pActiveCounters)
		{
			fixed (D3D11_COUNTER_TYPE* pType2 = &pType)
			{
				fixed (D3D11_COUNTER_DESC* pDesc2 = &pDesc)
				{
					return @this.CheckCounter(pDesc2, pType2, pActiveCounters2, szName, pNameLength, szUnits, pUnitsLength, szDescription, pDescriptionLength);
				}
			}
		}
	}

	internal unsafe static HRESULT GetPrivateData(this ID3D11Device.Interface @this, in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return @this.GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	internal unsafe static HRESULT SetPrivateData(this ID3D11Device.Interface @this, in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateData(guid2, DataSize, pData);
		}
	}

	internal unsafe static HRESULT SetPrivateDataInterface(this ID3D11Device.Interface @this, in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return @this.SetPrivateDataInterface(guid2, pData);
		}
	}
}
