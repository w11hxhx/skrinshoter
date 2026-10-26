using System;
using Common;

namespace SkrinshoterApp;

public class ChangeObjectStateAction<TVisualObject, TState> : IMergeableAction, IUndoableAction
{
	private readonly UndoActionKind actionKind;

	private readonly TVisualObject visualObject;

	private readonly TState oldState;

	private TState newState;

	private readonly Action<TVisualObject, TState> applyState;

	private readonly bool mergeEnabled;

	public ChangeObjectStateAction(UndoActionKind actionKind, TVisualObject visualObject, TState oldState, TState newState, Action<TVisualObject, TState> applyState, bool mergeEnabled = false)
	{
		this.actionKind = actionKind;
		this.visualObject = visualObject;
		this.oldState = oldState;
		this.newState = newState;
		this.applyState = applyState;
		this.mergeEnabled = mergeEnabled;
	}

	public void Undo()
	{
		applyState(visualObject, oldState);
	}

	public void Redo()
	{
		applyState(visualObject, newState);
	}

	public bool TryMerge(IMergeableAction action)
	{
		if (!mergeEnabled)
		{
			return false;
		}
		if (!(action is ChangeObjectStateAction<TVisualObject, TState> changeObjectStateAction))
		{
			return false;
		}
		if ((object)visualObject != (object)changeObjectStateAction.visualObject)
		{
			return false;
		}
		if (actionKind != changeObjectStateAction.actionKind)
		{
			return false;
		}
		newState = changeObjectStateAction.newState;
		return true;
	}

	public override string ToString()
	{
		return $"{actionKind.GetDescription()} {visualObject}";
	}
}
