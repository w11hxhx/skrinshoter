using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class OverlayWindowVMDesignFactory : DesignFactory<OverlayWindowVM>
{
	public override OverlayWindowVM Create(IServiceProvider sp)
	{
		return new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), GetVisual(), 1.0)
		{
			FrameRect = new Rect(20.0, 30.0, 139.0, 49.0)
		};
	}

	private static Visual GetVisual()
	{
		Window window = new Window();
		window.Width = 1.0;
		window.Height = 1.0;
		window.Left = -10000.0;
		window.Top = -10000.0;
		window.Show();
		window.Hide();
		return window;
	}
}
