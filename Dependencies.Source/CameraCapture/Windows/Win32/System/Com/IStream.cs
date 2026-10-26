using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[Guid("0000000C-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IStream
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IStream*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, HRESULT> Read_4;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, HRESULT> Write_5;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, long, SeekOrigin, ulong*, HRESULT> Seek_6;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, ulong, HRESULT> SetSize_7;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, IStream*, ulong, ulong*, ulong*, HRESULT> CopyTo_8;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, uint, HRESULT> Commit_9;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, HRESULT> Revert_10;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT> LockRegion_11;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT> UnlockRegion_12;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, STATSTG*, uint, HRESULT> Stat_13;

		internal unsafe delegate* unmanaged[Stdcall]<IStream*, IStream**, HRESULT> Clone_14;
	}

	[ComImport]
	[Guid("0000000C-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : ISequentialStream.Interface
	{
		[PreserveSig]
		new unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead);

		[PreserveSig]
		new unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten);

		[PreserveSig]
		unsafe HRESULT Seek(long dlibMove, SeekOrigin dwOrigin, [Optional] ulong* plibNewPosition);

		[PreserveSig]
		HRESULT SetSize(ulong libNewSize);

		[PreserveSig]
		unsafe HRESULT CopyTo(IStream* pstm, ulong cb, [Optional] ulong* pcbRead, [Optional] ulong* pcbWritten);

		[PreserveSig]
		HRESULT Commit(uint grfCommitFlags);

		[PreserveSig]
		HRESULT Revert();

		[PreserveSig]
		HRESULT LockRegion(ulong libOffset, ulong cb, uint dwLockType);

		[PreserveSig]
		HRESULT UnlockRegion(ulong libOffset, ulong cb, uint dwLockType);

		[PreserveSig]
		unsafe HRESULT Stat(STATSTG* pstatstg, uint grfStatFlag);

		[PreserveSig]
		unsafe HRESULT Clone(IStream** ppstm);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(12, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IStream*, Guid*, void**, HRESULT>)(*lpVtbl))((IStream*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, uint>)lpVtbl[1])((IStream*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, uint>)lpVtbl[2])((IStream*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, HRESULT>)lpVtbl[3])((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbRead);
	}

	public unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, void*, uint, uint*, HRESULT>)lpVtbl[4])((IStream*)Unsafe.AsPointer(ref this), pv, cb, pcbWritten);
	}

	public unsafe HRESULT Seek(long dlibMove, SeekOrigin dwOrigin, [Optional] ulong* plibNewPosition)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, long, SeekOrigin, ulong*, HRESULT>)lpVtbl[5])((IStream*)Unsafe.AsPointer(ref this), dlibMove, dwOrigin, plibNewPosition);
	}

	public unsafe HRESULT SetSize(ulong libNewSize)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, ulong, HRESULT>)lpVtbl[6])((IStream*)Unsafe.AsPointer(ref this), libNewSize);
	}

	public unsafe HRESULT CopyTo(IStream* pstm, ulong cb, [Optional] ulong* pcbRead, [Optional] ulong* pcbWritten)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, IStream*, ulong, ulong*, ulong*, HRESULT>)lpVtbl[7])((IStream*)Unsafe.AsPointer(ref this), pstm, cb, pcbRead, pcbWritten);
	}

	public unsafe HRESULT Commit(uint grfCommitFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, uint, HRESULT>)lpVtbl[8])((IStream*)Unsafe.AsPointer(ref this), grfCommitFlags);
	}

	public unsafe HRESULT Revert()
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, HRESULT>)lpVtbl[9])((IStream*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT LockRegion(ulong libOffset, ulong cb, uint dwLockType)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT>)lpVtbl[10])((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
	}

	public unsafe HRESULT UnlockRegion(ulong libOffset, ulong cb, uint dwLockType)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT>)lpVtbl[11])((IStream*)Unsafe.AsPointer(ref this), libOffset, cb, dwLockType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Stat(out STATSTG pstatstg, uint grfStatFlag)
	{
		fixed (STATSTG* pstatstg2 = &pstatstg)
		{
			return Stat(pstatstg2, grfStatFlag);
		}
	}

	public unsafe HRESULT Stat(STATSTG* pstatstg, uint grfStatFlag)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, STATSTG*, uint, HRESULT>)lpVtbl[12])((IStream*)Unsafe.AsPointer(ref this), pstatstg, grfStatFlag);
	}

	public unsafe HRESULT Clone(IStream** ppstm)
	{
		return ((delegate* unmanaged[Stdcall]<IStream*, IStream**, HRESULT>)lpVtbl[13])((IStream*)Unsafe.AsPointer(ref this), ppstm);
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
