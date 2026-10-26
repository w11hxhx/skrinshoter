using System.Windows;
using System.Windows.Input;

namespace SkrinshoterApp;

public abstract class FloatingObjectVM : LocalizableObservableObject
{
	protected bool IsFloating => OverlayWindowVM.FloatingObject == this;

	public OverlayWindowVM OverlayWindowVM { get; }

	public Cursor FloatingCursor { get; protected set; } = Cursors.Pen;

	protected FloatingObjectVM(OverlayWindowVM overlayWindowVM)
	{
		OverlayWindowVM = overlayWindowVM;
	}

	public void UpdateFloatingObjectPosition(Point mousePosition)
	{
		MoveInFloatingMode(mousePosition);
	}

	protected virtual void MoveInFloatingMode(Point mousePosition)
	{
	}
}
