using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint WAVE_FORMAT_EXTENSIBLE = 65534u;

	internal const uint WAVE_FORMAT_PCM = 1u;

	internal const uint WAVE_FORMAT_IEEE_FLOAT = 3u;

	internal const uint AUDCLNT_STREAMFLAGS_LOOPBACK = 131072u;

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern void CoTaskMemFree([Optional] void* pv);

	[DllImport("OLEAUT32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern void SysFreeString(BSTR bstrString);
}
