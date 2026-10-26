using System.CodeDom.Compiler;

namespace Windows.Win32.Security.Credentials;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum CRED_PERSIST : uint
{
	CRED_PERSIST_NONE,
	CRED_PERSIST_SESSION,
	CRED_PERSIST_LOCAL_MACHINE,
	CRED_PERSIST_ENTERPRISE
}
