using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class NumberObjectVMDesignFactory : DesignFactory<NumberObjectVM>
{
	public override NumberObjectVM Create(IServiceProvider sp)
	{
		return new NumberObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), new OverlayWindowStateManager(), new SelectionManager(), new UndoManager(), 30.0, Colors.Green, 69)
		{
			Center = new Point(110.0, 60.0),
			SelectionVisible = true
		};
	}
}
