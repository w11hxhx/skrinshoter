using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.System.Com;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct TYPEATTR
{
	internal Guid guid;

	internal uint lcid;

	internal uint dwReserved;

	internal int memidConstructor;

	internal int memidDestructor;

	internal PWSTR lpstrSchema;

	internal uint cbSizeInstance;

	internal TYPEKIND typekind;

	internal ushort cFuncs;

	internal ushort cVars;

	internal ushort cImplTypes;

	internal ushort cbSizeVft;

	internal ushort cbAlignment;

	internal ushort wTypeFlags;

	internal ushort wMajorVerNum;

	internal ushort wMinorVerNum;

	internal TYPEDESC tdescAlias;

	internal IDLDESC idldescType;
}
