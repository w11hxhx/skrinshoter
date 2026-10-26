using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("7B981CF0-560E-4116-9875-B099895F23D7")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFSourceReaderEx
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, BOOL*, HRESULT> GetStreamSelection_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, BOOL, HRESULT> SetStreamSelection_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, IMFMediaType**, HRESULT> GetNativeMediaType_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IMFMediaType**, HRESULT> GetCurrentMediaType_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint*, IMFMediaType*, HRESULT> SetCurrentMediaType_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, Guid*, PROPVARIANT*, HRESULT> SetCurrentPosition_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, uint*, uint*, long*, IMFSample**, HRESULT> ReadSample_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, HRESULT> Flush_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, Guid*, Guid*, void**, HRESULT> GetServiceForStream_12;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, Guid*, PROPVARIANT*, HRESULT> GetPresentationAttribute_13;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IMFMediaType*, uint*, HRESULT> SetNativeMediaType_14;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IUnknown*, HRESULT> AddTransformForStream_15;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, HRESULT> RemoveAllTransformsForStream_16;

		internal unsafe delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, Guid*, IMFTransform**, HRESULT> GetTransformForStream_17;
	}

	[ComImport]
	[Guid("7B981CF0-560E-4116-9875-B099895F23D7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IMFSourceReader.Interface
	{
		[PreserveSig]
		new unsafe HRESULT GetStreamSelection(uint dwStreamIndex, BOOL* pfSelected);

		[PreserveSig]
		new HRESULT SetStreamSelection(uint dwStreamIndex, BOOL fSelected);

		[PreserveSig]
		new unsafe HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, IMFMediaType** ppMediaType);

		[PreserveSig]
		new unsafe HRESULT GetCurrentMediaType(uint dwStreamIndex, IMFMediaType** ppMediaType);

		[PreserveSig]
		new unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, [Optional] uint* pdwReserved, IMFMediaType* pMediaType);

		[PreserveSig]
		new unsafe HRESULT SetCurrentPosition(Guid* guidTimeFormat, PROPVARIANT* varPosition);

		[PreserveSig]
		new unsafe HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, [Optional] uint* pdwActualStreamIndex, [Optional] uint* pdwStreamFlags, [Optional] long* pllTimestamp, [Optional] IMFSample** ppSample);

		[PreserveSig]
		new HRESULT Flush(uint dwStreamIndex);

		[PreserveSig]
		new unsafe HRESULT GetServiceForStream(uint dwStreamIndex, Guid* guidService, Guid* riid, void** ppvObject);

		[PreserveSig]
		new unsafe HRESULT GetPresentationAttribute(uint dwStreamIndex, Guid* guidAttribute, PROPVARIANT* pvarAttribute);

		[PreserveSig]
		unsafe HRESULT SetNativeMediaType(uint dwStreamIndex, [Optional] IMFMediaType* pMediaType, uint* pdwStreamFlags);

		[PreserveSig]
		unsafe HRESULT AddTransformForStream(uint dwStreamIndex, IUnknown* pTransformOrActivate);

		[PreserveSig]
		HRESULT RemoveAllTransformsForStream(uint dwStreamIndex);

		[PreserveSig]
		unsafe HRESULT GetTransformForStream(uint dwStreamIndex, uint dwTransformIndex, [Optional] Guid* pGuidCategory, IMFTransform** ppTransform);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2073566448, 22030, 16662, 152, 117, 176, 153, 137, 95, 35, 215);

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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint>)lpVtbl[1])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint>)lpVtbl[2])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, BOOL*, HRESULT>)lpVtbl[3])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, pfSelected);
	}

	public unsafe HRESULT SetStreamSelection(uint dwStreamIndex, BOOL fSelected)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, BOOL, HRESULT>)lpVtbl[4])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, fSelected);
	}

	public unsafe HRESULT GetNativeMediaType(uint dwStreamIndex, uint dwMediaTypeIndex, IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, IMFMediaType**, HRESULT>)lpVtbl[5])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, dwMediaTypeIndex, ppMediaType);
	}

	public unsafe HRESULT GetCurrentMediaType(uint dwStreamIndex, IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IMFMediaType**, HRESULT>)lpVtbl[6])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, ppMediaType);
	}

	internal unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, IMFMediaType* pMediaType)
	{
		return SetCurrentMediaType(dwStreamIndex, null, pMediaType);
	}

	public unsafe HRESULT SetCurrentMediaType(uint dwStreamIndex, [Optional] uint* pdwReserved, IMFMediaType* pMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint*, IMFMediaType*, HRESULT>)lpVtbl[7])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, pdwReserved, pMediaType);
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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[8])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), guidTimeFormat, varPosition);
	}

	public unsafe HRESULT ReadSample(uint dwStreamIndex, uint dwControlFlags, [Optional] uint* pdwActualStreamIndex, [Optional] uint* pdwStreamFlags, [Optional] long* pllTimestamp, [Optional] IMFSample** ppSample)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, uint*, uint*, long*, IMFSample**, HRESULT>)lpVtbl[9])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, dwControlFlags, pdwActualStreamIndex, pdwStreamFlags, pllTimestamp, ppSample);
	}

	public unsafe HRESULT Flush(uint dwStreamIndex)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, HRESULT>)lpVtbl[10])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex);
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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, Guid*, Guid*, void**, HRESULT>)lpVtbl[11])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, guidService, riid, ppvObject);
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
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[12])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, guidAttribute, pvarAttribute);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetNativeMediaType(uint dwStreamIndex, IMFMediaType* pMediaType, out uint pdwStreamFlags)
	{
		fixed (uint* pdwStreamFlags2 = &pdwStreamFlags)
		{
			return SetNativeMediaType(dwStreamIndex, pMediaType, pdwStreamFlags2);
		}
	}

	public unsafe HRESULT SetNativeMediaType(uint dwStreamIndex, [Optional] IMFMediaType* pMediaType, uint* pdwStreamFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IMFMediaType*, uint*, HRESULT>)lpVtbl[13])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, pMediaType, pdwStreamFlags);
	}

	public unsafe HRESULT AddTransformForStream(uint dwStreamIndex, IUnknown* pTransformOrActivate)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, IUnknown*, HRESULT>)lpVtbl[14])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, pTransformOrActivate);
	}

	public unsafe HRESULT RemoveAllTransformsForStream(uint dwStreamIndex)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, HRESULT>)lpVtbl[15])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex);
	}

	public unsafe HRESULT GetTransformForStream(uint dwStreamIndex, uint dwTransformIndex, [Optional] Guid* pGuidCategory, IMFTransform** ppTransform)
	{
		return ((delegate* unmanaged[Stdcall]<IMFSourceReaderEx*, uint, uint, Guid*, IMFTransform**, HRESULT>)lpVtbl[16])((IMFSourceReaderEx*)Unsafe.AsPointer(ref this), dwStreamIndex, dwTransformIndex, pGuidCategory, ppTransform);
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
