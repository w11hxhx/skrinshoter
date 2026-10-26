using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal static readonly PROPERTYKEY PKEY_Device_FriendlyName = new PROPERTYKEY
	{
		fmtid = new Guid(2757502286u, 57116, 20221, 128, 32, 103, 209, 70, 168, 80, 224),
		pid = 14u
	};

	internal static readonly PROPERTYKEY PKEY_Device_DeviceDesc = new PROPERTYKEY
	{
		fmtid = new Guid(2757502286u, 57116, 20221, 128, 32, 103, 209, 70, 168, 80, 224),
		pid = 2u
	};

	internal unsafe static HRESULT CoCreateInstance<T>(in Guid rclsid, IUnknown* pUnkOuter, CLSCTX dwClsContext, out T* ppv) where T : unmanaged
	{
		HRESULT result = CoCreateInstance(in rclsid, pUnkOuter, dwClsContext, typeof(T).GUID, out var ppv2);
		ppv = (T*)ppv2;
		return result;
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CoCreateInstance(in Guid rclsid, IUnknown* pUnkOuter, CLSCTX dwClsContext, in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				fixed (Guid* rclsid2 = &rclsid)
				{
					return CoCreateInstance(rclsid2, pUnkOuter, dwClsContext, riid2, ppv2);
				}
			}
		}
	}

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT CoCreateInstance(Guid* rclsid, [Optional] IUnknown* pUnkOuter, CLSCTX dwClsContext, Guid* riid, void** ppv);

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern void CoTaskMemFree([Optional] void* pv);

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT PropVariantClear(ref PROPVARIANT pvar)
	{
		fixed (PROPVARIANT* pvar2 = &pvar)
		{
			return PropVariantClear(pvar2);
		}
	}

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT PropVariantClear(PROPVARIANT* pvar);

	[DllImport("OLEAUT32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern void SysFreeString(BSTR bstrString);
}
