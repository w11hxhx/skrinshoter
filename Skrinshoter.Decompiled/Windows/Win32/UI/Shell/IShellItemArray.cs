using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.System.SystemServices;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace Windows.Win32.UI.Shell;

[ComImport]
[Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IShellItemArray
{
	unsafe void BindToHandler(IBindCtx pbc, Guid* bhid, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvOut);

	unsafe void GetPropertyStore(GETPROPERTYSTOREFLAGS flags, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	unsafe void GetPropertyDescriptionList(PROPERTYKEY* keyType, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	unsafe void GetAttributes(SIATTRIBFLAGS AttribFlags, SFGAO_FLAGS sfgaoMask, SFGAO_FLAGS* psfgaoAttribs);

	void GetCount(out uint pdwNumItems);

	void GetItemAt(uint dwIndex, out IShellItem ppsi);

	void EnumItems(out IEnumShellItems ppenumShellItems);
}
