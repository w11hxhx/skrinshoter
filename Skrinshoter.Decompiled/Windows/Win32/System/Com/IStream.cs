using System.CodeDom.Compiler;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("0000000C-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IStream : ISequentialStream
{
	[PreserveSig]
	new unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead);

	[PreserveSig]
	new unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten);

	unsafe void Seek(long dlibMove, SeekOrigin dwOrigin, [Optional] ulong* plibNewPosition);

	void SetSize(ulong libNewSize);

	unsafe void CopyTo(IStream pstm, ulong cb, [Optional] ulong* pcbRead, [Optional] ulong* pcbWritten);

	void Commit([MarshalAs(UnmanagedType.U4)] STGC grfCommitFlags);

	void Revert();

	void LockRegion(ulong libOffset, ulong cb, [MarshalAs(UnmanagedType.U4)] LOCKTYPE dwLockType);

	void UnlockRegion(ulong libOffset, ulong cb, uint dwLockType);

	unsafe void Stat(STATSTG* pstatstg, [MarshalAs(UnmanagedType.U4)] STATFLAG grfStatFlag);

	void Clone(out IStream ppstm);
}
