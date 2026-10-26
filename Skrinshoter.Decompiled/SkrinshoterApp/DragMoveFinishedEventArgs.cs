using System;

namespace SkrinshoterApp;

public class DragMoveFinishedEventArgs : EventArgs
{
	public bool IsMoved { get; }

	public DragMoveFinishedEventArgs(bool isMoved)
	{
		IsMoved = isMoved;
	}
}
