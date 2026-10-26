using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("A27003CF-2354-4F2A-8D6A-AB7CFF15437E")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFAsyncCallback
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncCallback*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint*, uint*, HRESULT> GetParameters_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFAsyncCallback*, IMFAsyncResult*, HRESULT> Invoke_5;
	}

	[ComImport]
	[Guid("A27003CF-2354-4F2A-8D6A-AB7CFF15437E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetParameters(uint* pdwFlags, uint* pdwQueue);

		[PreserveSig]
		unsafe HRESULT Invoke(IMFAsyncResult* pAsyncResult);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2725249999u, 9044, 20266, 141, 106, 171, 124, byte.MaxValue, 21, 67, 126);

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
		return ((delegate* unmanaged[Stdcall]<IMFAsyncCallback*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFAsyncCallback*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint>)lpVtbl[1])((IMFAsyncCallback*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint>)lpVtbl[2])((IMFAsyncCallback*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetParameters(out uint pdwFlags, out uint pdwQueue)
	{
		fixed (uint* pdwQueue2 = &pdwQueue)
		{
			fixed (uint* pdwFlags2 = &pdwFlags)
			{
				return GetParameters(pdwFlags2, pdwQueue2);
			}
		}
	}

	public unsafe HRESULT GetParameters(uint* pdwFlags, uint* pdwQueue)
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncCallback*, uint*, uint*, HRESULT>)lpVtbl[3])((IMFAsyncCallback*)Unsafe.AsPointer(ref this), pdwFlags, pdwQueue);
	}

	public unsafe HRESULT Invoke(IMFAsyncResult* pAsyncResult)
	{
		return ((delegate* unmanaged[Stdcall]<IMFAsyncCallback*, IMFAsyncResult*, HRESULT>)lpVtbl[4])((IMFAsyncCallback*)Unsafe.AsPointer(ref this), pAsyncResult);
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
