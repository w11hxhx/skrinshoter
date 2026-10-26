using System.CodeDom.Compiler;

namespace Windows.Win32.System.SystemInformation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct MEMORYSTATUSEX
{
	internal uint dwLength;

	internal uint dwMemoryLoad;

	internal ulong ullTotalPhys;

	internal ulong ullAvailPhys;

	internal ulong ullTotalPageFile;

	internal ulong ullAvailPageFile;

	internal ulong ullTotalVirtual;

	internal ulong ullAvailVirtual;

	internal ulong ullAvailExtendedVirtual;
}
