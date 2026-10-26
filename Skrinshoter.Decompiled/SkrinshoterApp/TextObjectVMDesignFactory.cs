using System;
using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public class TextObjectVMDesignFactory : DesignFactory<TextObjectVM>
{
	public override TextObjectVM Create(IServiceProvider sp)
	{
		return new TextObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), new OverlayWindowStateManager(), new SelectionManager(), new UndoManager(), 20.0, Colors.Green)
		{
			Point = new Point(20.0, 10.0),
			Text = "Test",
			SelectionVisible = true
		};
	}
}
