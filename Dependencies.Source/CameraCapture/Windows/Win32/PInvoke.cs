using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.System.Com;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint WM_DEVICECHANGE = 537u;

	internal const uint MF_VERSION = 131184u;

	internal const uint MFSTARTUP_LITE = 1u;

	internal static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE = new Guid(3322594814u, 9514, 18319, 160, 239, 188, 143, 165, 247, 202, 211);

	internal static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID = new Guid(2328057978u, 19175, 17112, 153, 224, 10, 96, 19, 238, 249, 15);

	internal static readonly Guid MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK = new Guid(1492167384, 8895, 20362, 187, 61, 210, 196, 151, 140, 110, 47);

	internal static readonly Guid MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME = new Guid(1624302937, 21240, 20386, 187, 206, 172, 219, 52, 168, 236, 1);

	internal static readonly Guid MFMediaType_Video = new Guid(1935960438, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MF_MT_MAJOR_TYPE = new Guid(1223401870u, 63689, 18055, 191, 17, 10, 116, 201, 249, 106, 143);

	internal static readonly Guid MF_MT_SUBTYPE = new Guid(4158868634u, 17128, 18196, 183, 75, 203, 41, 215, 44, 53, 229);

	internal static readonly Guid MF_MT_FRAME_SIZE = new Guid(374522685u, 54962, 16402, 184, 52, 114, 3, 8, 73, 163, 125);

	internal static readonly Guid MF_MT_FRAME_RATE = new Guid(3294208744u, 15660, 20036, 177, 50, 254, 229, 21, 108, 123, 176);

	internal static readonly Guid MFVideoFormat_Base = new Guid(0, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_RGB32 = new Guid(22, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_ARGB32 = new Guid(21, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_RGB24 = new Guid(20, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_RGB555 = new Guid(24, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_RGB565 = new Guid(23, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_RGB8 = new Guid(41, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_L8 = new Guid(50, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_L16 = new Guid(81, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_D16 = new Guid(80, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_AI44 = new Guid(875841857, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_AYUV = new Guid(1448433985, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_YUY2 = new Guid(844715353, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_YVYU = new Guid(1431918169, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_YVU9 = new Guid(961893977, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_UYVY = new Guid(1498831189, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_NV11 = new Guid(825316942, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_NV12 = new Guid(842094158, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_NV21 = new Guid(825382478, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_YV12 = new Guid(842094169, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_I420 = new Guid(808596553, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_IYUV = new Guid(1448433993, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y210 = new Guid(808530521, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y216 = new Guid(909193817, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y410 = new Guid(808531033, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y416 = new Guid(909194329, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y41P = new Guid(1345401945, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y41T = new Guid(1412510809, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Y42T = new Guid(1412576345, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_P210 = new Guid(808530512, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_P216 = new Guid(909193808, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_P010 = new Guid(808530000, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_P016 = new Guid(909193296, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_v210 = new Guid(808530550, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_v216 = new Guid(909193846, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_v410 = new Guid(808531062, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MP43 = new Guid(859066445, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MP4S = new Guid(1395937357, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_M4S2 = new Guid(844313677, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MP4V = new Guid(1446269005, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_WMV1 = new Guid(827739479, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_WMV2 = new Guid(844516695, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_WMV3 = new Guid(861293911, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_WVC1 = new Guid(826496599, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MSS1 = new Guid(827544397, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MSS2 = new Guid(844321613, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MPG1 = new Guid(826757197, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DVSL = new Guid(1819506276, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DVSD = new Guid(1685288548, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DVHD = new Guid(1684567652, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DV25 = new Guid(892499556, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DV50 = new Guid(808810084, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_DVH1 = new Guid(828929636, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_H264 = new Guid(875967048, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_H265 = new Guid(892744264, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_MJPG = new Guid(1196444237, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_420O = new Guid(1328558644, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_HEVC = new Guid(1129727304, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_HEVC_ES = new Guid(1398162760, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_VP80 = new Guid(808996950, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_VP90 = new Guid(809062486, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_ORAW = new Guid(1463898703, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_H263 = new Guid(859189832, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_A2R10G10B10 = new Guid(31, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_A16B16G16R16F = new Guid(113, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_VP10 = new Guid(808538198, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_AV1 = new Guid(825251393, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_Theora = new Guid(1868916852, 0, 16, 128, 0, 0, 170, 0, 56, 155, 113);

	internal static readonly Guid MFVideoFormat_H264_ES = new Guid(1061221616, 22050, 20472, 182, 216, 161, 122, 88, 75, 238, 94);

	internal static readonly Guid MFVideoFormat_MPEG2 = new Guid(3765272614u, 56134, 4559, 180, 209, 0, 128, 95, 108, 187, 234);

	internal static readonly Guid MFVideoFormat_H264_HDCP = new Guid(1561127389u, 38935, 18906, 189, 253, 245, 245, 185, 143, 24, 166);

	internal static readonly Guid MFVideoFormat_HEVC_HDCP = new Guid(1023283174, 1476, 18396, 157, 112, 75, 219, 41, 89, 114, 15);

	internal static readonly Guid MFVideoFormat_Base_HDCP = new Guid(3938695637u, 48404, 16951, 143, 31, 186, 180, 40, 228, 147, 18);

	internal static readonly Guid MF_LOW_LATENCY = new Guid(2619836698u, 60794, 16609, 136, 232, 178, 39, 39, 160, 36, 238);

	internal static readonly Guid MF_READWRITE_DISABLE_CONVERTERS = new Guid(2564141157u, 4980, 18503, 141, 93, 49, 82, 15, 238, 113, 86);

	internal static readonly Guid CLSID_VideoProcessorMFT = new Guid(2289384230u, 23332, 18877, 178, 231, 12, 68, 92, 120, 201, 130);

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DeleteObject(HGDIOBJ ho);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL FreeLibrary(HMODULE hLibModule);

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT MFEnumDeviceSources(IMFAttributes* pAttributes, out IMFActivate** pppSourceActivate, out uint pcSourceActivate)
	{
		fixed (uint* pcSourceActivate2 = &pcSourceActivate)
		{
			fixed (IMFActivate*** pppSourceActivate2 = &pppSourceActivate)
			{
				return MFEnumDeviceSources(pAttributes, pppSourceActivate2, pcSourceActivate2);
			}
		}
	}

	[DllImport("MF.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFEnumDeviceSources(IMFAttributes* pAttributes, IMFActivate*** pppSourceActivate, uint* pcSourceActivate);

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HRESULT MFStartup(uint Version, uint dwFlags);

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HRESULT MFShutdown();

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFCreateAttributes(IMFAttributes** ppMFAttributes, uint cInitialSize);

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFCreateMediaType(IMFMediaType** ppMFType);

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFCreateSample(IMFSample** ppIMFSample);

	[DllImport("MFPlat.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFCreate2DMediaBuffer(uint dwWidth, uint dwHeight, uint dwFourCC, BOOL fBottomUp, IMFMediaBuffer** ppBuffer);

	[DllImport("MFReadWrite.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT MFCreateSourceReaderFromMediaSource(IMFMediaSource* pMediaSource, [Optional] IMFAttributes* pAttributes, IMFSourceReader** ppSourceReader);

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

	[DllImport("OLEAUT32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern void SysFreeString(BSTR bstrString);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyIcon(HICON hIcon);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyCursor(HCURSOR hCursor);

	[OverloadResolutionPriority(1)]
	internal unsafe static ushort RegisterClass(in WNDCLASSW lpWndClass)
	{
		fixed (WNDCLASSW* lpWndClass2 = &lpWndClass)
		{
			return RegisterClass(lpWndClass2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "RegisterClassW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern ushort RegisterClass(WNDCLASSW* lpWndClass);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyMenu(HMENU hMenu);

	[OverloadResolutionPriority(1)]
	internal unsafe static HWND CreateWindowEx(WINDOW_EX_STYLE dwExStyle, string lpClassName, string lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, HWND hWndParent, SafeHandle hMenu, SafeHandle hInstance, void* lpParam)
	{
		bool success = false;
		bool success2 = false;
		try
		{
			fixed (char* ptr = lpWindowName)
			{
				fixed (char* ptr2 = lpClassName)
				{
					HMENU hMenu2;
					if (hMenu != null)
					{
						hMenu.DangerousAddRef(ref success);
						hMenu2 = (HMENU)hMenu.DangerousGetHandle();
					}
					else
					{
						hMenu2 = (HMENU)new IntPtr(0L);
					}
					HINSTANCE hInstance2;
					if (hInstance != null)
					{
						hInstance.DangerousAddRef(ref success2);
						hInstance2 = (HINSTANCE)hInstance.DangerousGetHandle();
					}
					else
					{
						hInstance2 = (HINSTANCE)new IntPtr(0L);
					}
					return CreateWindowEx(dwExStyle, ptr2, ptr, dwStyle, X, Y, nWidth, nHeight, hWndParent, hMenu2, hInstance2, lpParam);
				}
			}
		}
		finally
		{
			if (success)
			{
				hMenu.DangerousRelease();
			}
			if (success2)
			{
				hInstance.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "CreateWindowExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HWND CreateWindowEx(WINDOW_EX_STYLE dwExStyle, PCWSTR lpClassName, PCWSTR lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, HWND hWndParent, HMENU hMenu, HINSTANCE hInstance, [Optional] void* lpParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL DestroyWindow(HWND hWnd);

	[DllImport("USER32.dll", EntryPoint = "DefWindowProcW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern LRESULT DefWindowProc(HWND hWnd, uint Msg, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL UnregisterDeviceNotification(HDEVNOTIFY Handle);

	[OverloadResolutionPriority(1)]
	internal unsafe static UnregisterDeviceNotificationSafeHandle RegisterDeviceNotification(SafeHandle hRecipient, void* NotificationFilter, REGISTER_NOTIFICATION_FLAGS Flags)
	{
		bool success = false;
		try
		{
			if (hRecipient != null)
			{
				hRecipient.DangerousAddRef(ref success);
				HANDLE hRecipient2 = (HANDLE)hRecipient.DangerousGetHandle();
				return new UnregisterDeviceNotificationSafeHandle(RegisterDeviceNotification(hRecipient2, NotificationFilter, Flags));
			}
			throw new ArgumentNullException("hRecipient");
		}
		finally
		{
			if (success)
			{
				hRecipient.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "RegisterDeviceNotificationW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HDEVNOTIFY RegisterDeviceNotification(HANDLE hRecipient, void* NotificationFilter, REGISTER_NOTIFICATION_FLAGS Flags);
}
