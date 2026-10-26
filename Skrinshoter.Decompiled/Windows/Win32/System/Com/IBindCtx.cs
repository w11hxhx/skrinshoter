using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("0000000E-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IBindCtx
{
	void RegisterObjectBound([MarshalAs(UnmanagedType.IUnknown)] object punk);

	void RevokeObjectBound([MarshalAs(UnmanagedType.IUnknown)] object punk);

	void ReleaseBoundObjects();

	unsafe void SetBindOptions(BIND_OPTS* pbindopts);

	unsafe void GetBindOptions(BIND_OPTS* pbindopts);

	void GetRunningObjectTable(out IRunningObjectTable pprot);

	void RegisterObjectParam(PWSTR pszKey, [MarshalAs(UnmanagedType.IUnknown)] object punk);

	void GetObjectParam(PWSTR pszKey, [MarshalAs(UnmanagedType.IUnknown)] out object ppunk);

	void EnumObjectParam(out IEnumString ppenum);

	void RevokeObjectParam(PWSTR pszKey);
}
