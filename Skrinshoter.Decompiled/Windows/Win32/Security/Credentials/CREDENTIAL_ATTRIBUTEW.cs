using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Security.Credentials;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct CREDENTIAL_ATTRIBUTEW
{
	internal PWSTR Keyword;

	internal uint Flags;

	internal uint ValueSize;

	internal unsafe byte* Value;
}
