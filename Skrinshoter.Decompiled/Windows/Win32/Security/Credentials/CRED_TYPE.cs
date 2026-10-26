using System.CodeDom.Compiler;

namespace Windows.Win32.Security.Credentials;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum CRED_TYPE : uint
{
	CRED_TYPE_GENERIC = 1u,
	CRED_TYPE_DOMAIN_PASSWORD = 2u,
	CRED_TYPE_DOMAIN_CERTIFICATE = 3u,
	CRED_TYPE_DOMAIN_VISIBLE_PASSWORD = 4u,
	CRED_TYPE_GENERIC_CERTIFICATE = 5u,
	CRED_TYPE_DOMAIN_EXTENDED = 6u,
	CRED_TYPE_MAXIMUM = 7u,
	CRED_TYPE_MAXIMUM_EX = 1007u
}
