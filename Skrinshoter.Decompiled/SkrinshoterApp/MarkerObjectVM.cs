using System.Windows.Media;

namespace SkrinshoterApp;

public class MarkerObjectVM : PencilObjectVM
{
	public MarkerObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
	}

	protected override double GetEffectiveLineThickness()
	{
		return LineThickness * 3.0;
	}
}
