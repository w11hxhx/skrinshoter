using System;

namespace SkrinshoterApp;

public class SelectionManager
{
	public VisualObjectVM SelectedObject { get; private set; }

	public event EventHandler SelectedObjectChanged;

	public void Select(VisualObjectVM visualObject)
	{
		if (SelectedObject != visualObject)
		{
			SelectedObject = visualObject;
			RaiseSelectedObjectChanged();
		}
	}

	public void RaiseSelectedObjectChanged()
	{
		SelectedObjectChanged?.Invoke(this, EventArgs.Empty);
	}
}
