using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Direct3D11;

[Guid("C0BFA96C-E089-44FB-8EAF-26F8796190DA")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ID3D11DeviceContext
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Device**, void> GetDevice_4;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, uint*, void*, HRESULT> GetPrivateData_5;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, uint, void*, HRESULT> SetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_7;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> VSSetConstantBuffers_8;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> PSSetShaderResources_9;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11PixelShader*, ID3D11ClassInstance**, uint, void> PSSetShader_10;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> PSSetSamplers_11;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11VertexShader*, ID3D11ClassInstance**, uint, void> VSSetShader_12;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, int, void> DrawIndexed_13;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, void> Draw_14;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, D3D11_MAP, uint, D3D11_MAPPED_SUBRESOURCE*, HRESULT> Map_15;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, void> Unmap_16;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> PSSetConstantBuffers_17;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11InputLayout*, void> IASetInputLayout_18;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, uint*, uint*, void> IASetVertexBuffers_19;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, DXGI_FORMAT, uint, void> IASetIndexBuffer_20;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, int, uint, void> DrawIndexedInstanced_21;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, uint, void> DrawInstanced_22;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> GSSetConstantBuffers_23;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11GeometryShader*, ID3D11ClassInstance**, uint, void> GSSetShader_24;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D_PRIMITIVE_TOPOLOGY, void> IASetPrimitiveTopology_25;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> VSSetShaderResources_26;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> VSSetSamplers_27;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void> Begin_28;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void> End_29;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void*, uint, uint, HRESULT> GetData_30;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Predicate*, BOOL, void> SetPredication_31;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> GSSetShaderResources_32;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> GSSetSamplers_33;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView*, void> OMSetRenderTargets_34;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView*, uint, uint, ID3D11UnorderedAccessView**, uint*, void> OMSetRenderTargetsAndUnorderedAccessViews_35;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11BlendState*, float*, uint, void> OMSetBlendState_36;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilState*, uint, void> OMSetDepthStencilState_37;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11Buffer**, uint*, void> SOSetTargets_38;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void> DrawAuto_39;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void> DrawIndexedInstancedIndirect_40;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void> DrawInstancedIndirect_41;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, void> Dispatch_42;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void> DispatchIndirect_43;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RasterizerState*, void> RSSetState_44;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, D3D11_VIEWPORT*, void> RSSetViewports_45;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, RECT*, void> RSSetScissorRects_46;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, uint, uint, uint, ID3D11Resource*, uint, D3D11_BOX*, void> CopySubresourceRegion_47;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, ID3D11Resource*, void> CopyResource_48;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, D3D11_BOX*, void*, uint, uint, void> UpdateSubresource_49;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, ID3D11UnorderedAccessView*, void> CopyStructureCount_50;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RenderTargetView*, float*, void> ClearRenderTargetView_51;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11UnorderedAccessView*, uint*, void> ClearUnorderedAccessViewUint_52;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11UnorderedAccessView*, float*, void> ClearUnorderedAccessViewFloat_53;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilView*, uint, float, byte, void> ClearDepthStencilView_54;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ShaderResourceView*, void> GenerateMips_55;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, float, void> SetResourceMinLOD_56;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, float> GetResourceMinLOD_57;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, ID3D11Resource*, uint, DXGI_FORMAT, void> ResolveSubresource_58;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11CommandList*, BOOL, void> ExecuteCommandList_59;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> HSSetShaderResources_60;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11HullShader*, ID3D11ClassInstance**, uint, void> HSSetShader_61;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> HSSetSamplers_62;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> HSSetConstantBuffers_63;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> DSSetShaderResources_64;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DomainShader*, ID3D11ClassInstance**, uint, void> DSSetShader_65;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> DSSetSamplers_66;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> DSSetConstantBuffers_67;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> CSSetShaderResources_68;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11UnorderedAccessView**, uint*, void> CSSetUnorderedAccessViews_69;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ComputeShader*, ID3D11ClassInstance**, uint, void> CSSetShader_70;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> CSSetSamplers_71;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> CSSetConstantBuffers_72;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> VSGetConstantBuffers_73;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> PSGetShaderResources_74;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11PixelShader**, ID3D11ClassInstance**, uint*, void> PSGetShader_75;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> PSGetSamplers_76;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11VertexShader**, ID3D11ClassInstance**, uint*, void> VSGetShader_77;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> PSGetConstantBuffers_78;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11InputLayout**, void> IAGetInputLayout_79;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, uint*, uint*, void> IAGetVertexBuffers_80;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer**, DXGI_FORMAT*, uint*, void> IAGetIndexBuffer_81;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> GSGetConstantBuffers_82;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11GeometryShader**, ID3D11ClassInstance**, uint*, void> GSGetShader_83;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D_PRIMITIVE_TOPOLOGY*, void> IAGetPrimitiveTopology_84;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> VSGetShaderResources_85;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> VSGetSamplers_86;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Predicate**, BOOL*, void> GetPredication_87;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> GSGetShaderResources_88;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> GSGetSamplers_89;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView**, void> OMGetRenderTargets_90;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView**, uint, uint, ID3D11UnorderedAccessView**, void> OMGetRenderTargetsAndUnorderedAccessViews_91;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11BlendState**, float*, uint*, void> OMGetBlendState_92;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilState**, uint*, void> OMGetDepthStencilState_93;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11Buffer**, void> SOGetTargets_94;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RasterizerState**, void> RSGetState_95;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint*, D3D11_VIEWPORT*, void> RSGetViewports_96;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint*, RECT*, void> RSGetScissorRects_97;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> HSGetShaderResources_98;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11HullShader**, ID3D11ClassInstance**, uint*, void> HSGetShader_99;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> HSGetSamplers_100;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> HSGetConstantBuffers_101;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> DSGetShaderResources_102;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DomainShader**, ID3D11ClassInstance**, uint*, void> DSGetShader_103;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> DSGetSamplers_104;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> DSGetConstantBuffers_105;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void> CSGetShaderResources_106;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11UnorderedAccessView**, void> CSGetUnorderedAccessViews_107;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ComputeShader**, ID3D11ClassInstance**, uint*, void> CSGetShader_108;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void> CSGetSamplers_109;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void> CSGetConstantBuffers_110;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void> ClearState_111;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void> Flush_112;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D11_DEVICE_CONTEXT_TYPE> GetType_113;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint> GetContextFlags_114;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, BOOL, ID3D11CommandList**, HRESULT> FinishCommandList_115;
	}

	[ComImport]
	[Guid("C0BFA96C-E089-44FB-8EAF-26F8796190DA")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : ID3D11DeviceChild.Interface
	{
		[PreserveSig]
		new unsafe void GetDevice(ID3D11Device** ppDevice);

		[PreserveSig]
		new unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData);

		[PreserveSig]
		unsafe void VSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void PSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void PSSetShader([Optional] ID3D11PixelShader* pPixelShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		unsafe void PSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void VSSetShader([Optional] ID3D11VertexShader* pVertexShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		void DrawIndexed(uint IndexCount, uint StartIndexLocation, int BaseVertexLocation);

		[PreserveSig]
		void Draw(uint VertexCount, uint StartVertexLocation);

		[PreserveSig]
		unsafe HRESULT Map(ID3D11Resource* pResource, uint Subresource, D3D11_MAP MapType, uint MapFlags, [Optional] D3D11_MAPPED_SUBRESOURCE* pMappedResource);

		[PreserveSig]
		unsafe void Unmap(ID3D11Resource* pResource, uint Subresource);

		[PreserveSig]
		unsafe void PSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void IASetInputLayout([Optional] ID3D11InputLayout* pInputLayout);

		[PreserveSig]
		unsafe void IASetVertexBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppVertexBuffers, [Optional] uint* pStrides, [Optional] uint* pOffsets);

		[PreserveSig]
		unsafe void IASetIndexBuffer([Optional] ID3D11Buffer* pIndexBuffer, DXGI_FORMAT Format, uint Offset);

		[PreserveSig]
		void DrawIndexedInstanced(uint IndexCountPerInstance, uint InstanceCount, uint StartIndexLocation, int BaseVertexLocation, uint StartInstanceLocation);

		[PreserveSig]
		void DrawInstanced(uint VertexCountPerInstance, uint InstanceCount, uint StartVertexLocation, uint StartInstanceLocation);

		[PreserveSig]
		unsafe void GSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void GSSetShader([Optional] ID3D11GeometryShader* pShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		void IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY Topology);

		[PreserveSig]
		unsafe void VSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void VSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void Begin(ID3D11Asynchronous* pAsync);

		[PreserveSig]
		unsafe void End(ID3D11Asynchronous* pAsync);

		[PreserveSig]
		unsafe HRESULT GetData(ID3D11Asynchronous* pAsync, [Optional] void* pData, uint DataSize, uint GetDataFlags);

		[PreserveSig]
		unsafe void SetPredication([Optional] ID3D11Predicate* pPredicate, BOOL PredicateValue);

		[PreserveSig]
		unsafe void GSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void GSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void OMSetRenderTargets(uint NumViews, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView* pDepthStencilView);

		[PreserveSig]
		unsafe void OMSetRenderTargetsAndUnorderedAccessViews(uint NumRTVs, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView* pDepthStencilView, uint UAVStartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews, [Optional] uint* pUAVInitialCounts);

		[PreserveSig]
		unsafe void OMSetBlendState([Optional] ID3D11BlendState* pBlendState, [Optional] float* BlendFactor, uint SampleMask);

		[PreserveSig]
		unsafe void OMSetDepthStencilState([Optional] ID3D11DepthStencilState* pDepthStencilState, uint StencilRef);

		[PreserveSig]
		unsafe void SOSetTargets(uint NumBuffers, [Optional] ID3D11Buffer** ppSOTargets, [Optional] uint* pOffsets);

		[PreserveSig]
		void DrawAuto();

		[PreserveSig]
		unsafe void DrawIndexedInstancedIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs);

		[PreserveSig]
		unsafe void DrawInstancedIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs);

		[PreserveSig]
		void Dispatch(uint ThreadGroupCountX, uint ThreadGroupCountY, uint ThreadGroupCountZ);

		[PreserveSig]
		unsafe void DispatchIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs);

		[PreserveSig]
		unsafe void RSSetState([Optional] ID3D11RasterizerState* pRasterizerState);

		[PreserveSig]
		unsafe void RSSetViewports(uint NumViewports, [Optional] D3D11_VIEWPORT* pViewports);

		[PreserveSig]
		unsafe void RSSetScissorRects(uint NumRects, [Optional] RECT* pRects);

		[PreserveSig]
		unsafe void CopySubresourceRegion(ID3D11Resource* pDstResource, uint DstSubresource, uint DstX, uint DstY, uint DstZ, ID3D11Resource* pSrcResource, uint SrcSubresource, [Optional] D3D11_BOX* pSrcBox);

		[PreserveSig]
		unsafe void CopyResource(ID3D11Resource* pDstResource, ID3D11Resource* pSrcResource);

		[PreserveSig]
		unsafe void UpdateSubresource(ID3D11Resource* pDstResource, uint DstSubresource, [Optional] D3D11_BOX* pDstBox, void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch);

		[PreserveSig]
		unsafe void CopyStructureCount(ID3D11Buffer* pDstBuffer, uint DstAlignedByteOffset, ID3D11UnorderedAccessView* pSrcView);

		[PreserveSig]
		unsafe void ClearRenderTargetView(ID3D11RenderTargetView* pRenderTargetView, float* ColorRGBA);

		[PreserveSig]
		unsafe void ClearUnorderedAccessViewUint(ID3D11UnorderedAccessView* pUnorderedAccessView, uint* Values);

		[PreserveSig]
		unsafe void ClearUnorderedAccessViewFloat(ID3D11UnorderedAccessView* pUnorderedAccessView, float* Values);

		[PreserveSig]
		unsafe void ClearDepthStencilView(ID3D11DepthStencilView* pDepthStencilView, uint ClearFlags, float Depth, byte Stencil);

		[PreserveSig]
		unsafe void GenerateMips(ID3D11ShaderResourceView* pShaderResourceView);

		[PreserveSig]
		unsafe void SetResourceMinLOD(ID3D11Resource* pResource, float MinLOD);

		[PreserveSig]
		unsafe float GetResourceMinLOD(ID3D11Resource* pResource);

		[PreserveSig]
		unsafe void ResolveSubresource(ID3D11Resource* pDstResource, uint DstSubresource, ID3D11Resource* pSrcResource, uint SrcSubresource, DXGI_FORMAT Format);

		[PreserveSig]
		unsafe void ExecuteCommandList(ID3D11CommandList* pCommandList, BOOL RestoreContextState);

		[PreserveSig]
		unsafe void HSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void HSSetShader([Optional] ID3D11HullShader* pHullShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		unsafe void HSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void HSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void DSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void DSSetShader([Optional] ID3D11DomainShader* pDomainShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		unsafe void DSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void DSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void CSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void CSSetUnorderedAccessViews(uint StartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews, [Optional] uint* pUAVInitialCounts);

		[PreserveSig]
		unsafe void CSSetShader([Optional] ID3D11ComputeShader* pComputeShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances);

		[PreserveSig]
		unsafe void CSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void CSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void VSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void PSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void PSGetShader(ID3D11PixelShader** ppPixelShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void PSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void VSGetShader(ID3D11VertexShader** ppVertexShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void PSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void IAGetInputLayout(ID3D11InputLayout** ppInputLayout);

		[PreserveSig]
		unsafe void IAGetVertexBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppVertexBuffers, [Optional] uint* pStrides, [Optional] uint* pOffsets);

		[PreserveSig]
		unsafe void IAGetIndexBuffer([Optional] ID3D11Buffer** pIndexBuffer, [Optional] DXGI_FORMAT* Format, [Optional] uint* Offset);

		[PreserveSig]
		unsafe void GSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void GSGetShader(ID3D11GeometryShader** ppGeometryShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void IAGetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY* pTopology);

		[PreserveSig]
		unsafe void VSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void VSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void GetPredication([Optional] ID3D11Predicate** ppPredicate, [Optional] BOOL* pPredicateValue);

		[PreserveSig]
		unsafe void GSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void GSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void OMGetRenderTargets(uint NumViews, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView** ppDepthStencilView);

		[PreserveSig]
		unsafe void OMGetRenderTargetsAndUnorderedAccessViews(uint NumRTVs, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView** ppDepthStencilView, uint UAVStartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews);

		[PreserveSig]
		unsafe void OMGetBlendState([Optional] ID3D11BlendState** ppBlendState, [Optional] float* BlendFactor, [Optional] uint* pSampleMask);

		[PreserveSig]
		unsafe void OMGetDepthStencilState([Optional] ID3D11DepthStencilState** ppDepthStencilState, [Optional] uint* pStencilRef);

		[PreserveSig]
		unsafe void SOGetTargets(uint NumBuffers, [Optional] ID3D11Buffer** ppSOTargets);

		[PreserveSig]
		unsafe void RSGetState(ID3D11RasterizerState** ppRasterizerState);

		[PreserveSig]
		unsafe void RSGetViewports(uint* pNumViewports, [Optional] D3D11_VIEWPORT* pViewports);

		[PreserveSig]
		unsafe void RSGetScissorRects(uint* pNumRects, [Optional] RECT* pRects);

		[PreserveSig]
		unsafe void HSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void HSGetShader(ID3D11HullShader** ppHullShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void HSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void HSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void DSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void DSGetShader(ID3D11DomainShader** ppDomainShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void DSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void DSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		unsafe void CSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews);

		[PreserveSig]
		unsafe void CSGetUnorderedAccessViews(uint StartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews);

		[PreserveSig]
		unsafe void CSGetShader(ID3D11ComputeShader** ppComputeShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances);

		[PreserveSig]
		unsafe void CSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers);

		[PreserveSig]
		unsafe void CSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers);

		[PreserveSig]
		void ClearState();

		[PreserveSig]
		void Flush();

		[PreserveSig]
		new D3D11_DEVICE_CONTEXT_TYPE GetType();

		[PreserveSig]
		uint GetContextFlags();

		[PreserveSig]
		unsafe HRESULT FinishCommandList(BOOL RestoreDeferredContextState, [Optional] ID3D11CommandList** ppCommandList);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3233786220u, 57481, 17659, 142, 175, 38, 248, 121, 97, 144, 218);

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
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, void**, HRESULT>)(*lpVtbl))((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint>)lpVtbl[1])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint>)lpVtbl[2])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe void GetDevice(ID3D11Device** ppDevice)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Device**, void>)lpVtbl[3])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppDevice);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, uint*, void*, HRESULT>)lpVtbl[4])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, uint, void*, HRESULT>)lpVtbl[5])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, Guid*, IUnknown*, HRESULT>)lpVtbl[6])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), guid, pData);
	}

	public unsafe void VSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[7])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void PSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[8])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void PSSetShader([Optional] ID3D11PixelShader* pPixelShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11PixelShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[9])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pPixelShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void PSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[10])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void VSSetShader([Optional] ID3D11VertexShader* pVertexShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11VertexShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[11])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pVertexShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void DrawIndexed(uint IndexCount, uint StartIndexLocation, int BaseVertexLocation)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, int, void>)lpVtbl[12])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), IndexCount, StartIndexLocation, BaseVertexLocation);
	}

	public unsafe void Draw(uint VertexCount, uint StartVertexLocation)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, void>)lpVtbl[13])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), VertexCount, StartVertexLocation);
	}

	public unsafe HRESULT Map(ID3D11Resource* pResource, uint Subresource, D3D11_MAP MapType, uint MapFlags, [Optional] D3D11_MAPPED_SUBRESOURCE* pMappedResource)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, D3D11_MAP, uint, D3D11_MAPPED_SUBRESOURCE*, HRESULT>)lpVtbl[14])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pResource, Subresource, MapType, MapFlags, pMappedResource);
	}

	public unsafe void Unmap(ID3D11Resource* pResource, uint Subresource)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, void>)lpVtbl[15])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pResource, Subresource);
	}

	public unsafe void PSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[16])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void IASetInputLayout([Optional] ID3D11InputLayout* pInputLayout)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11InputLayout*, void>)lpVtbl[17])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pInputLayout);
	}

	internal unsafe void IASetVertexBuffers(uint StartSlot, ID3D11Buffer** ppVertexBuffers, ReadOnlySpan<uint> pStrides, ReadOnlySpan<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			fixed (uint* pStrides2 = pStrides)
			{
				if (pStrides.Length != pOffsets.Length)
				{
					throw new ArgumentException();
				}
				IASetVertexBuffers(StartSlot, (uint)pOffsets.Length, ppVertexBuffers, pStrides2, pOffsets2);
			}
		}
	}

	public unsafe void IASetVertexBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppVertexBuffers, [Optional] uint* pStrides, [Optional] uint* pOffsets)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, uint*, uint*, void>)lpVtbl[18])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppVertexBuffers, pStrides, pOffsets);
	}

	public unsafe void IASetIndexBuffer([Optional] ID3D11Buffer* pIndexBuffer, DXGI_FORMAT Format, uint Offset)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, DXGI_FORMAT, uint, void>)lpVtbl[19])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pIndexBuffer, Format, Offset);
	}

	public unsafe void DrawIndexedInstanced(uint IndexCountPerInstance, uint InstanceCount, uint StartIndexLocation, int BaseVertexLocation, uint StartInstanceLocation)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, int, uint, void>)lpVtbl[20])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), IndexCountPerInstance, InstanceCount, StartIndexLocation, BaseVertexLocation, StartInstanceLocation);
	}

	public unsafe void DrawInstanced(uint VertexCountPerInstance, uint InstanceCount, uint StartVertexLocation, uint StartInstanceLocation)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, uint, void>)lpVtbl[21])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), VertexCountPerInstance, InstanceCount, StartVertexLocation, StartInstanceLocation);
	}

	public unsafe void GSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[22])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void GSSetShader([Optional] ID3D11GeometryShader* pShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11GeometryShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[23])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY Topology)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D_PRIMITIVE_TOPOLOGY, void>)lpVtbl[24])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), Topology);
	}

	public unsafe void VSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[25])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void VSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[26])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void Begin(ID3D11Asynchronous* pAsync)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void>)lpVtbl[27])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pAsync);
	}

	public unsafe void End(ID3D11Asynchronous* pAsync)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void>)lpVtbl[28])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pAsync);
	}

	public unsafe HRESULT GetData(ID3D11Asynchronous* pAsync, [Optional] void* pData, uint DataSize, uint GetDataFlags)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Asynchronous*, void*, uint, uint, HRESULT>)lpVtbl[29])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pAsync, pData, DataSize, GetDataFlags);
	}

	public unsafe void SetPredication([Optional] ID3D11Predicate* pPredicate, BOOL PredicateValue)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Predicate*, BOOL, void>)lpVtbl[30])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pPredicate, PredicateValue);
	}

	public unsafe void GSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[31])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void GSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[32])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void OMSetRenderTargets(uint NumViews, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView* pDepthStencilView)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView*, void>)lpVtbl[33])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumViews, ppRenderTargetViews, pDepthStencilView);
	}

	internal unsafe void OMSetRenderTargetsAndUnorderedAccessViews(uint NumRTVs, ID3D11RenderTargetView** ppRenderTargetViews, ID3D11DepthStencilView* pDepthStencilView, uint UAVStartSlot, ID3D11UnorderedAccessView** ppUnorderedAccessViews, ReadOnlySpan<uint> pUAVInitialCounts)
	{
		fixed (uint* pUAVInitialCounts2 = pUAVInitialCounts)
		{
			OMSetRenderTargetsAndUnorderedAccessViews(NumRTVs, ppRenderTargetViews, pDepthStencilView, UAVStartSlot, (uint)pUAVInitialCounts.Length, ppUnorderedAccessViews, pUAVInitialCounts2);
		}
	}

	public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(uint NumRTVs, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView* pDepthStencilView, uint UAVStartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews, [Optional] uint* pUAVInitialCounts)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView*, uint, uint, ID3D11UnorderedAccessView**, uint*, void>)lpVtbl[34])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumRTVs, ppRenderTargetViews, pDepthStencilView, UAVStartSlot, NumUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void OMSetBlendState(ID3D11BlendState* pBlendState, ReadOnlySpan<float> BlendFactor, uint SampleMask)
	{
		fixed (float* blendFactor = BlendFactor)
		{
			if (BlendFactor.Length < 4)
			{
				throw new ArgumentException();
			}
			OMSetBlendState(pBlendState, blendFactor, SampleMask);
		}
	}

	public unsafe void OMSetBlendState([Optional] ID3D11BlendState* pBlendState, [Optional] float* BlendFactor, uint SampleMask)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11BlendState*, float*, uint, void>)lpVtbl[35])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pBlendState, BlendFactor, SampleMask);
	}

	public unsafe void OMSetDepthStencilState([Optional] ID3D11DepthStencilState* pDepthStencilState, uint StencilRef)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilState*, uint, void>)lpVtbl[36])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDepthStencilState, StencilRef);
	}

	internal unsafe void SOSetTargets(ID3D11Buffer** ppSOTargets, ReadOnlySpan<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			SOSetTargets((uint)pOffsets.Length, ppSOTargets, pOffsets2);
		}
	}

	public unsafe void SOSetTargets(uint NumBuffers, [Optional] ID3D11Buffer** ppSOTargets, [Optional] uint* pOffsets)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11Buffer**, uint*, void>)lpVtbl[37])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumBuffers, ppSOTargets, pOffsets);
	}

	public unsafe void DrawAuto()
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void>)lpVtbl[38])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe void DrawIndexedInstancedIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void>)lpVtbl[39])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pBufferForArgs, AlignedByteOffsetForArgs);
	}

	public unsafe void DrawInstancedIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void>)lpVtbl[40])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pBufferForArgs, AlignedByteOffsetForArgs);
	}

	public unsafe void Dispatch(uint ThreadGroupCountX, uint ThreadGroupCountY, uint ThreadGroupCountZ)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, uint, void>)lpVtbl[41])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ThreadGroupCountX, ThreadGroupCountY, ThreadGroupCountZ);
	}

	public unsafe void DispatchIndirect(ID3D11Buffer* pBufferForArgs, uint AlignedByteOffsetForArgs)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, void>)lpVtbl[42])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pBufferForArgs, AlignedByteOffsetForArgs);
	}

	public unsafe void RSSetState([Optional] ID3D11RasterizerState* pRasterizerState)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RasterizerState*, void>)lpVtbl[43])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pRasterizerState);
	}

	internal unsafe void RSSetViewports(ReadOnlySpan<D3D11_VIEWPORT> pViewports)
	{
		fixed (D3D11_VIEWPORT* pViewports2 = pViewports)
		{
			RSSetViewports((uint)pViewports.Length, pViewports2);
		}
	}

	public unsafe void RSSetViewports(uint NumViewports, [Optional] D3D11_VIEWPORT* pViewports)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, D3D11_VIEWPORT*, void>)lpVtbl[44])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumViewports, pViewports);
	}

	internal unsafe void RSSetScissorRects(ReadOnlySpan<RECT> pRects)
	{
		fixed (RECT* pRects2 = pRects)
		{
			RSSetScissorRects((uint)pRects.Length, pRects2);
		}
	}

	public unsafe void RSSetScissorRects(uint NumRects, [Optional] RECT* pRects)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, RECT*, void>)lpVtbl[45])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumRects, pRects);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void CopySubresourceRegion(ID3D11Resource* pDstResource, uint DstSubresource, uint DstX, uint DstY, uint DstZ, ID3D11Resource* pSrcResource, uint SrcSubresource, D3D11_BOX? pSrcBox)
	{
		D3D11_BOX valueOrDefault = pSrcBox.GetValueOrDefault();
		CopySubresourceRegion(pDstResource, DstSubresource, DstX, DstY, DstZ, pSrcResource, SrcSubresource, pSrcBox.HasValue ? (&valueOrDefault) : null);
	}

	public unsafe void CopySubresourceRegion(ID3D11Resource* pDstResource, uint DstSubresource, uint DstX, uint DstY, uint DstZ, ID3D11Resource* pSrcResource, uint SrcSubresource, [Optional] D3D11_BOX* pSrcBox)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, uint, uint, uint, ID3D11Resource*, uint, D3D11_BOX*, void>)lpVtbl[46])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDstResource, DstSubresource, DstX, DstY, DstZ, pSrcResource, SrcSubresource, pSrcBox);
	}

	public unsafe void CopyResource(ID3D11Resource* pDstResource, ID3D11Resource* pSrcResource)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, ID3D11Resource*, void>)lpVtbl[47])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDstResource, pSrcResource);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void UpdateSubresource(ID3D11Resource* pDstResource, uint DstSubresource, D3D11_BOX? pDstBox, void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch)
	{
		D3D11_BOX valueOrDefault = pDstBox.GetValueOrDefault();
		UpdateSubresource(pDstResource, DstSubresource, pDstBox.HasValue ? (&valueOrDefault) : null, pSrcData, SrcRowPitch, SrcDepthPitch);
	}

	public unsafe void UpdateSubresource(ID3D11Resource* pDstResource, uint DstSubresource, [Optional] D3D11_BOX* pDstBox, void* pSrcData, uint SrcRowPitch, uint SrcDepthPitch)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, D3D11_BOX*, void*, uint, uint, void>)lpVtbl[48])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDstResource, DstSubresource, pDstBox, pSrcData, SrcRowPitch, SrcDepthPitch);
	}

	public unsafe void CopyStructureCount(ID3D11Buffer* pDstBuffer, uint DstAlignedByteOffset, ID3D11UnorderedAccessView* pSrcView)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer*, uint, ID3D11UnorderedAccessView*, void>)lpVtbl[49])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDstBuffer, DstAlignedByteOffset, pSrcView);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ClearRenderTargetView(ID3D11RenderTargetView* pRenderTargetView, ReadOnlySpan<float> ColorRGBA)
	{
		fixed (float* colorRGBA = ColorRGBA)
		{
			if (ColorRGBA.Length < 4)
			{
				throw new ArgumentException();
			}
			ClearRenderTargetView(pRenderTargetView, colorRGBA);
		}
	}

	public unsafe void ClearRenderTargetView(ID3D11RenderTargetView* pRenderTargetView, float* ColorRGBA)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RenderTargetView*, float*, void>)lpVtbl[50])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pRenderTargetView, ColorRGBA);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ClearUnorderedAccessViewUint(ID3D11UnorderedAccessView* pUnorderedAccessView, ReadOnlySpan<uint> Values)
	{
		fixed (uint* values = Values)
		{
			if (Values.Length < 4)
			{
				throw new ArgumentException();
			}
			ClearUnorderedAccessViewUint(pUnorderedAccessView, values);
		}
	}

	public unsafe void ClearUnorderedAccessViewUint(ID3D11UnorderedAccessView* pUnorderedAccessView, uint* Values)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11UnorderedAccessView*, uint*, void>)lpVtbl[51])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pUnorderedAccessView, Values);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void ClearUnorderedAccessViewFloat(ID3D11UnorderedAccessView* pUnorderedAccessView, ReadOnlySpan<float> Values)
	{
		fixed (float* values = Values)
		{
			if (Values.Length < 4)
			{
				throw new ArgumentException();
			}
			ClearUnorderedAccessViewFloat(pUnorderedAccessView, values);
		}
	}

	public unsafe void ClearUnorderedAccessViewFloat(ID3D11UnorderedAccessView* pUnorderedAccessView, float* Values)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11UnorderedAccessView*, float*, void>)lpVtbl[52])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pUnorderedAccessView, Values);
	}

	public unsafe void ClearDepthStencilView(ID3D11DepthStencilView* pDepthStencilView, uint ClearFlags, float Depth, byte Stencil)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilView*, uint, float, byte, void>)lpVtbl[53])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDepthStencilView, ClearFlags, Depth, Stencil);
	}

	public unsafe void GenerateMips(ID3D11ShaderResourceView* pShaderResourceView)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ShaderResourceView*, void>)lpVtbl[54])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pShaderResourceView);
	}

	public unsafe void SetResourceMinLOD(ID3D11Resource* pResource, float MinLOD)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, float, void>)lpVtbl[55])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pResource, MinLOD);
	}

	public unsafe float GetResourceMinLOD(ID3D11Resource* pResource)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, float>)lpVtbl[56])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pResource);
	}

	public unsafe void ResolveSubresource(ID3D11Resource* pDstResource, uint DstSubresource, ID3D11Resource* pSrcResource, uint SrcSubresource, DXGI_FORMAT Format)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Resource*, uint, ID3D11Resource*, uint, DXGI_FORMAT, void>)lpVtbl[57])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDstResource, DstSubresource, pSrcResource, SrcSubresource, Format);
	}

	public unsafe void ExecuteCommandList(ID3D11CommandList* pCommandList, BOOL RestoreContextState)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11CommandList*, BOOL, void>)lpVtbl[58])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pCommandList, RestoreContextState);
	}

	public unsafe void HSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[59])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void HSSetShader([Optional] ID3D11HullShader* pHullShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11HullShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[60])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pHullShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void HSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[61])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void HSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[62])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void DSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[63])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void DSSetShader([Optional] ID3D11DomainShader* pDomainShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DomainShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[64])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pDomainShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void DSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[65])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void DSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[66])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void CSSetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[67])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	internal unsafe void CSSetUnorderedAccessViews(uint StartSlot, ID3D11UnorderedAccessView** ppUnorderedAccessViews, ReadOnlySpan<uint> pUAVInitialCounts)
	{
		fixed (uint* pUAVInitialCounts2 = pUAVInitialCounts)
		{
			CSSetUnorderedAccessViews(StartSlot, (uint)pUAVInitialCounts.Length, ppUnorderedAccessViews, pUAVInitialCounts2);
		}
	}

	public unsafe void CSSetUnorderedAccessViews(uint StartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews, [Optional] uint* pUAVInitialCounts)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11UnorderedAccessView**, uint*, void>)lpVtbl[68])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumUAVs, ppUnorderedAccessViews, pUAVInitialCounts);
	}

	public unsafe void CSSetShader([Optional] ID3D11ComputeShader* pComputeShader, [Optional] ID3D11ClassInstance** ppClassInstances, uint NumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ComputeShader*, ID3D11ClassInstance**, uint, void>)lpVtbl[69])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pComputeShader, ppClassInstances, NumClassInstances);
	}

	public unsafe void CSSetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[70])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void CSSetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[71])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void VSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[72])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void PSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[73])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void PSGetShader(ID3D11PixelShader** ppPixelShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11PixelShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[74])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppPixelShader, ppClassInstances, pNumClassInstances);
	}

	public unsafe void PSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[75])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void VSGetShader(ID3D11VertexShader** ppVertexShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11VertexShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[76])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppVertexShader, ppClassInstances, pNumClassInstances);
	}

	public unsafe void PSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[77])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void IAGetInputLayout(ID3D11InputLayout** ppInputLayout)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11InputLayout**, void>)lpVtbl[78])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppInputLayout);
	}

	internal unsafe void IAGetVertexBuffers(uint StartSlot, ID3D11Buffer** ppVertexBuffers, Span<uint> pStrides, Span<uint> pOffsets)
	{
		fixed (uint* pOffsets2 = pOffsets)
		{
			fixed (uint* pStrides2 = pStrides)
			{
				if (pStrides.Length != pOffsets.Length)
				{
					throw new ArgumentException();
				}
				IAGetVertexBuffers(StartSlot, (uint)pOffsets.Length, ppVertexBuffers, pStrides2, pOffsets2);
			}
		}
	}

	public unsafe void IAGetVertexBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppVertexBuffers, [Optional] uint* pStrides, [Optional] uint* pOffsets)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, uint*, uint*, void>)lpVtbl[79])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppVertexBuffers, pStrides, pOffsets);
	}

	public unsafe void IAGetIndexBuffer([Optional] ID3D11Buffer** pIndexBuffer, [Optional] DXGI_FORMAT* Format, [Optional] uint* Offset)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Buffer**, DXGI_FORMAT*, uint*, void>)lpVtbl[80])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pIndexBuffer, Format, Offset);
	}

	public unsafe void GSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[81])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void GSGetShader(ID3D11GeometryShader** ppGeometryShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11GeometryShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[82])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppGeometryShader, ppClassInstances, pNumClassInstances);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void IAGetPrimitiveTopology(out D3D_PRIMITIVE_TOPOLOGY pTopology)
	{
		fixed (D3D_PRIMITIVE_TOPOLOGY* pTopology2 = &pTopology)
		{
			IAGetPrimitiveTopology(pTopology2);
		}
	}

	public unsafe void IAGetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY* pTopology)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D_PRIMITIVE_TOPOLOGY*, void>)lpVtbl[83])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pTopology);
	}

	public unsafe void VSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[84])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void VSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[85])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void GetPredication([Optional] ID3D11Predicate** ppPredicate, [Optional] BOOL* pPredicateValue)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11Predicate**, BOOL*, void>)lpVtbl[86])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppPredicate, pPredicateValue);
	}

	public unsafe void GSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[87])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void GSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[88])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void OMGetRenderTargets(uint NumViews, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView** ppDepthStencilView)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView**, void>)lpVtbl[89])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumViews, ppRenderTargetViews, ppDepthStencilView);
	}

	public unsafe void OMGetRenderTargetsAndUnorderedAccessViews(uint NumRTVs, [Optional] ID3D11RenderTargetView** ppRenderTargetViews, [Optional] ID3D11DepthStencilView** ppDepthStencilView, uint UAVStartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11RenderTargetView**, ID3D11DepthStencilView**, uint, uint, ID3D11UnorderedAccessView**, void>)lpVtbl[90])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumRTVs, ppRenderTargetViews, ppDepthStencilView, UAVStartSlot, NumUAVs, ppUnorderedAccessViews);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void OMGetBlendState(ID3D11BlendState** ppBlendState, Span<float> BlendFactor, uint* pSampleMask)
	{
		fixed (float* blendFactor = BlendFactor)
		{
			if (BlendFactor.Length < 4)
			{
				throw new ArgumentException();
			}
			OMGetBlendState(ppBlendState, blendFactor, pSampleMask);
		}
	}

	public unsafe void OMGetBlendState([Optional] ID3D11BlendState** ppBlendState, [Optional] float* BlendFactor, [Optional] uint* pSampleMask)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11BlendState**, float*, uint*, void>)lpVtbl[91])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppBlendState, BlendFactor, pSampleMask);
	}

	public unsafe void OMGetDepthStencilState([Optional] ID3D11DepthStencilState** ppDepthStencilState, [Optional] uint* pStencilRef)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DepthStencilState**, uint*, void>)lpVtbl[92])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppDepthStencilState, pStencilRef);
	}

	public unsafe void SOGetTargets(uint NumBuffers, [Optional] ID3D11Buffer** ppSOTargets)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, ID3D11Buffer**, void>)lpVtbl[93])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), NumBuffers, ppSOTargets);
	}

	public unsafe void RSGetState(ID3D11RasterizerState** ppRasterizerState)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11RasterizerState**, void>)lpVtbl[94])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppRasterizerState);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void RSGetViewports(ref uint pNumViewports, Span<D3D11_VIEWPORT> pViewports)
	{
		fixed (D3D11_VIEWPORT* pViewports2 = pViewports)
		{
			fixed (uint* pNumViewports2 = &pNumViewports)
			{
				RSGetViewports(pNumViewports2, pViewports2);
			}
		}
	}

	public unsafe void RSGetViewports(uint* pNumViewports, [Optional] D3D11_VIEWPORT* pViewports)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint*, D3D11_VIEWPORT*, void>)lpVtbl[95])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pNumViewports, pViewports);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void RSGetScissorRects(ref uint pNumRects, Span<RECT> pRects)
	{
		fixed (RECT* pRects2 = pRects)
		{
			fixed (uint* pNumRects2 = &pNumRects)
			{
				RSGetScissorRects(pNumRects2, pRects2);
			}
		}
	}

	public unsafe void RSGetScissorRects(uint* pNumRects, [Optional] RECT* pRects)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint*, RECT*, void>)lpVtbl[96])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), pNumRects, pRects);
	}

	public unsafe void HSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[97])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void HSGetShader(ID3D11HullShader** ppHullShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11HullShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[98])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppHullShader, ppClassInstances, pNumClassInstances);
	}

	public unsafe void HSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[99])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void HSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[100])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void DSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[101])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void DSGetShader(ID3D11DomainShader** ppDomainShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11DomainShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[102])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppDomainShader, ppClassInstances, pNumClassInstances);
	}

	public unsafe void DSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[103])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void DSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[104])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void CSGetShaderResources(uint StartSlot, uint NumViews, [Optional] ID3D11ShaderResourceView** ppShaderResourceViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11ShaderResourceView**, void>)lpVtbl[105])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumViews, ppShaderResourceViews);
	}

	public unsafe void CSGetUnorderedAccessViews(uint StartSlot, uint NumUAVs, [Optional] ID3D11UnorderedAccessView** ppUnorderedAccessViews)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11UnorderedAccessView**, void>)lpVtbl[106])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumUAVs, ppUnorderedAccessViews);
	}

	public unsafe void CSGetShader(ID3D11ComputeShader** ppComputeShader, [Optional] ID3D11ClassInstance** ppClassInstances, [Optional] uint* pNumClassInstances)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, ID3D11ComputeShader**, ID3D11ClassInstance**, uint*, void>)lpVtbl[107])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), ppComputeShader, ppClassInstances, pNumClassInstances);
	}

	public unsafe void CSGetSamplers(uint StartSlot, uint NumSamplers, [Optional] ID3D11SamplerState** ppSamplers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11SamplerState**, void>)lpVtbl[108])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumSamplers, ppSamplers);
	}

	public unsafe void CSGetConstantBuffers(uint StartSlot, uint NumBuffers, [Optional] ID3D11Buffer** ppConstantBuffers)
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint, uint, ID3D11Buffer**, void>)lpVtbl[109])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), StartSlot, NumBuffers, ppConstantBuffers);
	}

	public unsafe void ClearState()
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void>)lpVtbl[110])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe void Flush()
	{
		((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, void>)lpVtbl[111])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public new unsafe D3D11_DEVICE_CONTEXT_TYPE GetType()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, D3D11_DEVICE_CONTEXT_TYPE>)lpVtbl[112])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint GetContextFlags()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, uint>)lpVtbl[113])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT FinishCommandList(BOOL RestoreDeferredContextState, [Optional] ID3D11CommandList** ppCommandList)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11DeviceContext*, BOOL, ID3D11CommandList**, HRESULT>)lpVtbl[114])((ID3D11DeviceContext*)Unsafe.AsPointer(ref this), RestoreDeferredContextState, ppCommandList);
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
