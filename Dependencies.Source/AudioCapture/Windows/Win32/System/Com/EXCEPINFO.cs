using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct EXCEPINFO
{
	internal ushort wCode;

	internal ushort wReserved;

	internal BSTR bstrSource;

	internal BSTR bstrDescription;

	internal BSTR bstrHelpFile;

	internal uint dwHelpContext;

	internal unsafe void* pvReserved;

	internal unsafe delegate* unmanaged[Stdcall]<EXCEPINFO*, HRESULT> pfnDeferredFillIn;

	internal int scode;
}
