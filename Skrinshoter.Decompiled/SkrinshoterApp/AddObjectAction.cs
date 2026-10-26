using System.Collections.ObjectModel;
using Common;

namespace SkrinshoterApp;

public class AddObjectAction : IUndoableAction
{
	private readonly ObservableCollection<VisualObjectVM> visualObjects;

	private readonly SelectionManager selectionManager;

	private int index;

	public VisualObjectVM VisualObject { get; }

	public AddObjectAction(VisualObjectVM visualObject, SelectionManager selectionManager)
	{
		VisualObject = visualObject;
		visualObjects = visualObject.OverlayWindowVM.VisualObjects;
		this.selectionManager = selectionManager;
		index = visualObjects.IndexOf(visualObject);
	}

	public void Undo()
	{
		visualObjects.Remove(VisualObject);
		if (selectionManager.SelectedObject == VisualObject)
		{
			selectionManager.Select(null);
		}
	}

	public void Redo()
	{
		if (index < 0 || index > visualObjects.Count)
		{
			index = visualObjects.Count;
		}
		visualObjects.Insert(index, VisualObject);
	}

	public override string ToString()
	{
		return $"{UndoActionKind.Add.GetDescription()} {VisualObject}";
	}
}
