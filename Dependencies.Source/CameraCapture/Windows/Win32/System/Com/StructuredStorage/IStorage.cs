using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com.StructuredStorage;

[Guid("0000000B-0000-0000-C000-000000000046")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IStorage
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, STGM, uint, uint, IStream**, HRESULT> CreateStream_4;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, void*, STGM, uint, IStream**, HRESULT> OpenStream_5;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, STGM, uint, uint, IStorage**, HRESULT> CreateStorage_6;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, IStorage*, STGM, ushort**, uint, IStorage**, HRESULT> OpenStorage_7;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint, Guid*, ushort**, IStorage*, HRESULT> CopyTo_8;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, IStorage*, PCWSTR, uint, HRESULT> MoveElementTo_9;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint, HRESULT> Commit_10;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, HRESULT> Revert_11;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint, void*, uint, IEnumSTATSTG**, HRESULT> EnumElements_12;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, HRESULT> DestroyElement_13;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, PCWSTR, HRESULT> RenameElement_14;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, global::System.Runtime.InteropServices.ComTypes.FILETIME*, global::System.Runtime.InteropServices.ComTypes.FILETIME*, global::System.Runtime.InteropServices.ComTypes.FILETIME*, HRESULT> SetElementTimes_15;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, Guid*, HRESULT> SetClass_16;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, uint, uint, HRESULT> SetStateBits_17;

		internal unsafe delegate* unmanaged[Stdcall]<IStorage*, STATSTG*, uint, HRESULT> Stat_18;
	}

	[ComImport]
	[Guid("0000000B-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT CreateStream(PCWSTR pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStream** ppstm);

		[PreserveSig]
		unsafe HRESULT OpenStream(PCWSTR pwcsName, [Optional] void* reserved1, STGM grfMode, uint reserved2, IStream** ppstm);

		[PreserveSig]
		unsafe HRESULT CreateStorage(PCWSTR pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStorage** ppstg);

		[PreserveSig]
		unsafe HRESULT OpenStorage(PCWSTR pwcsName, IStorage* pstgPriority, STGM grfMode, ushort** snbExclude, uint reserved, IStorage** ppstg);

		[PreserveSig]
		unsafe HRESULT CopyTo(uint ciidExclude, [Optional] Guid* rgiidExclude, [Optional] ushort** snbExclude, IStorage* pstgDest);

		[PreserveSig]
		unsafe HRESULT MoveElementTo(PCWSTR pwcsName, IStorage* pstgDest, PCWSTR pwcsNewName, uint grfFlags);

		[PreserveSig]
		HRESULT Commit(uint grfCommitFlags);

		[PreserveSig]
		HRESULT Revert();

		[PreserveSig]
		unsafe HRESULT EnumElements(uint reserved1, [Optional] void* reserved2, uint reserved3, IEnumSTATSTG** ppenum);

		[PreserveSig]
		HRESULT DestroyElement(PCWSTR pwcsName);

		[PreserveSig]
		HRESULT RenameElement(PCWSTR pwcsOldName, PCWSTR pwcsNewName);

		[PreserveSig]
		unsafe HRESULT SetElementTimes(PCWSTR pwcsName, global::System.Runtime.InteropServices.ComTypes.FILETIME* pctime, global::System.Runtime.InteropServices.ComTypes.FILETIME* patime, global::System.Runtime.InteropServices.ComTypes.FILETIME* pmtime);

		[PreserveSig]
		unsafe HRESULT SetClass(Guid* clsid);

		[PreserveSig]
		HRESULT SetStateBits(uint grfStateBits, uint grfMask);

		[PreserveSig]
		unsafe HRESULT Stat(STATSTG* pstatstg, uint grfStatFlag);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(11, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

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
		return ((delegate* unmanaged[Stdcall]<IStorage*, Guid*, void**, HRESULT>)(*lpVtbl))((IStorage*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint>)lpVtbl[1])((IStorage*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint>)lpVtbl[2])((IStorage*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateStream(string pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStream** ppstm)
	{
		fixed (char* ptr = pwcsName)
		{
			return CreateStream(ptr, grfMode, reserved1, reserved2, ppstm);
		}
	}

	public unsafe HRESULT CreateStream(PCWSTR pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStream** ppstm)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, STGM, uint, uint, IStream**, HRESULT>)lpVtbl[3])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, grfMode, reserved1, reserved2, ppstm);
	}

	internal unsafe HRESULT OpenStream(string pwcsName, STGM grfMode, uint reserved2, IStream** ppstm)
	{
		fixed (char* ptr = pwcsName)
		{
			return OpenStream(ptr, null, grfMode, reserved2, ppstm);
		}
	}

	public unsafe HRESULT OpenStream(PCWSTR pwcsName, [Optional] void* reserved1, STGM grfMode, uint reserved2, IStream** ppstm)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, void*, STGM, uint, IStream**, HRESULT>)lpVtbl[4])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, reserved1, grfMode, reserved2, ppstm);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT CreateStorage(string pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStorage** ppstg)
	{
		fixed (char* ptr = pwcsName)
		{
			return CreateStorage(ptr, grfMode, reserved1, reserved2, ppstg);
		}
	}

	public unsafe HRESULT CreateStorage(PCWSTR pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStorage** ppstg)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, STGM, uint, uint, IStorage**, HRESULT>)lpVtbl[5])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, grfMode, reserved1, reserved2, ppstg);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT OpenStorage(string pwcsName, IStorage* pstgPriority, STGM grfMode, in ushort* snbExclude, uint reserved, IStorage** ppstg)
	{
		fixed (ushort** snbExclude2 = &snbExclude)
		{
			fixed (char* ptr = pwcsName)
			{
				return OpenStorage(ptr, pstgPriority, grfMode, snbExclude2, reserved, ppstg);
			}
		}
	}

	public unsafe HRESULT OpenStorage(PCWSTR pwcsName, IStorage* pstgPriority, STGM grfMode, ushort** snbExclude, uint reserved, IStorage** ppstg)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, IStorage*, STGM, ushort**, uint, IStorage**, HRESULT>)lpVtbl[6])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, pstgPriority, grfMode, snbExclude, reserved, ppstg);
	}

	internal unsafe HRESULT CopyTo(ReadOnlySpan<Guid> rgiidExclude, ushort** snbExclude, IStorage* pstgDest)
	{
		fixed (Guid* rgiidExclude2 = rgiidExclude)
		{
			return CopyTo((uint)rgiidExclude.Length, rgiidExclude2, snbExclude, pstgDest);
		}
	}

	public unsafe HRESULT CopyTo(uint ciidExclude, [Optional] Guid* rgiidExclude, [Optional] ushort** snbExclude, IStorage* pstgDest)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint, Guid*, ushort**, IStorage*, HRESULT>)lpVtbl[7])((IStorage*)Unsafe.AsPointer(ref this), ciidExclude, rgiidExclude, snbExclude, pstgDest);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT MoveElementTo(string pwcsName, IStorage* pstgDest, string pwcsNewName, uint grfFlags)
	{
		fixed (char* ptr = pwcsNewName)
		{
			fixed (char* ptr2 = pwcsName)
			{
				return MoveElementTo(ptr2, pstgDest, ptr, grfFlags);
			}
		}
	}

	public unsafe HRESULT MoveElementTo(PCWSTR pwcsName, IStorage* pstgDest, PCWSTR pwcsNewName, uint grfFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, IStorage*, PCWSTR, uint, HRESULT>)lpVtbl[8])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, pstgDest, pwcsNewName, grfFlags);
	}

	public unsafe HRESULT Commit(uint grfCommitFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint, HRESULT>)lpVtbl[9])((IStorage*)Unsafe.AsPointer(ref this), grfCommitFlags);
	}

	public unsafe HRESULT Revert()
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, HRESULT>)lpVtbl[10])((IStorage*)Unsafe.AsPointer(ref this));
	}

	internal unsafe HRESULT EnumElements(IEnumSTATSTG** ppenum)
	{
		return EnumElements(0u, null, 0u, ppenum);
	}

	public unsafe HRESULT EnumElements(uint reserved1, [Optional] void* reserved2, uint reserved3, IEnumSTATSTG** ppenum)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint, void*, uint, IEnumSTATSTG**, HRESULT>)lpVtbl[11])((IStorage*)Unsafe.AsPointer(ref this), reserved1, reserved2, reserved3, ppenum);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT DestroyElement(string pwcsName)
	{
		fixed (char* ptr = pwcsName)
		{
			return DestroyElement(ptr);
		}
	}

	public unsafe HRESULT DestroyElement(PCWSTR pwcsName)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, HRESULT>)lpVtbl[12])((IStorage*)Unsafe.AsPointer(ref this), pwcsName);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT RenameElement(string pwcsOldName, string pwcsNewName)
	{
		fixed (char* ptr = pwcsNewName)
		{
			fixed (char* ptr2 = pwcsOldName)
			{
				return RenameElement(ptr2, ptr);
			}
		}
	}

	public unsafe HRESULT RenameElement(PCWSTR pwcsOldName, PCWSTR pwcsNewName)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, PCWSTR, HRESULT>)lpVtbl[13])((IStorage*)Unsafe.AsPointer(ref this), pwcsOldName, pwcsNewName);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetElementTimes(string pwcsName, in global::System.Runtime.InteropServices.ComTypes.FILETIME pctime, in global::System.Runtime.InteropServices.ComTypes.FILETIME patime, in global::System.Runtime.InteropServices.ComTypes.FILETIME pmtime)
	{
		fixed (global::System.Runtime.InteropServices.ComTypes.FILETIME* pmtime2 = &pmtime)
		{
			fixed (global::System.Runtime.InteropServices.ComTypes.FILETIME* patime2 = &patime)
			{
				fixed (global::System.Runtime.InteropServices.ComTypes.FILETIME* pctime2 = &pctime)
				{
					fixed (char* ptr = pwcsName)
					{
						return SetElementTimes(ptr, pctime2, patime2, pmtime2);
					}
				}
			}
		}
	}

	public unsafe HRESULT SetElementTimes(PCWSTR pwcsName, global::System.Runtime.InteropServices.ComTypes.FILETIME* pctime, global::System.Runtime.InteropServices.ComTypes.FILETIME* patime, global::System.Runtime.InteropServices.ComTypes.FILETIME* pmtime)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, PCWSTR, global::System.Runtime.InteropServices.ComTypes.FILETIME*, global::System.Runtime.InteropServices.ComTypes.FILETIME*, global::System.Runtime.InteropServices.ComTypes.FILETIME*, HRESULT>)lpVtbl[14])((IStorage*)Unsafe.AsPointer(ref this), pwcsName, pctime, patime, pmtime);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetClass(in Guid clsid)
	{
		fixed (Guid* ptr = &clsid)
		{
			return SetClass(ptr);
		}
	}

	public unsafe HRESULT SetClass(Guid* clsid)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, Guid*, HRESULT>)lpVtbl[15])((IStorage*)Unsafe.AsPointer(ref this), clsid);
	}

	public unsafe HRESULT SetStateBits(uint grfStateBits, uint grfMask)
	{
		return ((delegate* unmanaged[Stdcall]<IStorage*, uint, uint, HRESULT>)lpVtbl[16])((IStorage*)Unsafe.AsPointer(ref this), grfStateBits, grfMask);
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
		return ((delegate* unmanaged[Stdcall]<IStorage*, STATSTG*, uint, HRESULT>)lpVtbl[17])((IStorage*)Unsafe.AsPointer(ref this), pstatstg, grfStatFlag);
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
