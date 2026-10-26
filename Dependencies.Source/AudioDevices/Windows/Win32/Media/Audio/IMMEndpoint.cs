using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMMEndpoint
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMMEndpoint*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMMEndpoint*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMMEndpoint*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMMEndpoint*, EDataFlow*, HRESULT> GetDataFlow_4;
	}

	[ComImport]
	[Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetDataFlow(EDataFlow* pDataFlow);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(467703688, 26772, 16521, 133, 134, 154, 42, 108, 38, 90, 197);

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
		return ((delegate* unmanaged[Stdcall]<IMMEndpoint*, Guid*, void**, HRESULT>)(*lpVtbl))((IMMEndpoint*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMMEndpoint*, uint>)lpVtbl[1])((IMMEndpoint*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMMEndpoint*, uint>)lpVtbl[2])((IMMEndpoint*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetDataFlow(out EDataFlow pDataFlow)
	{
		fixed (EDataFlow* pDataFlow2 = &pDataFlow)
		{
			return GetDataFlow(pDataFlow2);
		}
	}

	public unsafe HRESULT GetDataFlow(EDataFlow* pDataFlow)
	{
		return ((delegate* unmanaged[Stdcall]<IMMEndpoint*, EDataFlow*, HRESULT>)lpVtbl[3])((IMMEndpoint*)Unsafe.AsPointer(ref this), pDataFlow);
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
