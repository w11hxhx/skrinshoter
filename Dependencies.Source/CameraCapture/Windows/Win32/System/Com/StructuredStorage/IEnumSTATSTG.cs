using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com.StructuredStorage;

[Guid("0000000D-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IEnumSTATSTG
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint, STATSTG*, uint*, HRESULT> Next_4;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint, HRESULT> Skip_5;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, HRESULT> Reset_6;

		internal unsafe delegate* unmanaged[Stdcall]<IEnumSTATSTG*, IEnumSTATSTG**, HRESULT> Clone_7;
	}

	[ComImport]
	[Guid("0000000D-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Next(uint celt, STATSTG* rgelt, [Optional] uint* pceltFetched);

		[PreserveSig]
		HRESULT Skip(uint celt);

		[PreserveSig]
		HRESULT Reset();

		[PreserveSig]
		unsafe HRESULT Clone(IEnumSTATSTG** ppenum);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(13, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, Guid*, void**, HRESULT>)(*lpVtbl))((IEnumSTATSTG*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint>)lpVtbl[1])((IEnumSTATSTG*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint>)lpVtbl[2])((IEnumSTATSTG*)Unsafe.AsPointer(ref this));
	}

	internal unsafe HRESULT Next(Span<STATSTG> rgelt, uint* pceltFetched)
	{
		fixed (STATSTG* rgelt2 = rgelt)
		{
			return Next((uint)rgelt.Length, rgelt2, pceltFetched);
		}
	}

	public unsafe HRESULT Next(uint celt, STATSTG* rgelt, [Optional] uint* pceltFetched)
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint, STATSTG*, uint*, HRESULT>)lpVtbl[3])((IEnumSTATSTG*)Unsafe.AsPointer(ref this), celt, rgelt, pceltFetched);
	}

	public unsafe HRESULT Skip(uint celt)
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, uint, HRESULT>)lpVtbl[4])((IEnumSTATSTG*)Unsafe.AsPointer(ref this), celt);
	}

	public unsafe HRESULT Reset()
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, HRESULT>)lpVtbl[5])((IEnumSTATSTG*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Clone(IEnumSTATSTG** ppenum)
	{
		return ((delegate* unmanaged[Stdcall]<IEnumSTATSTG*, IEnumSTATSTG**, HRESULT>)lpVtbl[6])((IEnumSTATSTG*)Unsafe.AsPointer(ref this), ppenum);
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
