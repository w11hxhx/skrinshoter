using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFMediaEventGenerator
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS, IMFMediaEvent**, HRESULT> GetEvent_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, IMFAsyncCallback*, IUnknown*, HRESULT> BeginGetEvent_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, IMFAsyncResult*, IMFMediaEvent**, HRESULT> EndGetEvent_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint, Guid*, HRESULT, PROPVARIANT*, HRESULT> QueueEvent_7;
	}

	[ComImport]
	[Guid("2CD0BD52-BCD5-4B89-B62C-EADC0C031E7D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetEvent(MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS dwFlags, IMFMediaEvent** ppEvent);

		[PreserveSig]
		unsafe HRESULT BeginGetEvent(IMFAsyncCallback* pCallback, IUnknown* punkState);

		[PreserveSig]
		unsafe HRESULT EndGetEvent(IMFAsyncResult* pResult, IMFMediaEvent** ppEvent);

		[PreserveSig]
		unsafe HRESULT QueueEvent(uint met, Guid* guidExtendedType, HRESULT hrStatus, PROPVARIANT* pvValue);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(751877458u, 48341, 19337, 182, 44, 234, 220, 12, 3, 30, 125);

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
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint>)lpVtbl[1])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint>)lpVtbl[2])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT GetEvent(MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS dwFlags, IMFMediaEvent** ppEvent)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, MEDIA_EVENT_GENERATOR_GET_EVENT_FLAGS, IMFMediaEvent**, HRESULT>)lpVtbl[3])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this), dwFlags, ppEvent);
	}

	public unsafe HRESULT BeginGetEvent(IMFAsyncCallback* pCallback, IUnknown* punkState)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, IMFAsyncCallback*, IUnknown*, HRESULT>)lpVtbl[4])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this), pCallback, punkState);
	}

	public unsafe HRESULT EndGetEvent(IMFAsyncResult* pResult, IMFMediaEvent** ppEvent)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, IMFAsyncResult*, IMFMediaEvent**, HRESULT>)lpVtbl[5])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this), pResult, ppEvent);
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
		return ((delegate* unmanaged[Stdcall]<IMFMediaEventGenerator*, uint, Guid*, HRESULT, PROPVARIANT*, HRESULT>)lpVtbl[6])((IMFMediaEventGenerator*)Unsafe.AsPointer(ref this), met, guidExtendedType, hrStatus, pvValue);
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
