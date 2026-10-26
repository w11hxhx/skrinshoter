using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[Guid("00020403-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ITypeComp
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ITypeComp*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeComp*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeComp*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeComp*, PWSTR, uint, ushort, ITypeInfo**, DESCKIND*, BINDPTR*, HRESULT> Bind_4;

		internal unsafe delegate* unmanaged[Stdcall]<ITypeComp*, PWSTR, uint, ITypeInfo**, ITypeComp**, HRESULT> BindType_5;
	}

	[ComImport]
	[Guid("00020403-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Bind(PWSTR szName, uint lHashVal, ushort wFlags, ITypeInfo** ppTInfo, DESCKIND* pDescKind, BINDPTR* pBindPtr);

		[PreserveSig]
		unsafe HRESULT BindType(PWSTR szName, uint lHashVal, ITypeInfo** ppTInfo, ITypeComp** ppTComp);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(132099, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<ITypeComp*, Guid*, void**, HRESULT>)(*lpVtbl))((ITypeComp*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeComp*, uint>)lpVtbl[1])((ITypeComp*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ITypeComp*, uint>)lpVtbl[2])((ITypeComp*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Bind(PWSTR szName, uint lHashVal, ushort wFlags, ITypeInfo** ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr)
	{
		fixed (BINDPTR* pBindPtr2 = &pBindPtr)
		{
			fixed (DESCKIND* pDescKind2 = &pDescKind)
			{
				return Bind(szName, lHashVal, wFlags, ppTInfo, pDescKind2, pBindPtr2);
			}
		}
	}

	public unsafe HRESULT Bind(PWSTR szName, uint lHashVal, ushort wFlags, ITypeInfo** ppTInfo, DESCKIND* pDescKind, BINDPTR* pBindPtr)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeComp*, PWSTR, uint, ushort, ITypeInfo**, DESCKIND*, BINDPTR*, HRESULT>)lpVtbl[3])((ITypeComp*)Unsafe.AsPointer(ref this), szName, lHashVal, wFlags, ppTInfo, pDescKind, pBindPtr);
	}

	public unsafe HRESULT BindType(PWSTR szName, uint lHashVal, ITypeInfo** ppTInfo, ITypeComp** ppTComp)
	{
		return ((delegate* unmanaged[Stdcall]<ITypeComp*, PWSTR, uint, ITypeInfo**, ITypeComp**, HRESULT>)lpVtbl[4])((ITypeComp*)Unsafe.AsPointer(ref this), szName, lHashVal, ppTInfo, ppTComp);
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
