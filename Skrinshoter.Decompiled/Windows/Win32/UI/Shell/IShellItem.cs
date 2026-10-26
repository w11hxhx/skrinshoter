using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.SystemServices;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IShellItem
{
	unsafe void BindToHandler(IBindCtx pbc, Guid* bhid, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	void GetParent(out IShellItem ppsi);

	unsafe void GetDisplayName(SIGDN sigdnName, PWSTR* ppszName);

	unsafe void GetAttributes(SFGAO_FLAGS sfgaoMask, SFGAO_FLAGS* psfgaoAttribs);

	void Compare(IShellItem psi, uint hint, out int piOrder);
}
