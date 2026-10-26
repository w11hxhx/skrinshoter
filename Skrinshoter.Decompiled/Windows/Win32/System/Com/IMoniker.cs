using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[ComImport]
[Guid("0000000F-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal interface IMoniker : IPersistStream, IPersist
{
	new unsafe void GetClassID(Guid* pClassID);

	[PreserveSig]
	new HRESULT IsDirty();

	new void Load(IStream pStm);

	new void Save(IStream pStm, BOOL fClearDirty);

	new void GetSizeMax(out ulong pcbSize);

	unsafe void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, Guid* riidResult, [MarshalAs(UnmanagedType.IUnknown)] out object ppvResult);

	unsafe void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

	void Reduce(IBindCtx pbc, uint dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

	void ComposeWith(IMoniker pmkRight, BOOL fOnlyIfNotGeneric, out IMoniker ppmkComposite);

	void Enum(BOOL fForward, out IEnumMoniker ppenumMoniker);

	[PreserveSig]
	HRESULT IsEqual(IMoniker pmkOtherMoniker);

	void Hash(out uint pdwHash);

	void IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

	unsafe void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, global::System.Runtime.InteropServices.ComTypes.FILETIME* pFileTime);

	void Inverse(out IMoniker ppmk);

	void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

	void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

	unsafe void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, PWSTR* ppszDisplayName);

	void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, PWSTR pszDisplayName, out uint pchEaten, out IMoniker ppmkOut);

	void IsSystemMoniker(out uint pdwMksys);
}
