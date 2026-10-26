using Windows.Win32.Graphics.Dxgi.Common;

namespace ScreenCaptureDda;

internal static class DxgiModeRotationExtensions
{
	public static Rotation ToRotation(this DXGI_MODE_ROTATION rotation)
	{
		return rotation switch
		{
			DXGI_MODE_ROTATION.DXGI_MODE_ROTATION_IDENTITY => Rotation.None, 
			DXGI_MODE_ROTATION.DXGI_MODE_ROTATION_ROTATE90 => Rotation.CW90, 
			DXGI_MODE_ROTATION.DXGI_MODE_ROTATION_ROTATE180 => Rotation.CW180, 
			DXGI_MODE_ROTATION.DXGI_MODE_ROTATION_ROTATE270 => Rotation.CW270, 
			_ => Rotation.None, 
		};
	}
}
