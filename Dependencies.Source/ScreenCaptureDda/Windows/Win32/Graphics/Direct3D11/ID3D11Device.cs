using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Direct3D11;

[Guid("DB6F6DDB-AC77-4E88-8253-819DF9BBF140")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ID3D11Device
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_BUFFER_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Buffer**, HRESULT> CreateBuffer_4;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE1D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture1D**, HRESULT> CreateTexture1D_5;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE2D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture2D**, HRESULT> CreateTexture2D_6;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE3D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture3D**, HRESULT> CreateTexture3D_7;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_SHADER_RESOURCE_VIEW_DESC*, ID3D11ShaderResourceView**, HRESULT> CreateShaderResourceView_8;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_UNORDERED_ACCESS_VIEW_DESC*, ID3D11UnorderedAccessView**, HRESULT> CreateUnorderedAccessView_9;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_RENDER_TARGET_VIEW_DESC*, ID3D11RenderTargetView**, HRESULT> CreateRenderTargetView_10;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_DEPTH_STENCIL_VIEW_DESC*, ID3D11DepthStencilView**, HRESULT> CreateDepthStencilView_11;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_INPUT_ELEMENT_DESC*, uint, void*, nuint, ID3D11InputLayout**, HRESULT> CreateInputLayout_12;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11VertexShader**, HRESULT> CreateVertexShader_13;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11GeometryShader**, HRESULT> CreateGeometryShader_14;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, ID3D11ClassLinkage*, ID3D11GeometryShader**, HRESULT> CreateGeometryShaderWithStreamOutput_15;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11PixelShader**, HRESULT> CreatePixelShader_16;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11HullShader**, HRESULT> CreateHullShader_17;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11DomainShader**, HRESULT> CreateDomainShader_18;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, void*, nuint, ID3D11ClassLinkage*, ID3D11ComputeShader**, HRESULT> CreateComputeShader_19;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11ClassLinkage**, HRESULT> CreateClassLinkage_20;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_BLEND_DESC*, ID3D11BlendState**, HRESULT> CreateBlendState_21;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_DEPTH_STENCIL_DESC*, ID3D11DepthStencilState**, HRESULT> CreateDepthStencilState_22;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_RASTERIZER_DESC*, ID3D11RasterizerState**, HRESULT> CreateRasterizerState_23;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_SAMPLER_DESC*, ID3D11SamplerState**, HRESULT> CreateSamplerState_24;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_QUERY_DESC*, ID3D11Query**, HRESULT> CreateQuery_25;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_QUERY_DESC*, ID3D11Predicate**, HRESULT> CreatePredicate_26;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_DESC*, ID3D11Counter**, HRESULT> CreateCounter_27;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint, ID3D11DeviceContext**, HRESULT> CreateDeferredContext_28;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, HANDLE, Guid*, void**, HRESULT> OpenSharedResource_29;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, DXGI_FORMAT, uint*, HRESULT> CheckFormatSupport_30;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, DXGI_FORMAT, uint, uint*, HRESULT> CheckMultisampleQualityLevels_31;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_INFO*, void> CheckCounterInfo_32;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_DESC*, D3D11_COUNTER_TYPE*, uint*, PSTR, uint*, PSTR, uint*, PSTR, uint*, HRESULT> CheckCounter_33;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_FEATURE, void*, uint, HRESULT> CheckFeatureSupport_34;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, uint*, void*, HRESULT> GetPrivateData_35;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, uint, void*, HRESULT> SetPrivateData_36;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_37;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, D3D_FEATURE_LEVEL> GetFeatureLevel_38;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint> GetCreationFlags_39;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, HRESULT> GetDeviceRemovedReason_40;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11DeviceContext**, void> GetImmediateContext_41;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint, HRESULT> SetExceptionMode_42;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Device*, uint> GetExceptionMode_43;
	}

	[ComImport]
	[Guid("DB6F6DDB-AC77-4E88-8253-819DF9BBF140")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT CreateBuffer(D3D11_BUFFER_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Buffer** ppBuffer);

		[PreserveSig]
		unsafe HRESULT CreateTexture1D(D3D11_TEXTURE1D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture1D** ppTexture1D);

		[PreserveSig]
		unsafe HRESULT CreateTexture2D(D3D11_TEXTURE2D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture2D** ppTexture2D);

		[PreserveSig]
		unsafe HRESULT CreateTexture3D(D3D11_TEXTURE3D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture3D** ppTexture3D);

		[PreserveSig]
		unsafe HRESULT CreateShaderResourceView(ID3D11Resource* pResource, [Optional] D3D11_SHADER_RESOURCE_VIEW_DESC* pDesc, [Optional] ID3D11ShaderResourceView** ppSRView);

		[PreserveSig]
		unsafe HRESULT CreateUnorderedAccessView(ID3D11Resource* pResource, [Optional] D3D11_UNORDERED_ACCESS_VIEW_DESC* pDesc, [Optional] ID3D11UnorderedAccessView** ppUAView);

		[PreserveSig]
		unsafe HRESULT CreateRenderTargetView(ID3D11Resource* pResource, [Optional] D3D11_RENDER_TARGET_VIEW_DESC* pDesc, [Optional] ID3D11RenderTargetView** ppRTView);

		[PreserveSig]
		unsafe HRESULT CreateDepthStencilView(ID3D11Resource* pResource, [Optional] D3D11_DEPTH_STENCIL_VIEW_DESC* pDesc, [Optional] ID3D11DepthStencilView** ppDepthStencilView);

		[PreserveSig]
		unsafe HRESULT CreateInputLayout(D3D11_INPUT_ELEMENT_DESC* pInputElementDescs, uint NumElements, void* pShaderBytecodeWithInputSignature, nuint BytecodeLength, [Optional] ID3D11InputLayout** ppInputLayout);

		[PreserveSig]
		unsafe HRESULT CreateVertexShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11VertexShader** ppVertexShader);

		[PreserveSig]
		unsafe HRESULT CreateGeometryShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11GeometryShader** ppGeometryShader);

		[PreserveSig]
		unsafe HRESULT CreateGeometryShaderWithStreamOutput(void* pShaderBytecode, nuint BytecodeLength, [Optional] D3D11_SO_DECLARATION_ENTRY* pSODeclaration, uint NumEntries, [Optional] uint* pBufferStrides, uint NumStrides, uint RasterizedStream, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11GeometryShader** ppGeometryShader);

		[PreserveSig]
		unsafe HRESULT CreatePixelShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11PixelShader** ppPixelShader);

		[PreserveSig]
		unsafe HRESULT CreateHullShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11HullShader** ppHullShader);

		[PreserveSig]
		unsafe HRESULT CreateDomainShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11DomainShader** ppDomainShader);

		[PreserveSig]
		unsafe HRESULT CreateComputeShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11ComputeShader** ppComputeShader);

		[PreserveSig]
		unsafe HRESULT CreateClassLinkage(ID3D11ClassLinkage** ppLinkage);

		[PreserveSig]
		unsafe HRESULT CreateBlendState(D3D11_BLEND_DESC* pBlendStateDesc, [Optional] ID3D11BlendState** ppBlendState);

		[PreserveSig]
		unsafe HRESULT CreateDepthStencilState(D3D11_DEPTH_STENCIL_DESC* pDepthStencilDesc, [Optional] ID3D11DepthStencilState** ppDepthStencilState);

		[PreserveSig]
		unsafe HRESULT CreateRasterizerState(D3D11_RASTERIZER_DESC* pRasterizerDesc, [Optional] ID3D11RasterizerState** ppRasterizerState);

		[PreserveSig]
		unsafe HRESULT CreateSamplerState(D3D11_SAMPLER_DESC* pSamplerDesc, [Optional] ID3D11SamplerState** ppSamplerState);

		[PreserveSig]
		unsafe HRESULT CreateQuery(D3D11_QUERY_DESC* pQueryDesc, [Optional] ID3D11Query** ppQuery);

		[PreserveSig]
		unsafe HRESULT CreatePredicate(D3D11_QUERY_DESC* pPredicateDesc, [Optional] ID3D11Predicate** ppPredicate);

		[PreserveSig]
		unsafe HRESULT CreateCounter(D3D11_COUNTER_DESC* pCounterDesc, [Optional] ID3D11Counter** ppCounter);

		[PreserveSig]
		unsafe HRESULT CreateDeferredContext(uint ContextFlags, [Optional] ID3D11DeviceContext** ppDeferredContext);

		[PreserveSig]
		unsafe HRESULT OpenSharedResource(HANDLE hResource, Guid* ReturnedInterface, [Optional] void** ppResource);

		[PreserveSig]
		unsafe HRESULT CheckFormatSupport(DXGI_FORMAT Format, uint* pFormatSupport);

		[PreserveSig]
		unsafe HRESULT CheckMultisampleQualityLevels(DXGI_FORMAT Format, uint SampleCount, uint* pNumQualityLevels);

		[PreserveSig]
		unsafe void CheckCounterInfo(D3D11_COUNTER_INFO* pCounterInfo);

		[PreserveSig]
		unsafe HRESULT CheckCounter(D3D11_COUNTER_DESC* pDesc, D3D11_COUNTER_TYPE* pType, uint* pActiveCounters, PSTR szName, [Optional] uint* pNameLength, PSTR szUnits, [Optional] uint* pUnitsLength, PSTR szDescription, [Optional] uint* pDescriptionLength);

		[PreserveSig]
		unsafe HRESULT CheckFeatureSupport(D3D11_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize);

		[PreserveSig]
		unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData);

		[PreserveSig]
		unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData);

		[PreserveSig]
		unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData);

		[PreserveSig]
		D3D_FEATURE_LEVEL GetFeatureLevel();

		[PreserveSig]
		uint GetCreationFlags();

		[PreserveSig]
		HRESULT GetDeviceRemovedReason();

		[PreserveSig]
		unsafe void GetImmediateContext(ID3D11DeviceContext** ppImmediateContext);

		[PreserveSig]
		HRESULT SetExceptionMode(uint RaiseFlags);

		[PreserveSig]
		uint GetExceptionMode();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3681512923u, 44151, 20104, 130, 83, 129, 157, 249, 187, 241, 64);

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT QueryInterface(in Guid riid, out void* ppvObject)
	{
		fixed (void** ppvObject2 = &ppvObject)
		{
			fixed (Guid* riid2 = &riid)
			{
				return QueryInterface(riid2, ppvObject2);
			}
		}
	}

	public unsafe HRESULT QueryInterface(Guid* riid, void** ppvObject)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, void**, HRESULT>)(*lpVtbl))((ID3D11Device*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint>)lpVtbl[1])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint>)lpVtbl[2])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateBuffer(in D3D11_BUFFER_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Buffer** ppBuffer)
	{
		fixed (D3D11_BUFFER_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return CreateBuffer(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppBuffer);
		}
	}

	public unsafe HRESULT CreateBuffer(D3D11_BUFFER_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Buffer** ppBuffer)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_BUFFER_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Buffer**, HRESULT>)lpVtbl[3])((ID3D11Device*)Unsafe.AsPointer(ref this), pDesc, pInitialData, ppBuffer);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateTexture1D(in D3D11_TEXTURE1D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture1D** ppTexture1D)
	{
		fixed (D3D11_TEXTURE1D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return CreateTexture1D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture1D);
		}
	}

	public unsafe HRESULT CreateTexture1D(D3D11_TEXTURE1D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture1D** ppTexture1D)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE1D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture1D**, HRESULT>)lpVtbl[4])((ID3D11Device*)Unsafe.AsPointer(ref this), pDesc, pInitialData, ppTexture1D);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateTexture2D(in D3D11_TEXTURE2D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture2D** ppTexture2D)
	{
		fixed (D3D11_TEXTURE2D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return CreateTexture2D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture2D);
		}
	}

	public unsafe HRESULT CreateTexture2D(D3D11_TEXTURE2D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture2D** ppTexture2D)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE2D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture2D**, HRESULT>)lpVtbl[5])((ID3D11Device*)Unsafe.AsPointer(ref this), pDesc, pInitialData, ppTexture2D);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateTexture3D(in D3D11_TEXTURE3D_DESC pDesc, D3D11_SUBRESOURCE_DATA? pInitialData, ID3D11Texture3D** ppTexture3D)
	{
		fixed (D3D11_TEXTURE3D_DESC* pDesc2 = &pDesc)
		{
			D3D11_SUBRESOURCE_DATA valueOrDefault = pInitialData.GetValueOrDefault();
			return CreateTexture3D(pDesc2, pInitialData.HasValue ? (&valueOrDefault) : null, ppTexture3D);
		}
	}

	public unsafe HRESULT CreateTexture3D(D3D11_TEXTURE3D_DESC* pDesc, [Optional] D3D11_SUBRESOURCE_DATA* pInitialData, [Optional] ID3D11Texture3D** ppTexture3D)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_TEXTURE3D_DESC*, D3D11_SUBRESOURCE_DATA*, ID3D11Texture3D**, HRESULT>)lpVtbl[6])((ID3D11Device*)Unsafe.AsPointer(ref this), pDesc, pInitialData, ppTexture3D);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateShaderResourceView(ID3D11Resource* pResource, D3D11_SHADER_RESOURCE_VIEW_DESC? pDesc, ID3D11ShaderResourceView** ppSRView)
	{
		D3D11_SHADER_RESOURCE_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return CreateShaderResourceView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppSRView);
	}

	public unsafe HRESULT CreateShaderResourceView(ID3D11Resource* pResource, [Optional] D3D11_SHADER_RESOURCE_VIEW_DESC* pDesc, [Optional] ID3D11ShaderResourceView** ppSRView)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_SHADER_RESOURCE_VIEW_DESC*, ID3D11ShaderResourceView**, HRESULT>)lpVtbl[7])((ID3D11Device*)Unsafe.AsPointer(ref this), pResource, pDesc, ppSRView);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateUnorderedAccessView(ID3D11Resource* pResource, D3D11_UNORDERED_ACCESS_VIEW_DESC? pDesc, ID3D11UnorderedAccessView** ppUAView)
	{
		D3D11_UNORDERED_ACCESS_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return CreateUnorderedAccessView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppUAView);
	}

	public unsafe HRESULT CreateUnorderedAccessView(ID3D11Resource* pResource, [Optional] D3D11_UNORDERED_ACCESS_VIEW_DESC* pDesc, [Optional] ID3D11UnorderedAccessView** ppUAView)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_UNORDERED_ACCESS_VIEW_DESC*, ID3D11UnorderedAccessView**, HRESULT>)lpVtbl[8])((ID3D11Device*)Unsafe.AsPointer(ref this), pResource, pDesc, ppUAView);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateRenderTargetView(ID3D11Resource* pResource, D3D11_RENDER_TARGET_VIEW_DESC? pDesc, ID3D11RenderTargetView** ppRTView)
	{
		D3D11_RENDER_TARGET_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return CreateRenderTargetView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppRTView);
	}

	public unsafe HRESULT CreateRenderTargetView(ID3D11Resource* pResource, [Optional] D3D11_RENDER_TARGET_VIEW_DESC* pDesc, [Optional] ID3D11RenderTargetView** ppRTView)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_RENDER_TARGET_VIEW_DESC*, ID3D11RenderTargetView**, HRESULT>)lpVtbl[9])((ID3D11Device*)Unsafe.AsPointer(ref this), pResource, pDesc, ppRTView);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateDepthStencilView(ID3D11Resource* pResource, D3D11_DEPTH_STENCIL_VIEW_DESC? pDesc, ID3D11DepthStencilView** ppDepthStencilView)
	{
		D3D11_DEPTH_STENCIL_VIEW_DESC valueOrDefault = pDesc.GetValueOrDefault();
		return CreateDepthStencilView(pResource, pDesc.HasValue ? (&valueOrDefault) : null, ppDepthStencilView);
	}

	public unsafe HRESULT CreateDepthStencilView(ID3D11Resource* pResource, [Optional] D3D11_DEPTH_STENCIL_VIEW_DESC* pDesc, [Optional] ID3D11DepthStencilView** ppDepthStencilView)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11Resource*, D3D11_DEPTH_STENCIL_VIEW_DESC*, ID3D11DepthStencilView**, HRESULT>)lpVtbl[10])((ID3D11Device*)Unsafe.AsPointer(ref this), pResource, pDesc, ppDepthStencilView);
	}

	internal unsafe HRESULT CreateInputLayout(ReadOnlySpan<D3D11_INPUT_ELEMENT_DESC> pInputElementDescs, void* pShaderBytecodeWithInputSignature, nuint BytecodeLength, ID3D11InputLayout** ppInputLayout)
	{
		fixed (D3D11_INPUT_ELEMENT_DESC* pInputElementDescs2 = pInputElementDescs)
		{
			return CreateInputLayout(pInputElementDescs2, (uint)pInputElementDescs.Length, pShaderBytecodeWithInputSignature, BytecodeLength, ppInputLayout);
		}
	}

	public unsafe HRESULT CreateInputLayout(D3D11_INPUT_ELEMENT_DESC* pInputElementDescs, uint NumElements, void* pShaderBytecodeWithInputSignature, nuint BytecodeLength, [Optional] ID3D11InputLayout** ppInputLayout)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_INPUT_ELEMENT_DESC*, uint, void*, UIntPtr, ID3D11InputLayout**, HRESULT>)lpVtbl[11])((ID3D11Device*)Unsafe.AsPointer(ref this), pInputElementDescs, NumElements, pShaderBytecodeWithInputSignature, BytecodeLength, ppInputLayout);
	}

	public unsafe HRESULT CreateVertexShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11VertexShader** ppVertexShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11VertexShader**, HRESULT>)lpVtbl[12])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppVertexShader);
	}

	public unsafe HRESULT CreateGeometryShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11GeometryShader** ppGeometryShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11GeometryShader**, HRESULT>)lpVtbl[13])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppGeometryShader);
	}

	internal unsafe HRESULT CreateGeometryShaderWithStreamOutput(void* pShaderBytecode, nuint BytecodeLength, ReadOnlySpan<D3D11_SO_DECLARATION_ENTRY> pSODeclaration, ReadOnlySpan<uint> pBufferStrides, uint RasterizedStream, ID3D11ClassLinkage* pClassLinkage, ID3D11GeometryShader** ppGeometryShader)
	{
		fixed (uint* pBufferStrides2 = pBufferStrides)
		{
			fixed (D3D11_SO_DECLARATION_ENTRY* pSODeclaration2 = pSODeclaration)
			{
				return CreateGeometryShaderWithStreamOutput(pShaderBytecode, BytecodeLength, pSODeclaration2, (uint)pSODeclaration.Length, pBufferStrides2, (uint)pBufferStrides.Length, RasterizedStream, pClassLinkage, ppGeometryShader);
			}
		}
	}

	public unsafe HRESULT CreateGeometryShaderWithStreamOutput(void* pShaderBytecode, nuint BytecodeLength, [Optional] D3D11_SO_DECLARATION_ENTRY* pSODeclaration, uint NumEntries, [Optional] uint* pBufferStrides, uint NumStrides, uint RasterizedStream, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11GeometryShader** ppGeometryShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, ID3D11ClassLinkage*, ID3D11GeometryShader**, HRESULT>)lpVtbl[14])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pSODeclaration, NumEntries, pBufferStrides, NumStrides, RasterizedStream, pClassLinkage, ppGeometryShader);
	}

	public unsafe HRESULT CreatePixelShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11PixelShader** ppPixelShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11PixelShader**, HRESULT>)lpVtbl[15])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppPixelShader);
	}

	public unsafe HRESULT CreateHullShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11HullShader** ppHullShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11HullShader**, HRESULT>)lpVtbl[16])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppHullShader);
	}

	public unsafe HRESULT CreateDomainShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11DomainShader** ppDomainShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11DomainShader**, HRESULT>)lpVtbl[17])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppDomainShader);
	}

	public unsafe HRESULT CreateComputeShader(void* pShaderBytecode, nuint BytecodeLength, [Optional] ID3D11ClassLinkage* pClassLinkage, [Optional] ID3D11ComputeShader** ppComputeShader)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, void*, UIntPtr, ID3D11ClassLinkage*, ID3D11ComputeShader**, HRESULT>)lpVtbl[18])((ID3D11Device*)Unsafe.AsPointer(ref this), pShaderBytecode, BytecodeLength, pClassLinkage, ppComputeShader);
	}

	public unsafe HRESULT CreateClassLinkage(ID3D11ClassLinkage** ppLinkage)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11ClassLinkage**, HRESULT>)lpVtbl[19])((ID3D11Device*)Unsafe.AsPointer(ref this), ppLinkage);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateBlendState(in D3D11_BLEND_DESC pBlendStateDesc, ID3D11BlendState** ppBlendState)
	{
		fixed (D3D11_BLEND_DESC* pBlendStateDesc2 = &pBlendStateDesc)
		{
			return CreateBlendState(pBlendStateDesc2, ppBlendState);
		}
	}

	public unsafe HRESULT CreateBlendState(D3D11_BLEND_DESC* pBlendStateDesc, [Optional] ID3D11BlendState** ppBlendState)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_BLEND_DESC*, ID3D11BlendState**, HRESULT>)lpVtbl[20])((ID3D11Device*)Unsafe.AsPointer(ref this), pBlendStateDesc, ppBlendState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateDepthStencilState(in D3D11_DEPTH_STENCIL_DESC pDepthStencilDesc, ID3D11DepthStencilState** ppDepthStencilState)
	{
		fixed (D3D11_DEPTH_STENCIL_DESC* pDepthStencilDesc2 = &pDepthStencilDesc)
		{
			return CreateDepthStencilState(pDepthStencilDesc2, ppDepthStencilState);
		}
	}

	public unsafe HRESULT CreateDepthStencilState(D3D11_DEPTH_STENCIL_DESC* pDepthStencilDesc, [Optional] ID3D11DepthStencilState** ppDepthStencilState)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_DEPTH_STENCIL_DESC*, ID3D11DepthStencilState**, HRESULT>)lpVtbl[21])((ID3D11Device*)Unsafe.AsPointer(ref this), pDepthStencilDesc, ppDepthStencilState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateRasterizerState(in D3D11_RASTERIZER_DESC pRasterizerDesc, ID3D11RasterizerState** ppRasterizerState)
	{
		fixed (D3D11_RASTERIZER_DESC* pRasterizerDesc2 = &pRasterizerDesc)
		{
			return CreateRasterizerState(pRasterizerDesc2, ppRasterizerState);
		}
	}

	public unsafe HRESULT CreateRasterizerState(D3D11_RASTERIZER_DESC* pRasterizerDesc, [Optional] ID3D11RasterizerState** ppRasterizerState)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_RASTERIZER_DESC*, ID3D11RasterizerState**, HRESULT>)lpVtbl[22])((ID3D11Device*)Unsafe.AsPointer(ref this), pRasterizerDesc, ppRasterizerState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateSamplerState(in D3D11_SAMPLER_DESC pSamplerDesc, ID3D11SamplerState** ppSamplerState)
	{
		fixed (D3D11_SAMPLER_DESC* pSamplerDesc2 = &pSamplerDesc)
		{
			return CreateSamplerState(pSamplerDesc2, ppSamplerState);
		}
	}

	public unsafe HRESULT CreateSamplerState(D3D11_SAMPLER_DESC* pSamplerDesc, [Optional] ID3D11SamplerState** ppSamplerState)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_SAMPLER_DESC*, ID3D11SamplerState**, HRESULT>)lpVtbl[23])((ID3D11Device*)Unsafe.AsPointer(ref this), pSamplerDesc, ppSamplerState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateQuery(in D3D11_QUERY_DESC pQueryDesc, ID3D11Query** ppQuery)
	{
		fixed (D3D11_QUERY_DESC* pQueryDesc2 = &pQueryDesc)
		{
			return CreateQuery(pQueryDesc2, ppQuery);
		}
	}

	public unsafe HRESULT CreateQuery(D3D11_QUERY_DESC* pQueryDesc, [Optional] ID3D11Query** ppQuery)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_QUERY_DESC*, ID3D11Query**, HRESULT>)lpVtbl[24])((ID3D11Device*)Unsafe.AsPointer(ref this), pQueryDesc, ppQuery);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreatePredicate(in D3D11_QUERY_DESC pPredicateDesc, ID3D11Predicate** ppPredicate)
	{
		fixed (D3D11_QUERY_DESC* pPredicateDesc2 = &pPredicateDesc)
		{
			return CreatePredicate(pPredicateDesc2, ppPredicate);
		}
	}

	public unsafe HRESULT CreatePredicate(D3D11_QUERY_DESC* pPredicateDesc, [Optional] ID3D11Predicate** ppPredicate)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_QUERY_DESC*, ID3D11Predicate**, HRESULT>)lpVtbl[25])((ID3D11Device*)Unsafe.AsPointer(ref this), pPredicateDesc, ppPredicate);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateCounter(in D3D11_COUNTER_DESC pCounterDesc, ID3D11Counter** ppCounter)
	{
		fixed (D3D11_COUNTER_DESC* pCounterDesc2 = &pCounterDesc)
		{
			return CreateCounter(pCounterDesc2, ppCounter);
		}
	}

	public unsafe HRESULT CreateCounter(D3D11_COUNTER_DESC* pCounterDesc, [Optional] ID3D11Counter** ppCounter)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_DESC*, ID3D11Counter**, HRESULT>)lpVtbl[26])((ID3D11Device*)Unsafe.AsPointer(ref this), pCounterDesc, ppCounter);
	}

	public unsafe HRESULT CreateDeferredContext(uint ContextFlags, [Optional] ID3D11DeviceContext** ppDeferredContext)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint, ID3D11DeviceContext**, HRESULT>)lpVtbl[27])((ID3D11Device*)Unsafe.AsPointer(ref this), ContextFlags, ppDeferredContext);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OpenSharedResource(SafeHandle hResource, in Guid ReturnedInterface, void** ppResource)
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
					return OpenSharedResource(hResource2, returnedInterface, ppResource);
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

	public unsafe HRESULT OpenSharedResource(HANDLE hResource, Guid* ReturnedInterface, [Optional] void** ppResource)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, HANDLE, Guid*, void**, HRESULT>)lpVtbl[28])((ID3D11Device*)Unsafe.AsPointer(ref this), hResource, ReturnedInterface, ppResource);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CheckFormatSupport(DXGI_FORMAT Format, out uint pFormatSupport)
	{
		fixed (uint* pFormatSupport2 = &pFormatSupport)
		{
			return CheckFormatSupport(Format, pFormatSupport2);
		}
	}

	public unsafe HRESULT CheckFormatSupport(DXGI_FORMAT Format, uint* pFormatSupport)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, DXGI_FORMAT, uint*, HRESULT>)lpVtbl[29])((ID3D11Device*)Unsafe.AsPointer(ref this), Format, pFormatSupport);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CheckMultisampleQualityLevels(DXGI_FORMAT Format, uint SampleCount, out uint pNumQualityLevels)
	{
		fixed (uint* pNumQualityLevels2 = &pNumQualityLevels)
		{
			return CheckMultisampleQualityLevels(Format, SampleCount, pNumQualityLevels2);
		}
	}

	public unsafe HRESULT CheckMultisampleQualityLevels(DXGI_FORMAT Format, uint SampleCount, uint* pNumQualityLevels)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, DXGI_FORMAT, uint, uint*, HRESULT>)lpVtbl[30])((ID3D11Device*)Unsafe.AsPointer(ref this), Format, SampleCount, pNumQualityLevels);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void CheckCounterInfo(out D3D11_COUNTER_INFO pCounterInfo)
	{
		fixed (D3D11_COUNTER_INFO* pCounterInfo2 = &pCounterInfo)
		{
			CheckCounterInfo(pCounterInfo2);
		}
	}

	public unsafe void CheckCounterInfo(D3D11_COUNTER_INFO* pCounterInfo)
	{
		((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_INFO*, void>)lpVtbl[31])((ID3D11Device*)Unsafe.AsPointer(ref this), pCounterInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CheckCounter(in D3D11_COUNTER_DESC pDesc, out D3D11_COUNTER_TYPE pType, out uint pActiveCounters, PSTR szName, uint* pNameLength, PSTR szUnits, uint* pUnitsLength, PSTR szDescription, uint* pDescriptionLength)
	{
		fixed (uint* pActiveCounters2 = &pActiveCounters)
		{
			fixed (D3D11_COUNTER_TYPE* pType2 = &pType)
			{
				fixed (D3D11_COUNTER_DESC* pDesc2 = &pDesc)
				{
					return CheckCounter(pDesc2, pType2, pActiveCounters2, szName, pNameLength, szUnits, pUnitsLength, szDescription, pDescriptionLength);
				}
			}
		}
	}

	public unsafe HRESULT CheckCounter(D3D11_COUNTER_DESC* pDesc, D3D11_COUNTER_TYPE* pType, uint* pActiveCounters, PSTR szName, [Optional] uint* pNameLength, PSTR szUnits, [Optional] uint* pUnitsLength, PSTR szDescription, [Optional] uint* pDescriptionLength)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_COUNTER_DESC*, D3D11_COUNTER_TYPE*, uint*, PSTR, uint*, PSTR, uint*, PSTR, uint*, HRESULT>)lpVtbl[32])((ID3D11Device*)Unsafe.AsPointer(ref this), pDesc, pType, pActiveCounters, szName, pNameLength, szUnits, pUnitsLength, szDescription, pDescriptionLength);
	}

	public unsafe HRESULT CheckFeatureSupport(D3D11_FEATURE Feature, void* pFeatureSupportData, uint FeatureSupportDataSize)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D11_FEATURE, void*, uint, HRESULT>)lpVtbl[33])((ID3D11Device*)Unsafe.AsPointer(ref this), Feature, pFeatureSupportData, FeatureSupportDataSize);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetPrivateData(in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	public unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, uint*, void*, HRESULT>)lpVtbl[34])((ID3D11Device*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateData(in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return SetPrivateData(guid2, DataSize, pData);
		}
	}

	public unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, uint, void*, HRESULT>)lpVtbl[35])((ID3D11Device*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateDataInterface(in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return SetPrivateDataInterface(guid2, pData);
		}
	}

	public unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, Guid*, IUnknown*, HRESULT>)lpVtbl[36])((ID3D11Device*)Unsafe.AsPointer(ref this), guid, pData);
	}

	public unsafe D3D_FEATURE_LEVEL GetFeatureLevel()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, D3D_FEATURE_LEVEL>)lpVtbl[37])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint GetCreationFlags()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint>)lpVtbl[38])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT GetDeviceRemovedReason()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, HRESULT>)lpVtbl[39])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	public unsafe void GetImmediateContext(ID3D11DeviceContext** ppImmediateContext)
	{
		((delegate* unmanaged[Stdcall]<ID3D11Device*, ID3D11DeviceContext**, void>)lpVtbl[40])((ID3D11Device*)Unsafe.AsPointer(ref this), ppImmediateContext);
	}

	public unsafe HRESULT SetExceptionMode(uint RaiseFlags)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint, HRESULT>)lpVtbl[41])((ID3D11Device*)Unsafe.AsPointer(ref this), RaiseFlags);
	}

	public unsafe uint GetExceptionMode()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Device*, uint>)lpVtbl[42])((ID3D11Device*)Unsafe.AsPointer(ref this));
	}

	internal unsafe HRESULT QueryInterface<T>(out T* ppv) where T : unmanaged
	{
		Guid gUID = typeof(T).GUID;
		void* ptr = default;
		HRESULT result = QueryInterface(&gUID, &ptr);
		if (result.Succeeded)
		{
			ppv = (T*)ptr;
		}
		else
		{
			ppv = null;
		}
		return result;
	}
}
