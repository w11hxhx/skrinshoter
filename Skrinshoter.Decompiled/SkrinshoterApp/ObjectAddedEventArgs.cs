using System;

namespace SkrinshoterApp;

public class ObjectAddedEventArgs : EventArgs
{
	public VisualObjectVM VisualObject { get; }

	public ObjectAddedEventArgs(VisualObjectVM visualObject)
	{
		VisualObject = visualObject;
	}
}
