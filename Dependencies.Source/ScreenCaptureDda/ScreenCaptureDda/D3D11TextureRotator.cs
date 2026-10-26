using System.Drawing;
using System.Runtime.InteropServices;
using Common;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi.Common;

namespace ScreenCaptureDda;

internal class D3D11TextureRotator : SafeDisposable
{
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 32)]
	private struct Params
	{
		public uint SrcX;

		public uint SrcY;

		public uint SrcWidth;

		public uint SrcHeight;

		public uint Rotation;
	}

	private readonly ComPtr<ID3D11Device> d3d11Device;

	private readonly ComPtr<ID3D11DeviceContext> d3d11DeviceContext;

	private readonly ComPtr<ID3D11VertexShader> vertexShader;

	private readonly ComPtr<ID3D11PixelShader> pixelShader;

	private readonly ComPtr<ID3D11Buffer> paramsBuffer;

	private ComPtr<ID3D11Texture2D> outputTexture;

	private Size outputTextureSize;

	private ComPtr<ID3D11RenderTargetView> renderTargetView;

	public unsafe D3D11TextureRotator(ComPtr<ID3D11Device> d3d11Device, ComPtr<ID3D11DeviceContext> d3d11DeviceContext)
	{
		this.d3d11Device = d3d11Device;
		this.d3d11DeviceContext = d3d11DeviceContext;
		try
		{
			fixed (byte* rotate_vs_4_ = Resources.rotate_vs_4_0)
			{
				ID3D11VertexShader* ptr = default;
				d3d11Device.Ptr->CreateVertexShader(rotate_vs_4_, (nuint)Resources.rotate_vs_4_0.Length, null, &ptr).ThrowOnFailure("ID3D11Device.CreateVertexShader()");
				vertexShader = ptr;
			}
			fixed (byte* rotate_ps_4_ = Resources.rotate_ps_4_0)
			{
				ID3D11PixelShader* ptr2 = default;
				d3d11Device.Ptr->CreatePixelShader(rotate_ps_4_, (nuint)Resources.rotate_ps_4_0.Length, null, &ptr2).ThrowOnFailure("ID3D11Device.CreatePixelShader()");
				pixelShader = ptr2;
			}
			D3D11_BUFFER_DESC d3D11_BUFFER_DESC = new D3D11_BUFFER_DESC
			{
				ByteWidth = (uint)sizeof(Params),
				Usage = D3D11_USAGE.D3D11_USAGE_DYNAMIC,
				BindFlags = D3D11_BIND_FLAG.D3D11_BIND_CONSTANT_BUFFER,
				CPUAccessFlags = D3D11_CPU_ACCESS_FLAG.D3D11_CPU_ACCESS_WRITE
			};
			ID3D11Buffer* ptr3 = default;
			d3d11Device.Ptr->CreateBuffer(&d3D11_BUFFER_DESC, null, &ptr3).ThrowOnFailure("ID3D11Device.CreateBuffer(paramsBuffer)");
			paramsBuffer = ptr3;
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe ComPtr<ID3D11Texture2D> Rotate(ComPtr<ID3D11Texture2D> texture, Rotation rotation)
	{
		texture.Ptr->GetDesc(out var pDesc);
		Size size = new Size((int)pDesc.Width, (int)pDesc.Height);
		Size requiredSize = ((rotation == Rotation.CW90 || rotation == Rotation.CW270) ? new Size(size.Height, size.Width) : size);
		EnsureOutputTextureCreated(requiredSize);
		ID3D11ShaderResourceView* ptr = default;
		d3d11Device.Ptr->CreateShaderResourceView((ID3D11Resource*)texture.Ptr, (D3D11_SHADER_RESOURCE_VIEW_DESC*)null, &ptr).ThrowOnFailure("ID3D11Device.CreateShaderResourceView()");
		using ((ComPtr<ID3D11ShaderResourceView>)ptr)
		{
			Params obj = new Params
			{
				SrcX = 0u,
				SrcY = 0u,
				SrcWidth = (uint)size.Width,
				SrcHeight = (uint)size.Height,
				Rotation = (uint)rotation
			};
			D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
			d3d11DeviceContext.Ptr->Map((ID3D11Resource*)paramsBuffer.Ptr, 0u, D3D11_MAP.D3D11_MAP_WRITE_DISCARD, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(paramsBuffer)");
			try
			{
				*(Params*)d3D11_MAPPED_SUBRESOURCE.pData = obj;
			}
			finally
			{
				d3d11DeviceContext.Ptr->Unmap((ID3D11Resource*)paramsBuffer.Ptr, 0u);
			}
			D3D11_VIEWPORT d3D11_VIEWPORT = new D3D11_VIEWPORT
			{
				TopLeftX = 0f,
				TopLeftY = 0f,
				Width = requiredSize.Width,
				Height = requiredSize.Height,
				MinDepth = 0f,
				MaxDepth = 1f
			};
			d3d11DeviceContext.Ptr->RSSetViewports(1u, &d3D11_VIEWPORT);
			d3d11DeviceContext.Ptr->IASetInputLayout(null);
			d3d11DeviceContext.Ptr->IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY.D3D_PRIMITIVE_TOPOLOGY_TRIANGLESTRIP);
			d3d11DeviceContext.Ptr->VSSetShader(vertexShader, null, 0u);
			d3d11DeviceContext.Ptr->PSSetShader(pixelShader, null, 0u);
			ID3D11Buffer* ptr2 = paramsBuffer.Ptr;
			d3d11DeviceContext.Ptr->PSSetConstantBuffers(0u, 1u, &ptr2);
			d3d11DeviceContext.Ptr->PSSetShaderResources(0u, 1u, &ptr);
			ID3D11RenderTargetView* ptr3 = renderTargetView.Ptr;
			d3d11DeviceContext.Ptr->OMSetRenderTargets(1u, &ptr3, null);
			d3d11DeviceContext.Ptr->Draw(4u, 0u);
			ID3D11ShaderResourceView* ptr4 = null;
			d3d11DeviceContext.Ptr->PSSetShaderResources(0u, 1u, &ptr4);
			ID3D11Buffer* ptr5 = null;
			d3d11DeviceContext.Ptr->PSSetConstantBuffers(0u, 1u, &ptr5);
			d3d11DeviceContext.Ptr->OMSetRenderTargets(0u, null, null);
			d3d11DeviceContext.Ptr->VSSetShader(null, null, 0u);
			d3d11DeviceContext.Ptr->PSSetShader(null, null, 0u);
			outputTexture.Ptr->AddRef();
			return outputTexture;
		}
	}

	private unsafe void EnsureOutputTextureCreated(Size requiredSize)
	{
		if (outputTexture.Ptr == null || renderTargetView.Ptr == null || !(outputTextureSize == requiredSize))
		{
			renderTargetView.Dispose();
			outputTexture.Dispose();
			D3D11_TEXTURE2D_DESC pDesc = new D3D11_TEXTURE2D_DESC
			{
				Width = (uint)requiredSize.Width,
				Height = (uint)requiredSize.Height,
				MipLevels = 1u,
				ArraySize = 1u,
				Format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
				SampleDesc = 
				{
					Count = 1u,
					Quality = 0u
				},
				Usage = D3D11_USAGE.D3D11_USAGE_DEFAULT,
				BindFlags = D3D11_BIND_FLAG.D3D11_BIND_RENDER_TARGET
			};
			ID3D11Texture2D* ptr = default;
			d3d11Device.Ptr->CreateTexture2D(in pDesc, null, &ptr).ThrowOnFailure("ID3D11Device.CreateTexture2D()");
			outputTexture = ptr;
			outputTextureSize = requiredSize;
			ID3D11RenderTargetView* ptr2 = default;
			d3d11Device.Ptr->CreateRenderTargetView((ID3D11Resource*)outputTexture.Ptr, (D3D11_RENDER_TARGET_VIEW_DESC*)null, &ptr2).ThrowOnFailure("ID3D11Device.CreateRenderTargetView()");
			renderTargetView = ptr2;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			renderTargetView.Dispose();
			outputTexture.Dispose();
			paramsBuffer.Dispose();
			pixelShader.Dispose();
			vertexShader.Dispose();
		}
	}
}
