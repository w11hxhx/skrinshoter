using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("00000109-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IPersistStream : IPersist
{
	new unsafe void GetClassID(Guid* pClassID);

	[PreserveSig]
	HRESULT IsDirty();

	void Load(IStream pStm);

	void Save(IStream pStm, BOOL fClearDirty);

	void GetSizeMax(out ulong pcbSize);
}
