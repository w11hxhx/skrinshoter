using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class MarkerObjectVMDesignFactory : DesignFactory<MarkerObjectVM>
{
	public override MarkerObjectVM Create(IServiceProvider sp)
	{
		MarkerObjectVM markerObjectVM = new MarkerObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), new OverlayWindowStateManager(), new SelectionManager(), new UndoManager(), 10.0, Colors.Green);
		markerObjectVM.points.AddRange(new _003C_003Ez__ReadOnlyArray<Point>(new Point[7]
		{
			new Point(10.0, 10.0),
			new Point(42.0, 60.0),
			new Point(75.0, 80.0),
			new Point(105.0, 60.0),
			new Point(135.0, 20.0),
			new Point(165.0, 40.0),
			new Point(200.0, 100.0)
		}));
		markerObjectVM.smoothingEnabled = true;
		markerObjectVM.UpdateProperties();
		markerObjectVM.SelectionVisible = true;
		return markerObjectVM;
	}
}
