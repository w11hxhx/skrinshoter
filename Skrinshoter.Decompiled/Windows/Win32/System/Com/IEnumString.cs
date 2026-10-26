using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("00000101-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IEnumString
{
	[PreserveSig]
	unsafe HRESULT Next(uint celt, PWSTR* rgelt, [Optional] uint* pceltFetched);

	[PreserveSig]
	HRESULT Skip(uint celt);

	void Reset();

	void Clone(out IEnumString ppenum);
}
