using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ISequentialStream
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ISequentialStream*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ISequentialStream*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ISequentialStream*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, HRESULT> Read_4;

		internal unsafe delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, HRESULT> Write_5;
	}

	[ComImport]
	[Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead);

		[PreserveSig]
		unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(208878128, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

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
		return ((delegate* unmanaged[Stdcall]<ISequentialStream*, Guid*, void**, HRESULT>)(*lpVtbl))((ISequentialStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ISequentialStream*, uint>)lpVtbl[1])((ISequentialStream*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ISequentialStream*, uint>)lpVtbl[2])((ISequentialStream*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead)
	{
		return ((delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, HRESULT>)lpVtbl[3])((ISequentialStream*)Unsafe.AsPointer(ref this), pv, cb, pcbRead);
	}

	public unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten)
	{
		return ((delegate* unmanaged[Stdcall]<ISequentialStream*, void*, uint, uint*, HRESULT>)lpVtbl[4])((ISequentialStream*)Unsafe.AsPointer(ref this), pv, cb, pcbWritten);
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
