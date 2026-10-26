using System.CodeDom.Compiler;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.Foundation;

namespace Windows.Win32.Security.Credentials;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct CREDENTIALW
{
	internal CRED_FLAGS Flags;

	internal CRED_TYPE Type;

	internal PWSTR TargetName;

	internal PWSTR Comment;

	internal FILETIME LastWritten;

	internal uint CredentialBlobSize;

	internal unsafe byte* CredentialBlob;

	internal CRED_PERSIST Persist;

	internal uint AttributeCount;

	internal unsafe CREDENTIAL_ATTRIBUTEW* Attributes;

	internal PWSTR TargetAlias;

	internal PWSTR UserName;
}
