using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.System.WinRT.Graphics.Capture;

[Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IGraphicsCaptureItemInterop
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, HWND, Guid*, void**, HRESULT> CreateForWindow_4;

		internal unsafe delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, HMONITOR, Guid*, void**, HRESULT> CreateForMonitor_5;
	}

	[ComImport]
	[Guid("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT CreateForWindow(HWND window, Guid* riid, void** result);

		[PreserveSig]
		unsafe HRESULT CreateForMonitor(HMONITOR monitor, Guid* riid, void** result);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(908650523, 15532, 19552, 183, 244, 35, 206, 14, 12, 51, 86);

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
		return ((delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, Guid*, void**, HRESULT>)(*lpVtbl))((IGraphicsCaptureItemInterop*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, uint>)lpVtbl[1])((IGraphicsCaptureItemInterop*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, uint>)lpVtbl[2])((IGraphicsCaptureItemInterop*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateForWindow(HWND window, in Guid riid, out void* result)
	{
		fixed (void** result2 = &result)
		{
			fixed (Guid* riid2 = &riid)
			{
				return CreateForWindow(window, riid2, result2);
			}
		}
	}

	public unsafe HRESULT CreateForWindow(HWND window, Guid* riid, void** result)
	{
		return ((delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, HWND, Guid*, void**, HRESULT>)lpVtbl[3])((IGraphicsCaptureItemInterop*)Unsafe.AsPointer(ref this), window, riid, result);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateForMonitor(HMONITOR monitor, in Guid riid, out void* result)
	{
		fixed (void** result2 = &result)
		{
			fixed (Guid* riid2 = &riid)
			{
				return CreateForMonitor(monitor, riid2, result2);
			}
		}
	}

	public unsafe HRESULT CreateForMonitor(HMONITOR monitor, Guid* riid, void** result)
	{
		return ((delegate* unmanaged[Stdcall]<IGraphicsCaptureItemInterop*, HMONITOR, Guid*, void**, HRESULT>)lpVtbl[4])((IGraphicsCaptureItemInterop*)Unsafe.AsPointer(ref this), monitor, riid, result);
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
