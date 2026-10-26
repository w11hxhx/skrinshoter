using System.Runtime.InteropServices;
using System.Text;

namespace Stfu;

internal static class NativeMethods
{
	[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out SID_NAME_USE peUse);
}
