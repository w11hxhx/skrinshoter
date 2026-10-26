using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("AC6B7889-0740-4D51-8619-905994A55CC6")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFAsyncResult
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown**, HRESULT> GetState_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, HRESULT> GetStatus_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, HRESULT, HRESULT> SetStatus_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown**, HRESULT> GetObject_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown*> GetStateNoAddRef_8;
	}

	[ComImport]
	[Guid("AC6B7889-0740-4D51-8619-905994A55CC6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetState(IUnknown** ppunkState);

		[PreserveSig]
		HRESULT GetStatus();

		[PreserveSig]
		HRESULT SetStatus(HRESULT hrStatus);

		[PreserveSig]
		unsafe HRESULT GetObject(IUnknown** ppObject);

		[PreserveSig]
		unsafe IUnknown* GetStateNoAddRef();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2892724361u, 1856, 19793, 134, 25, 144, 89, 148, 165, 92, 198);

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
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFAsyncResult*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, uint>)lpVtbl[1])((IMFAsyncResult*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, uint>)lpVtbl[2])((IMFAsyncResult*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT GetState(IUnknown** ppunkState)
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown**, HRESULT>)lpVtbl[3])((IMFAsyncResult*)Unsafe.AsPointer(ref this), ppunkState);
	}

	public unsafe HRESULT GetStatus()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, HRESULT>)lpVtbl[4])((IMFAsyncResult*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT SetStatus(HRESULT hrStatus)
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, HRESULT, HRESULT>)lpVtbl[5])((IMFAsyncResult*)Unsafe.AsPointer(ref this), hrStatus);
	}

	public unsafe HRESULT GetObject(IUnknown** ppObject)
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown**, HRESULT>)lpVtbl[6])((IMFAsyncResult*)Unsafe.AsPointer(ref this), ppObject);
	}

	public unsafe IUnknown* GetStateNoAddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncResult*, IUnknown*>)lpVtbl[7])((IMFAsyncResult*)Unsafe.AsPointer(ref this));
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
