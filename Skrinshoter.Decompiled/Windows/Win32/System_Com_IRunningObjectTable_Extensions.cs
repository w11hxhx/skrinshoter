using System.CodeDom.Compiler;
using System.Runtime.InteropServices.ComTypes;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_Com_IRunningObjectTable_Extensions
{
	internal unsafe static void NoteChangeTime(this Windows.Win32.System.Com.IRunningObjectTable @this, uint dwRegister, in FILETIME pfiletime)
	{
		fixed (FILETIME* pfiletime2 = &pfiletime)
		{
			@this.NoteChangeTime(dwRegister, pfiletime2);
		}
	}

	internal unsafe static void GetTimeOfLastChange(this Windows.Win32.System.Com.IRunningObjectTable @this, Windows.Win32.System.Com.IMoniker pmkObjectName, out FILETIME pfiletime)
	{
		fixed (FILETIME* pfiletime2 = &pfiletime)
		{
			@this.GetTimeOfLastChange(pmkObjectName, pfiletime2);
		}
	}
}
