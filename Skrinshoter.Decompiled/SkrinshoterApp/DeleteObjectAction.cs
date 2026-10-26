using System.Collections.ObjectModel;
using Common;

namespace SkrinshoterApp;

public class DeleteObjectAction : IUndoableAction
{
	private readonly VisualObjectVM visualObject;

	private readonly ObservableCollection<VisualObjectVM> visualObjects;

	private readonly SelectionManager selectionManager;

	private int index;

	public DeleteObjectAction(VisualObjectVM visualObject, SelectionManager selectionManager)
	{
		this.visualObject = visualObject;
		visualObjects = visualObject.OverlayWindowVM.VisualObjects;
		this.selectionManager = selectionManager;
		index = visualObjects.IndexOf(visualObject);
	}

	public void Undo()
	{
		if (index < 0 || index > visualObjects.Count)
		{
			index = visualObjects.Count;
		}
		visualObjects.Insert(index, visualObject);
	}

	public void Redo()
	{
		visualObjects.Remove(visualObject);
		if (selectionManager.SelectedObject == visualObject)
		{
			selectionManager.Select(null);
		}
	}

	public override string ToString()
	{
		return $"{UndoActionKind.Delete.GetDescription()} {visualObject}";
	}
}
