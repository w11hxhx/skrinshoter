using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_ADAPTER_DESC
{
	internal __char_128 Description;

	internal uint VendorId;

	internal uint DeviceId;

	internal uint SubSysId;

	internal uint Revision;

	internal nuint DedicatedVideoMemory;

	internal nuint DedicatedSystemMemory;

	internal nuint SharedSystemMemory;

	internal LUID AdapterLuid;
}
