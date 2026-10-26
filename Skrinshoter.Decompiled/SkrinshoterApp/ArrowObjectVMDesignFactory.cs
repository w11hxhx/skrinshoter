using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class ArrowObjectVMDesignFactory : DesignFactory<ArrowObjectVM>
{
	public override ArrowObjectVM Create(IServiceProvider sp)
	{
		return new ArrowObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), new OverlayWindowStateManager(), new SelectionManager(), new UndoManager(), 10.0, Colors.Green)
		{
			StartPoint = new Point(10.0, 10.0),
			EndPoint = new Point(200.0, 100.0),
			SelectionVisible = true
		};
	}
}
