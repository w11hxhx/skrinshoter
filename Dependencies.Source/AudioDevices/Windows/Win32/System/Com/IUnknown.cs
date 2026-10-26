using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[Guid("00000000-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IUnknown
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IUnknown*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IUnknown*, uint> Release_3;
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(0, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, HRESULT>)(*lpVtbl))((IUnknown*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)lpVtbl[1])((IUnknown*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IUnknown*, uint>)lpVtbl[2])((IUnknown*)Unsafe.AsPointer(ref this));
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
