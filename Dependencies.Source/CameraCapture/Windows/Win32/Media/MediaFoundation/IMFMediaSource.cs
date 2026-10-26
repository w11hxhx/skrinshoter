using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("279A808D-AEC7-40C8-9C6B-A6B492C78A66")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFMediaSource
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS, IMFMediaEvent**, HRESULT> GetEvent_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFAsyncCallback*, IUnknown*, HRESULT> BeginGetEvent_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFAsyncResult*, IMFMediaEvent**, HRESULT> EndGetEvent_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, uint, Guid*, HRESULT, PROPVARIANT*, HRESULT> QueueEvent_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, uint*, HRESULT> GetCharacteristics_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFPresentationDescriptor**, HRESULT> CreatePresentationDescriptor_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFPresentationDescriptor*, Guid*, PROPVARIANT*, HRESULT> Start_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT> Stop_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT> Pause_12;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT> Shutdown_13;
	}

	[ComImport]
	[Guid("279A808D-AEC7-40C8-9C6B-A6B492C78A66")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IMFMediaEventGenerator.Interface
	{
		[PreserveSig]
		new unsafe HRESULT GetEvent(MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS dwFlags, IMFMediaEvent** ppEvent);

		[PreserveSig]
		new unsafe HRESULT BeginGetEvent(IMFAsyncCallback* pCallback, IUnknown* punkState);

		[PreserveSig]
		new unsafe HRESULT EndGetEvent(IMFAsyncResult* pResult, IMFMediaEvent** ppEvent);

		[PreserveSig]
		new unsafe HRESULT QueueEvent(uint met, Guid* guidExtendedType, HRESULT hrStatus, PROPVARIANT* pvValue);

		[PreserveSig]
		unsafe HRESULT GetCharacteristics(uint* pdwCharacteristics);

		[PreserveSig]
		unsafe HRESULT CreatePresentationDescriptor(IMFPresentationDescriptor** ppPresentationDescriptor);

		[PreserveSig]
		unsafe HRESULT Start(IMFPresentationDescriptor* pPresentationDescriptor, Guid* pguidTimeFormat, PROPVARIANT* pvarStartPosition);

		[PreserveSig]
		HRESULT Stop();

		[PreserveSig]
		HRESULT Pause();

		[PreserveSig]
		HRESULT Shutdown();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(664436877u, 44743, 16584, 156, 107, 166, 180, 146, 199, 138, 102);

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
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFMediaSource*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, uint>)lpVtbl[1])((IMFMediaSource*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, uint>)lpVtbl[2])((IMFMediaSource*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT GetEvent(MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS dwFlags, IMFMediaEvent** ppEvent)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS, IMFMediaEvent**, HRESULT>)lpVtbl[3])((IMFMediaSource*)Unsafe.AsPointer(ref this), dwFlags, ppEvent);
	}

	public unsafe HRESULT BeginGetEvent(IMFAsyncCallback* pCallback, IUnknown* punkState)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFAsyncCallback*, IUnknown*, HRESULT>)lpVtbl[4])((IMFMediaSource*)Unsafe.AsPointer(ref this), pCallback, punkState);
	}

	public unsafe HRESULT EndGetEvent(IMFAsyncResult* pResult, IMFMediaEvent** ppEvent)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFAsyncResult*, IMFMediaEvent**, HRESULT>)lpVtbl[5])((IMFMediaSource*)Unsafe.AsPointer(ref this), pResult, ppEvent);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT QueueEvent(uint met, in Guid guidExtendedType, HRESULT hrStatus, in PROPVARIANT pvValue)
	{
		fixed (PROPVARIANT* pvValue2 = &pvValue)
		{
			fixed (Guid* guidExtendedType2 = &guidExtendedType)
			{
				return QueueEvent(met, guidExtendedType2, hrStatus, pvValue2);
			}
		}
	}

	public unsafe HRESULT QueueEvent(uint met, Guid* guidExtendedType, HRESULT hrStatus, PROPVARIANT* pvValue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, uint, Guid*, HRESULT, PROPVARIANT*, HRESULT>)lpVtbl[6])((IMFMediaSource*)Unsafe.AsPointer(ref this), met, guidExtendedType, hrStatus, pvValue);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCharacteristics(out uint pdwCharacteristics)
	{
		fixed (uint* pdwCharacteristics2 = &pdwCharacteristics)
		{
			return GetCharacteristics(pdwCharacteristics2);
		}
	}

	public unsafe HRESULT GetCharacteristics(uint* pdwCharacteristics)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, uint*, HRESULT>)lpVtbl[7])((IMFMediaSource*)Unsafe.AsPointer(ref this), pdwCharacteristics);
	}

	public unsafe HRESULT CreatePresentationDescriptor(IMFPresentationDescriptor** ppPresentationDescriptor)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFPresentationDescriptor**, HRESULT>)lpVtbl[8])((IMFMediaSource*)Unsafe.AsPointer(ref this), ppPresentationDescriptor);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Start(IMFPresentationDescriptor* pPresentationDescriptor, in Guid pguidTimeFormat, in PROPVARIANT pvarStartPosition)
	{
		fixed (PROPVARIANT* pvarStartPosition2 = &pvarStartPosition)
		{
			fixed (Guid* pguidTimeFormat2 = &pguidTimeFormat)
			{
				return Start(pPresentationDescriptor, pguidTimeFormat2, pvarStartPosition2);
			}
		}
	}

	public unsafe HRESULT Start(IMFPresentationDescriptor* pPresentationDescriptor, Guid* pguidTimeFormat, PROPVARIANT* pvarStartPosition)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, IMFPresentationDescriptor*, Guid*, PROPVARIANT*, HRESULT>)lpVtbl[9])((IMFMediaSource*)Unsafe.AsPointer(ref this), pPresentationDescriptor, pguidTimeFormat, pvarStartPosition);
	}

	public unsafe HRESULT Stop()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT>)lpVtbl[10])((IMFMediaSource*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Pause()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT>)lpVtbl[11])((IMFMediaSource*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Shutdown()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaSource*, HRESULT>)lpVtbl[12])((IMFMediaSource*)Unsafe.AsPointer(ref this));
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
