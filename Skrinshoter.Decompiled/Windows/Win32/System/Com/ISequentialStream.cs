using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface ISequentialStream
{
	[PreserveSig]
	unsafe HRESULT Read(void* pv, uint cb, [Optional] uint* pcbRead);

	[PreserveSig]
	unsafe HRESULT Write(void* pv, uint cb, [Optional] uint* pcbWritten);
}
