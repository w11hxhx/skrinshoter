using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Windows.Win32.Graphics.Dxgi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct DXGI_SWAP_CHAIN_DESC
{
	internal DXGI_MODE_DESC BufferDesc;

	internal DXGI_SAMPLE_DESC SampleDesc;

	internal DXGI_USAGE BufferUsage;

	internal uint BufferCount;

	internal HWND OutputWindow;

	internal BOOL Windowed;

	internal DXGI_SWAP_EFFECT SwapEffect;

	private uint _Flags;

	internal DXGI_SWAP_CHAIN_FLAG Flags
	{
		get
		{
			return (DXGI_SWAP_CHAIN_FLAG)_Flags;
		}
		set
		{
			_Flags = (uint)value;
		}
	}
}
