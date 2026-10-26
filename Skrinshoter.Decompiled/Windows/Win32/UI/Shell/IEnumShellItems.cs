using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("70629033-E363-4A28-A567-0DB78006E6D7")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IEnumShellItems
{
	unsafe void Next(uint celt, [Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IShellItem[] rgelt, [Optional] uint* pceltFetched);

	void Skip(uint celt);

	void Reset();

	void Clone(out IEnumShellItems ppenum);
}
