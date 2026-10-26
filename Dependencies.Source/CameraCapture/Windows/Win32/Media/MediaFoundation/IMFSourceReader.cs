using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("70AE66F2-C809-4E4F-8915-BDCB406B7993")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFSourceReader
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, BOOL*, HRESULT> GetStreamSelection_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, BOOL, HRESULT> SetStreamSelection_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint, IMFMediaType**, HRESULT> GetNativeMediaType_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, IMFMediaType**, HRESULT> GetCurrentMediaType_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint*, IMFMediaType*, HRESULT> SetCurrentMediaType_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, Guid*, PROPVARIANT*, HRESULT> SetCurrentPosition_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint, uint*, uint*, long*, IMFSample**, HRESULT> ReadSample_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, HRESULT> Flush_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, Guid*, Guid*, void**, HRESULT> GetServiceForStream_12;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, Guid*, PROPVARIANT*, HRESULT> GetPresentationAttribute_13;
	}

	[ComImport]
	[Guid("70AE66F2-C809-4E4F-8915-BDCB406B7993")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetStreamSelection(uint dwStreamIndex, BOOL* pfSelected);

		[PreserveSig]
		HRESULT SetStreamSelection(uint dwStreamIndex, BOOL fSelected);

		[PreserveSig]
		unsafe HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, IMFMediaType** ppMediaType);

		[PreserveSig]
		unsafe HRESULT GetCurrentMediaType(uint dwStreamIndex, IMFMediaType** ppMediaType);

		[PreserveSig]
		unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, [Optional] uint* pdwReserved, IMFMediaType* pMediaType);

		[PreserveSig]
		unsafe HRESULT SetCurrentPosition(Guid* guidTimeFormat, PROPVARIANT* varPosition);

		[PreserveSig]
		unsafe HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, [Optional] uint* pdwActualStreamIndex, [Optional] uint* pdwStreamFlags, [Optional] long* pllTimestamp, [Optional] IMFSample** ppSample);

		[PreserveSig]
		HRESULT Flush(uint dwStreamIndex);

		[PreserveSig]
		unsafe HRESULT GetServiceForStream(uint dwStreamIndex, Guid* guidService, Guid* riid, void** ppvObject);

		[PreserveSig]
		unsafe HRESULT GetPresentationAttribute(uint dwStreamIndex, Guid* guidAttribute, PROPVARIANT* pvarAttribute);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1890477810u, 51209, 20047, 137, 21, 189, 203, 64, 107, 121, 147);

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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFSourceReader*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint>)lpVtbl[1])((IMFSourceReader*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint>)lpVtbl[2])((IMFSourceReader*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetStreamSelection(uint dwStreamIndex, out BOOL pfSelected)
	{
		fixed (BOOL* pfSelected2 = &pfSelected)
		{
			return GetStreamSelection(dwStreamIndex, pfSelected2);
		}
	}

	public unsafe HRESULT GetStreamSelection(uint dwStreamIndex, BOOL* pfSelected)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, BOOL*, HRESULT>)lpVtbl[3])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, pfSelected);
	}

	public unsafe HRESULT SetStreamSelection(uint dwStreamIndex, BOOL fSelected)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, BOOL, HRESULT>)lpVtbl[4])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, fSelected);
	}

	public unsafe HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint, IMFMediaType**, HRESULT>)lpVtbl[5])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, dwMediaTypeIndex, ppMediaType);
	}

	public unsafe HRESULT GetCurrentMediaType(uint dwStreamIndex, IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, IMFMediaType**, HRESULT>)lpVtbl[6])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, ppMediaType);
	}

	internal unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, IMFMediaType* pMediaType)
	{
		return SetCurrentMediaType(dwStreamIndex, null, pMediaType);
	}

	public unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, [Optional] uint* pdwReserved, IMFMediaType* pMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint*, IMFMediaType*, HRESULT>)lpVtbl[7])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, pdwReserved, pMediaType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetCurrentPosition(in Guid guidTimeFormat, in PROPVARIANT varPosition)
	{
		fixed (PROPVARIANT* varPosition2 = &varPosition)
		{
			fixed (Guid* guidTimeFormat2 = &guidTimeFormat)
			{
				return SetCurrentPosition(guidTimeFormat2, varPosition2);
			}
		}
	}

	public unsafe HRESULT SetCurrentPosition(Guid* guidTimeFormat, PROPVARIANT* varPosition)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[8])((IMFSourceReader*)Unsafe.AsPointer(ref this), guidTimeFormat, varPosition);
	}

	public unsafe HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, [Optional] uint* pdwActualStreamIndex, [Optional] uint* pdwStreamFlags, [Optional] long* pllTimestamp, [Optional] IMFSample** ppSample)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, uint, uint*, uint*, long*, IMFSample**, HRESULT>)lpVtbl[9])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, dwControlFlags, pdwActualStreamIndex, pdwStreamFlags, pllTimestamp, ppSample);
	}

	public unsafe HRESULT Flush(uint dwStreamIndex)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, HRESULT>)lpVtbl[10])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetServiceForStream(uint dwStreamIndex, in Guid guidService, in Guid riid, out void* ppvObject)
	{
		fixed (void** ppvObject2 = &ppvObject)
		{
			fixed (Guid* riid2 = &riid)
			{
				fixed (Guid* guidService2 = &guidService)
				{
					return GetServiceForStream(dwStreamIndex, guidService2, riid2, ppvObject2);
				}
			}
		}
	}

	public unsafe HRESULT GetServiceForStream(uint dwStreamIndex, Guid* guidService, Guid* riid, void** ppvObject)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, Guid*, Guid*, void**, HRESULT>)lpVtbl[11])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, guidService, riid, ppvObject);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetPresentationAttribute(uint dwStreamIndex, in Guid guidAttribute, out PROPVARIANT pvarAttribute)
	{
		fixed (PROPVARIANT* pvarAttribute2 = &pvarAttribute)
		{
			fixed (Guid* guidAttribute2 = &guidAttribute)
			{
				return GetPresentationAttribute(dwStreamIndex, guidAttribute2, pvarAttribute2);
			}
		}
	}

	public unsafe HRESULT GetPresentationAttribute(uint dwStreamIndex, Guid* guidAttribute, PROPVARIANT* pvarAttribute)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReader*, uint, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[12])((IMFSourceReader*)Unsafe.AsPointer(ref this), dwStreamIndex, guidAttribute, pvarAttribute);
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
