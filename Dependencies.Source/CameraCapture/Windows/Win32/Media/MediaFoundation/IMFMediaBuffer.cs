using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("045FA593-8799-42B8-BC8D-8968C6453507")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFMediaBuffer
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, byte**, uint*, uint*, HRESULT> Lock_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, HRESULT> Unlock_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint*, HRESULT> GetCurrentLength_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint, HRESULT> SetCurrentLength_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint*, HRESULT> GetMaxLength_8;
	}

	[ComImport]
	[Guid("045FA593-8799-42B8-BC8D-8968C6453507")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Lock(byte** ppbBuffer, [Optional] uint* pcbMaxLength, [Optional] uint* pcbCurrentLength);

		[PreserveSig]
		HRESULT Unlock();

		[PreserveSig]
		unsafe HRESULT GetCurrentLength(uint* pcbCurrentLength);

		[PreserveSig]
		HRESULT SetCurrentLength(uint cbCurrentLength);

		[PreserveSig]
		unsafe HRESULT GetMaxLength(uint* pcbMaxLength);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(73377171u, 34713, 17080, 188, 141, 137, 104, 198, 69, 53, 7);

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
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFMediaBuffer*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint>)lpVtbl[1])((IMFMediaBuffer*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint>)lpVtbl[2])((IMFMediaBuffer*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Lock(out byte* ppbBuffer, uint* pcbMaxLength, uint* pcbCurrentLength)
	{
		fixed (byte** ppbBuffer2 = &ppbBuffer)
		{
			return Lock(ppbBuffer2, pcbMaxLength, pcbCurrentLength);
		}
	}

	public unsafe HRESULT Lock(byte** ppbBuffer, [Optional] uint* pcbMaxLength, [Optional] uint* pcbCurrentLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, byte**, uint*, uint*, HRESULT>)lpVtbl[3])((IMFMediaBuffer*)Unsafe.AsPointer(ref this), ppbBuffer, pcbMaxLength, pcbCurrentLength);
	}

	public unsafe HRESULT Unlock()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, HRESULT>)lpVtbl[4])((IMFMediaBuffer*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCurrentLength(out uint pcbCurrentLength)
	{
		fixed (uint* pcbCurrentLength2 = &pcbCurrentLength)
		{
			return GetCurrentLength(pcbCurrentLength2);
		}
	}

	public unsafe HRESULT GetCurrentLength(uint* pcbCurrentLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint*, HRESULT>)lpVtbl[5])((IMFMediaBuffer*)Unsafe.AsPointer(ref this), pcbCurrentLength);
	}

	public unsafe HRESULT SetCurrentLength(uint cbCurrentLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint, HRESULT>)lpVtbl[6])((IMFMediaBuffer*)Unsafe.AsPointer(ref this), cbCurrentLength);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetMaxLength(out uint pcbMaxLength)
	{
		fixed (uint* pcbMaxLength2 = &pcbMaxLength)
		{
			return GetMaxLength(pcbMaxLength2);
		}
	}

	public unsafe HRESULT GetMaxLength(uint* pcbMaxLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaBuffer*, uint*, HRESULT>)lpVtbl[7])((IMFMediaBuffer*)Unsafe.AsPointer(ref this), pcbMaxLength);
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
