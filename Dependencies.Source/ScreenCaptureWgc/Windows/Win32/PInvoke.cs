using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.System.WinRT;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint D3D11_SDK_VERSION = 7u;

	internal const uint INFINITE = uint.MaxValue;

	[DllImport("api-ms-win-core-winrt-l1-1-0.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HRESULT RoInitialize(RO_INIT_TYPE initType);

	[DllImport("api-ms-win-core-winrt-l1-1-0.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern void RoUninitialize();

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT RoGetActivationFactory(SafeHandle activatableClassId, in Guid iid, out void* factory)
	{
		bool success = false;
		try
		{
			fixed (void** factory2 = &factory)
			{
				fixed (Guid* iid2 = &iid)
				{
					if (activatableClassId != null)
					{
						activatableClassId.DangerousAddRef(ref success);
						HSTRING activatableClassId2 = (HSTRING)activatableClassId.DangerousGetHandle();
						return RoGetActivationFactory(activatableClassId2, iid2, factory2);
					}
					throw new ArgumentNullException("activatableClassId");
				}
			}
		}
		finally
		{
			if (success)
			{
				activatableClassId.DangerousRelease();
			}
		}
	}

	[DllImport("api-ms-win-core-winrt-l1-1-0.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT RoGetActivationFactory(HSTRING activatableClassId, Guid* iid, void** factory);

	[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HRESULT WindowsDeleteString(HSTRING @string);

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT WindowsCreateString(string sourceString, uint length, out WindowsDeleteStringSafeHandle @string)
	{
		fixed (char* ptr = sourceString)
		{
			HSTRING hSTRING = default;
			HRESULT result = WindowsCreateString(ptr, length, &hSTRING);
			@string = new WindowsDeleteStringSafeHandle(hSTRING);
			return result;
		}
	}

	[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT WindowsCreateString(PCWSTR sourceString, uint length, HSTRING* @string);

	internal unsafe static HRESULT D3D11CreateDevice(IDXGIAdapter* pAdapter, D3D_DRIVER_TYPE DriverType, HMODULE Software, D3D11_CREATE_DEVICE_FLAG Flags, ReadOnlySpan<D3D_FEATURE_LEVEL> pFeatureLevels, uint SDKVersion, ID3D11Device** ppDevice, D3D_FEATURE_LEVEL* pFeatureLevel, ID3D11DeviceContext** ppImmediateContext)
	{
		fixed (D3D_FEATURE_LEVEL* pFeatureLevels2 = pFeatureLevels)
		{
			return D3D11CreateDevice(pAdapter, DriverType, Software, Flags, pFeatureLevels2, (uint)pFeatureLevels.Length, SDKVersion, ppDevice, pFeatureLevel, ppImmediateContext);
		}
	}

	[DllImport("d3d11.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT D3D11CreateDevice([Optional] IDXGIAdapter* pAdapter, D3D_DRIVER_TYPE DriverType, HMODULE Software, D3D11_CREATE_DEVICE_FLAG Flags, [Optional] D3D_FEATURE_LEVEL* pFeatureLevels, uint FeatureLevels, uint SDKVersion, [Optional] ID3D11Device** ppDevice, [Optional] D3D_FEATURE_LEVEL* pFeatureLevel, [Optional] ID3D11DeviceContext** ppImmediateContext);

	[DllImport("d3d11.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT CreateDirect3D11DeviceFromDXGIDevice(IDXGIDevice* dxgiDevice, IInspectable** graphicsDevice);

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CreateDXGIFactory1(in Guid riid, out void* ppFactory)
	{
		fixed (void** ppFactory2 = &ppFactory)
		{
			fixed (Guid* riid2 = &riid)
			{
				return CreateDXGIFactory1(riid2, ppFactory2);
			}
		}
	}

	[DllImport("dxgi.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT CreateDXGIFactory1(Guid* riid, void** ppFactory);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL FreeLibrary(HMODULE hLibModule);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetCursorPos(out Point lpPoint)
	{
		fixed (Point* lpPoint2 = &lpPoint)
		{
			return GetCursorPos(lpPoint2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetCursorPos(Point* lpPoint);
}
