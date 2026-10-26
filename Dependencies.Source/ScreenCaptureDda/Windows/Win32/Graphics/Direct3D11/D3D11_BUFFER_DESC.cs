using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Direct3D11;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct D3D11_BUFFER_DESC
{
	internal uint ByteWidth;

	internal D3D11_USAGE Usage;

	private uint _BindFlags;

	private uint _CPUAccessFlags;

	private uint _MiscFlags;

	internal uint StructureByteStride;

	internal D3D11_BIND_FLAG BindFlags
	{
		get
		{
			return (D3D11_BIND_FLAG)_BindFlags;
		}
		set
		{
			_BindFlags = (uint)value;
		}
	}

	internal D3D11_CPU_ACCESS_FLAG CPUAccessFlags
	{
		get
		{
			return (D3D11_CPU_ACCESS_FLAG)_CPUAccessFlags;
		}
		set
		{
			_CPUAccessFlags = (uint)value;
		}
	}

	internal D3D11_RESOURCE_MISC_FLAG MiscFlags
	{
		get
		{
			return (D3D11_RESOURCE_MISC_FLAG)_MiscFlags;
		}
		set
		{
			_MiscFlags = (uint)value;
		}
	}
}
