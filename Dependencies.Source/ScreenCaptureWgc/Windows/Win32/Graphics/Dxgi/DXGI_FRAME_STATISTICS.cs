using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_FRAME_STATISTICS
{
	internal uint PresentCount;

	internal uint PresentRefreshCount;

	internal uint SyncRefreshCount;

	internal long SyncQPCTime;

	internal long SyncGPUTime;
}
