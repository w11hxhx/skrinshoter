using System;
using System.Drawing;
using Common;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.System.Com;

namespace ScreenCaptureDda;

public class BaseCapturerDda : SafeDisposable
{
	private protected ILogger logger;

	private protected string monitorName;

	private protected Rectangle captureArea;

	private protected MousePointer mousePointer;

	private protected readonly HighResolutionSleeper sleeper = new HighResolutionSleeper();

	private protected ComPtr<IDXGIFactory1> dxgiFactory;

	private protected ComPtr<IDXGIAdapter> dxgiAdapter;

	private protected ComPtr<IDXGIOutput1> dxgiOutput;

	private protected ComPtr<ID3D11Device> d3d11Device;

	private protected ComPtr<IUnknown> d3d11DeviceAsUnknown;

	private protected ComPtr<ID3D11DeviceContext> d3d11DeviceContext;

	private protected ComPtr<IDXGIOutputDuplication> outputDuplication;

	private protected ComPtr<IDXGIResource> desktopResource;

	private protected ComPtr<ID3D11Resource> stagingTexture;

	private protected Size lastCapturedImageSize;

	private Rotation rotation;

	private D3D11TextureRotator rotator;

	private protected unsafe virtual void CreateCaptureObjects()
	{
		CreateDxgiObjectsForMonitor();
		D3D11_CREATE_DEVICE_FLAG flags = (D3D11_CREATE_DEVICE_FLAG)0u;
		ID3D11Device* ptr = default;
		D3D_FEATURE_LEVEL d3D_FEATURE_LEVEL = default;
		ID3D11DeviceContext* ptr2 = default;
		PInvoke.D3D11CreateDevice(dxgiAdapter, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_UNKNOWN, default, flags, null, 0u, 7u, &ptr, &d3D_FEATURE_LEVEL, &ptr2).ThrowOnFailure("D3D11CreateDevice()");
		d3d11Device = ptr;
		d3d11DeviceContext = ptr2;
		d3d11Device.Ptr->QueryInterface(typeof(IUnknown).GUID, out var ppvObject).ThrowOnFailure("ID3D11Device.QueryInterface(IUnknown)");
		d3d11DeviceAsUnknown = (IUnknown*)ppvObject;
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Created Direct3D11 device: {featureLevel}", d3D_FEATURE_LEVEL);
		}
		CreateStagingTexture();
	}

	private protected unsafe void CreateDxgiObjectsForMonitor()
	{
		PInvoke.CreateDXGIFactory1(typeof(IDXGIFactory1).GUID, out var ppFactory).ThrowOnFailure("CreateDXGIFactory1()");
		dxgiFactory = (IDXGIFactory1*)ppFactory;
		uint num = 0u;
		IDXGIAdapter1* ptr = default;
		IDXGIOutput* ptr2 = default;
		while (true)
		{
			HRESULT hRESULT = dxgiFactory.Ptr->EnumAdapters1(num, &ptr);
			if (hRESULT == HRESULT.DXGI_ERROR_NOT_FOUND)
			{
				break;
			}
			hRESULT.ThrowOnFailure($"IDXGIFactory1.EnumAdapters1({num})");
			using (ComPtr<IDXGIAdapter1> comPtr = ptr)
			{
				comPtr.Ptr->GetDesc1(out var pDesc).ThrowOnFailure("IDXGIAdapter1.GetDesc1()");
				logger.LogInformation("Video adapter {index}: {desc}", num, pDesc.Description);
				uint num2 = 0u;
				while (true)
				{
					hRESULT = comPtr.Ptr->EnumOutputs(num2, &ptr2);
					if (hRESULT == HRESULT.DXGI_ERROR_NOT_FOUND)
					{
						break;
					}
					hRESULT.ThrowOnFailure($"IDXGIAdapter1.EnumOutputs({num2})");
					using (ComPtr<IDXGIOutput> comPtr2 = ptr2)
					{
						comPtr2.Ptr->GetDesc(out var pDesc2).ThrowOnFailure("IDXGIOutput.GetDesc()");
						logger.LogInformation("=====> Output {index}: {name}, DesktopCoordinates={x},{y},{width},{height}, AttachedToDesktop={attachedToDesktop}, Rotation={rotation}", num2, pDesc2.DeviceName, pDesc2.DesktopCoordinates.X, pDesc2.DesktopCoordinates.Y, pDesc2.DesktopCoordinates.Width, pDesc2.DesktopCoordinates.Height, (bool)pDesc2.AttachedToDesktop, pDesc2.Rotation);
						if (pDesc2.DeviceName.ToString() == monitorName)
						{
							comPtr.Ptr->QueryInterface(typeof(IDXGIAdapter).GUID, out ppFactory).ThrowOnFailure("IDXGIAdapter1.QueryInterface(IDXGIAdapter)");
							dxgiAdapter = (IDXGIAdapter*)ppFactory;
							comPtr2.Ptr->QueryInterface(typeof(IDXGIOutput1).GUID, out ppFactory).ThrowOnFailure("IDXGIOutput.QueryInterface(IDXGIOutput1)");
							dxgiOutput = (IDXGIOutput1*)ppFactory;
						}
					}
					num2++;
				}
			}
			num++;
		}
		if (dxgiOutput.Ptr == null)
		{
			throw new InvalidOperationException("Monitor " + monitorName + " not found.");
		}
	}

	private protected unsafe void CreateStagingTexture()
	{
		D3D11_TEXTURE2D_DESC pDesc = new D3D11_TEXTURE2D_DESC
		{
			Width = (uint)captureArea.Width,
			Height = (uint)captureArea.Height,
			MipLevels = 1u,
			ArraySize = 1u,
			Format = DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
			SampleDesc = 
			{
				Count = 1u,
				Quality = 0u
			},
			Usage = D3D11_USAGE.D3D11_USAGE_STAGING,
			BindFlags = (D3D11_BIND_FLAG)0,
			CPUAccessFlags = (D3D11_CPU_ACCESS_FLAG.D3D11_CPU_ACCESS_WRITE | D3D11_CPU_ACCESS_FLAG.D3D11_CPU_ACCESS_READ),
			MiscFlags = (D3D11_RESOURCE_MISC_FLAG)0
		};
		ID3D11Texture2D* ptr = default;
		d3d11Device.Ptr->CreateTexture2D(in pDesc, null, &ptr).ThrowOnFailure("ID3D11Device.CreateTexture2D()");
		using ComPtr<ID3D11Texture2D> comPtr = ptr;
		comPtr.Ptr->QueryInterface(typeof(ID3D11Resource).GUID, out var ppvObject).ThrowOnFailure("ID3D11Texture2D.QueryInterface(ID3D11Resource)");
		stagingTexture = (ID3D11Resource*)ppvObject;
	}

	private protected unsafe void ClearStagingTexture()
	{
		D3D11_MAPPED_SUBRESOURCE d3D11_MAPPED_SUBRESOURCE = default;
		d3d11DeviceContext.Ptr->Map(stagingTexture, 0u, D3D11_MAP.D3D11_MAP_WRITE, 0u, &d3D11_MAPPED_SUBRESOURCE).ThrowOnFailure("ID3D11DeviceContext.Map(stagingTexture, WRITE)");
		try
		{
			new Span<byte>(d3D11_MAPPED_SUBRESOURCE.pData, (int)(d3D11_MAPPED_SUBRESOURCE.RowPitch * captureArea.Height)).Fill(0);
		}
		finally
		{
			d3d11DeviceContext.Ptr->Unmap(stagingTexture, 0u);
		}
	}

	private protected unsafe void CopyCapturedImageToStagingTexture(ComPtr<ID3D11Texture2D> capturedImage)
	{
		capturedImage.Ptr->GetDesc(out var pDesc);
		Size size = new Size((int)pDesc.Width, (int)pDesc.Height);
		if (size != lastCapturedImageSize)
		{
			logger.LogInformation("Captured image size: Width={width}, Height={height}", size.Width, size.Height);
			ClearStagingTexture();
			lastCapturedImageSize = size;
		}
		Rectangle rectangle = Rectangle.Intersect(new Rectangle(Point.Empty, size), captureArea);
		if (!rectangle.IsEmpty)
		{
			D3D11_BOX value = new D3D11_BOX
			{
				left = (uint)rectangle.Left,
				top = (uint)rectangle.Top,
				front = 0u,
				right = (uint)rectangle.Right,
				bottom = (uint)rectangle.Bottom,
				back = 1u
			};
			d3d11DeviceContext.Ptr->CopySubresourceRegion(stagingTexture, 0u, 0u, 0u, 0u, (ID3D11Resource*)capturedImage.Ptr, 0u, value);
		}
	}

	private protected unsafe HRESULT TryCreateOutputDuplication(out ComPtr<IDXGIOutputDuplication> outputDuplication)
	{
		IDXGIOutputDuplication* ptr = default;
		HRESULT result = dxgiOutput.Ptr->DuplicateOutput(d3d11DeviceAsUnknown, &ptr);
		if (result.Succeeded)
		{
			outputDuplication = ptr;
			outputDuplication.Ptr->GetDesc(out var pDesc);
			logger.LogInformation("Desktop image description: Width={width}, Height={height}, RefreshRate={refreshRateN}/{refreshRateD}, Format={format}, ScanlineOrdering={scanlineOrdering}, Scaling={scaling}, Rotation={rotation}, DesktopImageInSystemMemory={desktopImageInSystemMemory}", pDesc.ModeDesc.Width, pDesc.ModeDesc.Height, pDesc.ModeDesc.RefreshRate.Numerator, pDesc.ModeDesc.RefreshRate.Denominator, pDesc.ModeDesc.Format, pDesc.ModeDesc.ScanlineOrdering, pDesc.ModeDesc.Scaling, pDesc.Rotation, (bool)pDesc.DesktopImageInSystemMemory);
			UpdateRotation(pDesc.Rotation.ToRotation());
		}
		return result;
	}

	private void UpdateRotation(Rotation rotation)
	{
		this.rotation = rotation;
		if (rotation == Rotation.None)
		{
			rotator?.Dispose();
			rotator = null;
		}
		else if (rotator == null)
		{
			rotator = new D3D11TextureRotator(d3d11Device, d3d11DeviceContext);
		}
	}

	private protected unsafe ComPtr<ID3D11Texture2D> RotateIfNeeded(ComPtr<IDXGIResource> capturedImage)
	{
		capturedImage.Ptr->QueryInterface(typeof(ID3D11Texture2D).GUID, out var ppvObject).ThrowOnFailure("IDXGIResource.QueryInterface(ID3D11Texture2D)");
		using ComPtr<ID3D11Texture2D> comPtr = (ID3D11Texture2D*)ppvObject;
		if (rotator != null)
		{
			return rotator.Rotate(comPtr, rotation);
		}
		comPtr.Ptr->AddRef();
		return comPtr;
	}

	private protected void DisposeCaptureObjects()
	{
		DisposeOutputDuplication();
		rotator?.Dispose();
		rotator = null;
		stagingTexture.Dispose();
		d3d11DeviceContext.Dispose();
		d3d11Device.Dispose();
		d3d11DeviceAsUnknown.Dispose();
		dxgiOutput.Dispose();
		dxgiAdapter.Dispose();
		dxgiFactory.Dispose();
	}

	private protected unsafe void DisposeOutputDuplication()
	{
		if (desktopResource.Ptr != null)
		{
			desktopResource.Dispose();
			outputDuplication.Ptr->ReleaseFrame();
		}
		outputDuplication.Dispose();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			DisposeCaptureObjects();
			sleeper.Dispose();
		}
	}
}
