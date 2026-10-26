using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("00000010-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IRunningObjectTable
{
	void Register(ROT_FLAGS grfFlags, [MarshalAs(UnmanagedType.IUnknown)] object punkObject, IMoniker pmkObjectName, out uint pdwRegister);

	void Revoke(uint dwRegister);

	void IsRunning(IMoniker pmkObjectName);

	void GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.IUnknown)] out object ppunkObject);

	unsafe void NoteChangeTime(uint dwRegister, global::System.Runtime.InteropServices.ComTypes.FILETIME* pfiletime);

	unsafe void GetTimeOfLastChange(IMoniker pmkObjectName, global::System.Runtime.InteropServices.ComTypes.FILETIME* pfiletime);

	void EnumRunning(out IEnumMoniker ppenumMoniker);
}
