using System.CodeDom.Compiler;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct FUNCDESC
{
	internal int memid;

	internal unsafe int* lprgscode;

	internal unsafe ELEMDESC* lprgelemdescParam;

	internal FUNCKIND funckind;

	internal INVOKEKIND invkind;

	internal CALLCONV callconv;

	internal short cParams;

	internal short cParamsOpt;

	internal short oVft;

	internal short cScodes;

	internal ELEMDESC elemdescFunc;

	internal FUNCFLAGS wFuncFlags;
}
