using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.WinRT.Direct3D11;

[Guid("A9B3D012-3DF2-4EE3-B8D1-8695F457D3C1")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IDirect3DDxgiInterfaceAccess
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, Guid*, void**, HRESULT> GetInterface_4;
	}

	[ComImport]
	[Guid("A9B3D012-3DF2-4EE3-B8D1-8695F457D3C1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetInterface(Guid* iid, void** p);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2847133714u, 15858, 20195, 184, 209, 134, 149, 244, 87, 211, 193);

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
		return ((delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, Guid*, void**, HRESULT>)(*lpVtbl))((IDirect3DDxgiInterfaceAccess*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, uint>)lpVtbl[1])((IDirect3DDxgiInterfaceAccess*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, uint>)lpVtbl[2])((IDirect3DDxgiInterfaceAccess*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetInterface(in Guid iid, out void* p)
	{
		fixed (void** p2 = &p)
		{
			fixed (Guid* iid2 = &iid)
			{
				return GetInterface(iid2, p2);
			}
		}
	}

	public unsafe HRESULT GetInterface(Guid* iid, void** p)
	{
		return ((delegate* unmanaged[Stdcall]<IDirect3DDxgiInterfaceAccess*, Guid*, void**, HRESULT>)lpVtbl[3])((IDirect3DDxgiInterfaceAccess*)Unsafe.AsPointer(ref this), iid, p);
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
