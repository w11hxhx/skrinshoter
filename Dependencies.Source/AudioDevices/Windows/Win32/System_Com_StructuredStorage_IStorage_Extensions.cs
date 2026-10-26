using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_StructuredStorage_IStorage_Extensions
{
	internal unsafe static HRESULT CreateStream(this IStorage.Interface @this, string pwcsName, STGM grfMode, uint reserved1, uint reserved2, Windows.Win32.System.Com.IStream** ppstm)
	{
		fixed (char* ptr = pwcsName)
		{
			return @this.CreateStream(ptr, grfMode, reserved1, reserved2, ppstm);
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT OpenStream(this IStorage.Interface @this, string pwcsName, STGM grfMode, uint reserved2, Windows.Win32.System.Com.IStream** ppstm)
	{
		fixed (char* ptr = pwcsName)
		{
			return @this.OpenStream(ptr, null, grfMode, reserved2, ppstm);
		}
	}

	internal unsafe static HRESULT CreateStorage(this IStorage.Interface @this, string pwcsName, STGM grfMode, uint reserved1, uint reserved2, IStorage** ppstg)
	{
		fixed (char* ptr = pwcsName)
		{
			return @this.CreateStorage(ptr, grfMode, reserved1, reserved2, ppstg);
		}
	}

	internal unsafe static HRESULT OpenStorage(this IStorage.Interface @this, string pwcsName, IStorage* pstgPriority, STGM grfMode, in ushort* snbExclude, uint reserved, IStorage** ppstg)
	{
		fixed (ushort** snbExclude2 = &snbExclude)
		{
			fixed (char* ptr = pwcsName)
			{
				return @this.OpenStorage(ptr, pstgPriority, grfMode, snbExclude2, reserved, ppstg);
			}
		}
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CopyTo(this IStorage.Interface @this, ReadOnlySpan<Guid> rgiidExclude, ushort** snbExclude, IStorage* pstgDest)
	{
		fixed (Guid* rgiidExclude2 = rgiidExclude)
		{
			return @this.CopyTo((uint)rgiidExclude.Length, rgiidExclude2, snbExclude, pstgDest);
		}
	}

	internal unsafe static HRESULT MoveElementTo(this IStorage.Interface @this, string pwcsName, IStorage* pstgDest, string pwcsNewName, uint grfFlags)
	{
		fixed (char* ptr = pwcsNewName)
		{
			fixed (char* ptr2 = pwcsName)
			{
				return @this.MoveElementTo(ptr2, pstgDest, ptr, grfFlags);
			}
		}
	}

	internal unsafe static HRESULT EnumElements(this IStorage.Interface @this, IEnumSTATSTG** ppenum)
	{
		return @this.EnumElements(0u, null, 0u, ppenum);
	}

	internal unsafe static HRESULT DestroyElement(this IStorage.Interface @this, string pwcsName)
	{
		fixed (char* ptr = pwcsName)
		{
			return @this.DestroyElement(ptr);
		}
	}

	internal unsafe static HRESULT RenameElement(this IStorage.Interface @this, string pwcsOldName, string pwcsNewName)
	{
		fixed (char* ptr = pwcsNewName)
		{
			fixed (char* ptr2 = pwcsOldName)
			{
				return @this.RenameElement(ptr2, ptr);
			}
		}
	}

	internal unsafe static HRESULT SetElementTimes(this IStorage.Interface @this, string pwcsName, in FILETIME pctime, in FILETIME patime, in FILETIME pmtime)
	{
		fixed (FILETIME* pmtime2 = &pmtime)
		{
			fixed (FILETIME* patime2 = &patime)
			{
				fixed (FILETIME* pctime2 = &pctime)
				{
					fixed (char* ptr = pwcsName)
					{
						return @this.SetElementTimes(ptr, pctime2, patime2, pmtime2);
					}
				}
			}
		}
	}

	internal unsafe static HRESULT SetClass(this IStorage.Interface @this, in Guid clsid)
	{
		fixed (Guid* ptr = &clsid)
		{
			return @this.SetClass(ptr);
		}
	}

	internal unsafe static HRESULT Stat(this IStorage.Interface @this, out Windows.Win32.System.Com.STATSTG pstatstg, uint grfStatFlag)
	{
		fixed (Windows.Win32.System.Com.STATSTG* pstatstg2 = &pstatstg)
		{
			return @this.Stat(pstatstg2, grfStatFlag);
		}
	}
}
