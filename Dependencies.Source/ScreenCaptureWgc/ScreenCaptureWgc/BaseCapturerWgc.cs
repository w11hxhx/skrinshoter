using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Logging;
using MousePointer;
using Windows.Foundation.Metadata;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.System.Com;
using Windows.Win32.System.WinRT;
using Windows.Win32.System.WinRT.Direct3D11;
using Windows.Win32.System.WinRT.Graphics.Capture;

namespace ScreenCaptureWgc;

public class BaseCapturerWgc : SafeDisposable
{
	private protected ILoggerFactory loggerFactory;

	private protected ILogger logger;

	private protected string monitorName;

	private protected MonitorInfo monitorInfo;

	private protected Rectangle captureArea;

	private protected bool showMousePointer;

	private protected IMouseOverlay mouseOverlay;

	private protected Thread captureThread;

	private protected readonly TaskCompletionSource<bool> captureThreadStartupCompletion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

	private protected readonly CancellationTokenSource cts = new CancellationTokenSource();

	private protected readonly HighResolutionSleeper sleeper = new HighResolutionSleeper();

	private protected ComPtr<IDXGIFactory1> dxgiFactory;

	private protected ComPtr<IDXGIAdapter> dxgiAdapter;

	private protected ComPtr<IDXGIOutput1> dxgiOutput;

	private protected ComPtr<ID3D11Device> d3d11Device;

	private protected ComPtr<ID3D11DeviceContext> d3d11DeviceContext;

	private protected ComPtr<ID3D11Resource> stagingTexture;

	private protected IDirect3DDevice winRtD3DDevice;

	private protected GraphicsCaptureItem captureItem;

	private protected Direct3D11CaptureFramePool captureFramePool;

	private protected GraphicsCaptureSession captureSession;

	private protected Size lastCapturedImageSize;

	private protected void CreateCaptureObjects()
	{
		CreateDxgiObjectsForMonitor();
		CreateD3D11Device();
		CreateStagingTexture();
		CreateWinRtD3DDevice();
		CreateCaptureItemForMonitor();
		CreateWgcObjects();
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
							comPtr.Ptr->QueryInterface<IDXGIAdapter>(out var ppv).ThrowOnFailure("IDXGIAdapter1.QueryInterface<IDXGIAdapter>()");
							dxgiAdapter = ppv;
							comPtr2.Ptr->QueryInterface<IDXGIOutput1>(out var ppv2).ThrowOnFailure("IDXGIOutput.QueryInterface<IDXGIOutput1>()");
							dxgiOutput = ppv2;
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

	private protected unsafe void CreateD3D11Device()
	{
		D3D11_CREATE_DEVICE_FLAG flags = D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT;
		ID3D11Device* ptr = default;
		D3D_FEATURE_LEVEL d3D_FEATURE_LEVEL = default;
		ID3D11DeviceContext* ptr2 = default;
		PInvoke.D3D11CreateDevice(dxgiAdapter, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_UNKNOWN, default, flags, null, 0u, 7u, &ptr, &d3D_FEATURE_LEVEL, &ptr2).ThrowOnFailure("D3D11CreateDevice()");
		d3d11Device = ptr;
		d3d11DeviceContext = ptr2;
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("Created Direct3D11 device: {featureLevel}", d3D_FEATURE_LEVEL);
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
		comPtr.Ptr->QueryInterface<ID3D11Resource>(out var ppv).ThrowOnFailure("ID3D11Texture2D.QueryInterface<ID3D11Resource>()");
		stagingTexture = ppv;
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

	private protected unsafe void CopyCapturedImageToStagingTexture(ComPtr<ID3D11Texture2D> capturedImage, Size capturedImageSize)
	{
		if (capturedImageSize != lastCapturedImageSize)
		{
			logger.LogInformation("Captured image size: Width={width}, Height={height}", capturedImageSize.Width, capturedImageSize.Height);
			ClearStagingTexture();
			lastCapturedImageSize = capturedImageSize;
		}
		Rectangle rectangle = Rectangle.Intersect(new Rectangle(Point.Empty, capturedImageSize), captureArea);
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

	private protected unsafe void CreateWinRtD3DDevice()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected Obj, but got Unknown
		d3d11Device.Ptr->QueryInterface<IDXGIDevice>(out var ppv).ThrowOnFailure("ID3D11Device.QueryInterface<IDXGIDevice>()");
		using ComPtr<IDXGIDevice> comPtr = ppv;
		IInspectable* ptr = default;
		PInvoke.CreateDirect3D11DeviceFromDXGIDevice(comPtr, &ptr).ThrowOnFailure("CreateDirect3D11DeviceFromDXGIDevice()");
		using ComPtr<IInspectable> comPtr2 = ptr;
		winRtD3DDevice = (IDirect3DDevice)Marshal.GetObjectForIUnknown((IntPtr)comPtr2.Ptr);
	}

	private protected unsafe void CreateCaptureItemForMonitor()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected Obj, but got Unknown
		dxgiOutput.Ptr->GetDesc(out var pDesc).ThrowOnFailure("IDXGIOutput1.GetDesc()");
		HMONITOR monitor = pDesc.Monitor;
		PInvoke.WindowsCreateString("Windows.Graphics.Capture.GraphicsCaptureItem", (uint)"Windows.Graphics.Capture.GraphicsCaptureItem".Length, out var @string).ThrowOnFailure("WindowsCreateString()");
		try
		{
			PInvoke.RoGetActivationFactory(@string, typeof(IGraphicsCaptureItemInterop).GUID, out var factory).ThrowOnFailure("RoGetActivationFactory()");
			using ComPtr<IGraphicsCaptureItemInterop> comPtr = (IGraphicsCaptureItemInterop*)factory;
			comPtr.Ptr->CreateForMonitor(monitor, typeof(IInspectable).GUID, out var result).ThrowOnFailure("IGraphicsCaptureItemInterop.CreateForMonitor()");
			using ComPtr<IInspectable> comPtr2 = (IInspectable*)result;
			captureItem = (GraphicsCaptureItem)Marshal.GetObjectForIUnknown((IntPtr)comPtr2.Ptr);
		}
		finally
		{
			@string.Dispose();
		}
	}

	private protected void CreateWgcObjects()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		captureFramePool = Direct3D11CaptureFramePool.CreateFreeThreaded(winRtD3DDevice, (DirectXPixelFormat)87, 1, captureItem.Size);
		captureSession = captureFramePool.CreateCaptureSession(captureItem);
		if (ApiInformation.IsPropertyPresent("Windows.Graphics.Capture.GraphicsCaptureSession", "IsCursorCaptureEnabled"))
		{
			SetIsCursorCaptureEnabled(showMousePointer);
		}
		else
		{
			logger.LogWarning("IsCursorCaptureEnabled is not supported");
		}
		if (ApiInformation.IsPropertyPresent("Windows.Graphics.Capture.GraphicsCaptureSession", "IsBorderRequired"))
		{
			SetIsBorderRequired(value: false);
		}
		else
		{
			logger.LogWarning("IsBorderRequired is not supported");
		}
		if (ApiInformation.IsPropertyPresent("Windows.Graphics.Capture.GraphicsCaptureSession", "MinUpdateInterval"))
		{
			SetMinUpdateInterval(TimeSpan.Zero);
		}
		else
		{
			logger.LogWarning("MinUpdateInterval is not supported");
		}
		captureSession.StartCapture();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetIsCursorCaptureEnabled(bool value)
	{
		captureSession.IsCursorCaptureEnabled = value;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetIsBorderRequired(bool value)
	{
		captureSession.IsBorderRequired = value;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void SetMinUpdateInterval(TimeSpan value)
	{
		captureSession.MinUpdateInterval = value;
	}

	private protected unsafe ComPtr<ID3D11Texture2D> GetTextureFromSurface(IDirect3DSurface surface)
	{
		using ComPtr<IUnknown> comPtr = (IUnknown*)(void*)Marshal.GetIUnknownForObject(surface);
		comPtr.Ptr->QueryInterface<IDirect3DDxgiInterfaceAccess>(out var ppv).ThrowOnFailure("IUnknown.QueryInterface<IDirect3DDxgiInterfaceAccess>()");
		using ComPtr<IDirect3DDxgiInterfaceAccess> comPtr2 = ppv;
		comPtr2.Ptr->GetInterface(typeof(ID3D11Texture2D).GUID, out var p).ThrowOnFailure("IDirect3DDxgiInterfaceAccess.GetInterface(ID3D11Texture2D)");
		return new ComPtr<ID3D11Texture2D>((ID3D11Texture2D*)p);
	}

	private protected void DisposeCaptureObjects()
	{
		try
		{
			GraphicsCaptureSession val = captureSession;
			if (val != null)
			{
				val.Dispose();
			}
		}
		catch
		{
		}
		captureSession = null;
		try
		{
			Direct3D11CaptureFramePool val2 = captureFramePool;
			if (val2 != null)
			{
				val2.Dispose();
			}
		}
		catch
		{
		}
		captureFramePool = null;
		captureItem = null;
		try
		{
			((IDisposable)winRtD3DDevice)?.Dispose();
		}
		catch
		{
		}
		winRtD3DDevice = null;
		stagingTexture.Dispose();
		d3d11DeviceContext.Dispose();
		d3d11Device.Dispose();
		dxgiOutput.Dispose();
		dxgiAdapter.Dispose();
		dxgiFactory.Dispose();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			cts.Cancel();
			captureThread?.Join();
			cts.Dispose();
			sleeper.Dispose();
			mouseOverlay?.Dispose();
		}
	}
}
