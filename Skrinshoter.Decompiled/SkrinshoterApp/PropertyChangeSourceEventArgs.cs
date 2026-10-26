using System;

namespace SkrinshoterApp;

public class PropertyChangeSourceEventArgs : EventArgs
{
	public bool IsProgrammaticChange { get; }

	public PropertyChangeSourceEventArgs(bool isProgrammaticChange)
	{
		IsProgrammaticChange = isProgrammaticChange;
	}
}
