using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class OvalObjectVMDesignFactory : DesignFactory<OvalObjectVM>
{
	public override OvalObjectVM Create(IServiceProvider sp)
	{
		return new OvalObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), new OverlayWindowStateManager(), new SelectionManager(), new UndoManager(), 10.0, Colors.Green)
		{
			Rect = new Rect(10.0, 10.0, 200.0, 100.0),
			SelectionVisible = true
		};
	}
}
